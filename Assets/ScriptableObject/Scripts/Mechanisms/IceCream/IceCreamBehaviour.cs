using UnityEngine;

public class IceCreamBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 12f;
    private BallHealthBehaviour ballHealthBehaviour;

    public void Initialize(Transform playerTransform)
    {
        target = playerTransform;
        ballHealthBehaviour = playerTransform.gameObject.GetComponent<BallHealthBehaviour>();
        Debug.Log("Ice Cream Initialized. Target position: " + target.position);
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        Destroy(this,10);
    }

    private void Update()
    {
        if (target == null) {
            Debug.Log("Target is null");
            return;
        }

        //transform.position += transform.forward * speed * Time.deltaTime;
        Debug.Log("Moving towards target. Current position: " + transform.position);
        transform.Rotate(new Vector3(0, 1, 0), 360 * Time.deltaTime); // Rotate around itself
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Debug.Log("Player hit by ice cream, deals 10 damage");
            ballHealthBehaviour.TakeDamage(10);
            Destroy(gameObject);
        }
        // else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        // {
        //     Debug.Log("Ice Cream hit the ground and destroyed");
        //     Destroy(gameObject);
        // }
    }
}
