using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionScript : MonoBehaviour
{
    private Animator myAnim;
    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Puzzle"))
        {
            Debug.Log("TRUE:");

            myAnim.enabled = false;
        }
    }
}
