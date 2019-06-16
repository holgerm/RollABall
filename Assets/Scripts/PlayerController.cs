using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public MovementStrategy MovementStrategy
    {
        get;
        set;
    }

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;

    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    private Rigidbody rb;

    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

#if UNITY_EDITOR
        MovementStrategy = new KeyboardStrategy();
#elif UNITY_IOS || UNITY_ANDROID
        MovementStrategy = new DragStrategy();
#endif
    }

    Vector3 oldPosition;

    // Each physics step..
    void FixedUpdate()
    {
        // Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
        Vector3 movement = MovementStrategy.GetMovement();

        // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
        // multiplying it by 'speed' - our public player speed that appears in the inspector
        rb.AddForce(movement * speed);

        GameController.Instance.AddPlayTime(Time.fixedDeltaTime);
        if (oldPosition != null && oldPosition != Vector3.zero)
        {
            float way = (oldPosition - rb.position).magnitude;
            GameController.Instance.AddMovedWay(way);
        }
        oldPosition = rb.position;

    }

    void OnTriggerEnter(Collider other)
    {
        // ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("Pick Up"))
        {
            // Make the other game object (the pick up) inactive, to make it disappear
            other.gameObject.SetActive(false);

            if (GameController.Instance != null)
            {
                GameController.Instance.Point();
            }
        }
        if (other.gameObject.CompareTag("Deathwall"))
        {
            GameController.Instance.DeathWallHit();
        }
    }

}
