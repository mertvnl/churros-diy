using Game.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game.Interfaces 
{
    public interface IVirtualCamera
    {
        CameraID CameraID { get; }
        CinemachineVirtualCamera VirtualCamera { get; }
        void ActivateCamera(float blendDuration);
    }
}

