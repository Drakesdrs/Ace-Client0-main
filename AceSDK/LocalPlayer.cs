using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.Memory;

namespace Ace_client.AceSDK
{
    public class LocalPlayer : PlayerEntity
    {
        public LocalPlayer(ulong address) : base(address)
        {
        }
    }


}
