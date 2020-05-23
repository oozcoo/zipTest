using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        WISetup wi = new WISetup();
        wi.Run();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
