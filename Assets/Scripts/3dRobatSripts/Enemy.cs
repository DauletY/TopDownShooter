
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private float maxDistanceDelta = 0f;
    private Rigidbody rb;
    Vector2 position;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        if ((Vector3.Distance(transform.position , target.position) > 2f))
        {
            position = Vector3.MoveTowards(transform.position, target.position, maxDistanceDelta * Time.deltaTime);
            GetComponent<Transform>().LookAt(position);
        }
        rb.transform.position = position;
    }
}