using UnityEngine;
using UnityEngine.UI;

public class PlayerAccelerometerController : PlayerController
{

    protected override Vector3 GetMovement()
    {
        float moveHorizontal = Input.acceleration.x;
        float moveVertical = Input.acceleration.y;

        return new Vector3(moveHorizontal, 0.0f, moveVertical);
    }

}
