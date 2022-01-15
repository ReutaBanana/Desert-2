using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerHitBox : MonoBehaviour
{
    BoxCollider col;
    float offset = 1f;

    float duration;

    private void Awake()
    {
        WalkingController.OnFacingChange += RefeshFacing;
        WalkingController.OnInteract+= StartCollisionCheck;
        col = GetComponent<BoxCollider>();
        //col.enabled = false;
    }
    private void Update()
    {
        if(col.enabled)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                //col.enabled = false;
            }
        }
    }
    void StartCollisionCheck(float dur)
    {
        col.enabled = true;
        duration = dur;
    }
    void RefeshFacing(FacingDirection fd)
    {
        switch (fd)
        {
            case FacingDirection.North:
                col.center = Vector3.forward * offset;
                break;
            case FacingDirection.East:
                col.center = Vector3.right * offset;
                break;
            case FacingDirection.West:
                col.center = Vector3.left * offset;
                break;
            default:
                col.center = Vector3.back * offset;
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
    }
}
