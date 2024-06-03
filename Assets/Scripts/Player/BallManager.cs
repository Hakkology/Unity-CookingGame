using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }
    public Rigidbody PlayerRigidbody { get; private set; }
    public BallHealthBehaviour PlayerHealth { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            PlayerRigidbody = GetComponent<Rigidbody>();
            PlayerHealth = GetComponent<BallHealthBehaviour>();
        }
    }
}
