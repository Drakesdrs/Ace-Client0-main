using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.AceSDK
{
    public class Gamerule : SDKHandler
    {
        public string name => AceMCM.szRead(address + 0x20);

        public PropertyField<bool> toggle;
        public Boolean toggle1;

        public Gamerule(ulong address) : base(address)
        {
            toggle = address + 0x4;
            toggle1 = true;
        }
    }
}
