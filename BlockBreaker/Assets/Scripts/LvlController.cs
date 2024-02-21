using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{
    public static LvlController instance;

    public float gameSpeed = 2;

    public float cameraSpeed;

    public int obstaclesAmount = 6;

    public float damageTime = 0.1f;

    //public Color easyColor, mediumColor, hardColor;

    public float obstaclesDistance = 13;

    public ObjectPool pickupPool;
    public Vector2 xLimit;

    public float multiplier = 1;
    public float cicleTime = 10;

    public bool gameOver = true;

    private int points;

    private Transform player;

    private void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {

        player = FindObjectOfType<PlayerController>().transform;

        while (gameOver)
        {
            yield return null;
        }

        SpawnPickups();

        InvokeRepeating("IncreaseDifficulty", cicleTime, cicleTime);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
    }

    public void StartGame()
    {
        
        gameOver = false;
    }

    public void GameOver()
    {
        gameOver = true;
        gameSpeed = 0;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Score(int amount)
    {
        points += amount;
    }

    void IncreaseDifficulty()
    {
        obstaclesAmount += 2;

        multiplier *= 1.1f;
    }

    void SpawnPickups()
    {
        pickupPool.GetObject().transform.position = new Vector2(Random.Range(xLimit.x, xLimit.y), player.position.y + 15);

        Invoke("SpawnPickups", Random.Range(1f, 3f));
    }
}
