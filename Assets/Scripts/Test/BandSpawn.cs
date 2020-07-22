
using UnityEngine;

public class BandSpawn : MonoBehaviour
{
    public GameObject[] _spawns;
    public GameObject spawn = null;

    [SerializeField]
    private float time = 0f;
    [SerializeField]
    private float repeatRate = 0f;

    private void Start()
    {
        InvokeRepeating("Spawn", time, repeatRate);
    }
    private void Spawn()
    {
        float random = Random.Range(0, _spawns.Length);
        Instantiate(spawn, _spawns[(int)random].transform.position, Quaternion.identity);
       
    }
}
