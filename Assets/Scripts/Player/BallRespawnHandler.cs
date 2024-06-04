using UnityEngine;

public class BallRespawnHandler : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialPosition;
    private float respawnThreshold;
    void Start() => rb = GetComponent<Rigidbody>();
    void Update()
    {
        if (transform.position.y < respawnThreshold) 
            Respawn();
    }
    public void SetRespawn(GameSceneData data)
    {
        SetInitialPosition(data.playerSpawnPosition);
        SetRespawnThreshold(data.playerRespawnYPosition);
    }
    private void SetInitialPosition(Vector3 position) => initialPosition = position;
    private void SetRespawnThreshold(float threshold) => respawnThreshold = threshold;
    private void Respawn() {
        LevelManager.SoundManager.PlaySound(SoundEffect.Respawn);
        rb.velocity = Vector3.zero;
        transform.position = initialPosition; 
    } 

}
