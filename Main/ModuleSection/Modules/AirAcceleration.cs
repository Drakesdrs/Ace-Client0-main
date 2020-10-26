using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class AirAcceleration : TickingModule
    {
        

        public AirAcceleration() : base("Movement")
        {

        }

        public override void onTick()
        {
            Minecraft.clientInstance.localPlayer.airAcceleration = 10;
        }
    }
}
