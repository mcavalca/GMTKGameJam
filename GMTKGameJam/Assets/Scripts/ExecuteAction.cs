using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExecuteAction : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject interactivePoint;
    private float fillAmountBar = 0;
    private float timeToActionValue;
    private float timeToActionStep;

    void Start() {
        // actionBar = this.transform.GetChild(1).gameObject;
        fillAmountBar = this.transform.GetChild(1).GetComponent<Image>().fillAmount;
        timeToActionStep = playerObject.GetComponent<PlayerControl>().timeToAction;
        timeToActionValue = 50 * timeToActionStep;
        timeToActionStep = 1/timeToActionValue;
    }

    void FixedUpdate()
    {
        if (playerObject.GetComponent<PlayerControl>().IsPlayerAvailable()) {
            if((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && interactivePoint.GetComponent<InteractivePoints>().interactivePointExists(1)) {
                executeActions();
                interactivePoint.GetComponent<InteractivePoints>().AddPoint(1);
            }
            if((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && interactivePoint.GetComponent<InteractivePoints>().interactivePointExists(2)) {
                executeActions();
                interactivePoint.GetComponent<InteractivePoints>().AddPoint(2);
            }
            if((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) && interactivePoint.GetComponent<InteractivePoints>().interactivePointExists(3)) {
                executeActions();
                interactivePoint.GetComponent<InteractivePoints>().AddPoint(3);
            }
            if((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) && interactivePoint.GetComponent<InteractivePoints>().interactivePointExists(4)) {
                executeActions();
                interactivePoint.GetComponent<InteractivePoints>().AddPoint(4);
            }
        }
        this.transform.GetChild(1).GetComponent<Image>().fillAmount -= timeToActionStep;
    }

    private void executeActions() {
        playerObject.GetComponent<PlayerControl>().PlayerInteracted();
        playerObject.GetComponent<PlayerControl>().StopActionCoroutine();
        playerObject.GetComponent<PlayerControl>().MoveToNextAction();
    }
}
