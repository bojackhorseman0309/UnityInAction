using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    private const string xmlApi =
        "https://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&APPID=f6da9bc6cadc1e2af95f9ece051b2b6c";

    private const string jsonApi =
        "https://api.openweathermap.org/data/2.5/weather?q=Chicago,us&APPID=f6da9bc6cadc1e2af95f9ece051b2b6c";

    private const string webImage = "https://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

    private const string localApi = "http://localhost:8080/uia/api.php";


    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
    {
        using (UnityWebRequest request = (form == null) ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("network problem: " + request.error);
            }
            else if (request.responseCode != (long) HttpStatusCode.OK)
            {
                Debug.LogError("response error: " + request.responseCode);
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return
            CallAPI(xmlApi, null, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return
            CallAPI(jsonApi, null, callback);
    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<String> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("message", name);
        form.AddField("cloud_value", cloudValue.ToString());
        form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

        return CallAPI(localApi, form, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}