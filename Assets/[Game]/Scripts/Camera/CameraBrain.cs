using Cinemachine;
using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime 
{
    public class CameraBrain : MonoBehaviour
    {
        private CinemachineBrain _cinemachineBrain;
        public CinemachineBrain CinemachineBrain => _cinemachineBrain == null ? _cinemachineBrain = GetComponent<CinemachineBrain>() : _cinemachineBrain;

        private void Awake()
        {
            CameraManager.Instance.SetCameraBrain(this);
        }        
    }
}

