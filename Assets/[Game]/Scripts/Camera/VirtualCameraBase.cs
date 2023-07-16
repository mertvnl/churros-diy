using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;
using Cinemachine;
using Game.Managers;
using Sirenix.OdinInspector;
using Game.Enums;

namespace Game.Runtime 
{
    public abstract class VirtualCameraBase : MonoBehaviour, IVirtualCamera
    {
        private CinemachineVirtualCamera _virtualCamera;
        public CinemachineVirtualCamera VirtualCamera => _virtualCamera == null ? _virtualCamera = GetComponent<CinemachineVirtualCamera>() : _virtualCamera;

        [field: SerializeField] public CameraID CameraID { get; private set; }

        protected virtual void OnEnable()
        {
            CameraManager.Instance.AddCamera(this);
        }

        protected virtual void OnDisable()
        {
            CameraManager.Instance.RemoveCamera(this);
        }

        public virtual void ActivateCamera(float blendDuration)
        {
            CameraManager.Instance.ActivateCamera(this, blendDuration);
        }
    }
}

