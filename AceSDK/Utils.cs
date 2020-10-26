using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using static Ace_client.AceSDK.Utils;

namespace Ace_client.AceSDK
{

    public static class Utils
    {
        public static Random random = new Random();
        public static BinaryFormatter binaryFormatter = new BinaryFormatter();

        #region Vectors (not the fuckin c++ kind, k?)

        public class Vec2<T>
        {
            public T x, y;
        }
        public class Vec3<T> : Vec2<T>
        {
            public T z;
        }
        public class Vec4<T> : Vec3<T>
        {
            public T w;
        }

        public class Vec2f : Vec2<float> { }
        public class Vec3f : Vec3<float> { }
        public class Vec4f : Vec4<float> { }

        public class Vec2d : Vec2<double> { }
        public class Vec3d : Vec3<double> { }
        public class Vec4d : Vec4<double> { }

        public class Vec2i : Vec2<int> { }
        public class Vec3i : Vec3<int> { }
        public class Vec4i : Vec4<int> { }

        public class Vec2ui : Vec2<uint> { }
        public class Vec3ui : Vec3<uint> { }
        public class Vec4ui : Vec4<uint> { }

        public class Vec2l : Vec2<long> { }
        public class Vec3l : Vec3<long> { }
        public class Vec4l : Vec4<long> { }

        public class Vec2ul : Vec2<ulong> { }
        public class Vec3ul : Vec3<ulong> { }
        public class Vec4ul : Vec4<ulong> { }

        public class Vec2s : Vec2<short> { }
        public class Vec3s : Vec3<short> { }
        public class Vec4s : Vec4<short> { }

        public class Vec2us : Vec2<ushort> { }
        public class Vec3us : Vec3<ushort> { }
        public class Vec4us : Vec4<ushort> { }

        public class Vec2b : Vec2<byte> { }
        public class Vec3b : Vec3<byte> { }
        public class Vec4b : Vec4<byte> { }

        #endregion

        public static readonly float   g_cfPI = 3.1415926535f;
        public static readonly double  g_cdbPI = 3.14159265358979324;
        public static readonly decimal g_cMPI = 3.1415926535897932384626433832795028841971694M;

        public static Vec3f directionalVector(float yaw, float pitch)
        {
            Vec3f vec3 = new Vec3f();
            vec3.x = (float)Math.Cos(yaw) * (float)Math.Cos(pitch);
            vec3.y = (float)Math.Sin(pitch);
            vec3.z = (float)Math.Sin(yaw) * (float)Math.Cos(pitch);
            return vec3;
        }

        public static Vec2f getCalculationsToPos(Vec3f localPos, Vec3f targetPos)
        {
            Vec2f vec2 = new Vec2f();
            float dX = localPos.x - targetPos.x;
            float dY = localPos.y - targetPos.y;
            float dZ = localPos.z - targetPos.z;
            double distance = Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
            vec2.x = -((float)Math.Atan2(dY, (float)distance) * (float)3.13810205 / g_cfPI);
            vec2.y = -((float)Math.Atan2(dZ, dX) * (float)3.13810205 / g_cfPI) + (float)1.569051027;
            return vec2;
        }

        public static double distanceBetween(Vec3f localPos, Vec3f targetPos)
        {
            float dX = localPos.x - targetPos.x;
            float dY = localPos.y - targetPos.y;
            float dZ = localPos.z - targetPos.z;
            return Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
        }

        public static Vec3f posBetween(Vec3f localPos, Vec3f targetPos)
        {
            float dX = localPos.x + targetPos.x;
            float dY = localPos.y + targetPos.y;
            float dZ = localPos.z + targetPos.z;
            Vec3f newPos = new Vec3f()
            {
                x = dX / 2,
                y = dY / 2,
                z = dZ / 2
            };
            return newPos;
        }

        
        public static Entity getClosestEntity(List<Entity> EntitiesArr)
        {
            List<double> distances = new List<double>();

            foreach (Entity currEnt in EntitiesArr)
            {
                distances.Add(currEnt.distanceTo(Minecraft.clientInstance.localPlayer));
            }

            if (distances.Count() > 0)
            {
                distances.Sort();

                foreach (Entity ent in EntitiesArr)
                {
                    if (ent.distanceTo(Minecraft.clientInstance.localPlayer) == distances[0]) return ent;
                }
            }

            return null;
        }
    }
}
