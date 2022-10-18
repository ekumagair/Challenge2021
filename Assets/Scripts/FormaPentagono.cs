using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormaPentagono : MonoBehaviour
{
    public GameObject criar;
    public Vector2[] posQuadrados;
    public float distanciaQuadrados;
    public float multTintaQuadrados;

    void Posicoes()
    {
        posQuadrados[0] = new Vector2(transform.position.x - distanciaQuadrados, transform.position.y - distanciaQuadrados);
        posQuadrados[1] = new Vector2(transform.position.x + distanciaQuadrados, transform.position.y - distanciaQuadrados);
        posQuadrados[2] = new Vector2(transform.position.x + distanciaQuadrados, transform.position.y + distanciaQuadrados);
        posQuadrados[3] = new Vector2(transform.position.x - distanciaQuadrados, transform.position.y + distanciaQuadrados);
    }

    public void GerarInimigos()
    {
        Posicoes();

        for (int i = 0; i < posQuadrados.Length; i++)
        {
            var spawned = Instantiate(criar, transform.position, transform.rotation);

            if(spawned.GetComponent<FormaBase>() != null)
            {
                spawned.GetComponent<FormaBase>().corR = GetComponent<FormaBase>().corR;
                spawned.GetComponent<FormaBase>().corG = GetComponent<FormaBase>().corG;
                spawned.GetComponent<FormaBase>().corB = GetComponent<FormaBase>().corB;
                spawned.GetComponent<FormaBase>().tintaMult = multTintaQuadrados;
            }

            if(spawned.GetComponent<FormaMovimentoEspecifico>() != null)
            {
                spawned.GetComponent<FormaMovimentoEspecifico>().usandoMovimentoEspecifico = true;
                spawned.GetComponent<FormaMovimentoEspecifico>().destinoEspecifico = posQuadrados[i];
            }
        }
    }
}
