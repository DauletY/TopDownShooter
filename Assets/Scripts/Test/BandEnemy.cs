
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

    public Image healthImg;
    public GameObject deathPrefab;
    private Rigidbody2D r2;

    public Transform enemyCrtl = null;
   

    private void Start()
    {
        if (enemyCrtl == null)
        {
            print("...");
        }
        r2 = GetComponent<Rigidbody2D>();
        _band = GameObject.Find("Player").GetComponent<Transform>();    
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

        Debug.DrawLine(transform.position, _band.position, Color.blue * 100f);
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
