using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    Camera cam;
    public float zoomSpeed = 0.5f;        // The rate of change of the field of view

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchCount != 2)
            return;

        // There are two touches on the device..

        // Store both touches.
        Touch touch_0 = Input.GetTouch(0);
        Touch touch_1 = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        Vector2 alteTouchPosition_0 = touch_0.position - touch_0.deltaPosition;
        Vector2 alteTouchPosition_1 = touch_1.position - touch_1.deltaPosition;

        // Find the magnitude of the vector (the distance) between the touches in each frame.
        float alterTouchAbstand = (alteTouchPosition_0 - alteTouchPosition_1).magnitude;
        float neuerTouchAbstand = (touch_0.position - touch_1.position).magnitude;

        // Find the difference in the distances between each frame.
        float deltaTouchAbstand = alterTouchAbstand - neuerTouchAbstand;

        // Change the field of view based on the change in distance between the touches.
        cam.fieldOfView += deltaTouchAbstand * zoomSpeed;

        // Clamp the field of view to make sure it's between 0 and 180.
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 179.9f);
    }
}