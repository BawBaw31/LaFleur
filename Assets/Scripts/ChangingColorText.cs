using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingColorText : MonoBehaviour
{   
    public Gradient gradient;
    public float scaleTime;
    float t = 0f;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        text.color = gradient.Evaluate(t);
    }

    void Update()
    {
        t += Time.deltaTime * scaleTime;
        Color color = gradient.Evaluate(t);
        text.color = color;
        CheckScaleSens();
    }

    void CheckScaleSens() {
        if (t >= 1)
        {
            t = 0.0f;
        } 
    }
}
