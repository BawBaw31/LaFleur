using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingColorImage : MonoBehaviour
{   
    public Gradient gradient;
    public float scaleTime;
    float t = 0f;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.color = gradient.Evaluate(t);
    }

    void Update()
    {
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
