using Dreamteck.Splines;
using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class Churros : MonoBehaviour
    {
        private SplineComputer _splineComputer;
        public SplineComputer SplineComputer => _splineComputer == null ? _splineComputer = GetComponent<SplineComputer>() : _splineComputer;

        private SplineMesh _splineMesh;
        public SplineMesh SplineMesh => _splineMesh == null ? _splineMesh = GetComponent<SplineMesh>() : _splineMesh;

        private void Start()
        {
            ChurrosManager.Instance.SetCurrentChurros(this);
        }
    }
}