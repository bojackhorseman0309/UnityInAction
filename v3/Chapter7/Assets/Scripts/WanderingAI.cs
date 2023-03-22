using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public const float baseSpeed = 3.0f;

    private bool isAlive;
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    private void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
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