using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Managers;
using Game.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace Game.Runtime 
{
    public class ToppingSpawner : MonoBehaviour
    {
        private ToppingBottle _toppingBottle;
        private ToppingBottle ToppingBottle => _toppingBottle == null ? _toppingBottle = GetComponent<ToppingBottle>() : _toppingBottle;

        private float DefaultHeight => ToppingBottle.DefaultPosition.y;

        private const int MIN_SPAWN_COUNT = 3;
        private const int MAX_SPAWN_COUNT = 6;

        private const float HEIGHT_OFFSET = 0.05f;
        private const float MOVEMENT_DURATION = 0.15f;
        private const Ease MOVEMENT_EASE = Ease.Linear;

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

            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
            _spawnSequence.Append(body.DOLocalMoveY(DefaultHeight + HEIGHT_OFFSET, MOVEMENT_DURATION).SetEase(MOVEMENT_EASE).OnComplete(() => SpawnTopping()))
            .Append(body.DOLocalMoveY(DefaultHeight, MOVEMENT_DURATION * 0.8f).SetEase(MOVEMENT_EASE))
            .SetLoops(-1, LoopType.Restart);
        }

        private void StopSpawn() 
        {
            _spawnSequence?.Kill();            
        }

        private void SpawnTopping() 
        {
            int spawnCount = Random.Range(MIN_SPAWN_COUNT, MAX_SPAWN_COUNT);
            for (int i = 0; i < spawnCount; i++)
            {                       
                Topping topping = Instantiate(GetToppingPrefab(), GetSpawnPosition(), GetSpawnRotation()).GetComponent<Topping>();
                topping.Initialize();
            }            
        }

        private Vector3 GetSpawnPosition() 
        {
            float radius = visual.bounds.size.x / 2f;
            Vector2 unitCircle = Random.insideUnitCircle * radius;
            Vector3 spawnPosition = body.position + new Vector3(unitCircle.x, 0f, unitCircle.y);
            return spawnPosition;
        }

        private Quaternion GetSpawnRotation() 
        {
            float randomRotation = Random.Range(0f, 360f);
            return Quaternion.Euler(new Vector3(0f, randomRotation, 0f));
        }

        private GameObject GetToppingPrefab() 
        {
            List<GameObject> prefabs = new(ToppingManager.Instance.CurrentToppingData.Prefabs);
            prefabs.Shuffle();
            return prefabs[0];
        }
    }
}

