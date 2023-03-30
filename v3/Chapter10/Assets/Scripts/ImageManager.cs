using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ImageManager : MonoBehaviour, IGameManager
    {
        private NetworkService network;

        private Texture2D webImage;
        public ManagerStatus status { get; private set; }

        public void Startup(NetworkService service)
        {
            Debug.Log("Image manager starting...");

            network = service;

            status = ManagerStatus.Started;
        }

        public void GetWebImage(Action<Texture2D> callback)
        {
            if (webImage == null)
                StartCoroutine(network.DownloadImage(image =>
                {
                    webImage = image;
                    callback(webImage);
                }));
            else
                callback(webImage);
        }
    }
}