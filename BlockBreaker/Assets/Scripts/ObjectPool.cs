using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ref tutorial https://www.youtube.com/watch?v=1nts5v-p5lQ
public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int amount;

    public float minX, minY, maxX, maxY;
    public float coolDown;
    public float spawnTime;

    public GameObject[] prefabs;

    private int index;

    // Use this for initialization
    void Awake()
    {

        prefabs = new GameObject[amount];

        for (int i = 0; i < amount; i++)
        {
            prefabs[i] = Instantiate(prefab, new Vector2(0, 15), Quaternion.identity);
            prefabs[i].SetActive(false);
        }

    }

    private void Update()
    {
        if(Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + coolDown;
        }
    }

    public GameObject GetObject()
    {

        index++;
        if (index >= amount)
        {
            index = 0;
        }

        prefabs[index].SetActive(true);

        return prefabs[index];
    }

    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(prefab, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
