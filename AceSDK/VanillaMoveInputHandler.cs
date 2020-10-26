using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.AceSDK
{
    public class VanillaMoveInputHandler : SDKHandler
    {
        public PropertyField<bool> isCrouching;
        public PropertyField<bool> isJumping;
        public PropertyField<bool> isHoldingSprint;

        public PropertyField<bool> isPressingForward;
        public PropertyField<bool> isPressingBackward;
        public PropertyField<bool> isPressingLeft;
        public PropertyField<bool> isPressingRight;

        public VanillaMoveInputHandler(ulong address): base(address)
        {
            isCrouching     = address + 0x48;
            isJumping       = address + 0x5B;
            isHoldingSprint = address + 0x5C;

            isPressingForward  = address + 0x5F;
            isPressingBackward = address + 0x60;
            isPressingLeft     = address + 0x61;
            isPressingRight    = address + 0x62;
        }
    }
}
