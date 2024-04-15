using System;
using UnityEngine;

public class FlyingMovement : IMovement
{
    private MovementController movementController;
    private Transform ballTransform;
    private Rigidbody ballRB;

    private Vector3 targetPosition;


    public FlyingMovement(MovementController controller, Transform transform, Rigidbody rb)
    {
        movementController = controller;
        ballTransform = transform;
        ballRB = rb;
    }

    public void Init()
    {
        Debug.Log("Initialize Flying");
        ballRB.drag = 0.5f;
    }

    public void Update()
    {
        CheckState();
    }

    public void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            ApplyMinimalForceTowardsMouse();
        }
    }

    public void Cancel()
    {
        Debug.Log("Exiting Flying State");
    }

    private void ApplyMinimalForceTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        targetPosition = new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z);
        Vector3 forceDirection = (targetPosition - ballTransform.position).normalized;

        Vector3 force = forceDirection * MovementConstants.FlyingForce;
        ballRB.AddForce(force, ForceMode.Force);
    }

    private void CheckState()
    {
        RaycastHit hit;
        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 20f))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hit.distance < 2f) 
            {
                switch (hitLayer)
                {
                    case 6:  // Ground Layer
                        movementController.ChangeState(MovementState.Rolling);
                        break;
                    case 7:  // Sliding layer
                        movementController.ChangeState(MovementState.Sliding);
                        break;
                    case 4:  // Water layer
                        movementController.ChangeState(MovementState.Water);
                        break;
                }
            }
        }
    }
}
