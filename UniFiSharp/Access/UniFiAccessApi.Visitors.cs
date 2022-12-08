using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
