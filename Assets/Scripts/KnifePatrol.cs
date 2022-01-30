using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePatrol : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public float finalY;

    float speed;
    Vector2 targetPosition;
    PolygonCollider2D col;

    void Start()
    {   
        col = GetComponent<PolygonCollider2D>();
        speed = Random.Range(minSpeed, maxSpeed);
        targetPosition = GetFinalPosition();
    }

    void Update()
    {
        if((Vector2)transform.position != targetPosition) {
            Patrol();
        } else {
            Destroy(gameObject);
        }
    }

    Vector2 GetFinalPosition() {
        return new Vector2(transform.position.x, finalY);
    }

    void Patrol() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
