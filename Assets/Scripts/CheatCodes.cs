using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    private GameObject lockedDoorLevel2;
    private GameObject lockedDoorLevel3;
    private GameObject puzzleLevel4;

    private HandlingDoors lockedDoor2;
    private HandlingDoors lockedDoor3;
    private PuzzleThreeMan level4script;
    private GameObject player;
    private CharacterMovement playerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.gameObject.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPuzzles();
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(lockedDoorLevel2 != null)
            {
                UnlockPuzzle(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(lockedDoorLevel3 != null)
            {
                UnlockPuzzle(3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if(puzzleLevel4 != null)
            {
                UnlockPuzzle(4);
            }
        }
    }

    private void UnlockPuzzle(int puzzleNumber)
    {
        switch(puzzleNumber)
        {
            case 2:
                //lockedDoor2.isLockedDoorPuzzle1 = false;
                playerScript.hasOrangeKey = true;
                break;
            case 3:
                //lockedDoor3.isLockedDoorPuzzle2 = false;
                playerScript.hasBlueKey = true;

                break;
            case 4:
                level4script.MoveBox();
                break;
            default:
                Debug.Log("not avialble puzzle number");
                break;
        }
        Debug.Log("Unlocked puzzle number: " + puzzleNumber);
    }

    private void FindPuzzles()
    {
        lockedDoorLevel2 = GameObject.Find("Level2LockedDoor");
        if (lockedDoorLevel2 != null)
            lockedDoor2 = lockedDoorLevel2.GetComponent<HandlingDoors>();
        lockedDoorLevel3 = GameObject.Find("Level3LockedDoor");
        if (lockedDoorLevel3 != null)
            lockedDoor3 = lockedDoorLevel3.GetComponent<HandlingDoors>();
        puzzleLevel4 = GameObject.Find("Level4Puzzle");
        if (puzzleLevel4 != null)
            level4script = puzzleLevel4.GetComponent<PuzzleThreeMan>();
    }
}
