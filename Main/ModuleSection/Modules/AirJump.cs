using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ace_client.Main.KeySection;
using System.Windows.Forms;
using Ace_client.Memory;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class AirJump : TickingModule
    {
        public AirJump() : base(Keys.G, "Movement")
        {

        }

        public override void onTick()
        {
            Minecraft.clientInstance.localPlayer.onGround ^= 1;
        }
    }
}
