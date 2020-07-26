
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BandManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static string BandDie(RaycastHit2D hit2D, string name, float time, GameObject bandPrefab) {
        
        MonoBehaviour com = FindObjectOfType<Component>() as MonoBehaviour;
        Hub hub =FindObjectOfType<Hub>();
        if (hit2D.collider.name == "Player") {
              Band._health -= Time.deltaTime;
              if(Band._health == 0f || hub.slider.value == 0f ) {    
                        
                    Band.speed = 0f;  
                    Band._health = 0f;
                    Band.reloadTime = 0f;
                    Instantiate(bandPrefab, hit2D.collider.transform.position, Quaternion.identity);
                    if(Band.animator == null || Band.rb == null) {
                        Band.animator = null;
                        Band.rb = null;
                    }
                    TopDownView.Call(name, time);
                                        Debug.Log("player die!");                        
              }
                               
        }
        return "Player";
    }
}
