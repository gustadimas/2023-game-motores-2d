using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoResposta : MonoBehaviour
{
    [SerializeField] public TMP_InputField resposta;
    [SerializeField] GameObject pergunta;

    public void Continuou()
    {
        Time.timeScale = 1;
        pergunta.SetActive(false);
    }

    public void ReceberRespMulti1()
    {
        if (resposta.text == "45")
            Continuou();

        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Fase 1");
        }
    }

    public void ReceberRespMulti2()
    {
        if (resposta.text == "147")
            Continuou();

        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Fase 1");
        }
    }

    public void ReceberRespMulti3()
    {
        if (resposta.text == "81")
            Continuou();

        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Fase 1");
        }
    }

    public void ReceberRespDiv()
    {
        if (resposta.text == "3")
        {
            Continuou();
            GameObject.FindObjectOfType<Inimigo>().IniciarDialogo();
        }

        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Fase 4");
        }
    }
}
