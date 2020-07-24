using System.Security.Cryptography;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class BandWeapon : MonoBehaviour{

    public Transform _fireRate = null;
    public Transform _sparkPrefab = null;
    public Transform _muzzleFlashPrefab = null;
    public LayerMask _layerMask = default(LayerMask);

    public GameObject _bulletTailePrefab = null;
    public float reloadTime = 0;
    public  int fireRate = 0;
    public int clipSize = 1;
    public  static float timeToFire = 0;
    public  static int shotsFired = 0;
    public float effectSpawnRate = 10;
    public AudioClip clip;
    public AudioClip reloadClip;
    BandUI bandUI = new BandUI();
    //---------------------------static members
    
    private Base @base = new Base();
    bool reloading = false;
    private Camera cam;
    ParticleSystem particle;
    private float timeToSpawnEffect = 0;
    ConcreateCreator concreateCreator = null;
    private void Start()
    {
        bandUI.source = GetComponentInChildren <AudioSource>();
        bandUI.source1 = GetComponentInChildren<AudioSource>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        particle = GetComponentInChildren<ParticleSystem>();
        Band.rb = transform.parent.GetComponentInChildren<Rigidbody2D>();
        concreateCreator = new ConcreateCreator(bandUI.source);
        concreateCreator = new ConcreateCreator(bandUI.source1);

        fireRate = Band.fireRate;
        clipSize = Band.clipSize;
        reloadTime = Band.reloadTime;
        shotsFired = 0;
    }

    [System.Obsolete]
    private void Update()
    {
        if(reloading) return;
        if(InputR.GetKeyDown(Base.Keyboard.R)) {
            //  print("Reload");
            StartCoroutine(Reload());
            concreateCreator.audio.PlayOneShot(reloadClip);
            reloading = true;
        }
        if(shotsFired >= clipSize) {
            StartCoroutine(Reload());
            particle.emissionRate = 0f;
            reloading = true;
            return;
        }
        if (fireRate == 0)
        {
            Band.animator.SetBool("Shot", false);
            concreateCreator.audio.Stop();
            particle.emissionRate = 0f;
            if (InputR.ButtonDown("Fire1"))
            {
                 Hit();
                shotsFired++;
                 particle.emissionRate = 1;
                // Debug.Log(shotsFired);
                Band.animator.SetBool("Shot", true);
                
            }
        }
        else
        {
            
            particle.emissionRate = 0f;
            Band.animator.SetBool("Shot", false);
            // I am shooting any thing
            if (InputR.Button("Fire1"))
            {
                if (Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / (float)fireRate;
                    shotsFired++;
                    Hit();
                    concreateCreator.audio.PlayOneShot(clip);
                }
                 particle.emissionRate = fireRate;
                Band.animator.SetBool("Shot", true);
         
            }
        }
        @base.UpdateAimController(cam, transform);
    }

    private IEnumerator Reload()
    {
        Debug.Log("Reload...");
        yield return new WaitForSeconds(reloadTime);
        shotsFired = 0;
        reloading = false;

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
        if(Time.time >= timeToSpawnEffect) {
            if(raycast.collider != null){
                // effect
                  Transform trail = Instantiate(_bulletTailePrefab, _fireRate.position, Quaternion.identity).GetComponent<Transform>();
                
                LineRenderer ln = trail.GetComponent<LineRenderer>();
                Vector3 endPoint = new Vector3(raycast.point.x, raycast.point.y, _fireRate.position.z);
                ln.useWorldSpace = true;
                ln.SetPosition(0, _fireRate.position);
                ln.SetPosition(1, endPoint);
                Destroy(trail.gameObject, 0.02f);

                Transform spark = Instantiate(_sparkPrefab, raycast.point, Quaternion.LookRotation(raycast.normal));
                Destroy(spark.gameObject, 0.2f);
                MuzzleFlash();
            }
            else
            {
                // effect
                Effect(direction);
            }
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        if (raycast.collider != null)
        {
           // Debug.Log("We hit " + raycast.collider.name);
            Debug.DrawLine(fireDirection, raycast.point, Color.red);

            BandEnemy bandEnemy = raycast.collider.GetComponent<BandEnemy>();

            if (bandEnemy.healthImg.fillAmount >= 1 || raycast.collider.tag == "Enemy")
            {
                bandEnemy.healthImg.fillAmount -= 0.3f;
                //print(bandEnemy.healthImg.fillAmount);
                bandEnemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                bandEnemy.Health();
            }
        }
        else
        {
            // ..
        }        
    }
    void Effect (Vector3 shotDir) {
		float trailAngle = Mathf.Atan2(shotDir.y, shotDir.x) * Mathf.Rad2Deg;
		Quaternion trailRot = Quaternion.AngleAxis(trailAngle, Vector3.forward);
	
		Transform trail = Instantiate (_bulletTailePrefab, _fireRate.position, trailRot).GetComponent<Transform>();
		
		Destroy (trail.gameObject, 0.02f);
		
		MuzzleFlash();
	}
    void MuzzleFlash() {
        Transform mazzle = Instantiate(_muzzleFlashPrefab, _fireRate.position, Quaternion.identity)  as Transform;
        mazzle.parent = _fireRate;
        float randomSize = Random.Range(0.6f, 1f);
        mazzle.localScale = new Vector3(randomSize, randomSize, randomSize);
        Destroy(mazzle.gameObject, 0.02f);

       // Debug.DrawLine(transform.position, direction, Color.green);
    }
}