using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public string identifier;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Managers.Weather.LogWeather(identifier);
        triggered = true;
    }
}