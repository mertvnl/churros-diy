using Game.Runtime;
using DG.Tweening;
using UnityEngine;

namespace Game.Props
{
    public class FryerBasket : MonoBehaviour
    {
        [SerializeField] private ChurrosFryer churrosFryer;
        
        private Transform _myTransform;

        private Vector3 _initialPos;
        private Tween _curTween;
        private float _distance;

        [Header("Tween Values")]
        [SerializeField] private Vector3 lowerPos;
        [SerializeField] private Ease movementEase = Ease.Linear;
        [SerializeField] private float movementDuration = 2f;

        private void OnEnable()
        {
            AssignInitialValues();

            churrosFryer.OnStartFry.AddListener(SoakInFat);
            churrosFryer.OnStopFry.AddListener(PickUpBasket);
        }

        private void OnDisable()
        {
            churrosFryer.OnStartFry.RemoveListener(SoakInFat);
            churrosFryer.OnStopFry.RemoveListener(PickUpBasket);
        }

        private void AssignInitialValues()
        {
            _myTransform = transform;

            _initialPos = _myTransform.localPosition;
            _distance = Vector3.Distance(_initialPos, lowerPos);
        }

        private void PickUpBasket()
        {
            MoveBasket(_initialPos);
        }

        private void SoakInFat()
        {
            MoveBasket(lowerPos);
        }

        private void MoveBasket(Vector3 targetPos)
        {
            if (_curTween != null)
                _curTween.Kill();

            float curDistance = Vector3.Distance(_myTransform.localPosition, targetPos);
            float duration = (movementDuration * curDistance) / _distance;

            _curTween = _myTransform.DOLocalMove(targetPos, duration).SetEase(movementEase).OnComplete(()=>
            {
                _curTween = null;
            }).SetLink(gameObject);
        }

        // TODO
        private void SpawnChurro()
        {
            
        }
    }
}
