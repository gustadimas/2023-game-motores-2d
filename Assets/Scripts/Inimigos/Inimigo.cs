using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoDialogo;
    [SerializeField] string[] falas;
    [SerializeField] float delayEntreLetras;
    [SerializeField] GameObject dialogoInimigo;

    bool dialogoInimAtivo = false;
    private int index = 0;

    [SerializeField] GameObject jogador;

    BotaoResposta resposta;

    bool barreiraCaiu = false;

    void Start()
    {
        dialogoInimigo.SetActive(false);
        textoDialogo.text = "";
    }

    private void FixedUpdate()
    {
        if (barreiraCaiu == true && dialogoInimAtivo == false)
        {
            //IniciarDialogo();
            Invoke("IniciarDialogo", 1f);
            jogador.GetComponent<Rigidbody2D>().isKinematic = true;
            jogador.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void Update()
    {
        if (dialogoInimAtivo)
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
                    else
                    {
                        index = 0;
                        dialogoInimigo.SetActive(false);
                        gameObject.SetActive(false);
                        jogador.GetComponent<Rigidbody2D>().isKinematic = false;
                        jogador.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    }
                }
            }
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
        dialogoInimigo.SetActive(true);
        dialogoInimAtivo = true;
        StartCoroutine(TipoDeFala(falas[index]));
    }

    public void BarreiraCaiu()
    {
        barreiraCaiu = true;
    }
}
