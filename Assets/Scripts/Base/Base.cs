
using System;
using System.Security.Cryptography;
using UnityEngine;
public class Base
{
    public enum Move
    {
        rigth = 1, left = -1
    }
    public enum Jump
    {
        up = 1,
        down = -1
    }
    public enum Keyboard
    {
        A = 97,
        D = 100,
        S = 115,
        W = 119
    }
    public enum Shot
    {
        shot = 0
    }
    public enum Recharge
    {
        ammo = 5
    }

    private float x;
    private float y;
    private Vector2 getVector1;
    private Vector2 getVector2;

    public Base () { }
    public Base (float x , float y)
    {
        this.x = x;
        this.y = y;
    }
    public Base(Vector2 vector1 , Vector2 vector2)
    {
        this.getVector1 = vector1;
        this.getVector2 = vector2;
    }

    #region public static field 
    public static Vector2 getVector;
    #endregion

    #region static classes
    private static PlayerClass playerClass = new PlayerClass();
    #endregion
    private Vector2 Run(Move move, Jump jump, Transform transform, float speed)
    {
        Vector2 vector = new Vector2(transform.position.x, transform.position.y);
        switch (move)
        {
            case Move.rigth:
                vector += Vector2.right * speed * Time.deltaTime;
                break;
            case Move.left:
                vector -= Vector2.left * speed * Time.deltaTime;
                break;
        }
        switch (jump)
        {
            case Jump.up:
                vector += Vector2.up * 100f * Time.deltaTime;
                break;
            case Jump.down:
                vector -= Vector2.down * -100f * Time.deltaTime;
                break;
        }
        return transform.position = vector;
    }

    internal void Shoot(Transform firePoint, Rigidbody2D rb2, Transform transform, ForceMode2D force, object layer)
    {
        MonoBehaviour.Instantiate(rb2, transform.position, Quaternion.identity);
    }

    public void SetKeyboard(Transform transform, float speed)
    {

        if (InputR.GetKey(Keyboard.D))
        {
            Run(Move.rigth, 0, transform, speed);
            MonoBehaviour.print("Rigth->");
        }
        else if (InputR.GetKey(Keyboard.A))
        {
            Run(Move.left, 0, transform, -speed);
            MonoBehaviour.print("<-Left");
        }
        else if (InputR.GetKeyUp(Keyboard.W))
        {
            Run(0,Jump.up ,transform, 100f);
            MonoBehaviour.print("Up");
        }
        else if( InputR.GetKeyDown(Keyboard.S))
        {
            Run(0, Jump.down, transform, 100f);
            MonoBehaviour.print("Down");
        }
    }

    public void Shoot(Transform transform, Rigidbody rigidbody, float speed)
    {
  
        Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float angle = Mathf.Atan2(vector.y - transform.position.y, vector.x - transform.position.x) * 180 / Mathf.PI;
        var vectorMouse = new Vector2(vector.x - transform.position.x, vector.y - transform.position.y).normalized;

        if (InputR.GetMouseButtonDown(Shot.shot)){
            GameObject ob = MonoBehaviour.Instantiate(rigidbody.gameObject, transform.position, Quaternion.identity);
            Rigidbody rb = ob.GetComponent<Rigidbody>();
            rb.AddForce(vectorMouse * speed,ForceMode.Impulse);
            MonoBehaviour.Destroy(ob, .9f);
        }
    }
    public void CameraRobat(Camera camera, Vector3 vector) //formal param
    {
        camera.transform.position = new Vector3(vector.x , vector.y, -10f);
        vector = camera.transform.position;
    }
    public void Traslate(Transform transform, Vector2 translation)
    {
        transform.Translate(translation);
    }
    public void Rotate(Transform transform,  Vector3 vector, float angle)
    {
        transform.Rotate(vector, angle);
    }
    public void Shoot(Transform firePoint, Rigidbody2D rigidbody, Transform transform, ForceMode2D force, LayerMask layer, float knockBack, Transform BulletTrailPrefab, Transform HitParticlesPrefab, Transform MuzzleFlashPrefab)
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y); //положение огневой точки
        Vector2 shotDir = new Vector2();
        if (Vector2.Distance(mousePosition, firePointPosition) > 0.5f)
            shotDir = mousePosition - firePointPosition;
        else
            shotDir = firePoint.parent.up;
        shotDir.Normalize();
       
        shotDir.x += UnityEngine.Random.Range(playerAimOffset, playerAimOffset);
        shotDir.y += UnityEngine.Random.Range(playerAimOffset, playerAimOffset);
        shotDir.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, shotDir, 100, layer);
        float timeToSpawnEffect = 0;
        float effectSpawnRete = 10;
        if (Time.time >= timeToSpawnEffect)
        {
            if (hit.collider != null)
            {
                Effect(hit, BulletTrailPrefab, firePoint, HitParticlesPrefab, MuzzleFlashPrefab);
            }
            else
            {
                Effect(new Vector3(shotDir.x, shotDir.y, 0f), BulletTrailPrefab, firePoint, MuzzleFlashPrefab);
            }
            timeToSpawnEffect = Time.time + 1 / effectSpawnRete;
        }
        Debug.DrawLine(firePointPosition, shotDir * 100, Color.cyan);

        if(hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name);
        }
        rigidbody.AddRelativeForce(-transform.up * knockBack, force);
    }
    private void Effect(RaycastHit2D hit, Transform BulletTrailPrefab, Transform firePoint, Transform HitParticlesPrefab, Transform MuzzleFlashPrefab)
    {
        Transform trail = MonoBehaviour.Instantiate(BulletTrailPrefab, firePoint.position, Quaternion.identity) as Transform;
        LineRenderer ln = trail.GetComponent<LineRenderer>();
        Vector3 endPoint = new Vector3(hit.point.x, hit.point.y, firePoint.position.z);
        ln.useWorldSpace = true;
        ln.SetPosition(0, firePoint.position);
        ln.SetPosition(1, endPoint);
        MonoBehaviour.Destroy(trail.gameObject, 0.02f);

        Transform sparks = MonoBehaviour.Instantiate(HitParticlesPrefab, hit.point, Quaternion.LookRotation(hit.normal)) as Transform;
        MonoBehaviour.Destroy(sparks.gameObject, 0.2f);

         MuzzleFlash(MuzzleFlashPrefab, firePoint);
    }
    private void Effect(Vector3 shotDir, Transform BulletTrailPrefab, Transform firePoint, Transform MuzzleFlashPrefab)
    {
        float trailAngle = Mathf.Atan2(shotDir.y, shotDir.x) * Mathf.Rad2Deg;
        Quaternion trailRot = Quaternion.AngleAxis(trailAngle, Vector3.forward);

        Transform trail = MonoBehaviour.Instantiate(BulletTrailPrefab, firePoint.position, trailRot) as Transform;

        MonoBehaviour.Destroy(trail.gameObject, 0.02f);

        MuzzleFlash(MuzzleFlashPrefab, firePoint);
    }
    private  void MuzzleFlash(Transform MuzzleFlashPrefab, Transform _firePoint)
    {
        Transform clone = MonoBehaviour.Instantiate(MuzzleFlashPrefab, _firePoint.position, _firePoint.rotation) as Transform;
        clone.parent = _firePoint;
        float size = UnityEngine.Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        MonoBehaviour.Destroy(clone.gameObject, 0.02f);
    }
   
    public void UpdateAimController(Camera cam, Transform transform)
    {

        Vector3 pos = cam.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Константа преобразования радианов в градусы
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    public Vector2 MousePosition ()
    {
        return new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }
    public Vector3 MousePosition(Vector3 mousePosition)
    {
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    #region static members
    public static float playerAimOffset
    {
        get
        {
            return ((1f / (float)playerClass.acuracy) * 8f);
        }
    }
    #endregion

}

public  class PlayerClass
{
    public int acuracy  =50;
    public PlayerClass() { }
    public PlayerClass(int acuracy)
    {
        this.acuracy = acuracy;
    }

}

[Serializable]
public class InputR 
{
    public static float GetInputH
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public static float GetInputV
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }
    public static float GetH
    {
        get
        {
            return Input.GetAxisRaw("Horizontal");
        }
    }
    public static float GetV
    {
        get
        {
            return Input.GetAxisRaw("Vertical");
        }
    }
    public static bool GetKey(Base.Keyboard key)
    {
        return Input.GetKey((KeyCode)key);
    }
    public static bool GetKeyUp(Base.Keyboard key)
    {
        return Input.GetKeyUp((KeyCode)key);
    }
    public static bool GetKeyDown(Base.Keyboard key)
    {
        return Input.GetKeyDown((KeyCode)key);
    }
    public static bool GetMouseButtonDown(Base.Shot shot)
    {
        return Input.GetMouseButtonDown((int)shot);
    }
    public static bool ButtonDown(string str)
    {
        return Input.GetButtonDown(str);
    }
    public static bool Button(string str)
    {
        return Input.GetButton(str);
    }
}