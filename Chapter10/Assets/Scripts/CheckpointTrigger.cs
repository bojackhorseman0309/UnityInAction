using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public string identifier;

    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
        {
            return;
        }

        Managers.Weather.LogWeather(identifier);
        _triggered = true;
    }
}