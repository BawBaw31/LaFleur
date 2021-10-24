using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaFleur : MonoBehaviour
{

    Collider2D col;
    public float speed;
    public GameObject loosePanel;
    public GameObject startPanel;
    public GameObject scoreText;
    public GameObject GameManager;
    private bool firstTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
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
            GameManager.GetComponent<FlameSpawning>().enabled = true;
            scoreText.GetComponent<DisplayScore>().enabled = true;
            firstTouch = true;
        }
    }

    void MoveTouchScreen()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            transform.position = new Vector2(touchPosition.x, touchPosition.y);
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
        if (collision.tag == "Flame")
        {
            scoreText.GetComponent<DisplayScore>().endGame();
            loosePanel.SetActive(true);
        }
    }
}
