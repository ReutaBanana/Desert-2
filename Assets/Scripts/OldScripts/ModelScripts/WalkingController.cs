using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    North,
    East,
    South,
    West
}
public class WalkingController : Controller
{
    Vector3 walkVelocity;
    Vector3 previousWalkVelocity;
    FacingDirection facing=FacingDirection.South;
    float adjVerVelocity;
    float jumpPressTime;
 
    public float walkspeed = 1.5f;
    public float jumpSpeed = 2f;
    public float interactDuration = 0.1f;

    public delegate void FacingChangeHandler(FacingDirection fd);
    public static event FacingChangeHandler OnFacingChange;

    public delegate void HitBoxEventHandler(float dur);
    public static event HitBoxEventHandler OnInteract;
    
    private void Start()
    {
        if(OnFacingChange!=null)
        {
            OnFacingChange(facing);
        }
    }

    public override void ReadInput(InputData data)
    {
        previousWalkVelocity = walkVelocity;
        ResetMovmentToZero();

        if (data.axes[0]!=0f)
        {
            walkVelocity += Vector3.forward * data.axes[0] * walkspeed;
        }
        if (data.axes[1] != 0f)
        {
            walkVelocity += Vector3.right * data.axes[1]*walkspeed;
        }
        if(data.buttons[0])
        {
            if (jumpPressTime==0f)
            {
                if(Grounded())
                {
                    adjVerVelocity = jumpSpeed;

                }
            }
            jumpPressTime += Time.deltaTime;
        }
        else
        {
            jumpPressTime = 0f;
        }

        if (data.buttons[1])
        {
            if (OnInteract!=null)
            {
                OnInteract(interactDuration);
            }
        }


            newInput = true;
    }
    private void LateUpdate()
    {
        if(!newInput)
        {
            previousWalkVelocity = walkVelocity;
            ResetMovmentToZero();
            jumpPressTime = 0f;
        }
        if(previousWalkVelocity!=walkVelocity)
        {
            CheckForFacingChange();
        }
        rb.velocity = new Vector3(walkVelocity.x,rb.velocity.y+adjVerVelocity, walkVelocity.z);
        newInput = false;
    }

    void CheckForFacingChange()
    {
        if(walkVelocity==Vector3.zero)
        {
            return;
        }
        if (walkVelocity.x == 0 || walkVelocity.z == 0)
        {
            ChangeFacing(walkVelocity);
        }
        else
        {
            if(previousWalkVelocity.x==0)
            {
                ChangeFacing(new Vector3(walkVelocity.x, 0, 0));
            }
            else if (previousWalkVelocity.z==0)
            {
                ChangeFacing(new Vector3(0, 0, walkVelocity.z));
            }
            else
            {
                ChangeFacing(walkVelocity);
                Debug.Log("Unexpected walkvelocity value. ");
            }
        }
    }
    //NOTE: Method assumes only X or Z value will be non-zero in dir parameter, will default to V value
    void ChangeFacing(Vector3 dir)
    {
        if(dir.z!=0)
        {
            facing = (dir.z > 0) ? FacingDirection.North : FacingDirection.South;
        }
        if(dir.x!=0)
        {
            facing = (dir.x > 0) ? FacingDirection.East : FacingDirection.West;
        }
        if(OnFacingChange!=null)
        {
            OnFacingChange(facing);
        }
    }

    void ResetMovmentToZero()
    {
        walkVelocity = Vector3.zero;
        adjVerVelocity = 0f;
    }
    bool Grounded()
    {
        return(Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y+0.1f));
    }
}
