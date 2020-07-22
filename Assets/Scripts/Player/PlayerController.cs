using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float speed;
    public static Animator animator;
    Base @base = new Base();
   
    Vector2 getV = new Vector2();
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        getV = new Vector2(InputR.GetH, InputR.GetV);
        getV *= speed;
        animator.SetFloat("speed", Mathf.Abs(getV.x + getV.y));
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(getV, ForceMode2D.Impulse);
    }
}
