﻿using UnityEngine;

public class DragStrategy : MovementStrategy
{
    float slowDown = 0.5f;
    public Vector3 GetMovement()
    {
        if (Input.touchCount != 1)
            return Vector3.zero;

        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            if (raycastHit.collider.CompareTag("Player"))
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        Debug.Log("Touch on ball pos: " + Input.GetTouch(0).position);
                        break;
                    case TouchPhase.Ended:
                        Debug.Log("Ball thrown pos: " + Input.GetTouch(0).position);
                        break;
                    case TouchPhase.Moved:
                        Debug.Log("Dragging ball pos: " + Input.GetTouch(0).position);
                        movement = Input.GetTouch(0).deltaPosition * slowDown;
                        break;
                }
            }
            else
            {
                Debug.Log("Touch on something else");
            }
        }

        Vector3 movement3D = new Vector3(movement.x, 0.0f, movement.y);
        return movement3D;
    }

    Vector2 movement;

}
