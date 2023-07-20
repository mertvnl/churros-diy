using Cinemachine;
using Game.Enums;
using Game.Interfaces;
using Game.Runtime;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Managers 
{
    public class CameraManager : Singleton<CameraManager>
    {        
        private Dictionary<CameraID, IVirtualCamera> _virtualCameras = new();
        public Dictionary<CameraID, IVirtualCamera> VirtualCameras { get => _virtualCameras; private set => _virtualCameras = value; }
        public CameraBrain CameraBrain { get; private set; }
        public IVirtualCamera CurrentActiveCamera { get; private set; }
        public ICameraTarget CurrentCameraTarget { get; private set; }

        private const int ACTIVE_PRIORITY = 20;
        private const int PASSIVE_PRIORITY = 9;
        private const CinemachineBlendDefinition.Style BRAIN_BLEND_EASE = CinemachineBlendDefinition.Style.EaseInOut;

        public void AddCamera(IVirtualCamera virtualCamera)
        {
            if (VirtualCameras.ContainsKey(virtualCamera.CameraID))
                return;

            VirtualCameras.Add(virtualCamera.CameraID, virtualCamera);
        }

        public void RemoveCamera(IVirtualCamera virtualCamera)
        {
            if (!VirtualCameras.ContainsKey(virtualCamera.CameraID))
                return;

            VirtualCameras.Remove(virtualCamera.CameraID);
        }

        public void ActivateCamera(CameraID cameraID, float blendDuration = 0)
        {
            if (!VirtualCameras.ContainsKey(cameraID))
                return;
            
            IVirtualCamera virtualCamera = VirtualCameras[cameraID];
            ActivateCamera(virtualCamera, blendDuration);
        }

        public void ActivateCamera(IVirtualCamera targetVirtualCamera, float blendDuration)
        {
            if (CurrentActiveCamera == targetVirtualCamera)
                return;

            SetBrainBlend(blendDuration);
            SetVirtualCameraPriority(targetVirtualCamera, ACTIVE_PRIORITY);

            foreach (IVirtualCamera virtualCamera in VirtualCameras.Values)
            {
                if (virtualCamera == targetVirtualCamera)
                    continue;

                SetVirtualCameraPriority(virtualCamera, PASSIVE_PRIORITY);
            }

            CurrentActiveCamera = targetVirtualCamera;
        }

        public void SetCameraBrain(CameraBrain cameraBrain)
        {
            CameraBrain = cameraBrain;
        }

        public void SetCameraTarget(ICameraTarget cameraTarget)
        {
            CurrentCameraTarget = cameraTarget;
            ApplyCameraTarget(CurrentActiveCamera, CurrentCameraTarget);
        }

        private void SetBrainBlend(float blendDuration)
        {
            if (CameraBrain == null)
                return;

            CameraBrain.CinemachineBrain.m_DefaultBlend = new(BRAIN_BLEND_EASE, blendDuration);
        }

        private void SetVirtualCameraPriority(IVirtualCamera virtualCamera, int priority)
        {
            virtualCamera.VirtualCamera.Priority = priority;
        }

        private void ApplyCameraTarget(IVirtualCamera virtualCamera, ICameraTarget cameraTarget)
        {
            if (virtualCamera == null || cameraTarget == null)
                return;

            virtualCamera.VirtualCamera.m_LookAt = cameraTarget.Body;
            virtualCamera.VirtualCamera.m_Follow = cameraTarget.Body;
        }
    }
}

