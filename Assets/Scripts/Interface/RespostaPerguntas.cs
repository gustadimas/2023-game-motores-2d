using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RespostaPerguntas : MonoBehaviour
{
    [SerializeField] TMP_InputField resposta;
    [SerializeField] GameObject pergunta;
    [SerializeField] GameObject botao;

    bool exibiuPergunta = false;

    private void FixedUpdate()
    {
        if (Time.timeScale == 1 && exibiuPergunta)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("jogador") || collision.gameObject.CompareTag("tiro"))
        {
            exibiuPergunta = true;
            Pausou();
        }        
    }

    public void Pausou()
    {
        Time.timeScale = 0;
        pergunta.SetActive(true);
        Resposta();
    }

    void Resposta()
    {
        resposta.gameObject.SetActive(true);
        botao.SetActive(true);
    }
}
