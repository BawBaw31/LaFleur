using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaFleur : MonoBehaviour
{
    public float speed;
    public GameObject loosePanel;
    public GameObject startPanel;
    public GameObject scoreText;
    public GameObject gameManager;
    public string enemyTags;

    private bool firstTouch = false;
    private bool dragging = false;
    private float dist;
    private Vector3 offset;
    private Transform toDrag;
    private List<GameObject> enemys;

    void Update()
    {
        if (firstTouch)
        {
            MoveTouchScreen();
            MoveKeyboard();
        } else
        {
            checkFirstTouch();
        }
    }

    void checkFirstTouch()
    {
        if (Input.touchCount > 0 
            || Input.GetAxis("Horizontal") > 0 
            || Input.GetAxis("Vertical") > 0)
        {

            startPanel.SetActive(false);
            gameManager.GetComponent<EnemySpawning>().enabled = true;
            scoreText.GetComponent<DisplayScore>().enabled = true;
            firstTouch = true;
        }
    }

    void MoveTouchScreen()
    {
        Vector3 v3;
 
        if (Input.touchCount <= 0) {
            return;
        }
 
        Touch touch = Input.GetTouch(0);
        Vector3 pos = touch.position;

        if(touch.phase == TouchPhase.Began) {
            Vector2 TouchPosition = Camera.main.ScreenToWorldPoint(pos); 
            RaycastHit2D hit = Physics2D.Raycast(TouchPosition, Vector2.zero);

            if(hit && (hit.collider.tag == "Lafleur"))
            {
                toDrag = hit.transform;
                dist = hit.transform.position.z - Camera.main.transform.position.z;
                v3 = new Vector3(pos.x, pos.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                offset = toDrag.position - v3;
                dragging = true;
            }
        }

        if (dragging && touch.phase == TouchPhase.Moved) {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v3 + offset;
        }

        if (dragging && 
        (touch.phase == TouchPhase.Ended || 
        touch.phase == TouchPhase.Canceled)) {
            dragging = false;
        }
    }

    void MoveKeyboard() 
    {
        float xDirection = Input.GetAxis("Horizontal");
        float yDirection = Input.GetAxis("Vertical");
        Vector3 moveDirection  = new Vector3(xDirection, yDirection, 0);
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyTags.Contains(collision.tag))
        {
            scoreText.GetComponent<DisplayScore>().endGame();
            loosePanel.SetActive(true);
            StopEnemys();
        }
    }

    void StopEnemys() {
        string[] allEnemyTags = enemyTags.Split(' ');
        gameManager.GetComponent<EnemySpawning>().enabled = false;
        foreach (string enemyTag in allEnemyTags)
        {
            GameObject[] enemyGroup = GameObject.FindGameObjectsWithTag(enemyTag);
            foreach (GameObject enemy in enemyGroup) {
                MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = false;
                }
            }
        }
        Destroy(gameObject);
    }
}
