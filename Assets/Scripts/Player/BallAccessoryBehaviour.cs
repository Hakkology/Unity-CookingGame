using UnityEngine;

public class BallAccessoryBehaviour : MonoBehaviour
{
    public Transform target;
    private Vector3 initialOffset;
    private Rigidbody targetRigidbody;
    public bool isDynamicOffset = false;
    public float forwardDistance;

    void Start()
    {
        if (target != null)
        {
            // Calculate the initial offset from the target's position
            initialOffset = transform.position - target.position;
            targetRigidbody = target.GetComponent<Rigidbody>();
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // When the player is moving, update the accessory's position dynamically
            if (isDynamicOffset && targetRigidbody.velocity.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(targetRigidbody.velocity.normalized);
                transform.position = target.position + targetRigidbody.velocity.normalized * forwardDistance;
            }
            else
            {
                // If not moving or not using dynamic offset, keep the original offset
                transform.position = target.position + initialOffset;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
