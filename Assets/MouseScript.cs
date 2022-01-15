using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    [SerializeField] TriggerDoor mouseTrigger;
    private Animator animator;
    [SerializeField] AudioSource soundEffect;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseTrigger.isTrigger)
        {
            animator.SetTrigger("MouseOne");
            soundEffect.Play();
        }

    }
}
