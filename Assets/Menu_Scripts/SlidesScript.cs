using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidesScript : MonoBehaviour
{
    bool pressionando = false;
    bool podeTocar = true;
    public int slideAtual = 0;
    public GameObject[] slidesLista;
    public GameObject[] botoes;

    void Start()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pressionando = false;
        podeTocar = true;
        MenuStaticClass.menuFaseSelecionada = 1;
        PlayerPrefs.SetInt("jogandoPelaPrimeiraVez", 1);
        MenuStaticClass.jogandoPelaPrimeiraVez = true;
    }

    void Update()
    {
        if (Input.touchCount > 0 && podeTocar)
        {
            pressionando = true;
        }
        else if (Input.GetButton("Fire1") && podeTocar)
        {
            pressionando = true;
        }
        else
        {
            pressionando = false;
        }

        if(pressionando == true && podeTocar)
        {
            StartCoroutine(Tocou());
        }

        if (slideAtual > 0 && slideAtual < slidesLista.Length)
        {
            slidesLista[slideAtual - 1].transform.position = Vector2.MoveTowards(slidesLista[slideAtual - 1].transform.position, new Vector2(-7, 0), 0.1f);
        }

        if(slideAtual == slidesLista.Length - 1 && botoes != null)
        {
            foreach (GameObject go in botoes)
            {
                Destroy(go);
            }
        }

        if (slideAtual < slidesLista.Length)
        {
            slidesLista[slideAtual].transform.position = Vector2.MoveTowards(slidesLista[slideAtual].transform.position, new Vector2(0, 0), 0.1f);
        }
    }

    IEnumerator Tocou()
    {
        podeTocar = false;
        slideAtual++;

        if(slideAtual > slidesLista.Length - 1)
        {
            SceneManager.LoadScene("Fase1");
        }

        yield return new WaitForSeconds(1f);
        podeTocar = true;
    }
}