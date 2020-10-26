using Ace_client.AceSDK;
using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public  class Fly : TickingModule
    {
        
        
           
                public Fly() : base("Movement")
                {

                }

                public override void onDisable()
                {
                    base.onDisable();

                    if(Minecraft.clientInstance.localPlayer.currentGamemode != 1)
                        Minecraft.clientInstance.localPlayer.canFly ^= false;
                }

                public override void onTick()
                {
                    Minecraft.clientInstance.localPlayer.canFly ^= true;
                }
            
        
      
    }
}
