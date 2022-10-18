using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorVidas : MonoBehaviour
{
    public int meuNumero;
    SpriteRenderer sr;
    GameObject jogador;
    public Canvas canvas;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        jogador = GameObject.Find("Jogador");
    }

    private void Update()
    {
        if (canvas.enabled == true)
        {
            if (jogador.GetComponent<Jogador>().vida >= meuNumero)
            {
                sr.enabled = true;
            }
            else
            {
                sr.enabled = false;
            }
        }
    }
}
