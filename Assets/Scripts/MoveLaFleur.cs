using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaFleur : MonoBehaviour
{

    Collider2D col;
    public float speed;
    public GameObject loosePanel;
    public GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            transform.position = new Vector2(touchPosition.x, touchPosition.y);
        }
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
