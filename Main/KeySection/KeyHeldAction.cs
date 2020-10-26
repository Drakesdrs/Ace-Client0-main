using Ace_client.Main.ActionSection;
using Ace_client.Main.ModuleSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.KeySection
{
    public class KeyHeldAction : Keybind
    {
        public KeyHeldAction(Module module)
        {
            this.keyDownActions.Add(new EnableModuleAction(module));
            this.keyUpActions.Add(new DisableModuleAction(module));
        }
    }
}
