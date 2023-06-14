using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Reflection;

public class GuiaApresentacao : MonoBehaviour
{
    public Image guia;
    [HideInInspector] public bool imagem;
    public GameObject jogador;

    [SerializeField] TextMeshProUGUI textoDialogo;
    [SerializeField] string[] falas;
    [SerializeField] float delayEntreLetras;
    public GameObject CaixaDeDialogo;

    bool dialogoAtivo = false;
    bool chamouDialogo = false;
    bool guiaAparece = false;
    int index = 0;

    void Start()
    {
        Invoke("GuiaAparece", 3f);
        jogador.GetComponent<MovimentoJogador>().move = false;
    }

    void Update()
    {
        if (!guiaAparece) 
            return;

        if (dialogoAtivo)
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                if (!Input.GetKeyDown(KeyCode.Escape))
                {
                    if (textoDialogo.text != falas[index])
                    {
                        StopAllCoroutines();
                        textoDialogo.text = falas[index];
                    }
                    else if (index < falas.Length - 1)
                    {
                        index++;
                        StartCoroutine(TipoDeFala(falas[index]));
                    }
                    else if (index >= falas.Length - 1)
                    {
                        CaixaDeDialogo.SetActive(false);
                        imagem = true;
                    }
                }
            }
        }

        if (guia.color.a >= 0.99f)
        {
            if (chamouDialogo == false)
            {
                CaixaDeDialogo.SetActive(true);
                index = 0;
                chamouDialogo = true;
                IniciarDialogo();
            }
        }

        if (imagem == false)
        {
            guia.color += new Color(0, 0, 0, 0.01f);

            if (guia.color.a > 1) 
                guia.color = new Color(guia.color.r, guia.color.g, guia.color.b, 1);
        }
        else
        {
            guia.color += new Color(0, 0, 0, -0.01f);
            jogador.GetComponent<MovimentoJogador>().move = true;
        }
    }

    IEnumerator TipoDeFala(string fala)
    {
        textoDialogo.text = "";

        foreach (char letra in fala)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(delayEntreLetras);
        }
    }

    public void IniciarDialogo()
    {
        dialogoAtivo = true;
        StartCoroutine(TipoDeFala(falas[index]));
    }

    void GuiaAparece()
    {
        guiaAparece = true;
    }
}
