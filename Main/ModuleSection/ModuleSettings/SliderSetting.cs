using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.ModuleSettings
{
    public unsafe  class SliderSetting : SettingHelper
    {
        public int min = 0;
        public int* value = null;
        public int max = 0;

        public unsafe SliderSetting(string text, int min, int* value, int max) : base(text)
        {
            this.min = min;
            this.value = value;
            this.max = max;
        }
    }
}
