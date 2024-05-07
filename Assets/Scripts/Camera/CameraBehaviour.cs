using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 15, -12);
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.nearClipPlane = 0.3f;
        cam.farClipPlane = 1000f;
        LevelManager.SceneHandler.OnPlayerSpawned += UpdatePlayerTransform;
    }

    void OnDestroy() => LevelManager.SceneHandler.OnPlayerSpawned -= UpdatePlayerTransform;
    private void UpdatePlayerTransform(GameObject newPlayer)
    {
        Transform ballPlayerTransform = newPlayer.transform.Find("BallPlayer");

        if (ballPlayerTransform != null)
        {
            player = ballPlayerTransform.gameObject;
            Debug.Log("BallPlayer found and set.");
        }
        else
        {
            Debug.LogError("BallPlayer child not found in the new player GameObject.");
        }
    }
    
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform);
        }
    }
}
