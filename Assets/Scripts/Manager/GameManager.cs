using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject settingsWindow;
    public GameObject scoreWindow;

    private BallBehaviour ballBehaviour;
    private bool isPaused;

    void Start()
    {
        isPaused = false;
        ballBehaviour = FindObjectOfType<BallBehaviour>();
        if (ballBehaviour == null)
        {
            Debug.LogError("BallBehaviour component not found in the scene!");
        }
    }

    void Update(){

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        if (ballBehaviour != null)
        {
            ballBehaviour.PauseBall(); 
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        if (ballBehaviour != null)
        {
            ballBehaviour.ResumeBall();
        }
    }
}