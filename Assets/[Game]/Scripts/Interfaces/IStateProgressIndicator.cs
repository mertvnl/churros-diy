using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interfaces 
{
    public interface IStateProgressIndicator
    {
        void UpdateProgress(float progress);
    }
}

