
using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class BandEnemy : MonoBehaviour
{
      

    [SerializeField]
     [Range(0,10f)]
    private float speed;
    [SerializeField]
    private Transform _band;
    public LayerMask layer;
    public Image healthImg;
    public GameObject deathPrefab;
    public GameObject bandPrefab;
    public float stopDist;
    private Rigidbody2D r2;

    private void Start()
    {
        r2 = GetComponent<Rigidbody2D>();  
        _band = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
            
    }
    private void Update()
    {
        // rotate z axis
        if(_band != null) {
            Vector3 enemyPos = _band.position;
            Vector3 lookDir = enemyPos - transform.position;
            float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward); 
        }else {}  
    }
    private void FixedUpdate() {
          
         RaycastHit2D hit2D =default(RaycastHit2D);
        
            if(_band != null)
            if(Vector2.Distance(transform.position, _band.position) > stopDist) {
                        //Debug.Log("stop!");            
                        Vector3 enemyDir = new Vector2(_band.position.x, _band.position.y);
                        //Vector2 targetDir = (_band.position - enemyDir).normalized;
                        hit2D = Physics2D.Raycast(transform.position, enemyDir, stopDist + 2f,layer); 
                        
                        r2.velocity = enemyDir - transform.position;            
                        
                        if(hit2D.collider != null ) {
                                
                           
                             print("true!");
                            if(hit2D.collider.tag == "Player") {
                                    Band band = hit2D.collider.GetComponent<Band>();
                                    Debug.DrawLine(transform.position , hit2D.point ,Color.red , 100f );
                                    Band._health -=  Time.deltaTime;
                                    Hub  hub = FindObjectOfType<Hub>();
                                    if(Band._health == 0f || hub.slider.value == 0f ) {  
                                            Band.speed = 0;
                                            Band._health = 0f;
                                            Band.reloadTime = 0f;      
                                            Instantiate(bandPrefab, _band.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                                            StartCoroutine(TopDownView.Call("Menu", 3.5f));
                                            Destroy(band.gameObject, 0.04f);
                                            
                                    }
                            }
                        }
            }
    }
    public void Health()
    {
        if (healthImg.fillAmount == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject ob =  Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Transform tran = ob.GetComponent<Transform>();
        Destroy(ob.gameObject, 20f);
        Destroy(gameObject);
        tran = GameObject.FindGameObjectWithTag("Respawn").transform;
        if (tran)
        {
            Destroy(tran.gameObject,1f);
            //print("nub");
        }
        else { print("nil"); }
    }
}
