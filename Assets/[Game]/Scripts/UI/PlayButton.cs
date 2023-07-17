using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI 
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dayTextMesh;

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelLoaded.AddListener(UpdateDayText);
        }

        private void OnDisable()
        {
            LevelManager.Instance.OnLevelLoaded.RemoveListener(UpdateDayText);
        }

        public void StartLevel() 
        {
            LevelManager.Instance.StartLevel();
            GameStateManager.Instance.CurrentStateMachine.EnterNextState();
        }

        private void UpdateDayText() 
        {
            dayTextMesh.SetText("Day " + SaveManager.GetInt("FakeLevel", 1));
        }
    }
}

