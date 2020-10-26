using Ace_client.Main.ModuleSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ActionSection
{
    public class DisableModuleAction : Action
    {
        public Module module;

        public void run()
        {
            module.enabled = false;
        }

        public DisableModuleAction(Module module)
        {
            this.module = module;
        }
    }
}
