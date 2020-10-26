using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.AceSDK
{
    public class Level : SDKHandler
    {
        public enum RayHitType
        {
            BLOCK = 0,
            ENTITY = 1,
            AIR = 3,
        }
        public enum BlockFace
        {
            NONE = 0,
            BOTTOM = 0,
            TOP = 1,
            NORTH = 2,
            SOUTH = 3,
            WEST = 4,
            EAST = 5
        }
        public PropertyField<RayHitType> rayHitType;
        public PropertyField<BlockFace> blockFace;

        public PropertyField<int> lookingBlockSide;
        public PropertyField<int> lookingState;

        public PropertyField<int> lookingBlockX;
        public PropertyField<int> lookingBlockY;
        public PropertyField<int> lookingBlockZ;

        public PropertyField<ulong> startList;
        public PropertyField<ulong> endList;
        public PropertyField<ulong> setLookingEnt;
        
        public Entity lookingEntity => new Entity(AceMCM.Read<ulong>(address + 0x900));

        public string workingDirectory => AceMCM.szPtrRead(address + 0x28D);

        public Level(ulong address) : base(address)
        {
            rayHitType    = address + 0x8E0;
            rayHitType    = address + 0x8E4;
            startList = address + 0x40;
            endList = address + 0x48;



            lookingBlockSide = address + 0x8E4;
            lookingBlockX = address + 0x8EC;
            lookingBlockY = address + 0x8D8;
            lookingBlockZ = address + 0x8FC;
            setLookingEnt = address + 0x900;
            lookingState = address + 0x8D8;
        }

        public List<Entity> getMovingEntities
        {
            get
            {
                List<Entity> Entities = new List<Entity>();
             
                for (ulong ent = startList; ent < endList; ent += 0x8)
                {
                    if (ent == startList) continue;
                    Entity entObj = new Entity(AceMCM.Read<ulong>(ent));
                    if (entObj.movedTick > 1) Entities.Add(entObj);
                }
                return Entities;
            }
        }

        public List<Entity> getAllEntities
        {
            get
            {
                List<Entity> Entities = new List<Entity>();
                
                for (ulong ent = startList; ent < endList; ent += 0x8)
                {
                    if (ent == startList) continue;
                    Entity entObj = new Entity(AceMCM.Read<ulong>(ent));
                    Entities.Add(entObj);
                }
                return Entities;
            }
        }

        public List<Gamerule> gamerules
        {
            get
            {
                List<Gamerule> returnRules = new List<Gamerule>();
                for (ulong ruleIndex = 0; ruleIndex < 28; ruleIndex++)
                {
                    returnRules.Add(new Gamerule(AceMCM.Read<ulong>(address + 0x340) + (ruleIndex * 176)));
                }
                return returnRules;
            }
        }


    }
}
