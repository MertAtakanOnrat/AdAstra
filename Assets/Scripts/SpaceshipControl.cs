using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipControl : MonoBehaviour
{
    [SerializeField]
    GameObject bullet1Prefab = default;
    [SerializeField]
    GameObject bullet2Prefab = default;
    [SerializeField]
    GameObject explosionPrefab = default;

    const float movementPower = 7;

    GameControl gameControl;
    // Start is called before the first frame update
    void Start()
    {
        gameControl = Camera.main.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0)
        {
            position.x += horizontalInput * movementPower * Time.deltaTime;
        }
        if (verticalInput != 0)
        {
            position.y += verticalInput * movementPower * Time.deltaTime;
        }
        transform.position = position;

        if (Input.GetButtonDown("Jump"))
        {
            GameObject.FindGameObjectWithTag("Audio").GetComponent<VoiceControl>().Fire();
            Vector3 bullet1Position = gameObject.transform.position;
            bullet1Position.x += 1.0f;
            bullet1Position.y += 1.2f;

            Vector3 bullet2Position = gameObject.transform.position;
            bullet2Position.x -= 1.0f;
            bullet2Position.y += 1.2f;

            Instantiate(bullet1Prefab, bullet1Position, Quaternion.identity);
            Instantiate(bullet2Prefab, bullet2Position, Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            GameObject.FindGameObjectWithTag("Audio").GetComponent<VoiceControl>().SpaceshipExplosion();
            gameControl.FinishGame();
            Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
