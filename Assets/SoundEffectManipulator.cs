using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManipulator : MonoBehaviour
{
    [SerializeField] AudioSource soundEffect;
    private Rigidbody rb;
    private bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if((rb.velocity.x>0||rb.velocity.y>0)&& !played)
        {
            soundEffect.Play();
            played = true;
        }
    }
}
