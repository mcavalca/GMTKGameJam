using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveAction : MonoBehaviour
{

    public GameObject interactiveObject;
    public List<Sprite> interactiveObjectSprite = new List<Sprite>();
    public int interactiveTotal;

    public void ActivateInteraction(int actionNumber) {
        interactiveObject.GetComponent<SpriteRenderer>().sprite = interactiveObjectSprite[actionNumber];
    }
}
