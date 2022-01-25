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
    public Image Loader;
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
                Debug.Log("HEYYY");
                Loader.enabled = false;
            }
        }
    }

    IEnumerator PostRequest(string url)
    {
        Loader.enabled = true;
        
        string json = "{\"player\" : \""+nameField.text+"\", \"value\" : "+scoreValue+"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", ApiManager.apiKEY);

        yield return request.SendWebRequest();

        if (string.IsNullOrEmpty (request.error)) {
            var response = JSON.Parse(request.downloadHandler.text);

            if (request.responseCode == 200) {
                // LOGS
                Debug.Log(response["msg"]);
                SceneManager.LoadScene("MainMenu");
            } else {
                Loader.enabled = false;
                // TODO : DISPLAY SOME ON SCREEN
                // LOGS
                Debug.LogError(request.responseCode + ": " + response["msg"]);
            }
            
        }
        else {
            Loader.enabled = false;
            // TODO : DISPLAY SOME ON SCREEN
            // LOGS
            Debug.LogError(request.error);
        }
    }

}
