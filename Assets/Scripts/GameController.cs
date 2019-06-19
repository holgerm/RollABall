using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text countText;
    public Text winText;
    public Canvas SettingsCanvas;
    public GameObject player;

    public static GameController Instance;
    public GameObject southBorder;

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

    [SerializeField]
    GameObject groundPrefab;
    [SerializeField]
    Transform map;
    [SerializeField] GameObject[] pickUps;

    public void StartGame()
    {
        count = 0;
        winText.text = "";
        PlayedTime = 0f;
        wallHit = 0;

        // Run the SetCountText function to update the UI (see below)
        UpdateTexts();

        SettingsCanvas.gameObject.SetActive(false);
        player.SetActive(true);

        GameObject newGroundObject = Instantiate(groundPrefab, new Vector3(0, 0, 0), Quaternion.identity, map);
        newGroundObject = Instantiate(groundPrefab, new Vector3(0, 0, 20), Quaternion.identity, map);
        GameObject newPickUp = Instantiate(pickUps[Random.Range(0, pickUps.Length)], newGroundObject.transform.position, Quaternion.identity, map);
        newGroundObject = Instantiate(groundPrefab, new Vector3(0, 0, 40), Quaternion.identity, map);
        newPickUp = Instantiate(pickUps[Random.Range(0, pickUps.Length)], newGroundObject.transform.position, Quaternion.identity, map);

        southBorder.transform.position = new Vector3(0, 0, -10);
        southBorder.gameObject.SetActive(true);

        player.transform.position = new Vector3(0, 0, 0);
        //newGroundObject.transform.SetParent(parent.transform);
        //GameObject newPickUp = Instantiate(pickUps[Random.Range(0, pickUps.Length)], newGroundObject.transform.position, Quaternion.identity);
        //newGroundObject.transform.SetParent(parent.transform);

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

    internal void DeathWallHit()
    {
        winText.text = "You Lost!\n\n";
        SettingsCanvas.gameObject.SetActive(true);
        player.SetActive(false);
        OnGameStateChanged();
    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    void UpdateTexts()
    {
        // in case we lost:
        if (winText.text == "You Lost!\n\n")
        {
            return;
        }

        // in case we are still playing:
        // Update the text field of our 'countText' variable
        countText.text =
            "Count: " + count.ToString() +
            "\nTime: " + string.Format("{0:0.0}s", PlayedTime) +
            "\nWay: " + string.Format("{0:0.0}", MovedWay) +
            "\nWall Hits: " + string.Format("{0}", wallHit);

        // In Case we won:
        if (count >= 12000)
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
