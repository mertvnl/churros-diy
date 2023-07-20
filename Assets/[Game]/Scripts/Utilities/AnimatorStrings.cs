using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Helpers 
{
    public static class AnimatorStrings 
    {
        public static readonly int Walk = Animator.StringToHash(nameof(Walk));
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));
        public static readonly int Order = Animator.StringToHash(nameof(Order));
        public static readonly int Happy = Animator.StringToHash(nameof(Happy));
        public static readonly int Sad = Animator.StringToHash(nameof(Sad));
    }
}

