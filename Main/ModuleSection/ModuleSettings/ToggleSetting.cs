using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.ModuleSettings
{
    public class ToggleSetting : SettingHelper
    {
        public bool value = false;

        public ToggleSetting(string text, bool value) : base(text)
        {
            this.value = value;
        }
    }
}
//gg