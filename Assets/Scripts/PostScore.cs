using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;

public class PostScore : MonoBehaviour
{
    public Image Loader;
    public Text ErrorMsg;
    public Text scoreTxt;
    public InputField nameField;
    int scoreValue;

    void Start()
    {
        ErrorMsg.enabled = false;
        Loader.enabled = false;
    }

    void OnEnable()
    {
        scoreValue  =  PlayerPrefs.GetInt("score");
        scoreTxt.text = "Score : " + scoreValue;
    }

    public void SubmitScore() {
        if (nameField.text != "")
        {
            // Class ApiManager contains string apiUrl
            StartCoroutine(PostRequest(ApiManager.apiUrl+"score"));
        }
    }

    IEnumerator PostRequest(string url)
    {
        ErrorMsg.enabled = false;
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
                Debug.Log(request.responseCode + ": " + response["msg"]);
                SceneManager.LoadScene("MainMenu");
            } else {
                Loader.enabled = false;
                ErrorMsg.enabled = true;
                Debug.LogError(request.responseCode + ": " + response["msg"]);
            }
            
        }
        else {
            Loader.enabled = false;
            ErrorMsg.enabled = true;
            Debug.LogError("Web Error : " +  request.error);
        }
    }

}
