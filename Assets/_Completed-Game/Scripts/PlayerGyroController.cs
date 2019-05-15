// Include the namespace required to use Unity UI

using UnityEngine;
using UnityEngine.UI;

public class PlayerGyroController : PlayerController
{

    protected override Vector3 GetMovement()
    {
        float moveHorizontal = Input.gyro.attitude.eulerAngles.x;
        float moveVertical = Input.gyro.attitude.eulerAngles.y;


        return new Vector3(moveHorizontal, 0.0f, moveVertical);
    }
}