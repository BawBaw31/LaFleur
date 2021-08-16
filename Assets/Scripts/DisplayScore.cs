using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public float timeLaps;
    float lastScoreTime = 0.0f;
    int scoreValue = 0;
    bool isAlive = true;
    Text score;
    
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {
            lastScoreTime += Time.fixedDeltaTime;
            if (lastScoreTime > timeLaps) {
                scoreValue += 1;
                lastScoreTime = 0;
            }
            score.text = "Score: " + scoreValue;
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", scoreValue);
    }

    public void endGame() {
        isAlive = false;
    }
}
