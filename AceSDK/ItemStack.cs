using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.AceSDK
{
    public class ItemStack : SDKHandler
    {
        public PropertyField<byte> stackSize;

        public ItemStack(ulong address) : base(address) 
        {
            stackSize = address + 0x22;
        }

        /*public List<Item> items
        {
            get
            {
                return null;
            }
        }*/
    }
}
