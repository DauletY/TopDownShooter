using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public UnityEngine.UI.Text Go = null;
    public UnityEngine.UI.Image Img = null;

    public float start, end, time;
    public int baseTime = 3;
    private void Start()
    {
        Img.gameObject.SetActive(false);
        Go.text = baseTime.ToString();
    }

    private void Update()
    {
        start += Time.deltaTime;
        while (start >= end)
        {
            start--;
            Count();
            start = 0;
            if (baseTime == 0)
            {
                Go.gameObject.SetActive(false);
                baseTime = 0;
                // Start Coroutine
                StartCoroutine(TopDownView.Call("Test", time));
                Img.gameObject.SetActive(true);
            }
        }
    }
    private void Count()
    {
        baseTime -= 1;
        Go.text = baseTime.ToString();
    }
}
