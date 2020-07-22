
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform _firePoint;
    [Header("Force Mode 2D")]
    public ForceMode2D _force;
    [Header("Артка шекынс")]
    public float knockBack;
    [Header("Cлой физикага ойына арналган")]
    public LayerMask layer;


    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    public Transform HitParticlesPrefab;
    /// <summary>
    /// private classes
    /// </summary>
    Base @base = new Base();
    Rigidbody2D _rb2;
    Camera cam;

    private int _fire_rate = 0;
    private int _shotsFired = 0;
    private float _timeToFire = 0;
    

    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
        _rb2 =  transform.parent.GetComponent<Rigidbody2D>();
     
        if (_firePoint == null)
        {
            Debug.LogError("No firePoint? WHAT?!");
        }
    }
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        if (_fire_rate == 0)
        {
            PlayerController.animator.SetBool("IsShooting", false);
            if (InputR.ButtonDown("Fire1"))
            {
                @base.Shoot(_firePoint, _rb2, transform, _force, layer, knockBack,BulletTrailPrefab,HitParticlesPrefab,MuzzleFlashPrefab);
                _shotsFired++;
                PlayerController.animator.SetBool("IsShooting", true);
            }
        }
        else 
        {
            PlayerController.animator.SetBool("IsShooting", false);
            if (InputR.Button("Fire1"))
            {
                if (Time.time > _timeToFire)
                {
                    _timeToFire = Time.time + 1 / (float)_fire_rate;
                    @base.Shoot(_firePoint, _rb2, transform, _force, layer, knockBack, BulletTrailPrefab,HitParticlesPrefab, MuzzleFlashPrefab);
                    _shotsFired++;
                }
                PlayerController.animator.SetBool("IsShooting", true);
            }
        }
        Transform body = GameObject.FindGameObjectWithTag("Body").transform;
        @base.UpdateAimController(cam, body);
    }

}