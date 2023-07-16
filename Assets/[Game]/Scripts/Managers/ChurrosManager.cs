using Game.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class ChurrosManager : Singleton<ChurrosManager>
    {
        public Churros CurrentChurros {  get; private set; }

        public void SetCurrentChurros(Churros churros)
        {
            CurrentChurros = churros;
        }
    }
}