using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * 6;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position -= Vector3.forward * Time.deltaTime * 6;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * 6;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * 6;
        }
    }
}
