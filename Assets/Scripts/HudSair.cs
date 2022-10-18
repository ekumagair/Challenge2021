using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudSair : MonoBehaviour
{
    Image img;
    int cliques = 0;
    Color normal;
    Color apertado;
    AudioSource _as;

    private void Start()
    {
        img = GetComponent<Image>();
        cliques = 0;
        _as = GetComponent<AudioSource>();

        normal = new Color(0.8f, 0.8f, 0.8f, 0.6f);
        apertado = new Color(1f, 1f, 1f, 1f);
    }

    private void Update()
    {
        if(cliques <= 0)
        {
            img.color = normal;
        }
        else
        {
            img.color = apertado;
        }
    }

    public void ClicarSair()
    {
        cliques++;
        StartCoroutine(VoltarClique(2f));
        _as.PlayOneShot(_as.clip);

        if (cliques >= 2)
        {
            Debug.Log("Clicou em sair");
            MenuStaticClass.menuIrParaFases = true;
            Time.timeScale = 1;
            AudioListener.pause = false;
            SceneManager.LoadScene("Menus");
        }
    }

    IEnumerator VoltarClique(float t)
    {
        yield return new WaitForSeconds(t);
        cliques = 0;
    }
}
