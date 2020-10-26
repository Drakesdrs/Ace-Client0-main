using Ace_client.AceSDK;
using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.AceSDK
{
    public class Entity : Actor
    {
        public string name       => AceMCM.szRead   (address + 0x818);
        public string entityType => AceMCM.szPtrRead(address + 0x418);

        public PropertyField<bool>   onGround;
        public PropertyField<bool>   didEnterWaterBool;
        public PropertyField<bool>   hasTouchedDown;
        public PropertyField<bool>   isCollidingSide;
        public PropertyField<bool>   canFly;
        public PropertyField<bool>   isFlying;
        public PropertyField<int>    isInWater;
        public PropertyField<int>    currentGamemode;
        public PropertyField<float>  fallDistance;
       
        public PropertyField<float>  airAcceleration;
        public PropertyField<float>  web;
        public PropertyField<float>  stepHeight;
        public PropertyField<ushort> ageInTicks;
        public PropertyField<int>    timeOnDeathScreen;
        public PropertyField<int>    damageTime;
        public PropertyField<int>    movedTick;
        

        public Vec3Field<float> Velocity;

        public PropertyField<float> currentX1;
        public PropertyField<float> currentY1;
        public PropertyField<float> currentZ1;

        public PropertyField<int> heldItemCount;

        public Vec3Field<float> Position_lower;
        public Vec3Field<float> Position_upper;
        public Vec3Field<float> Position_3;

        public Vec3Field<float> eyePosition;
        public Vec2Field<float> viewAngles;
        public Vec2Field<float> old_viewAngles;

        public PropertyField<float> bodyYaw;
        public PropertyField<float> old_bodyYaw;
        public PropertyField<float> yaw;
        public PropertyField<float> pitch;

        public PropertyField<float> hitboxWidth;
        public PropertyField<float> hitboxHeight;

        public Level level;
        public PlayerAttributes playerAttributes;
       

        public Entity(ulong address): base(address)
        {
            onGround          = address + 0x1A0;
            hasTouchedDown    = address + 0x1A1;
            isCollidingSide   = address + 0x1A2;
            canFly            = address + 0x8C4;
            isFlying          = address + 0x8B8;
            currentGamemode   = address + 0x1C84;
            fallDistance      = address + 0x19C;
            airAcceleration   = address + 0x308;
            stepHeight        = address + 0x200;
            isInWater         = address + 0x21D;
            ageInTicks        = address + 0x26C;
            timeOnDeathScreen = address + 0x6BC;
            damageTime        = address + 0x630;
            movedTick         = address + 0x304;
            
           
            web               = address + 0x214;
            

            Position_lower    = address + 0x458;
            Position_upper    = address + 0x464;
            Position_3        = address + 0x488;

            Velocity          = address + 0x494;

            heldItemCount     = address + 0x2078;

            currentX1         = address + 0x458;
            currentY1         = address + 0x45C;
            currentZ1         = address + 0x460;

            hitboxWidth       = address + 0x474;
            hitboxHeight      = address + 0x478;


            //viewAngles      = address + 0x100;
            //old_viewAngles  = address + 0x108;

            eyePosition       = address + 0x120;

            bodyYaw           = address + 0x620;
            old_bodyYaw       = address + 0x624;
            yaw               = address + 0x104;
            pitch             = address + 0x100;

            level = new Level(AceMCM.Read<ulong>(address + 0x330));
            playerAttributes = new PlayerAttributes(AceMCM.Read<ulong>(address + 0x0));
        }

        public double distanceTo(Entity e)
        {
            float dX = currentX1 - e.currentX1;
            float dY = currentY1 - e.currentY1;
            float dZ = currentZ1 - e.currentZ1;
            return Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
        }

        public Utils.Vec3f lookingVec
        {
            get
            {
                return Utils.directionalVector((Minecraft.clientInstance.localPlayer.yaw + 89.9f) * (float)Math.PI / 178F, Minecraft.clientInstance.localPlayer.pitch * (float)Math.PI / 178F);
            }
        }

        /* public Utils.Vec3f lookingVec
         {
             get
             {
                 return Utils.directionalVector((Minecraft.clientInstance.localPlayer.yaw + 89.9f) * (float)Math.PI / 178F, Minecraft.clientInstance.localPlayer.pitch * (float)Math.PI / 178F);
             }
         }*/
    }
}

