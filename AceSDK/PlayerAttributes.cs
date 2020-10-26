using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.Memory;

namespace Ace_client.AceSDK
{
    public class PlayerAttributes : SDKHandler
    {
        public PropertyField<float> playerSpeed;
        public PlayerAttributes(ulong address) : base(address)
        {
            playerSpeed = address + 0x9C;
        }
    }
}
