using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Helpers
{
    public class LeanInputController : MonoBehaviour
    {
        public static LeanInputController Instance = null;

        public UnityEvent OnFingerDown { get; private set; } = new();
        public UnityEvent OnFingerUp { get; private set; } = new();

        public void Awake()
        {
            Instance = this;
        }

        public void FingerDown(LeanFinger leanFinger)
        {
            OnFingerDown.Invoke();
        }

        public void FingerUp(LeanFinger leanFinger)
        {
            OnFingerUp.Invoke();
        }
    }
}

