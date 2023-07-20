using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Managers;
using Game.Helpers;
using DG.Tweening;
using DG.Tweening.Core;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using Game.Enums;

namespace Game.Runtime 
{
    public class ToppingSpawner : MonoBehaviour
    {
        private ToppingBottle _toppingBottle;
        private ToppingBottle ToppingBottle => _toppingBottle == null ? _toppingBottle = GetComponent<ToppingBottle>() : _toppingBottle;

        private float DefaultHeight => ToppingBottle.DefaultPosition.y;

        private const float SPAWN_RADIUS = 0.025f;
        private const float SPAWN_OFFSET = 0.01f;
        private const int MIN_SPAWN_COUNT = 3;
        private const int MAX_SPAWN_COUNT = 6;

        private const float HEIGHT_OFFSET = 0.05f;
        private const float MOVEMENT_DURATION = 0.15f;
        private const Ease MOVEMENT_EASE = Ease.Linear;

        [SerializeField] private Transform body;        
        
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
            _spawnSequence.Append(body.DOLocalMoveY(DefaultHeight + HEIGHT_OFFSET, MOVEMENT_DURATION).SetEase(MOVEMENT_EASE))
            .Append(body.DOLocalMoveY(DefaultHeight, MOVEMENT_DURATION * 0.8f).SetEase(MOVEMENT_EASE).OnComplete(() => SpawnTopping()))
            .SetLoops(-1, LoopType.Restart);
        }

        private void StopSpawn() 
        {
            _spawnSequence?.Kill();
            Vector3 stopPosition = body.localPosition;
            stopPosition.y = DefaultHeight;
            body.localPosition = stopPosition;
        }

        private void SpawnTopping() 
        {
            int spawnCount = Random.Range(MIN_SPAWN_COUNT, MAX_SPAWN_COUNT);
            for (int i = 0; i < spawnCount; i++)
            {
                PoolingManager.Instance.Instantiate(GetTopping(), GetSpawnPosition(), GetSpawnRotation());                
            }

            HapticManager.PlayHaptic(Lofelt.NiceVibrations.HapticPatterns.PresetType.LightImpact);
            SoundManager.Instance.PlaySound(Models.AudioID.ShakePour);
        }

        private Vector3 GetSpawnPosition() 
        {            
            Vector2 unitCircle = Random.insideUnitCircle * SPAWN_RADIUS;
            Vector3 spawnPosition = body.position + new Vector3(unitCircle.x, 0f, unitCircle.y) + SPAWN_OFFSET *  Vector3.down;
            return spawnPosition;
        }

        private Quaternion GetSpawnRotation() 
        {
            float randomRotation = Random.Range(0f, 360f);
            return Quaternion.Euler(new Vector3(0f, randomRotation, 0f));
        }

        private PoolID GetTopping() 
        {
            List<PoolID> poolIDs = new(ToppingManager.Instance.CurrentToppingData.PoolIDs);
            poolIDs.Shuffle();
            return poolIDs[0];
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(body.position + SPAWN_OFFSET * Vector3.down, SPAWN_RADIUS);
        }
    }
}

