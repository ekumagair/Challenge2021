using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGaleriaSeta : MonoBehaviour
{
    public int quadroAdicionar;

    private void Update()
    {
        // Só permite ver quadros de fases já completadas (menuFaseDesbloqueada - 1).
        if (quadroAdicionar == 1)
        {
            if (MenuStaticClass.menuFaseDesbloqueada - 1 <= MenuStaticClass.menuQuadroSelecionado)
            {
                GetComponent<Button>().interactable = false;
                //Debug.Log(quadroAdicionar + " diz: " + (MenuStaticClass.menuFaseDesbloqueada - 1) + " é menor ou igual a " + MenuStaticClass.menuQuadroSelecionado);
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        }

        // Não permitir a visualização de quadros que são antes da fase 1, porque eles não existem.
        if (quadroAdicionar == -1)
        {
            if (MenuStaticClass.menuQuadroSelecionado <= 1)
            {
                GetComponent<Button>().interactable = false;
                //Debug.Log(quadroAdicionar + " diz: " + MenuStaticClass.menuQuadroSelecionado + " é menor ou igual a 1");
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        }
    }

    public void MudarQuadro()
    {
        MenuStaticClass.menuQuadroSelecionado += quadroAdicionar;
        Debug.Log("Quadro atual: " + MenuStaticClass.menuQuadroSelecionado);
    }
}
