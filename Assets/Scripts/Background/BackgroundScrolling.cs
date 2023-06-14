using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrolling : MonoBehaviour
{
    float distancia;
    float rePos;
    float tamanho;
    float posInicial;

    Transform cam;

    public float efeitoParallax;

    void Start()
    {
        posInicial = transform.position.x;
        tamanho = GetComponent<Renderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    void Update()
    {
        rePos = cam.position.x * (1 - efeitoParallax);

        distancia = cam.position.x * efeitoParallax;

        transform.position = new Vector3(posInicial + distancia, transform.position.y, transform.position.z);

        if (rePos > posInicial + tamanho)
        {
            posInicial += tamanho;
        }
        else if (rePos < posInicial - tamanho)
        {
            posInicial -= tamanho;
        }
    }
}
