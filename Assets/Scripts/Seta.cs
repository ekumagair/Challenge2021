using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta : MonoBehaviour
{
    public GameObject alvo;
    public bool temporario = false;

    private void Start()
    {
        if(temporario == true)
        {
            StartCoroutine(DestruirSeta(5f));
        }
    }

    void Update()
    {
        if(alvo == null)
        {
            Destroy(gameObject);
        }

        OlharParaAlvo();
    }

    // Cada seta aponta para um inimigo específico.
    void OlharParaAlvo()
    {
        if (alvo != null)
        {
            Vector3 alvoPos = alvo.transform.position;

            Vector2 direction = new Vector2(
                alvoPos.x - transform.position.x,
                alvoPos.y - transform.position.y
            );

            transform.up = direction;
        }
    }

    // Destruir a seta depois de alguns segundos (apenas na fase infinita)
    IEnumerator DestruirSeta(float t)
    {
        yield return new WaitForSeconds(t);

        Destroy(gameObject);
    }
}
