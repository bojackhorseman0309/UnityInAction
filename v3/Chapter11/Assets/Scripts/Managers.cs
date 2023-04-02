using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
public class Managers : MonoBehaviour
{
    private List<IGameManager> startSequence;
    public static AudioManager Audio { get; private set; }

    private void Awake()
    {
        Audio = GetComponent<AudioManager>();

        startSequence = new List<IGameManager>();
        startSequence.Add(Audio);

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