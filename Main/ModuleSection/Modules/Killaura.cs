using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class Killaura : Module
    {
        public Killaura() : base("Combat")
        {

        }

        public override void onEnable()
        {
            killauraThreadStart();
        }

        public void killauraThreadStart()
        {
            if (enabled)
            {
                Entity closestEnt = Utils.getClosestEntity(Minecraft.clientInstance.localPlayer.level.getMovingEntities);

                Minecraft.clientInstance.localPlayer.level.setLookingEnt = closestEnt.address;
                Minecraft.clientInstance.localPlayer.level.lookingState = 1;
            }

            killauraThreadStart();

        }

        public override void onDisable()
        {
            
        }
    }
}
