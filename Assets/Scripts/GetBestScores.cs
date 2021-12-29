using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class GetBestScores : MonoBehaviour
{
    Text bestScores;

    // Start is called before the first frame update
    void Start()
    {
        bestScores = GetComponent<Text>();

        // class ApiManager contains string apiUrl
        StartCoroutine(GetRequest(ApiManager.apiUrl+"score/best/3"));
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("api-key", ApiManager.apiKEY);
            yield return webRequest.SendWebRequest();

            print(webRequest.downloadHandler.text);

            var response = JSON.Parse(webRequest.downloadHandler.text);
            bestScores.text = "";
            foreach(JSONNode score in response["scores"])
            {
                bestScores.text += score["player"]+" : "+score["value"]+"\n";
            }

        }
    }
}
