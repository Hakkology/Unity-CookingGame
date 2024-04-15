using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset = new Vector3(0, 15, -12);

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(45, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
