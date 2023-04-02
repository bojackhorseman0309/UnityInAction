using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    private const string xmlApi =
        "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&appid=apitoken";

    private const string jsonApi =
        "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&appid=apitoken";

    private const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

    private const string localApi = "http://localhost/uia/api.php";

    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
    {
        using (var request = form == null ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError($"network problem: {request.error}");
            else if (request.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError($"response error: {request.responseCode}");
            else
                callback(request.downloadHandler.text);
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, null, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, null, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        var request = UnityWebRequestTexture.GetTexture(webImage);

        yield return request.SendWebRequest();

        callback(DownloadHandlerTexture.GetContent(request));
    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        var form = new WWWForm();
        form.AddField("message", name);
        form.AddField("cloud_value", cloudValue.ToString());
        form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

        return CallAPI(localApi, form, callback);
    }
}