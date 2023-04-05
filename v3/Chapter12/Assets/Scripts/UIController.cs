using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthLabel;
        [SerializeField] private InventoryPopup popup;
        [SerializeField] private TMP_Text levelEnding;

        private void OnEnable()
        {
            Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
            Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
            Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
            Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
            Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
            Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
            Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
        }

        private void Start()
        {
            OnHealthUpdated();

            levelEnding.gameObject.SetActive(false);
            popup.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                bool isShowing = popup.gameObject.activeSelf;
                popup.gameObject.SetActive(!isShowing);
                popup.Refresh();
            }
        }

        private void OnHealthUpdated()
        {
            string message = $"Health: {Managers.Player.health}/{Managers.Player.maxHealth}";
            healthLabel.text = message;
        }

        private void OnLevelComplete()
        {
            StartCoroutine(CompleteLevel());
        }

        private IEnumerator CompleteLevel()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "Level Complete!";

            yield return new WaitForSeconds(2);

            Managers.Mission.GoToNext();
        }

        private void OnLevelFailed()
        {
            StartCoroutine(FailLevel());
        }

        private IEnumerator FailLevel()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "Level Failed";

            yield return new WaitForSeconds(2);

            Managers.Player.Respawn();
            Managers.Mission.RestartCurrent();
        }

        public void SaveGame()
        {
            Managers.Data.SaveGameState();
        }

        public void LoadGame()
        {
            Managers.Data.LoadGameState();
        }
        
        private void OnGameComplete()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "You Finished the Game!";
        }
    }
}