using UnityEngine;

public class IceCreamBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 3f;

    public void Initialize(Transform playerTransform)
    {
        target = playerTransform;
        transform.LookAt(playerTransform.position);
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
            // Apply damage logic here
            Destroy(gameObject); // Destroy ice cream on hit
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject); // Destroy ice cream when it hits the ground
        }
    }
}
