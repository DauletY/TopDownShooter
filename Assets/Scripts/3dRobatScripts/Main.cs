
using UnityEngine;

public class Main : MonoBehaviour
{
    public Camera _camera;
    Base robat = new Base();

    public  Transform transformId;
    public new Rigidbody rigidbody;
    private void Start()
    {
        gameObject.layer = 9;
        Debug.Log(gameObject.layer.ToString());
    }
    private void Update()
    {
        robat.SetKeyboard(transform, 10f);
        robat.Shoot(transformId, rigidbody, 20f);
        robat.CameraRobat(_camera, transform.position);
    }
}