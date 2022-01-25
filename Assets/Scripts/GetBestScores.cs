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

    // Start is called before the first frame update
    void Start()
    {
        Loader.enabled = true;
        bestScores = GetComponent<Text>();

        // class ApiManager contains string apiUrl
        StartCoroutine(GetRequest(ApiManager.apiUrl+"score/best/3"));
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // class ApiManager contains string apiKEY
            webRequest.SetRequestHeader("api-key", ApiManager.apiKEY);
            yield return webRequest.SendWebRequest();

            if (string.IsNullOrEmpty (webRequest.error)) {
                // print(webRequest.downloadHandler.text);
                var response = JSON.Parse(webRequest.downloadHandler.text);
                bestScores.text = "";
                Loader.enabled = false;
                foreach(JSONNode score in response["scores"])
                {
                    bestScores.text += score["player"]+" : "+score["value"]+"\n";
                }
            } else {
                Loader.enabled = false;
                // TODO : DISPLAY SOME
                Debug.LogError(webRequest.error);
            }
        }
    }
}
