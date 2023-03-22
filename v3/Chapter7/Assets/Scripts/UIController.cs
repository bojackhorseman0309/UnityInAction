using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private SettingsPopup settingsPopup;

    private int score;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void Start()
    {
        score = 0;
        scoreLabel.text = score.ToString();

        settingsPopup.Close();
    }

    private void OnEnemyHit()
    {
        score += 1;
        scoreLabel.text = score.ToString();
    }

    public void onOpenSettings()
    {
        settingsPopup.Open();
    }
}