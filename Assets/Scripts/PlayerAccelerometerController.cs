using UnityEngine;
using UnityEngine.UI;

public class PlayerAccelerometerController : PlayerController
{
    public float standardTilt = 0.6f;

    protected override Vector3 GetMovement()
    {
        float moveHorizontal = Input.acceleration.x;
        float moveVertical = Input.acceleration.y  + standardTilt;

        Debug.Log("y: " + moveVertical);

        return new Vector3(moveHorizontal, 0.0f, moveVertical);
    }

}
