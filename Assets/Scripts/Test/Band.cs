using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : MonoBehaviour
{
    public static Animator animator;
    public float speed = 10;
    public static Rigidbody2D rb;
    Vector2 force = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
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
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
