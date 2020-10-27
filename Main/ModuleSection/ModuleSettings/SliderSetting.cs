using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.ModuleSettings
{
    public unsafe class SliderSetting : SettingHelper
    {
        public SettingsValue min;
        public SettingsValue value;
        public SettingsValue max;

        public unsafe SliderSetting(string text, SettingsValue min, SettingsValue value, SettingsValue max) : base(text)
        {
            this.min = min;
            this.value = value;
            this.max = max;
        }
    }
}
