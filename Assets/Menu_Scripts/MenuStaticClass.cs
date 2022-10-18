using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuStaticClass
{
    // Neste script, est�o vari�veis que afetar�o a gameplay tamb�m.

    public static int menuFaseDesbloqueada = 1; // Fase desbloqueada no momento, mas n�o completada. No in�cio, isso � = 1.
                                            // Para verificar quantas fases foram realmente completadas, use [menuFaseDesbloqueada - 1].
    public static int menuFaseSelecionada; // Fase selecionada no menu. O valor s� � mudado quando o jogador aperta o bot�o da fase e inicia ela.

    public static int menuQuadroSelecionado = 1;

    public static bool menuFaseInfinita = false; // Ativa as regras da fase infinita (fase 17)

    public static int menu_dinheiro = 0; // Dinheiro
    public static bool comprouVel;
    public static bool comprouTiros;
    public static bool comprouRes;

    // Configura��es
    public static bool menuConfigSFX = true;
    public static bool menuConfigMusica = true;
    public static bool menuConfigParticulas = true;
    public static bool menuConfigInverter = false;
    public static bool menuConfigCentralizar = true;
    public static bool menuConfigPosVidas = true;
    public static bool menuConfigAfastarCam = true;
    public static bool menuConfigPosSairEsq = false;
    public static bool menuConfigPosVidas2 = false;
    public static bool menuConfigJoyStick = false;

    public static bool menuIrParaFases = false; // Ir direto para sele��o de fases quando voltar de outra cena.

    public static bool menuContinuou = false; // Selecionou a op��o "continuar" no fim de jogo.

    // Estado do jogo quando o jogador perdeu.
    public static bool usandoVel, usandoTiros, usandoRes;
    public static Vector2 perdeuPos;
    public static Quaternion perdeuRot;
    public static Vector2[] posicoesInimigos = new Vector2[200];
    public static int inimigosDestruidos;

    public static int inimigosDestruidosTotal;

    public static bool jogandoPelaPrimeiraVez = true;
}