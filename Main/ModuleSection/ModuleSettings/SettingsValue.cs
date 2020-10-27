using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.ModuleSettings
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct SettingsValue
    {
        [FieldOffset(0)]
        public float* fValue;
        [FieldOffset(0)]
        public int* i32Value;

        [FieldOffset(4)]
        public SettingsValueTypeEnum type;

        public unsafe SettingsValue(float* v)
        {
            i32Value = null;
            fValue = v;
            type = SettingsValueTypeEnum.FLOAT;
        }

        public unsafe SettingsValue(int* v)
        {
            fValue = null;
            i32Value = v;
            type = SettingsValueTypeEnum.INT;
        }
    }

    public enum SettingsValueTypeEnum
    {
        FLOAT,
        INT
    }
}
