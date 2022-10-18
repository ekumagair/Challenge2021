using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorRastro : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(Rastro());
    }

    IEnumerator Rastro()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.05f);

            Color _cor = GetComponent<SpriteRenderer>().color;
            float _corA = _cor.a - (5f * Time.deltaTime);
            Debug.Log(_cor.a);

            _cor = new Color(_cor.r, _cor.g, _cor.b, _corA);
            GetComponent<SpriteRenderer>().color = _cor;
        }

        Destroy(gameObject);
    }
}
