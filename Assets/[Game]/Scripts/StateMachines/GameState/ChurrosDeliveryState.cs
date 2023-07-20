using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enums;
using Game.UI;
using System;
using DG.Tweening;
using UnityEditorInternal;
using Game.Helpers;

namespace Game.Runtime 
{
    public class ChurrosDeliveryState : GameStateBase
    {
        private CurrencyPanel CurrencyPanel => CurrencyManager.Instance.CurrencyPanel;

        private const float MOVEMENT_MIN_DELAY = 0.5f;
        private const float MOVEMENT_MAX_DELAY = 0.75f;

        private const float STATE_DELAY = 0.5f;
        private const float CLAIM_DELAY = 1f;
        private const float CAMERA_BLEND_DURATION = 0.5f;    
      
        private const int GEM_SPAWN_AMOUNT = 10;
        private const float SPAWN_DELAY = 0.05f;
        private const float SPAWN_RADIUS = 250f;
        private const float SPAWN_OFFSET = 50f;

        private readonly WaitForSeconds SpawnDelay = new WaitForSeconds(SPAWN_DELAY);

        public ChurrosDeliveryState(GameStateMachine stateMachine) : base(stateMachine) { }        

        public override IEnumerator EnterState()
        {
            yield return new WaitForSeconds(STATE_DELAY);
            CameraManager.Instance.ActivateCamera(CameraID.DeliveryCamera, CAMERA_BLEND_DURATION);
            GameStateManager.Instance.OnEnterChurrosDeliveryState.Invoke();

            yield return new WaitForSeconds(CLAIM_DELAY);
            StateMachine.StartCoroutine(ClaimCoroutine());
            yield break;
        }

        private IEnumerator ClaimCoroutine()
        {
            Action completeAction = null;
            int reward = GetReward();
            int currencyAmountPerGem = reward / GEM_SPAWN_AMOUNT;
            int remainder = reward % GEM_SPAWN_AMOUNT;

            for (int i = 0; i < GEM_SPAWN_AMOUNT; i++)
            {
                completeAction = i == (GEM_SPAWN_AMOUNT - 1) ? CompleteClaim : completeAction;
                CurrencyPanel.CreateCurrency(GetSpawnPosition(), currencyAmountPerGem, completeAction, GetMovementDelay());             
                yield return SpawnDelay;
            }

            CurrencyManager.Instance.AddCurrency(remainder);           
        }

        private void CompleteClaim()
        {            
            CurrencyManager.Instance.OnSuccessRewardClaimed.Invoke();
        }

        private Vector3 GetSpawnPosition() 
        {
            Vector2 unitCircle = UnityEngine.Random.insideUnitCircle * SPAWN_RADIUS;
            Vector3 originPosition = Utilities.WorldToUISpace(CurrencyPanel.Canvas, ChurrosManager.Instance.CurrentChurros.transform.position) + SPAWN_OFFSET * Vector3.up;
            Vector3 spawnPosition = originPosition + (Vector3)unitCircle;
            return spawnPosition;
        }

        private int GetReward()
        {
            return 100;
        }

        private float GetMovementDelay() 
        {
            return UnityEngine.Random.Range(MOVEMENT_MIN_DELAY, MOVEMENT_MAX_DELAY);
        }
    }
}

