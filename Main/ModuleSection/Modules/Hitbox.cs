using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class Hitbox : TickingModule
    {
        public Hitbox() : base("Combat")
        {

        }

        public override void onTick()
        {
            List<Entity> entList = Minecraft.clientInstance.localPlayer.level.getMovingEntities;
            foreach (Entity e in entList)
            {
                e.hitboxWidth  ^= 3.0f; // 1.8 to 2.0
                e.hitboxHeight ^= 1.0f; // 0.6 to 0.8
            }
        }

        public override void onDisable()
        {
            List<Entity> entList = Minecraft.clientInstance.localPlayer.level.getAllEntities;
            foreach (Entity e in entList)
            {
                e.hitboxWidth ^= 1.8f; // 2.0 to 1.8
                e.hitboxHeight ^= 0.6f; // 0.8 to 0.6
               
            }
        }
    }
}
