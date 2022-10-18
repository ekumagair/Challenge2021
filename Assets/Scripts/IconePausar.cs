using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconePausar : MonoBehaviour
{
    public Sprite[] sprites;
    SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
        {
            _sr.sprite = sprites[0];
        }
        else
        {
            _sr.sprite = sprites[1];
        }
    }
}
