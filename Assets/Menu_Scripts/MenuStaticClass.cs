using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuStaticClass
{
    // Neste script, estão variáveis que afetarão a gameplay também.

    public static int menuFaseDesbloqueada = 1; // Fase desbloqueada no momento, mas não completada. No início, isso é = 1.
                                            // Para verificar quantas fases foram realmente completadas, use [menuFaseDesbloqueada - 1].
    public static int menuFaseSelecionada; // Fase selecionada no menu. O valor só é mudado quando o jogador aperta o botão da fase e inicia ela.

    public static int menuQuadroSelecionado = 1;

    public static bool menuFaseInfinita = false; // Ativa as regras da fase infinita (fase 17)

    public static int menu_dinheiro = 0; // Dinheiro
    public static bool comprouVel;
    public static bool comprouTiros;
    public static bool comprouRes;

    // Configurações
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

    public static bool menuIrParaFases = false; // Ir direto para seleção de fases quando voltar de outra cena.

    public static bool menuContinuou = false; // Selecionou a opção "continuar" no fim de jogo.

    // Estado do jogo quando o jogador perdeu.
    public static bool usandoVel, usandoTiros, usandoRes;
    public static Vector2 perdeuPos;
    public static Quaternion perdeuRot;
    public static Vector2[] posicoesInimigos = new Vector2[200];
    public static int inimigosDestruidos;

    public static int inimigosDestruidosTotal;

    public static bool jogandoPelaPrimeiraVez = true;
}