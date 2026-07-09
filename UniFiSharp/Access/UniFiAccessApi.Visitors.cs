using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniFiSharp.Access.Models;

namespace UniFiSharp.Access
{
    public partial class UniFiAccessApi
    {
        public async Task<Visitor> AddVisitor(Visitor visitor)
        {
            return await this.RestClient.UniFiPost<Visitor>("access/api/v2/visitor", visitor);
        }

        public async Task<List<Visitor>> GetVisitors(int batchSize = 50)
        {
            var visitorList = new List<Visitor>();
            List<Visitor> tempList;
            var pageNum = 1;

            do
            {
                tempList = await this.RestClient.UniFiGet<List<Visitor>>($"access/api/v2/visitors?page_num={pageNum}&page_size={batchSize}");
                visitorList.AddRange(tempList);
                pageNum++;
            }
            while (tempList.Count == batchSize);

            return visitorList;
        }

        public async Task RemoveVisitor(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            if (visitor.Id == Guid.Empty)
            {
                throw new ArgumentNullException("visitorId");
            }

            await this.RestClient.UniFiDelete($"access/api/v2/visitor/{visitor.Id}?is_force=true");
        }
    }
}
