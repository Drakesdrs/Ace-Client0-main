using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class IceWalk : Module
    {
        public static IceWalk instance;
        
        public IceWalk() : base("Movement")
        {
            instance = this;
        }

        public override void onEnable()
        {
            base.onEnable();
            Statics.Fasts.IceWalkOn();
        }
        public override void onDisable()
        {
            base.onDisable();
            Statics.Fasts.IceWalkOff();
        }
    }
}
