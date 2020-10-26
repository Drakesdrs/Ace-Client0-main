using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.Memory;

namespace Ace_client.AceSDK
{
    public class PlayerInventoryProxy : SDKHandler
    {
        public Inventory inventory;
        public PropertyField<ulong> selectedHotbarSlot;

        public PlayerInventoryProxy(ulong address) : base(address)
        {
            inventory = new Inventory(AceMCM.Read<ulong>(address + 0xB0));
            selectedHotbarSlot = address + 0x10;
        }
    }
}
