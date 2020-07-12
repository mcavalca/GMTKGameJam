using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateChuveiro : MonoBehaviour
{
    public GameObject playerObject; 
    public GameObject interactiveObject;
    
    public void activateChuveiro() {
        interactiveObject.GetComponent<Animator>().SetBool("chuveiroLigado", true);
    }

}
