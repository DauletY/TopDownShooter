using System.Security.Cryptography;
using UnityEngine;

class BandWeapon : MonoBehaviour{

    public Transform _fireRate = null;
    public Transform _sparkPrefab = null;
    public Transform _muzzleFlashPrefab = null;
    public LayerMask _layerMask = default(LayerMask);

    public GameObject _bulletTailePrefab = null;
    private readonly int fireRate = 0;
    private  float timeToFire = 0;
    private int shotsFired = 0;
    private Base @base = new Base();
    private Camera cam;


    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Band.rb = transform.parent.GetComponentInChildren<Rigidbody2D>();

        if (Band.rb != null)
        {
            Debug.Log("there if rb");
        }
        else
        {
            Debug.Log 
                (
                    "no there is rb " 
                );
        }
    }
    private void Update()
    {
        if (fireRate == 0)
        {

            Band.animator.SetBool("Shot", false);
            if (InputR.ButtonDown("Fire1"))
            {
                shotsFired++;
                Hit();
               // Debug.Log(shotsFired);
                Band.animator.SetBool("Shot", true);
            }
        }
        else
        {
            Band.animator.SetBool("Shot", false);
            if (InputR.Button("Fire1"))
            {
                if (Time.time >= timeToFire)
                {
                    timeToFire = Time.time + (1 / (float)fireRate);
                    shotsFired++;
                    Hit();
                }
                Band.animator.SetBool("Shot", true);
            }
        }
        @base.UpdateAimController(cam, transform);
    }
    void Hit()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = new Vector2(_fireRate.position.x, _fireRate.position.y);
        Vector2 direction = new Vector2();
        float distance = 10f;

        direction = mousePosition - fireDirection;
        transform.up = direction;

        RaycastHit2D raycast = Physics2D.Raycast(fireDirection, direction, distance, _layerMask) ;

        if (raycast.collider != null)
        {
            Debug.Log("We hit " + raycast.collider.name);
            Debug.DrawLine(fireDirection, raycast.point, Color.red);

            BandEnemy bandEnemy = raycast.collider.GetComponent<BandEnemy>();

            if (bandEnemy.healthImg.fillAmount >= 1 || raycast.collider.tag == "Enemy")
            {
                bandEnemy.healthImg.fillAmount -= 0.3f;
                print(bandEnemy.healthImg.fillAmount);
                
                bandEnemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
               
                Transform trail = Instantiate(_bulletTailePrefab, _fireRate.position, Quaternion.identity).GetComponent<Transform>();
               
                LineRenderer ln = trail.GetComponent<LineRenderer>();
                Vector3 endPoint = new Vector3(raycast.point.x, raycast.point.y, _fireRate.position.z);
                ln.useWorldSpace = true;
                ln.SetPosition(0, _fireRate.position);
                ln.SetPosition(1, endPoint);
                Destroy(trail.gameObject, 0.02f);

                Transform spark = Instantiate(_sparkPrefab, raycast.point, Quaternion.LookRotation(raycast.normal));
                Destroy(spark.gameObject, 0.2f);

                bandEnemy.Health();
            }
        }
        else
        {

        }
        Transform mazzle = Instantiate(_muzzleFlashPrefab, _fireRate.position, Quaternion.identity)  as Transform;
        mazzle.parent = _fireRate;
        float randomSize = Random.Range(0.6f, 1f);
        mazzle.localScale = new Vector3(randomSize, randomSize, randomSize);
        Destroy(mazzle.gameObject, 0.02f);

        Debug.DrawLine(transform.position, direction, Color.green);
    }
}