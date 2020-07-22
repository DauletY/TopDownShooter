using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BandEnemyCrtl : MonoBehaviour
{
       
    public  Transform _robat = null;
    // Start is called before the first frame update
    void Start()
    {
        _robat = GetComponentInParent<Transform>();
        if (_robat == null)
        {
            Debug.Log("please try again");
        }
    }
    public void Health()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
