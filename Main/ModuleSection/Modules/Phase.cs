using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class Phase : SparinglyTickingModule
    {
        public Phase() : base("Movement")
        {
        }

        public override void onDisable()
        {
            Minecraft.clientInstance.localPlayer.Position_upper.Y = Minecraft.clientInstance.localPlayer.Position_lower.Y + 1.8f;
        }
        public override void onSpareTick()
        {
            Minecraft.clientInstance.localPlayer.Position_upper.Y = Minecraft.clientInstance.localPlayer.Position_lower.Y + 0f;
        }
    }
}
