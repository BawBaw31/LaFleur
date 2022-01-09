using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using System.Net;

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
            try {
                StartCoroutine(PostRequest(ApiManager.apiUrl+"score"));
            } catch(WebException e)
            {
                Debug.Log(e);
            }
        }
    }

    IEnumerator PostRequest(string url)
    {
        // TODO : ADD LOADER
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
            // TODO : DISPLAY SOME ON SCREEN
            Debug.Log(request.error);
            Debug.Log(request.responseCode + ": " + response["msg"]);
        }
        else
        {
            if (request.responseCode == 200)
            {
                Debug.Log(response["msg"]);
                SceneManager.LoadScene("MainMenu");
            } else 
            {
                // TODO : DISPLAY SOME ON SCREEN
                Debug.Log(request.responseCode + ": " + response["msg"]);
            }
        }
    }

}
