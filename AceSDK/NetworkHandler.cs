using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.Memory;

namespace Ace_client.AceSDK
{
    public class NetworkHandler : SDKHandler
    {
        public RakNetInstance rakNetInstance;

        public NetworkHandler(ulong address) : base(address)
        {
            rakNetInstance = new RakNetInstance(AceMCM.Read<ulong>(address + 0x18));
        }

    }
}
