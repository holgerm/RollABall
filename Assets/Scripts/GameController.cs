using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text countText;
    public Text winText;

    public static GameController Instance;
    public GameObject pickups;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        // Set the count to zero 
        count = 0;

        // Run the SetCountText function to update the UI (see below)
        SetCountAndWinText();

        // Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winText.text = "";

        Instance = this;

    }

    public void StartGame()
    {
        count = 0;
        winText.text = "";
        GetComponent<Canvas>().gameObject.SetActive(false);

        Transform[] pickupTs = (Transform[])pickups.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < pickupTs.Length; i++)
        {
            Transform pickup = pickupTs[i];
            pickup.gameObject.SetActive(true);
        }

    }

    public void Point()
    {
        // Add one to the score variable 'count'
        count = count + 1;

        // Run the 'SetCountText()' function (see below)
        SetCountAndWinText();
        Handheld.Vibrate();
    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    void SetCountAndWinText()
    {
        // Update the text field of our 'countText' variable
        countText.text = "Count: " + count.ToString();

        // Check if our 'count' is equal to or exceeded 12
        if (count >= 12)
        {
            // Set the text value of our 'winText'
            winText.text = "You Win!";
            GetComponent<Canvas>().gameObject.SetActive(true);
        }
    }

    // When this game object intersects a collider with 'is trigger' checked, 
    // store a reference to that collider in a variable named 'other'..
    // Update is called once per frame
    void Update()
    {

    }
}
