using Dreamteck.Splines;
using Game.Managers;
using Lean.Touch;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class ChurrosGenerator : MonoBehaviour
    {
        private SplineComputer _splineComputer;
        public SplineComputer SplineComputer => _splineComputer == null ? _splineComputer = ChurrosManager.Instance.CurrentChurros.SplineComputer : _splineComputer;

        private PastryBag _pastryBag;
        private PastryBag PastryBag => _pastryBag == null ? _pastryBag = GetComponent<PastryBag>() : _pastryBag;

        [SerializeField] private LayerMask spawnableLayer;
        [SerializeField] private Transform raycastPoint;

        private Vector3 _lastSpawnPoint = Vector3.negativeInfinity;
        private bool _isAvailable;

        private const float MIN_SPAWN_DISTANCE = 0.02f;
        private const float MAX_SPAWN_DISTANCE = 0.2f;
        private const float POINT_SPAWN_OFFSET = 0.05f;
        private const int MAX_SPAWN_SPLINE_POINT = 100;

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelLoaded.AddListener(Initialize);
        }

        private void OnDisable()
        {
            LevelManager.Instance.OnLevelLoaded.RemoveListener(Initialize);
        }

        private void Update()
        {
            CheckSpawnPosition();
        }

        public void SetActivation(bool status)
        {
            _isAvailable = status;
        }

        private void Initialize()
        {
            SetActivation(false);
        }

        private void CheckSpawnPosition()
        {
            if (!_isAvailable)
                return;

            if (Physics.Raycast(raycastPoint.position, Vector3.down, out RaycastHit hit, 100, spawnableLayer))
            {
                Vector3 spawnPoint = hit.point + Vector3.up * POINT_SPAWN_OFFSET;

                if (IsFirstPoint())
                {
                    SpawnSplinePoint(spawnPoint);
                    return;
                }

                float pointDistance = Vector3.Distance(spawnPoint, _lastSpawnPoint);

                if (pointDistance > MIN_SPAWN_DISTANCE && pointDistance < MAX_SPAWN_DISTANCE)
                    SpawnSplinePoint(spawnPoint);
            }
        }

        private void SpawnSplinePoint(Vector3 spawnPosition)
        {
            if (!CanSpawnPoint())
                return;

            _lastSpawnPoint = spawnPosition;
            SplinePoint newPoint = new(spawnPosition);
            SplineComputer.SetPoint(SplineComputer.pointCount, newPoint);
            SetMeshCount();

            if (IsMaxPointReached())
            {
                CompleteGeneration();
                GameStateManager.Instance.CurrentStateMachine.EnterNextState();
            }

            HapticManager.PlayHaptic(Lofelt.NiceVibrations.HapticPatterns.PresetType.SoftImpact);
        }

        private void SetMeshCount()
        {
            ChurrosManager.Instance.CurrentChurros.SplineMesh.GetChannel(0).count = SplineComputer.pointCount;
        }

        [Button]
        private void CompleteGeneration()
        {
            _isAvailable = false;
        }

        private bool CanSpawnPoint()
        {
            if (!_isAvailable)
                return false;

            if (!PastryBag.IsControlable)
                return false;

            if (IsMaxPointReached())
                return false;

            return true;
        }

        private bool IsMaxPointReached()
        {
            return SplineComputer.pointCount >= MAX_SPAWN_SPLINE_POINT;
        }

        private bool IsFirstPoint()
        {
            return SplineComputer.pointCount == 0;
        }
    }
}