using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    Canvas settingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        settingsCanvas.gameObject.SetActive(false);
    }
}
