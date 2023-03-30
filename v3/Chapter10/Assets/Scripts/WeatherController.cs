using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material sky;
    [SerializeField] private Light sun;

    private float fullIntensity;

    private void Start()
    {
        fullIntensity = sun.intensity;
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    private void OnWeatherUpdated()
    {
        SetOvercast(Managers.Weather.cloudValue);
    }

    private void SetOvercast(float value)
    {
        Debug.Log($"Blend value: {value}");
        sky.SetFloat("_Blend", value);
        sun.intensity = fullIntensity - fullIntensity * value;
    }
}