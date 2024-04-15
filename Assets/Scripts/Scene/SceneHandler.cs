using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
    public List<SceneData> sceneDataList; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    
    public void ChangeScene(GameState state)
    {
        SceneData sceneData = sceneDataList.Find(scene => scene.state == state);
        if (sceneData != null)
        {
            SceneManager.LoadScene(sceneData.sceneName);
        }
        else
        {
            Debug.LogError("Scene not found for state: " + state.ToString());
        }
    }
}