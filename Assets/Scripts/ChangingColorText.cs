﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingColorText : MonoBehaviour
{   
    public Gradient gradient;
    public float scaleTime;
    // public float duration;
    float t = 0f;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.color = gradient.Evaluate(t);
    }

    // Update is called once per frame
    void Update()
    {
        // float value = Mathf.Lerp(0f, 1f, t);
        // t += Time.deltaTime / duration;
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