using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGaleriaQuadro : MonoBehaviour
{
    public Sprite[] spritesQuadro;
    SpriteRenderer _srQuadro;

    void Start()
    {
        _srQuadro = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _srQuadro.sprite = spritesQuadro[MenuStaticClass.menuQuadroSelecionado - 1];
    }
}
