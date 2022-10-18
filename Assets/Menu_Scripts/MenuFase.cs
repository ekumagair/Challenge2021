using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFase : MonoBehaviour
{
    public int qualFase; // Para qual fase este botão leva.
    public bool infinita = false; // É a fase infinita?
    MenuScript scripts;

    private void Start()
    {
        scripts = GameObject.Find("MenuScripts").GetComponent<MenuScript>();
    }

    private void Update()
    {
        if(MenuStaticClass.menuFaseDesbloqueada < qualFase)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }

    public void ClicouFase()
    {
        Debug.Log("Clicou fase " + qualFase);
        PlayerPrefs.SetInt("faseDesbloqueada", MenuStaticClass.menuFaseDesbloqueada);
        MenuStaticClass.menuFaseSelecionada = qualFase;
        MenuStaticClass.menuFaseInfinita = infinita;
        scripts.EsconderBotao("B_Fase" + qualFase, "Text" + qualFase);
        scripts.IrPara(45, 0, true);
    }
}