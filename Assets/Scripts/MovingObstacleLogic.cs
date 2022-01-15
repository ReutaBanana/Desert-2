using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleLogic : MonoBehaviour
{
    [SerializeField] GameObject Obstacle;
    private Animator animator;
    bool rotating = false;
    public float smoothTime = 5.0f; //rotate over 5 seconds
    public bool isCharcterHit;
    public bool isCrateHit;
    [SerializeField] GameObject soundEffect;
    // Start is called before the first frame update
    void Start()
    {
        animator = Obstacle.GetComponent<Animator>();
    }

    void Update()
    {
        if (isCharcterHit||isCrateHit)
        {
            if (!rotating)
            {
                animator.SetBool("Rotating", true);
                this.gameObject.GetComponent<Animator>().SetBool("Rotating", true);
                soundEffect.SetActive(true);
                rotating = true;
            }
        }
        else
        {
            if (rotating)
            {
                animator.SetBool("Rotating", false);
                this.gameObject.GetComponent<Animator>().SetBool("Rotating", false);
                soundEffect.SetActive(false);
                rotating = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Crate"))
        {
            isCrateHit = true;
           
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crate"))
        {
            isCrateHit = false;
            //StartCoroutine(querentine());
        }
    }
    IEnumerator querentine()
    {
        yield return new WaitForSeconds(20);
        isCrateHit = false;


    }
}
