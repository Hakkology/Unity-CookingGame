using UnityEngine;

public class IceCreamBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 3f;
    private BallHealthBehaviour ballHealthBehaviour;

    public void Initialize(Transform playerTransform)
    {
        target = playerTransform;
        ballHealthBehaviour = playerTransform.gameObject.GetComponent<BallHealthBehaviour>();
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
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
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
