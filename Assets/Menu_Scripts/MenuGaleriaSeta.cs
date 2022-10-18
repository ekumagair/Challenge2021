using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGaleriaSeta : MonoBehaviour
{
    public int quadroAdicionar;

    private void Update()
    {
        // S� permite ver quadros de fases j� completadas (menuFaseDesbloqueada - 1).
        if (quadroAdicionar == 1)
        {
            if (MenuStaticClass.menuFaseDesbloqueada - 1 <= MenuStaticClass.menuQuadroSelecionado)
            {
                GetComponent<Button>().interactable = false;
                //Debug.Log(quadroAdicionar + " diz: " + (MenuStaticClass.menuFaseDesbloqueada - 1) + " � menor ou igual a " + MenuStaticClass.menuQuadroSelecionado);
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        }

        // N�o permitir a visualiza��o de quadros que s�o antes da fase 1, porque eles n�o existem.
        if (quadroAdicionar == -1)
        {
            if (MenuStaticClass.menuQuadroSelecionado <= 1)
            {
                GetComponent<Button>().interactable = false;
                //Debug.Log(quadroAdicionar + " diz: " + MenuStaticClass.menuQuadroSelecionado + " � menor ou igual a 1");
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
