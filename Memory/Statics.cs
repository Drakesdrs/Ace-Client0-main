using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Memory
{
    public class Statics //good to have if you want to optimize some stuff so you dont have to struggle with other type of stuff in the future.
    {
        public class Fasts
        {
            // v1.16.40 - AutoSneak
            public static unsafe void AutoSneakOn()
                => AceMCM.WriteBase<byte>(0x10513A0, 0xEB);

            public static unsafe void AutoSneakOff()
                => AceMCM.WriteBase<byte>(0x10513A0, 0x75);

            // v1.16.40 - NoWeb
            public static void NoWebOn()
               => AceMCM.WriteBaseBytes(0x12FE2A5, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            public static void NoWebOff()
                => AceMCM.WriteBaseBytes(0x12FE2A5, new byte[] { 0xF3, 0x0F, 0x11, 0x89, 0x14, 0x02, 0, 0 });

            // v1.16.40 - ConstantWeb
            public static void ConstantWebOn()
               => AceMCM.WriteBaseBytes(0x12FBD75, new byte[] { 0x90, 0x90});
            public static void ConstantWebOff()
                => AceMCM.WriteBaseBytes(0x12FBD75, new byte[] { 0x76, 0x36 });

            // v1.16.40 - Instabreak
            public static void InstabreakOn()
               => AceMCM.WriteBaseBytes(0x154E0DF, new byte[] { 0xF3, 0x0F, 0x11, 0x57, 0x20 });
            public static void InstabreakOff()
                => AceMCM.WriteBaseBytes(0x154E0DF, new byte[] { 0xF3, 0x0F, 0x11, 0x4F, 0x20 });

            // v1.16.40 - Reach
            public static void ReachOn()
               => AceMCM.WriteBaseBytes(0x65252A, new byte[] { 0x90, 0x90 });
            public static void ReachOff()
                => AceMCM.WriteBaseBytes(0x65252A, new byte[] { 0x74, 0x14 });

            // v1.16.40 - FallSaver
            public static void FallSaverOn()
               => AceMCM.WriteBaseBytes(0x12FBDEE, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            public static void FallSaverOff()
                => AceMCM.WriteBaseBytes(0x12FBDEE, new byte[] { 0x0F, 0x84, 0xB0, 0x05, 0, 0 });


            // v1.16.40 - Fly
            public static void FlyOn()
                => AceMCM.WriteBaseBytes(0x1BFED0C, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            public static void FlyOff()
                => AceMCM.WriteBaseBytes(0x1BFED0C, new byte[] { 0x80, 0xBF, 0xC4, 0x08, 0, 0, 0 });


            // v1.16.40 - IceWalk
            public static void IceWalkOn()
                => AceMCM.WriteBaseBytes(0x1491772, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            public static void IceWalkOff()
                => AceMCM.WriteBaseBytes(0x1491772, new byte[] { 0x45, 0x38, 0xA6, 0xA0, 1, 0, 0 });


            // v1.16.40 - Coords
            public static void CoordsOn()
                => AceMCM.WriteBaseBytes(0x680C6D, new byte[] { 0x90, 0x90, 0x90, 0x90 });
            public static void CoordsOff()
                => AceMCM.WriteBaseBytes(0x680C6D, new byte[] { 0x80, 0x78, 0x04, 0x00 });


            // v1.16.40 - NoFire
            public static void NoFireOn()
                => AceMCM.WriteBaseBytes(0x1497AC7, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
            public static void NoFireOff()
                => AceMCM.WriteBaseBytes(0x1497AC7, new byte[] { 0x89, 0x8F, 0x7C, 2, 0, 0 });


            // v1.16.40 - Airjump
            public static void AirJumpOn()
            {
                AceMCM.WriteBaseBytes(0x12FD1D9, new byte[] { 0xE9, 0x23, 0xD4, 0, 0, 0x90, 0x90 });
                AceMCM.WriteBaseBytes(0x130A601, new byte[] { 0xC7, 0x87, 0xA0, 1, 0, 0, 1, 0, 0, 0, 0xE9, 0xCF, 0x2B, 0xFF, 0xFF });
            }
            public static void AirJumpOff()
            {
                AceMCM.WriteBaseBytes(0x12FD1D9, new byte[] { 0x44, 0x88, 0x87, 0xA0, 1, 0, 0 });
                AceMCM.WriteBaseBytes(0x130A601, new byte[] { 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC });
            }

            

            /*
            class ClientInstance - 1.16.40
            {
                public:
            class Game *Game; //0x0098
            class LevelRenderer *LevelRenderer; //0x00C8
            class LoopbackPacketSender *LoopbackPacketSender; //0x00D8
            class HolographicPlatform *HolographicPlatform; //0x00E0
            class VoiceSystem *VoiceSystem; //0x00E8
            class VanillaMoveInputHandler *VanillaMoveInputHandler; //0x00F0
            class HitDetectSystem *HitDetectSystem; //0x0108
            class UserAuthentication *UserAuthentication; //0x0110
            class VanillaSceneFactory *VanillaSceneFactory; //0x0120
            class LocalPlayer *LocalPlayer; //0x0140
            class SceneStack *SceneStack; //0x0460
            class ContentCatalogService *ContentCatalogService; //0x0470
            class UIProfanityContext *UIProfanityContext; //0x0478
            class TaskGroup *TaskGroup; //0x04A0
            class ActorRenderDispatcher *ActorRenderDispatcher; //0x04B8
            class ItemInHandRenderer *ItemInHandRenderer; //0x04C8
            class GuiData *GuiData; //0x04D8
            class ToastManager *ToastManager; //0x04F0

            }; //Size: 0x13E0
            static_assert(sizeof(ClientInstance) == 0x13E0);
            */
        }
    }
}
