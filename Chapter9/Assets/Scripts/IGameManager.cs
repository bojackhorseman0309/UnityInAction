using DefaultNamespace;

public interface IGameManager
{
    ManagerStatus status { get; }

    void Startup();
}