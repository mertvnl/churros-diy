using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI 
{
    public class PlayButton : MonoBehaviour
    {
        public void StartLevel() 
        {
            LevelManager.Instance.StartLevel();
        }
    }
}

