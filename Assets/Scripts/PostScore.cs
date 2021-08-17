using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;

public class PostScore : MonoBehaviour
{
    
    public Text scoreTxt;
    public InputField nameField;
    // TouchScreenKeyboard keyboard;
    // string instagramName;
    int scoreValue;

    void Start()
    {
        // OpenKeyboard();
    }

    void Update()
    {
        // if(keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
        // {
        //     instagramName = keyboard.text;
        // }
    }

    void OnEnable()
    {
        scoreValue  =  PlayerPrefs.GetInt("score");
        scoreTxt.text = "Score : " + scoreValue;
    }

    // public void OpenKeyboard() {
    //     keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    // }

    public void SubmitScore() {
        if (nameField.text != "")
        {
            // class ApiManager contains string apiUrl
            StartCoroutine(PostRequest(ApiManager.apiUrl+"score"));
        }
    }

    IEnumerator PostRequest(string url)
    {
        // Display this on screen
        print("Loading...");
        string json = "{\"player\" : \""+nameField.text+"\", \"value\" : "+scoreValue+"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        var response = JSON.Parse(request.downloadHandler.text);

        if (request.isNetworkError)
        {
            // Display some on screen
            Debug.Log(request.error);
            Debug.Log(response["msg"]);
        }
        else
        {
            // go to main menu
            Debug.Log(response["msg"]);
            SceneManager.LoadScene("MainMenu");
        }
    }

}
