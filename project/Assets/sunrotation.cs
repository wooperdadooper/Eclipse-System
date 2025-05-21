using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// motion
public class NewBehaviourScript : MonoBehaviour
{

    public float x = 0.1f;
    public float y = 0.1f;
    public float z = 0.1f;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x, y, z);
    }
}
