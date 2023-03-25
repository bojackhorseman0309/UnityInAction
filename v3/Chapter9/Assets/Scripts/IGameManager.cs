using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public interface IGameManager
{
    ManagerStatus status { get; }

    void Startup();
}
