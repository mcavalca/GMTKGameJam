using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePoints : MonoBehaviour
{

    public GameObject interactiveObject;
    public GameObject interactiveMenu;
    public GameObject playerObject;
    [Range (1, 4)]
    public int interactiveTotalActions;
    public List<int> interactiveActionPoint = new List<int>();
    public bool isPorta;
    public int countActionNumber = 0;

    void Awake() {
        foreach(Transform currentChild in interactiveMenu.transform) {
            if (countActionNumber < interactiveTotalActions) {
                currentChild.gameObject.SetActive(true);                
                countActionNumber++;
            }
            else
                currentChild.gameObject.SetActive(false);
        }
    }

    public void AddPoint(int actionPerformed) {
        actionPerformed--;
        if (interactivePointExists(actionPerformed)) {
            int pointToAdd = interactiveActionPoint[actionPerformed];
            playerObject.GetComponent<PlayerControl>().AddPoint(pointToAdd);
        }
    }

    public bool returnPorta() {
        return this.isPorta;
    }

    public bool interactivePointExists(int actionPerformed) {
        return (actionPerformed <= countActionNumber);
    }
}
