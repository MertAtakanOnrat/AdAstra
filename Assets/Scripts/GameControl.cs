using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    List<GameObject> asteroidPrefabs = new List<GameObject>();
    [SerializeField]
    List<GameObject> spaceShipPrefab = new List<GameObject>();

    List<GameObject> asteroidList = new List<GameObject>();

    GameObject spaceShip;

    [SerializeField]
    int difficulty = 1;
    [SerializeField]
    int multiplier = 5;

    UIControl uIControl;
    // Start is called before the first frame update
    void Start()
    {
        uIControl = GetComponent<UIControl>();
    }
   
    public void StartGame()
    {
        uIControl.GameIsStarted();
        spaceShip = Instantiate(spaceShipPrefab[Random.Range(0,8)]);
        spaceShip.transform.position = new Vector3(0, ScreenCalculator.Bottom + 2f);
        SpawnAsteroid(5);
    }

    void SpawnAsteroid(int adet)
    {
        Vector3 position = new Vector3();
        for (int i = 0; i < adet; i++)
        {
            position.z = -Camera.main.transform.position.z;
            position = Camera.main.ScreenToWorldPoint(position);
            position.x = Random.Range(ScreenCalculator.Left, ScreenCalculator.Right);
            position.y = ScreenCalculator.Top - 1;

            GameObject asteroid = Instantiate(asteroidPrefabs[Random.Range(0, 4)], position, Quaternion.identity);
            asteroidList.Add(asteroid);
        }
    }

    public void AsteroidDestroyed(GameObject asteroid)
    {
        uIControl.AsteroidDestroyed(asteroid);
        asteroidList.Remove(asteroid);
        if (asteroidList.Count <= difficulty)
        {
            difficulty++;
            SpawnAsteroid(difficulty * multiplier);
        }
    }

    public void FinishGame()
    {
        foreach (GameObject asteroid in asteroidList)
        {
            asteroid.GetComponent<Asteroid>().DestroyAsteroid();
        }
        asteroidList.Clear();
        difficulty = 1;
        uIControl.GameOver();
    }
}
