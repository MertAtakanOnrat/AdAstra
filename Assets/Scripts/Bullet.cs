using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    CountdownTimer countdownTimer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        countdownTimer = gameObject.AddComponent<CountdownTimer>();
        countdownTimer.TotalTime = 3;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
