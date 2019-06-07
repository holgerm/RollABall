using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text countText;
    public Text winText;
    public Canvas SettingsCanvas;

    public static GameController Instance;
    public GameObject pickups;

    private int count;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnGameStateChanged += UpdateTexts;
    }

    public void StartGame()
    {
        count = 0;
        winText.text = "";
        PlayedTime = 0f;
        wallHit = 0;

        // Run the SetCountText function to update the UI (see below)
        UpdateTexts();

        SettingsCanvas.gameObject.SetActive(false);

        Transform[] pickupTs = (Transform[])pickups.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < pickupTs.Length; i++)
        {
            Transform pickup = pickupTs[i];
            pickup.gameObject.SetActive(true);
        }
    }

    int wallHit;
    internal void WallHit()
    {
        wallHit += 1;
        Handheld.Vibrate();
    }

    private float _playedTime;
    public float PlayedTime
    {
        protected set
        {
            _playedTime = value;
            OnGameStateChanged();
        }
        get
        {
            return _playedTime;
        }
    }

    public delegate void Void2Void();
    public event Void2Void OnGameStateChanged;

    public void AddPlayTime(float deltatime)
    {
        // Add playtime:
        PlayedTime += deltatime;
    }

    public void Point()
    {
        // Add one to the score variable 'count'
        count = count + 1;
        OnGameStateChanged();

        Handheld.Vibrate();
    }

    private float _movedWay;
    public float MovedWay
    {
        protected set
        {
            _movedWay = value;
            OnGameStateChanged();
        }
        get
        {
            return _movedWay;
        }
    }

    public void AddMovedWay(float newWay)
    {
        MovedWay += newWay;
    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    void UpdateTexts()
    {
        // Update the text field of our 'countText' variable
        countText.text =
            "Count: " + count.ToString() +
            "\nTime: " + string.Format("{0:0.0}s", PlayedTime) +
            "\nWay: " + string.Format("{0:0.0}", MovedWay) +
            "\nWall Hits: " + string.Format("{0}", wallHit);

        // Check if our 'count' is equal to or exceeded 12
        if (count >= 12)
        {
            // Set the text value of our 'winText'
            winText.text = "You Win!\n\n" + countText.text;
            SettingsCanvas.gameObject.SetActive(true);
        }
        else
        {
            winText.text = "";
        }
    }

}
