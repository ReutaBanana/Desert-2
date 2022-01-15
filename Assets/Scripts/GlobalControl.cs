using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl instance;
    public bool marsAvailable = true;
    public bool moonActive = false;
    public bool MarsActive = false;
    public bool music = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void SetActive(string planetName)
    {
        if (planetName == "Moon")
        {
            moonActive = true;
        }
        if(planetName=="Mars")
        {
            MarsActive = true;
        }
    }
}
