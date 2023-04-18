using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextRoom2 : MonoBehaviour
{
    string frase = "WHAT HAPPEND TO ME? \nI'm going to \ncollect all the stones \n             ";
    public TextMeshProUGUI texto;
    public Image Dialogue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Reloj());
    }

    IEnumerator Reloj()
    {
        foreach (char caracter in frase)
        {
            texto.text = texto.text + caracter;
            yield return new WaitForSeconds(0.1f);
        }
        Text.Destroy(texto);
        Text.Destroy(Dialogue);
    }
}
