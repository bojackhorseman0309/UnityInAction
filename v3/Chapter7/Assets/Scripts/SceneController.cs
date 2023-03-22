using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;
    public float speed = 3.0f;
    public const float baseSpeed = 3.0f;

    void Update()
    {
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            enemy.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
    
    void OnEnable()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    
    void OnDisable()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    
    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }
}