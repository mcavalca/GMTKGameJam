using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D playerBody;
    public GameObject playerObject;
    public GameObject interactiveObject;

    private float playerSpeed = 3f;
    private float moveDirection = 0f;
    private float playerPos;

    private bool playerActionEnabled;
    private int totalNumberOfActions = 0;
    private List<bool> actionToDo = new List<bool>();
    private List<bool> actionsDone = new List<bool>();
    private List<float> interactiveObjectPositionX = new List<float>();
    private float interactiveObjectPosition;
    private float threshold = 0.5f;

    private void Awake() {
        playerBody = GetComponent<Rigidbody2D>();
        playerPos = playerBody.gameObject.transform.position.x;
        playerActionEnabled = false;
        foreach(Transform currentChild in interactiveObject.transform){
            totalNumberOfActions++;
            actionToDo.Add(false);
            actionsDone.Add(false);
            interactiveObjectPositionX.Add(currentChild.transform.position.x);
        }
    }

    void Start() {
    }

    void Update() {
    }

    private void FixedUpdate() {
        for (int actionNumber = 0; actionNumber < totalNumberOfActions; actionNumber++)
            interactiveObjectPosition = interactiveObjectPositionX[actionNumber];
            moveDirection = CalculateMoveDirection(playerPos, interactiveObjectPosition);
            StartCoroutine(ActionFunction(actionNumber));
    }

    private void MovePlayer() {
        playerBody.gameObject.transform.Translate(playerSpeed * moveDirection * Time.deltaTime, 0, 0);
    }

    private IEnumerator ActionFunction(int currentAction) {
        MovePlayer();
        playerPos = playerBody.gameObject.transform.position.x;
        if((playerPos >= interactiveObjectPosition - threshold) && (playerPos <= interactiveObjectPosition + threshold)) {
            playerActionEnabled = true;
            actionToDo[currentAction] = true;
            moveDirection = 0f;
            yield return new WaitForSecondsRealtime(2);
        }
    }

    private float CalculateMoveDirection(float currentPos, float nextPos) {
        float nextMoveDirection;
        float distance = (nextPos - currentPos);
        nextMoveDirection = distance/Math.Abs(distance);
        return nextMoveDirection;
    }

}