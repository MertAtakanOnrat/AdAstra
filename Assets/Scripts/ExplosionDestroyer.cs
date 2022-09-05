using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    CountdownTimer countdownTimer;


    // Start is called before the first frame update
    void Start()
    {
        countdownTimer = gameObject.AddComponent<CountdownTimer>();
        countdownTimer.TotalTime = 1;
        countdownTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTimer.Finished)
        {
            Destroy(gameObject);
        }
    }
}
