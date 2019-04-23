﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    Light light;
    float startInt;
    public int blinkInt;
    SpriteRenderer sprite;
    float startAlpha;
    public float blinkAlpha; 
    public float duration;
    bool isBlinking;
    // Start is called before the first frame update
    void Start()
    {
        isBlinking = false;
        light = this.GetComponentInChildren<Light>();
        startInt = light.intensity;
        sprite = this.GetComponentInChildren<SpriteRenderer>();
        startAlpha = sprite.color.a;
        Debug.Log(startAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBlinking)
        {
            StartCoroutine(WaitBlinkSwitch());
        }
    }

    IEnumerator WaitBlinkSwitch()
    {
        isBlinking = true;
        light.intensity = blinkInt;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, blinkAlpha);
        yield return new WaitForSeconds(duration);
        light.intensity = startInt;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, startAlpha);
        
        float r = Random.RandomRange(1, 15);
        yield return new WaitForSeconds(duration*r);
        isBlinking = false;
    }
}
