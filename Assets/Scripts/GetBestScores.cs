using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class GetBestScores : MonoBehaviour
{
    Text bestScores;
    string url;
    public GameObject VarContainer;
    // ApiUrl script contains url string  

    // Start is called before the first frame update
    void Start()
    {
        url = VarContainer.GetComponent<ApiUrl>().url;
        bestScores = GetComponent<Text>();
        // StartCoroutine(GetRequest("https://lafleur-game-server.herokuapp.com/api/score/best/3"));
        StartCoroutine(GetRequest(url+"score/best/3"));
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            print(webRequest.downloadHandler.text);

            var response = JSON.Parse(webRequest.downloadHandler.text);
            bestScores.text = "";
            foreach(JSONNode score in response["scores"])
            {
                bestScores.text += score["player"]+" : "+score["value"]+"\n";
            }
            print(bestScores.text);

        }
    }
}
