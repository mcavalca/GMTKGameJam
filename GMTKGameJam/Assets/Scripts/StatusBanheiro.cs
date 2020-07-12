using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBanheiro : MonoBehaviour
{
    public GameObject banheiro;
    public Sprite banheiroLimpoBG;
    public Sprite banheiroSujoBG;
    public bool sujabanheirobool;
    
    void Update()
    {
        if (!sujabanheirobool) limpaBanheiro();
        else sujaBanheiro();
    }

    void limpaBanheiro() {
        banheiro.GetComponent<SpriteRenderer>().sprite = banheiroLimpoBG;
    }

    void sujaBanheiro() {
        banheiro.GetComponent<SpriteRenderer>().sprite = banheiroSujoBG;
    }
}
