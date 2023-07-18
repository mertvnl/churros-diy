using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Managers;
using Game.Utilities;
using DG.Tweening;

namespace Game.Runtime 
{
    public class ToppingBottleSpawner : MonoBehaviour
    {
        private ToppingBottle _toppingBottle;
        private ToppingBottle ToppingBottle => _toppingBottle == null ? _toppingBottle = GetComponent<ToppingBottle>() : _toppingBottle;

        private float DefaultHeight => ToppingBottle.DefaultPosition.y;

        private const float HEIGHT_OFFSET = 0.5f;
        private const float MOVEMENT_DURATION = 0.2f;

        [SerializeField] private Transform body;
        [SerializeField] private MeshRenderer visual;
        
        private Sequence _spawnSequence;

        private void OnEnable()
        {
            ToppingBottle.OnDisabled.AddListener(StopSpawn);
        }

        private void OnDisable()
        {
            ToppingBottle.OnDisabled.RemoveListener(StopSpawn);
        }

        private void Update()
        {
            if (!ToppingBottle.IsActive)
                return;

            if(Input.GetMouseButtonDown(0))
            {
                StartSpawn();
            }
            else if(Input.GetMouseButtonUp(0)) 
            {
                StopSpawn();
            }
        }

        private void StartSpawn() 
        {
            _spawnSequence?.Kill();
            _spawnSequence = DOTween.Sequence();
            _spawnSequence.Append(body.DOLocalMoveY(DefaultHeight + HEIGHT_OFFSET, MOVEMENT_DURATION).SetEase(Ease.Linear))
            .Append(body.DOLocalMoveY(DefaultHeight + HEIGHT_OFFSET, MOVEMENT_DURATION).SetEase(Ease.Linear).OnComplete(() => SpawnTopping()))
            .SetLoops(-1, LoopType.Restart);
        }

        private void StopSpawn() 
        {
            _spawnSequence?.Kill();
        }

        private void SpawnTopping() 
        {
            GameObject prefab = GetToppingPrefab();
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(prefab, spawnPosition, Random.rotation);
        }

        private Vector3 GetSpawnPosition() 
        {
            float radius = visual.bounds.size.x;
            Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * radius;
            return spawnPosition;
        }

        private GameObject GetToppingPrefab() 
        {
            List<GameObject> prefabs = new(ToppingManager.Instance.CurrentToppingData.Prefabs);
            prefabs.Shuffle();
            return prefabs[0];
        }
    }
}

