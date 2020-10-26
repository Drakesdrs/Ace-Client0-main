using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.ModuleSettings
{
    public unsafe  class SliderFloatSetting : SettingHelper
    {
        public float min = 0;
        public float* value = null;
        public float max = 0;
        public unsafe SliderFloatSetting(string text, float min, float* value, float max) : base(text)
        {
            this.min = min;
            this.value = value;
            this.max = max;
        }
    }
}
