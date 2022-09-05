using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    float totalTime = 0;
    float passingTime = 0;

    bool running = false;
    bool started = false;

    /// <summary>
    /// Geri sayım sayacının toplam süresini ayarlar
    /// </summary>
    public float TotalTime
    {
        set
        {
            if (!running)
            {
                totalTime = value;
            }
        }
    }
    /// <summary>
    /// Geri sayımın bitip bitmediğini söyler 
    /// </summary>
    public bool Finished
    {
        get
        {
            return started && !running;
        }
    }
    /// <summary>
    /// Sayaci calistirir
    /// </summary>
    public void Run()
    {
        if (totalTime > 0)
        {
            running = true;
            started = true;
            passingTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            passingTime += Time.deltaTime;
            if (passingTime >= totalTime)
            {
                running = false;
            }
        }
    }
}
