using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostScore : MonoBehaviour
{
    
    public Text scoreTxt;
    TouchScreenKeyboard keyboard;
    string instagramName;
    int scoreValue;

    void Start()
    {
        OpenKeyboard();
    }

    void Update()
    {
        if(keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
        {
            instagramName = keyboard.text;
        }
    }

    void OnEnable()
    {
        scoreValue  =  PlayerPrefs.GetInt("score");
        scoreTxt.text = "Score : " + scoreValue;
    }

    public void OpenKeyboard() {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void submitScore() {
        if (instagramName != null)
        {
            print("score : " + scoreValue);
            print("Insta : " + instagramName);
            // http post request with name + score
        }
    }

}
