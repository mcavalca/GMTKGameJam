using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D playerBody;
    public GameObject playerObject;
    public GameObject interactiveObjectsList;

    private float playerSpeed = 3f;
    public float moveDirection = 0f;
    public float playerPos;

    private bool playerActionEnabled;
    private bool isAction;
    public int totalNumberOfActions = 0;
    public int actionNumber;
    private List<bool> actionToDo = new List<bool>();
    private List<bool> actionsDone = new List<bool>();
    public List<float> interactiveObjectPositionX = new List<float>();
    public float interactiveObjectPosition;
    private float threshold = 0.3f;

    private void Awake() {
        playerBody = GetComponent<Rigidbody2D>();
        playerPos = playerBody.gameObject.transform.position.x;
        playerActionEnabled = false;
        foreach(Transform currentChild in interactiveObjectsList.transform){
            totalNumberOfActions++;
            actionToDo.Add(false);
            actionsDone.Add(false);
            interactiveObjectPositionX.Add(currentChild.transform.position.x);
        }
        interactiveObjectPositionX.Add(3f);
        moveDirection = CalculateMoveDirection(playerPos, interactiveObjectPositionX[0]);
        actionNumber = 0;
        interactiveObjectPosition = interactiveObjectPositionX[0];
        isAction = true;
    }

    void Start() {/*
        for (actionNumber = 0; actionNumber < totalNumberOfActions; actionNumber++)
            interactiveObjectPosition = interactiveObjectPositionX[actionNumber];
            StartCoroutine(ActionFunction(actionNumber)); */
    }

    void Update() {
    }

    private void FixedUpdate() {
        MovePlayer();
        if((playerPos >= interactiveObjectPosition - threshold) && (playerPos <= interactiveObjectPosition + threshold) && (isAction)) {
            StartCoroutine(ActionFunction(actionNumber));
        }
    }

    private void MovePlayer() {
        playerObject.GetComponent<Animator>().SetBool("isWalking", checkPlayerMoving(moveDirection));
        playerPos = playerBody.gameObject.transform.position.x;
        playerBody.gameObject.transform.Translate(playerSpeed * moveDirection * Time.deltaTime, 0, 0);
    }

    private IEnumerator ActionFunction(int currentAction) {
        //if((playerPos >= interactiveObjectPosition - threshold) && (playerPos <= interactiveObjectPosition + threshold)) {
            isAction = false;
            playerActionEnabled = true;
            actionToDo[currentAction] = true;
            moveDirection = 0f;
            yield return new WaitForSeconds(6f);
            actionNumber++;
            interactiveObjectPosition = interactiveObjectPositionX[actionNumber];
            moveDirection = CalculateMoveDirection(playerPos, interactiveObjectPosition);
            isAction = true;
        //}
    }

    private float CalculateMoveDirection(float currentPos, float nextPos) {
        float nextMoveDirection;
        float distance = (nextPos - currentPos);
        nextMoveDirection = distance/Math.Abs(distance);
        return nextMoveDirection;
    }

    private bool checkPlayerMoving(float movementValue) {
        if (movementValue != 0f) return true;
        return false;
    }
}