using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class GetBestScores : MonoBehaviour
{
    Text bestScores;
    public Image Loader;
    public Text ErrorMsg;

    // Start is called before the first frame update
    void Start()
    {
        ErrorMsg.enabled = false;
        Loader.enabled = true;
        bestScores = GetComponent<Text>();

        // class ApiManager contains string apiUrl
        StartCoroutine(GetRequest(ApiManager.apiUrl+"score/best/3"));
    }

    bool IsJSON (string input) {
        return input.StartsWith("{") && input.EndsWith("}")  
        || input.StartsWith("[") && input.EndsWith("]"); 
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // class ApiManager contains string apiKEY
            webRequest.SetRequestHeader("api-key", ApiManager.apiKEY);
            yield return webRequest.SendWebRequest();

            if (string.IsNullOrEmpty (webRequest.error)
            && IsJSON(webRequest.downloadHandler.text)) {
                var response = JSON.Parse(webRequest.downloadHandler.text);
                bestScores.text = "";
                Loader.enabled = false;

                foreach(JSONNode score in response["scores"]) {
                    bestScores.text += score["player"]+" : "+score["value"]+"\n";
                }
            } else {
                Loader.enabled = false;
                ErrorMsg.enabled = true;
                Debug.LogError("Web Error : " + webRequest.error);
            }
        }
    }
}
