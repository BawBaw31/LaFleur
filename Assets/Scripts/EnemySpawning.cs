using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public GameObject skull;
    public GameObject knife;
    public float timeLaps;
    public int knifeY;
    public int[] knifePossibleX = new int[11];
    float lastSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawn += Time.fixedDeltaTime;
        if (lastSpawn > timeLaps) 
        {
            Vector3 skullSpawnPosition = GetRandomPosition();
            Instantiate(skull, skullSpawnPosition, Quaternion.identity);
            Vector3 knifeSpawnPosition = GetRandomPositionVert();
            Instantiate(knife, knifeSpawnPosition, Quaternion.identity);
            if (timeLaps > 3) {
                timeLaps -= 0.5f;
            }
            lastSpawn = 0;
        }
    }
    Vector3 GetRandomPosition() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, 0);
    }
    Vector3 GetRandomPositionVert() {
        int randomIndex = Random.Range(0, knifePossibleX.Length);
        return new Vector3(knifePossibleX[randomIndex], knifeY, 0);
    }
}
