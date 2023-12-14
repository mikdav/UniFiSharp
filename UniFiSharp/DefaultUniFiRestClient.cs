﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GcmSharp.Serialization;
using RestSharp;
using UniFiSharp.Json;

namespace UniFiSharp
{
    internal class DefaultUniFiRestClient : RestClient, IUniFiRestClient
    {
        private string _username, _password, _code, _csrf_token;
        private bool _useModernApi;

        internal DefaultUniFiRestClient(Uri baseUrl, string username, string password, string code,
            bool ignoreSslValidation, bool useModernApi) :
            this(baseUrl, username, password, ignoreSslValidation, useModernApi)
        {
            _code = code;
        }

        internal DefaultUniFiRestClient(Uri baseUrl, string username, string password, bool ignoreSslValidation,
            bool useModernApi) : base(baseUrl)
        {
            _username = username;
            _password = password;
            _useModernApi = useModernApi;

            CookieContainer = new CookieContainer();

            AddHandler("application/json", () => NewtonsoftJsonSerializer.Default);
            AddHandler("text/json", () => NewtonsoftJsonSerializer.Default);
            AddHandler("text/x-json", () => NewtonsoftJsonSerializer.Default);
            AddHandler("text/javascript", () => NewtonsoftJsonSerializer.Default);
            AddHandler("*+json", () => NewtonsoftJsonSerializer.Default);

            if (ignoreSslValidation)
            {
                this.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            }
        }

        public async Task<MessageEnvelope> UniFiGet(string url)
        {
            return await UniFiRequest(Method.GET, url);
        }

        public async Task<T> UniFiGet<T>(string url)
        {
            return await UniFiRequest<T>(Method.GET, url);
        }

        public async Task<IList<T>> UniFiGetMany<T>(string url)
        {
            return await UniFiRequestMany<T>(Method.GET, url);
        }

        public async Task UniFiPost(string url, object jsonBody)
        {
            await UniFiRequest(Method.POST, url, jsonBody);
        }

        public async Task<T> UniFiPost<T>(string url, object jsonBody)
        {
            return await UniFiRequest<T>(Method.POST, url, jsonBody);
        }

        public async Task<IList<T>> UniFiPostMany<T>(string url, object jsonBody)
        {
            return await UniFiRequestMany<T>(Method.POST, url, jsonBody);
        }

        public async Task UniFiPut(string url, object jsonBody)
        {
            await UniFiRequest(Method.PUT, url, jsonBody);
        }

        public async Task<T> UniFiPut<T>(string url, object jsonBody)
        {
            return await UniFiRequest<T>(Method.PUT, url, jsonBody);
        }

        public async Task<IList<T>> UniFiPutMany<T>(string url, object jsonBody)
        {
            return await UniFiRequestMany<T>(Method.PUT, url, jsonBody);
        }

        public async Task UniFiDelete(string url)
        {
            await UniFiRequest(Method.DELETE, url);
        }

        public async Task UnifiFileUpload(string url, string name, string fileName, string contentType, byte[] data)
        {
            await UnifiMultipartFormRequest(url, name, fileName, contentType, data);
        }

        private async Task<MessageEnvelope> UniFiRequest(Method method, string url, object jsonBody = null)
        {
            var request = new RestRequest(url, method, DataFormat.Json);
            if ((method == Method.POST || method == Method.PUT) && jsonBody != null)
                request.AddJsonBody(jsonBody);
            return await ExecuteRequest<object>(request);
        }

        private async Task<T> UniFiRequest<T>(Method method, string url, object jsonBody = null)
        {
            var request = new RestRequest(url, method, DataFormat.Json);
            if ((method == Method.POST || method == Method.PUT) && jsonBody != null)
                request.AddJsonBody(jsonBody);

            var envelope = await ExecuteRequest<T>(request);

            if (envelope.Metadata != null && envelope.Metadata.ResultCode.Equals("error", StringComparison.OrdinalIgnoreCase))
            {
                throw new UniFiApiException($"UniFi API returned an error: {envelope.Metadata.Message}");
            }

            return envelope.Data;
        }

        private async Task<IList<T>> UniFiRequestMany<T>(Method method, string url, object jsonBody = null)
           
        {
            var request = new RestRequest(url, method, DataFormat.Json);
            if ((method == Method.POST || method == Method.PUT) && jsonBody != null)
                request.AddJsonBody(jsonBody);
            var envelope = await ExecuteRequest<T[]>(request);
            return (envelope.Data == null) ? new List<T>() : new List<T>(envelope.Data);
        }

        public async Task<LoginResult> Authenticate()
        {
            if (_useModernApi)
            {
                var request = new RestRequest("api/auth/login", Method.POST, DataFormat.Json);
                request.AddJsonBody(new
                {
                    username = _username,
                    password = _password,
                    token = _code,
                    rememberMe = false
                });

                request.JsonSerializer = NewtonsoftJsonSerializer.Default;

                var response = await ExecuteAsync<LoginResult>(request);
                _csrf_token = response.Headers.Where(x => "X-CSRF-Token".Equals(x.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value.ToString();
                return response.Data;
            }
            else
            {
                await UniFiPost("api/login", new
                {
                    username = _username,
                    password = _password,
                    remember = false,
                    strict = true
                });
                return new LoginResult();
            }
        }

        private async Task<MessageEnvelope<T>> ExecuteRequest<T>(IRestRequest request,
            bool attemptReauthentication = true)
        {
            if (_useModernApi)
            {
                if (request.Resource.StartsWith("api/"))
                    request.Resource = "proxy/network/" + request.Resource; // default to network
                else request.Resource = "proxy/" + request.Resource; // switch for others
            }

            request.AddHeader("Referrer", BaseUrl.ToString());
            FollowRedirects = true;

            if (CookieContainer.GetCookies(BaseUrl).Count > 0)
            {
                var csrf_token = CookieContainer.GetCookies(this.BaseUrl)["csrf_token"]?.Value;

                if (csrf_token != null)
                {
                    request.AddHeader("X-Csrf-Token", csrf_token);
                }

                if (_useModernApi)
                {
                    if (_csrf_token != null)
                    {
                        request.AddHeader("X-CSRF-Token", _csrf_token);
                    }
                }
            }
            
            request.JsonSerializer = NewtonsoftJsonSerializer.Default;

            var response = await ExecuteAsync<MessageEnvelope<T>>(request);
            var envelope = response.Data;

            if (envelope == null && !response.IsSuccessful)
                throw response.ErrorException;

            if (!envelope.IsSuccessfulResponse &&
                envelope.Metadata?.Message == "api.err.LoginRequired" && // will fail for access devices
                attemptReauthentication)
            {
                await Authenticate();
                return await ExecuteRequest<T>(request, false);
            }

            return response.Data;
        }

        /// <summary>
        ///     Upload a file to the UniFi controller. The only known use of this at the moment is for uploading .ogg files for the
        ///     AP-AC-EDU APs
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <param name="data"></param>
        /// <param name="attemptReauthentication"></param>
        /// <returns></returns>
        private async Task UnifiMultipartFormRequest(string url, string name, string fileName, string contentType,
            byte[] data, bool attemptReauthentication = true)
        {
            // Note the UniFi controller will return 404 when uploading a file - however the file *is* successfully uploaded. 

            // Bodge to work around the fact uploads don't return the normal metadata if unauthorized
            FollowRedirects = false;

            var request = new RestRequest(url, Method.POST)
            {
                AlwaysMultipartFormData = true
            };

            request.AddHeader("Referrer", BaseUrl.ToString());

            if (CookieContainer.GetCookies(BaseUrl).Count > 0)
            {
                var csrf_token = CookieContainer.GetCookies(BaseUrl)["csrf_token"]?.Value;

                if (csrf_token != null)
                {
                    request.AddHeader("X-Csrf-Token", csrf_token);
                }
            }

            request.AddParameter("name", name, ParameterType.RequestBody);
            request.AddFileBytes("filedata", data, fileName, contentType);

            var response = await ExecuteAsync(request);

            // Bodge to authenticate if needed (if we're being redirected back to the login page, then we need to attempt to authenticate)
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var redirectLocation = response.Headers.ToList().Find(x => x.Name == "Location").Value.ToString();

                if (redirectLocation.Contains("/manage/account/login?redirect"))
                {
                    await Authenticate();
                    await UnifiMultipartFormRequest(url, name, fileName, contentType, data, false);
                }
            }
        }
    }
}