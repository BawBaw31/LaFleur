using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePatrol : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float maxSpeed;
    public float minSpeed;
    public float secondsToMaxDifficulty;
    public float spawnTime;
    public float flameDuration;

    Vector2 targetPosition;
    float timer = 0.0f;
    float speed;
    CircleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GetRandomPosition();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set Timer
        timer += Time.fixedDeltaTime;

        // Activate Collider2D
        if(timer >= spawnTime && !col.enabled)
        {
            col.enabled = true;
        }

        // Flame death
        if (timer >= flameDuration) 
        {
            Destroy(gameObject);
        }

        // Flame Patrol
        if((Vector2)transform.position != targetPosition){
            speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPercent());
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        } else {
            targetPosition = GetRandomPosition();
        }
    }

    Vector2 GetRandomPosition() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    float GetDifficultyPercent() {
        return Mathf.Clamp01(timer / secondsToMaxDifficulty);
    }
}
