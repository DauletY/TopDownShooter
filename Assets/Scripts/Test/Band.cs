using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BandUI {
    // player band ui 
    public AudioSource source;
    public AudioSource source1;
}
public class Band : MonoBehaviour
{
    public Transform cameraHere = null;
    public float cameraZ = 0f;
    public static float _health = 10;
    // клип өлшемі
    public static int clipSize = 20;
    // ату жылдамдығы
    public static int fireRate = 10;   
    // қайта зарядтау уақыты
    public static float reloadTime = 1f;
    public static Animator animator;
    public static float speed = 10;
    public static Rigidbody2D rb;
    public static  GameObject activ;
  
    Vector2 force = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        _health= 10;
        fireRate = 10;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        // cam here
       cameraHere.position = new Vector3(transform.position.x, transform.position.y, cameraZ);
    }

    // Update is called once per frame
    void Update()
    {
        force = new Vector2(InputR.GetH,InputR.GetV);
        force *= speed;
        animator.SetFloat("speed", Mathf.Abs(force.x + force.y));
    }
    private void FixedUpdate()
    {
        cameraHere.position = new Vector3(rb.position.x ,rb.position.y, cameraZ);
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
