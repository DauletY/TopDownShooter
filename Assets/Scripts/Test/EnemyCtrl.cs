using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
internal class EnemyUI
{
    private float health = 0;
    public Text mouseText = null;
    public Image healthBar = null;

    public EnemyUI() { }
   
    internal void Init(Component it)
    {
        healthBar = GameObject.Find("White").GetComponent<Image>();
        if (healthBar == null)
        {
            Debug.Log("..");
            return;
        }
    }
    internal void Health()
    {
        healthBar.fillAmount -= health;
    }
}
public class EnemyCtrl : MonoBehaviour
{
    [SerializeField]
    private float speedl = 0f;
    [SerializeField]
    private float slow = 0f;
    
    public Transform _face = null;
  
    [SerializeField]
    EnemyUI enemyUI = new EnemyUI();
   
    Rigidbody2D rb;
    Vector2 mouseCtrl;
    Base @base = new Base();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Base.getVector.x = InputR.GetH ;
        Base.getVector.y = InputR.GetV ;

        mouseCtrl = @base.MousePosition();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Base.getVector  * speedl * Time.fixedDeltaTime);

        Vector2 direction = mouseCtrl - rb.position;
        float angle = Mathf.Atan2(mouseCtrl.x, mouseCtrl.y) * Mathf.Rad2Deg;
        rb.rotation = -angle;


        if (InputR.Button("Fire1") )
        {
            // UI your walk controller turn on -> set
            rb.MovePosition(rb.position + mouseCtrl * slow * Time.fixedDeltaTime);
        }
    }
}
