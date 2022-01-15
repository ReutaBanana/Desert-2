using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleThreeMan : MonoBehaviour
{
    [SerializeField] GameObject box1;
    [SerializeField] GameObject box2;
    [SerializeField] GameObject box3;
    private Animator box1Animator;
    private Animator box2Animator;
    private Animator box3Animator;

    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    private MovingObstacleLogic button1Logic;
    private MovingObstacleLogic button2Logic;
    private MovingObstacleLogic button3Logic;

    public List<int> record=new List<int> { 9 };


    // Start is called before the first frame update
    void Start()
    {
        button1Logic = button1.GetComponent<MovingObstacleLogic>();
        button2Logic = button2.GetComponent<MovingObstacleLogic>();
        button3Logic = button3.GetComponent<MovingObstacleLogic>();
        box1Animator = box1.GetComponent<Animator>();
        box2Animator = box2.GetComponent<Animator>();
        box3Animator = box3.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckIfWining())
        {
            if (button1Logic.isCharcterHit && record[record.Count - 1] != 1)
            {
                record.Add(1);
            }
            else if (button2Logic.isCharcterHit && record[record.Count - 1] != 2)
            {
                record.Add(2);
            }
            else if (button3Logic.isCharcterHit && record[record.Count - 1] != 3)
            {
                record.Add(3);
            }
        }
        else
        {
            Debug.Log("I WOM");
            MoveBox();
        }
    }
    public void MoveBox()
    {
        box1Animator.enabled = false;
        box2Animator.enabled = false;
        box3Animator.enabled = false;
        box1.transform.localPosition=new Vector3 (13.495f, 9.8f, -3.5f);
        box2.transform.localPosition=new Vector3 (16.19f, 9.8f, -3.5f);
        box3.transform.localPosition=new Vector3 (19.58f, 9.8f, -3.5f);
    }
    private bool CheckIfWining()
    {
        if(record.Count>4)
        {
            for (int i = 0; i < record.Count; i++)
            {
                if (record[i] == 1)
                {
                    if(record.Count>i+1)
                    {
                        if (record[i + 1] == 3)
                        {
                            if(record.Count>i+2)
                            {
                                if (record[i + 2] == 2)
                                {
                                    if(record.Count>i+3)
                                    {
                                        if (record[i + 3] == 3)
                                        {
                                            return true;
                                        }
                                    }
                                    
                                }
                            }
                           
                        }
                    }
                    
                }
            }
        } 
        return false;
    }
}
