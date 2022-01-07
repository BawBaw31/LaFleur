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
    int scoreValue;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnEnable()
    {
        scoreValue  =  PlayerPrefs.GetInt("score");
        scoreTxt.text = "Score : " + scoreValue;
    }

    public void SubmitScore() {
        if (nameField.text != "")
        {
            // class ApiManager contains string apiUrl
            StartCoroutine(PostRequest(ApiManager.apiUrl+"score"));
        }
    }

    IEnumerator PostRequest(string url)
    {
        // FIX : ADD LOADER
        print("Loading...");
        
        string json = "{\"player\" : \""+nameField.text+"\", \"value\" : "+scoreValue+"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", ApiManager.apiKEY);

        yield return request.SendWebRequest();
        var response = JSON.Parse(request.downloadHandler.text);

        if (request.isNetworkError)
        {
            // Display some on Log
            Debug.Log(request.error);
            Debug.Log(response["msg"]);
        }
        else
        {
            // Go to main menu
            Debug.Log(response["msg"]);
            SceneManager.LoadScene("MainMenu");
        }
    }

}
