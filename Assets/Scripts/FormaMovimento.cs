using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormaMovimento : MonoBehaviour
{
    Vector2 posInicial;
    Vector2 posObjetivo;
    float qualEixo;
    float qualEixoObjetivo;
    public float velocidade;
    public float distancia;
    float dirAnterior;
    float objetivoDistancia;
    bool calcularNovaPos = false;

    public float movimentoInicial = 1f;
    public bool horizontal;

    void Awake()
    {
        NovaPos(movimentoInicial);
    }

    void Update()
    {
        if (horizontal)
        {
            qualEixo = transform.position.x;
            qualEixoObjetivo = posObjetivo.x;
        }
        else
        {
            qualEixo = transform.position.y;
            qualEixoObjetivo = posObjetivo.y;
        }

        calcularNovaPos = true;

        if ((qualEixo >= qualEixoObjetivo || qualEixo == qualEixoObjetivo) && dirAnterior > 0 && calcularNovaPos == true)
        {
            calcularNovaPos = false;
            NovaPos(-1f);
        }
        if ((qualEixo <= qualEixoObjetivo || qualEixo == qualEixoObjetivo) && dirAnterior < 0 && calcularNovaPos == true)
        {
            calcularNovaPos = false;
            NovaPos(1f);
        }

        if(distancia != 0 && velocidade != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, posObjetivo, Time.deltaTime * velocidade);
        }
    }

    void NovaPos(float dir)
    {
        posInicial = transform.position;

        if(horizontal)
        {
            objetivoDistancia = transform.position.x + distancia * dir;
            posObjetivo = new Vector2(objetivoDistancia, transform.position.y);
        }
        else
        {
            objetivoDistancia = transform.position.y + distancia * dir;
            posObjetivo = new Vector2(transform.position.x, objetivoDistancia);
        }

        dirAnterior = dir;
    }
}
