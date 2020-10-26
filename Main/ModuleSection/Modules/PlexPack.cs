using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class PlexPack : Module
    {
        public PlexPack() : base("Other")
        {

        }

        public override void onEnable()
        {
            
            IceWalk.instance.enabled = true;
        }
        public override void onDisable()
        {
            
            IceWalk.instance.enabled = false;
        }
    }
}
