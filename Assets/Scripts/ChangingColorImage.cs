using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingColorImage : MonoBehaviour
{   
    public Gradient gradient;
    public float scaleTime;
    // public float duration;
    float t = 0f;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = gradient.Evaluate(t);
    }

    // Update is called once per frame
    void Update()
    {
        // float value = Mathf.Lerp(0f, 1f, t);
        // t += Time.deltaTime / duration;
        t += Time.deltaTime * scaleTime;
        Color color = gradient.Evaluate(t);
        image.color = color;
        CheckScaleSens();
    }

    void CheckScaleSens() {
        if (t >= 1)
        {
            t = 0.0f;
        } 
    }
}
