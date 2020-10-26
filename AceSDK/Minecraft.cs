using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.Memory;

namespace Ace_client.AceSDK
{
    public class Minecraft
    {
        public static ClientInstance clientInstance
        {
            get
            {
                return new ClientInstance(AceMCM.BaseEvaluatePointer(0x36966F8, AceMCM.CheatEngineByteToUlongs("80 0")));
            }
        }
    }
}
