using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CodePuzzle : MonoBehaviour
{
    [SerializeField] GameObject trigger;
    [SerializeField] CinemachineVirtualCamera puzzleCamera;
    [SerializeField] GameObject player;
    private CharacterMovement playerMovmentScript;

    //[SerializeField] text yellowAmount;
    //[SerializeField] TextMesh redAmount;
    //[SerializeField] TextMesh greenAmount;

    private TriggerDoor script;

    private int yellowAmount = 0;
    private int redAmount = 0;
    private int greenAmount = 0;

    [SerializeField] TextMeshProUGUI yellow;
    [SerializeField] TextMeshProUGUI red;
    [SerializeField] TextMeshProUGUI green;

    [SerializeField] GameObject key;
    private Rigidbody keyRb;
    private GameObject keyTrigger;

    [SerializeField] Canvas canvas;

    private int pressCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        script = trigger.GetComponent<TriggerDoor>();
        key.SetActive(false);
        keyRb = key.GetComponent<Rigidbody>();
        keyTrigger = key.transform.GetChild(0).gameObject;
        canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        playerMovmentScript = player.GetComponent<CharacterMovement>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (redAmount == 5 && greenAmount == 6 && yellowAmount == 3)
        {
            if (key != null&& !playerMovmentScript.hasBlueKey)
            {
                key.SetActive(true);
                keyTrigger.SetActive(false);
                if (keyRb.velocity.y == 0)
                {
                    keyTrigger.SetActive(true);
                }
            }

        }
        if(keyTrigger.GetComponent<TriggerDoor>().isTrigger&&!playerMovmentScript.hasBlueKey)
        {
            playerMovmentScript.GetBlueKey();
            key.SetActive(false);
        }
        if (script.isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (pressCount % 2 == 0)
                {
                    puzzleCamera.Priority = 100;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    puzzleCamera.Priority = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                pressCount++;
            }
        }
        else
        {
            puzzleCamera.Priority = 1;
            Cursor.lockState = CursorLockMode.Locked;
            pressCount = 0;
        }
    }
    public void AddNumber(string color)
    {
        if (color == "Yellow")
        {
            if (yellowAmount <= 9)
            {
                yellowAmount++;
                yellow.text = yellowAmount.ToString();
            }
            else
            {
                yellowAmount = 0;
            }
        }
        else if (color == "Red")
        {
            if (redAmount <= 9)
            {
                redAmount++;
                red.text = redAmount.ToString();
            }
            else
            {
                redAmount = 0;
            }
        }
        else if (color == "Green")
        {
            if (greenAmount <= 9)
            {
                greenAmount++;
                green.text = greenAmount.ToString();
            }
            else
            {
                greenAmount = 0;
            }
        }
    }
    public void DeleteNumber(string color)
    {
        if (color == "Yellow")
        {
            if (yellowAmount == 0)
            {
                yellowAmount = 9;

            }
            else

            {
                yellowAmount--;
            }
            yellow.text = yellowAmount.ToString();

        }
        else if (color == "Red")
        {
            if (redAmount == 0)
            {
                redAmount = 9;
            }
            else
            {
                redAmount--;

            }
            red.text = redAmount.ToString();

        }
        else if (color == "Green")
        {
            if (greenAmount == 0)
            {
                greenAmount = 9;
            }
            else
            {
                greenAmount--;
            }
            green.text = greenAmount.ToString();

        }
    }
    public void checkifworking()
    {
        Debug.Log("yes");
    }
}
