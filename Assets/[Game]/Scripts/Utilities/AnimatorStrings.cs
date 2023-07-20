using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Helpers 
{
    public static class AnimatorStrings 
    {
        public static readonly int Walk = Animator.StringToHash(nameof(Walk));
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));
    }
}

