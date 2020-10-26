using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.AceSDK
{
    public abstract class SDKHandler
    {
        public ulong address;

        public SDKHandler(ulong address)
        {
            this.address = address;
        }
    }
}
