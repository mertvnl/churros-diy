using Game.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers 
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        public GameStateMachine CurrentStateMachine { get; private set; }

        public void SetCurrentStateMachine(GameStateMachine stateMachine) 
        {
            CurrentStateMachine = stateMachine;
        }
    }
}

