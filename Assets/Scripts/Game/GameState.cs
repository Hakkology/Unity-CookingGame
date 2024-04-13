public class GameSceneState : IGameState
{
    private string sceneName;

    public GameSceneState(string sceneName)
    {
        this.sceneName = sceneName;
    }

    public void EnterState()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ExitState()
    {
        // Gerekli temizlik işlemleri
    }

    public void UpdateState()
    {
        // Oyun sahnesi güncellemeleri
    }
}