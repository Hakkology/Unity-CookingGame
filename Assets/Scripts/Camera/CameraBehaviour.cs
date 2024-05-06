using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset = new Vector3(0, 15, -12);
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.nearClipPlane = 0.3f; // Adjust near clipping plane
        cam.farClipPlane = 1000f;  // Adjust far clipping plane
    }

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}
