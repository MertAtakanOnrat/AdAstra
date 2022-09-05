using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public SpriteRenderer rink;

    // Start is called before the first frame update
    void Start()
    {
        float orthoSize = rink.bounds.size.x * Screen.height / Screen.width * 0.5f;

        Camera.main.orthographicSize = orthoSize;
    }

}
