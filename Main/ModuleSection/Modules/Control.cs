using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class Control : TickingModule
    {
        public Control() : base("Movement")
        {

        }

        public override void onTick()
        {
            float playerYaw = Minecraft.clientInstance.localPlayer.yaw;

            bool MovingForward = Minecraft.clientInstance.vanillaMoveInputHandler.isPressingForward;
            bool MovingBackward = Minecraft.clientInstance.vanillaMoveInputHandler.isPressingBackward;
            bool MovingLeft = Minecraft.clientInstance.vanillaMoveInputHandler.isPressingLeft;
            bool MovingRight = Minecraft.clientInstance.vanillaMoveInputHandler.isPressingRight;

           if (MovingForward)
           {
               if (!MovingLeft && !MovingRight)
               {
                   playerYaw += 90F;
               }
               if (MovingLeft)
               {
                   playerYaw += 45F;
               }
               else if (MovingRight)
               {
                   playerYaw += 135F;
               }
           }
           else if (MovingBackward)
           {
               if (!MovingLeft && !MovingRight)
               {
                   playerYaw -= 90F;
               }
               if (MovingLeft)
               {
                   playerYaw -= 45F;
               }
               else if (MovingRight)
               {
                   playerYaw -= 135F;
               }
           }
           else if (!MovingForward && !MovingBackward)
           {
               if (!MovingLeft && MovingRight)
               {
                   playerYaw += 180F;
                }
                Minecraft.clientInstance.localPlayer.Velocity.X = 0;
                Minecraft.clientInstance.localPlayer.Velocity.Z = 0;
            }

           if (MovingForward | MovingRight | MovingLeft | MovingBackward)
           {
               float calcYaw = (playerYaw) * ((float)Math.PI / 180F);
               float calcPitch = (Minecraft.clientInstance.localPlayer.pitch) ^ -((float)Math.PI / 180f);

               Minecraft.clientInstance.localPlayer.Velocity.X = (float)Math.Cos(calcYaw) * 0.3F;
               Minecraft.clientInstance.localPlayer.Velocity.Z = (float)Math.Sin(calcYaw) * 0.3F;
           }
        }

        public override void onDisable()
        {

        }
    }
}   
