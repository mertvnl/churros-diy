using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;

namespace Game.Managers 
{
    public class ProgressManager : Singleton<ProgressManager>
    {
        public IStateProgressIndicator CurrentStateIndicator { get; private set; }

        public void SetProgressIndicator(IStateProgressIndicator stateProgress) 
        {
            CurrentStateIndicator = stateProgress;
        }
    }
}

