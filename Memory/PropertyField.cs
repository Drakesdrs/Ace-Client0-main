using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ace_client.AceSDK.Utils;

namespace Ace_client.Memory
{
    /// <summary>
    /// WARNING: Must use a type that is 64 bits or lower.
    /// </summary>
    /// <typeparam name="T">Type to be used. Must be 64 bits or less. For anything higher, use a regular property.</typeparam>
    public class PropertyField<T>
        where T : unmanaged
    {
        public ulong address;

        public PropertyField(ulong address)
        {
            this.address = address;
        }
        public static implicit operator PropertyField<T>(ulong address) => new PropertyField<T>(address);

        public static unsafe implicit operator T(PropertyField<T> prop) => prop.value;
        public static T operator ~(PropertyField<T> prop)
        {
            return prop.value;
        }

        public static T operator ^(T val, PropertyField<T> prop)
        {
            prop.value = val;
            return val;
        }

        public static PropertyField<T> operator ^(PropertyField<T> prop, T val)
        {
            prop.value = val;
            return prop;
        }

        public static unsafe PropertyField<T> operator ^(PropertyField<T> prop, int val)
        {
            prop.value = *(T*)(void*)&val;
            return prop;
        }

        public static unsafe PropertyField<T> operator ^(PropertyField<T> prop, long val)
        {
            prop.value = *(T*)(void*)&val;
            return prop;
        }

        public static unsafe PropertyField<T> operator ^(PropertyField<T> prop, ulong val)
        {
            prop.value = *(T*)(void*)&val;
            return prop;
        }

        public static unsafe PropertyField<T> operator ^(PropertyField<T> prop, short val)
        {
            prop.value = *(T*)(void*)&val;
            return prop;
        }

        public static unsafe PropertyField<T> operator ^(PropertyField<T> prop, ushort val)
        {
            prop.value = *(T*)(void*)&val;
            return prop;
        }

        public static unsafe PropertyField<T> operator ^(PropertyField<T> prop, byte val)
        {
            prop.value = *(T*)(void*)&val;
            return prop;
        }

        public static unsafe PropertyField<T> operator ^(PropertyField<T> prop, float val)
        {
            prop.value = *(T*)(void*)&val;
            return prop;
        }

        public T value
        {
            get => AceMCM.Read<T>(this.address);
            set => AceMCM.Write(this.address, value);
        }
    }

    public class Vec3Field<T> where T : unmanaged
    {
        public ulong address;
        public Vec3Field(ulong address)
        {
            this.address = address;
        }
        public static implicit operator Vec3Field<T>(ulong address)
            => new Vec3Field<T>(address);

        public static Vec3Field<T> operator ^(Vec3Field<T> field, Vec3<T> val)
        {
            AceMCM.WriteVec3(field.address, val);
            return field;
        }

        public static implicit operator Vec3<T>(Vec3Field<T> field)
            => AceMCM.ReadVec3<T>(field.address);

        public T X
        {
            get => AceMCM.Read<T>(this.address + 0);
            set => AceMCM.Write(this.address + 0, value);
        }

        public T Y
        {
            get => AceMCM.Read<T>(this.address + 4);
            set => AceMCM.Write(this.address + 4, value);
        }

        public T Z
        {
            get => AceMCM.Read<T>(this.address + 8);
            set => AceMCM.Write(this.address + 8, value);
        }
    }

    public class Vec2Field<T> where T : unmanaged
    {
        public ulong address;
        public Vec2Field(ulong address)
        {
            this.address = address;
        }
        public static implicit operator Vec2Field<T>(ulong address)
            => new Vec2Field<T>(address);

        public static Vec2Field<T> operator ^(Vec2Field<T> field, Vec2<T> val)
        {
            AceMCM.WriteVec2(field.address, val);
            return field;
        }

        public static implicit operator Vec2<T>(Vec2Field<T> field)
            => AceMCM.ReadVec2<T>(field.address);

        public float X
        {
            get => AceMCM.Read<float>(this.address + 0);
            set => AceMCM.Write(this.address + 0, value);
        }

        public float Y
        {
            get => AceMCM.Read<float>(this.address + 4);
            set => AceMCM.Write(this.address + 4, value);
        }
    }
}
