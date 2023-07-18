using Dreamteck.Splines;
using Game.Managers;
using Game.Models;
using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class CreamGenerator : MonoBehaviour
    {
        private LeanSelectableByFinger _leanSelectable;
        private LeanSelectableByFinger LeanSelectable => _leanSelectable == null ? _leanSelectable = GetComponent<LeanSelectableByFinger>() : _leanSelectable;
        
        public bool IsFirstPoint => _currentPointCount == 0;

        [Header("References")]
        [SerializeField] private SplineComputer creamSplinePrefab;
        [SerializeField] private Transform raycastPoint;

        [Space(10)]
        [Header("Settings")]
        [SerializeField] private LayerMask churrosLayer;
        [SerializeField] private float creamDistanceThreshold = 1.0f;

        private Vector3 _lastSpawnPoint = Vector3.negativeInfinity;
        private int _currentPointCount;
        private int _totalPointCount;
        private SplineComputer _currentSpline;
        private SplineMesh _currentSplineMesh;
        private bool _isCreamingInProgress;
        private Color _targetMeshColor;

        private const float MIN_SPAWN_DISTANCE = 0.02f;
        private const float MAX_SPAWN_DISTANCE = 0.3f;
        private const float POINT_SPAWN_OFFSET = 0f;
        private const int MAX_SPAWN_SPLINE_POINT = 150;

        private void OnEnable()
        {
            LeanSelectable.OnSelectedFinger.AddListener(CreateCreamSpline);
            EventManager.OnCreamItemSelected.AddListener(UpdateMeshColor);
        }

        private void OnDisable()
        {
            LeanSelectable.OnSelectedFinger.RemoveListener(CreateCreamSpline);
            EventManager.OnCreamItemSelected.RemoveListener(UpdateMeshColor);
        }

        private void UpdateMeshColor(CreamData data)
        {
            _targetMeshColor = data.Color;
        }

        private void CreateCreamSpline(LeanFinger arg0)
        {
            _currentSpline = Instantiate(creamSplinePrefab, ChurrosManager.Instance.CurrentChurros.transform);
            _currentSplineMesh = _currentSpline.GetComponent<SplineMesh>();
            _currentSplineMesh.GetComponent<MeshRenderer>().material.color = _targetMeshColor;
            _currentPointCount = 0;
            _lastSpawnPoint = Vector3.negativeInfinity;
            _isCreamingInProgress = false;
        }

        private void Update()
        {
            CheckHeight();
        }

        private void CheckHeight()
        {
            if (!LeanSelectable.IsSelected)
                return;

            if (Physics.Raycast(raycastPoint.position + (Vector3.up * 0.2f), Vector3.down, out RaycastHit hit, creamDistanceThreshold, churrosLayer))
            {
                Vector3 spawnPoint = hit.point + Vector3.up * POINT_SPAWN_OFFSET;

                if (IsFirstPoint)
                {
                    _isCreamingInProgress = true;
                    SpawnSplinePoint(spawnPoint);
                    return;
                }

                float pointDistance = Vector3.Distance(spawnPoint, _lastSpawnPoint);

                if (pointDistance > MIN_SPAWN_DISTANCE && pointDistance < MAX_SPAWN_DISTANCE)
                    SpawnSplinePoint(spawnPoint);
            }
            else
            {
                if (_isCreamingInProgress)
                    CreateCreamSpline(null);
            }
        }

        private void SpawnSplinePoint(Vector3 spawnPosition)
        {
            if (!CanSpawnPoint())
                return;

            _currentPointCount++;
            _totalPointCount++;
            _lastSpawnPoint = spawnPosition;
            SplinePoint newPoint = new(spawnPosition);
            _currentSpline.SetPoint(_currentSpline.pointCount, newPoint);
            SetMeshCount();

            if (IsMaxPointReached())
            {
                //TODO: Automatically Finish Cream Generation State.
                LeanSelectable.Deselect();
                LeanSelectable.OnSelectedFingerUp.Invoke(new LeanFinger());
                LeanSelectable.enabled = false;
            }
        }

        private bool CanSpawnPoint()
        {
            if (!LeanSelectable.IsSelected)
                return false;

            if (IsMaxPointReached())
                return false;

            return true;
        }

        private bool IsMaxPointReached()
        {
            return _totalPointCount >= MAX_SPAWN_SPLINE_POINT;
        }

        private void SetMeshCount()
        {
            _currentSplineMesh.GetChannel(0).count = _currentSpline.pointCount;
        }
    }
}