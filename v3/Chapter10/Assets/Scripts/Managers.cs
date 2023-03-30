using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(ImageManager))]
public class Managers : MonoBehaviour
{
    private List<IGameManager> startSequence;
    public static WeatherManager Weather { get; private set; }
    public static ImageManager Image { get; private set; }

    private void Awake()
    {
        Weather = GetComponent<WeatherManager>();
        Image = GetComponent<ImageManager>();

        startSequence = new List<IGameManager>();
        startSequence.Add(Weather);
        startSequence.Add(Image);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        var network = new NetworkService();

        foreach (var manager in startSequence) manager.Startup(network);

        yield return null;

        var numModules = startSequence.Count;
        var numReady = 0;

        while (numReady < numModules)
        {
            var lastReady = numReady;
            numReady = 0;

            foreach (var manager in startSequence)
                if (manager.status == ManagerStatus.Started)
                    numReady++;

            if (numReady > lastReady) Debug.Log($"Progress: {numReady}/{numModules}");

            yield return null;
        }

        Debug.Log("All managers started up");
    }
}