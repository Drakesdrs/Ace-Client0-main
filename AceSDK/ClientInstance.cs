using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.Memory;

namespace Ace_client.AceSDK
{
    public class ClientInstance : SDKHandler 
    {
        public Game gameInstance; // 1.16.40
      //public LevelRenderer levelRenderer; // 1.16.40
        public LoopbackPacketSender loopbackPacketSender; // 1.16.40
        public HolographicPlatform holographicPlatform; // 1.16.40
        public VoiceSystem voiceSystem; // 1.16.40
        public VanillaMoveInputHandler vanillaMoveInputHandler; // 1.16.40
        public HitDetectSystem hitDetectSystem; // 1.16.40
        public UserAuthentication userAuthentication; // 1.16.40
        public VanillaSceneFactory vanillaSceneFactory; // 1.16.40
        public LocalPlayer localPlayer; // 1.16.40
        public SceneStack sceneStack; // 1.16.40
        public ContentCatalogService contentCatalogService; // 1.16.40
        public UIProfanityContext uIProfanityContext; // 1.16.40
        public TaskGroup taskGroup; // 1.16.40
        public ActorRenderDispatcher actorRenderDispatcher; // 1.16.40
        public ItemInHandRenderer itemInHandRenderer; // 1.16.40
        public GuiData guiData; // 1.16.40
        public ToastManager toastManager; // 1.16.40

        public ClientInstance(ulong address) : base(address)
        {
            gameInstance            = new Game                   (AceMCM.Read<ulong>(address + 0x98 )); 
          //levelRenderer           = new LevelRenderer          (AceMCM.Read<ulong>(address + 0xC8 ));
            loopbackPacketSender    = new LoopbackPacketSender   (AceMCM.Read<ulong>(address + 0xD8 ));
            voiceSystem             = new VoiceSystem            (AceMCM.Read<ulong>(address + 0xE8 ));
            vanillaMoveInputHandler = new VanillaMoveInputHandler(AceMCM.Read<ulong>(address + 0xF0 ));
            holographicPlatform     = new HolographicPlatform    (AceMCM.Read<ulong>(address + 0x108));
            userAuthentication      = new UserAuthentication     (AceMCM.Read<ulong>(address + 0x110));
            vanillaSceneFactory     = new VanillaSceneFactory    (AceMCM.Read<ulong>(address + 0x120));
            localPlayer             = new LocalPlayer            (AceMCM.Read<ulong>(address + 0x140));
            sceneStack              = new SceneStack             (AceMCM.Read<ulong>(address + 0x460));
            uIProfanityContext      = new UIProfanityContext     (AceMCM.Read<ulong>(address + 0x478));
            taskGroup               = new TaskGroup              (AceMCM.Read<ulong>(address + 0x4A0));
            actorRenderDispatcher   = new ActorRenderDispatcher  (AceMCM.Read<ulong>(address + 0x4B8));
            itemInHandRenderer      = new ItemInHandRenderer     (AceMCM.Read<ulong>(address + 0x4C8));
            guiData                 = new GuiData                (AceMCM.Read<ulong>(address + 0x4D8));
            toastManager            = new ToastManager           (AceMCM.Read<ulong>(address + 0x4F0));
        }

    }
}
