using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] imagens;
    private int imagemAtual = 0;

    void Start()
    {
        Application.targetFrameRate = 60;

        Invoke("ShowEstatistics", 16f);
    }

    void Update()
    {
        imagens[imagemAtual].GetComponent<Image>().color -= new Color(0, 0, 0, 1f);
    }

    private void ShowEstatistics()
    {
        if (imagemAtual < imagens.Length - 1)
        {
            imagens[imagemAtual].SetActive(false);

            imagens[imagemAtual + 1].SetActive(true);

            imagemAtual++;
        }

        Invoke("ShowEstatistics", 16f);
    }
}
