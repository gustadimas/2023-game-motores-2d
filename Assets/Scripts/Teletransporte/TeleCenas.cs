using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleCenas : MonoBehaviour
{
    public int ilevelToLoad;
    public string sLevelToLoad;
    public bool useIntToLoadLevel = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("jogador"))
        {
            AudioManager.instance.Play("Passou");

            if (useIntToLoadLevel)
                SceneManager.LoadScene(ilevelToLoad);
            else
                SceneManager.LoadScene(sLevelToLoad);
        }
    }
}
