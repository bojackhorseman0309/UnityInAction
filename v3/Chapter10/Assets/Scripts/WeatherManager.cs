using System;
using System.Xml;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class WeatherManager : MonoBehaviour, IGameManager
    {
        private NetworkService network;
        public float cloudValue { get; private set; }
        public ManagerStatus status { get; private set; }

        public void Startup(NetworkService service)
        {
            Debug.Log("Weather manager starting...");

            network = service;
            StartCoroutine(network.GetWeatherJSON(OnJSONDataLoaded));

            status = ManagerStatus.Initializing;
        }

        public void OnXMLDataLoaded(string data)
        {
            var doc = new XmlDocument();
            doc.LoadXml(data);
            XmlNode root = doc.DocumentElement;

            var node = root.SelectSingleNode("clouds");
            var value = node.Attributes["value"].Value;
            cloudValue = Convert.ToInt32(value) / 100f;
            Debug.Log($"Value: {cloudValue}");

            Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

            status = ManagerStatus.Started;
        }

        public void OnJSONDataLoaded(string data)
        {
            var root = JObject.Parse(data);

            var clouds = root["clouds"];
            cloudValue = (float)clouds["all"] / 100f;
            Debug.Log($"Value: {cloudValue}");

            Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

            status = ManagerStatus.Started;
        }

        public void LogWeather(string name)
        {
            StartCoroutine(network.LogWeather(name, cloudValue, OnLogged));
        }

        private void OnLogged(string response)
        {
            Debug.Log(response);
        }
    }
}