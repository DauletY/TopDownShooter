
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
    public float stopDist;
    private Rigidbody2D r2;

    bool inSight = false;

    private void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
        _band = GameObject.Find("Player").GetComponent<Transform>();    

        if(_band == null) {
            print("nil");
        }
    }
    private void Update()
    {
        Vector3 enemyDir = new Vector2(_band.position.x, _band.position.y);
        // transform.up = enemyDir + _band.localEulerAngles;
        r2.velocity = enemyDir - transform.position;

        Vector3 enemyPos = _band.position;
        Vector3 lookDir = enemyPos - transform.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward); 

        inSight = false;
        if(stopDist > Vector2.Distance(transform.position, _band.position)) {
            Vector2 targetDir = (_band.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, targetDir, stopDist + 2f,layer); 
            
            /* if(hit2D.collider != null) {
                    print("true: " + hit2D.collider.name);
            }else {
                 print("false" + hit2D.collider.name);    
            }    */  
            if(hit2D.collider.tag == "Player") {
                  inSight = true;
                  Debug.Log("player die!");
                  //Band._health--;
            }
            Debug.DrawLine(transform.position, hit2D.point, Color.red);
        }
        /*
      
        Debug.DrawLine(transform.position,hit2D.point, Color.blue * 100f); */
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
