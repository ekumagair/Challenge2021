using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseInfinita : MonoBehaviour
{
    // Script ativo apenas na fase infinita.

    public GameObject[] inimigos;
    public float inimigoMinX, inimigoMaxX;
    public float inimigoMinY, inimigoMaxY;
    float mult = 1.1f;

    void Start()
    {
        MenuStaticClass.menuFaseInfinita = true;
        mult = 1.1f;
        StartCoroutine(CriarInimigos(2f));
    }

    // Criar um inimigo em um local aleatório da fase.
    IEnumerator CriarInimigos(float t)
    {
        Vector2 inimigoPos;
        inimigoPos = new Vector2(Random.Range(inimigoMinX, inimigoMaxX), Random.Range(inimigoMinY, inimigoMaxY));

        yield return new WaitForSeconds(t);

        var inimigoCriado = Instantiate(inimigos[Random.Range(0, 4)], inimigoPos, transform.rotation);
        inimigoCriado.GetComponent<FormaBase>().corR = (byte) Random.Range(0, 255);
        inimigoCriado.GetComponent<FormaBase>().corG = (byte) Random.Range(0, 255);
        inimigoCriado.GetComponent<FormaBase>().corB = (byte) Random.Range(0, 255);

        GetComponent<FaseEventosScript>().GerarSetaApontandoPara(inimigoCriado, true);
        StartCoroutine(inimigoCriado.GetComponent<FormaBase>().Piscar());

        StartCoroutine(CriarInimigos((Random.Range(2f, 4f) * mult) + 0.1f));

        if(mult > 0.4f)
        {
            mult -= 0.1f;
        }
    }
}
