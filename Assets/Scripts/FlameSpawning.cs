using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSpawning : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public GameObject flame;
    public float timeLaps;
    float lastSpawn = 0.0f;
    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = GetRandomPosition();
        Instantiate(flame, spawnPosition, Quaternion.identity);
        lastSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawn += Time.fixedDeltaTime;
        if (lastSpawn > timeLaps) 
        {
            spawnPosition = GetRandomPosition();
            Instantiate(flame, spawnPosition, Quaternion.identity);
            lastSpawn = 0;
        }
    }
    Vector3 GetRandomPosition() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, 0);
    }
}
