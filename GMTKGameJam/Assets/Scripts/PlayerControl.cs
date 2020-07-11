using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D playerBody;
    public GameObject playerObject;
    public GameObject interactiveObject;

	private float playerSpeed = 3f;
    private float moveDirection = -1f;

    private bool playerActionEnabled;
    private int totalNumberOfActions = 0;
    // private int currentActionNumber = 0;
    private List<bool> actionToDo = new List<bool>();
    private List<bool> actionsDone = new List<bool>();
    private List<float> interactiveObjectPositionX = new List<float>();

    private void Awake() {
        playerBody = GetComponent<Rigidbody2D>();
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
            StartCoroutine(ActionFunction(actionNumber));
    }

    private void MovePlayer() {
        playerBody.gameObject.transform.Translate(playerSpeed * moveDirection * Time.deltaTime, 0, 0);
    }

    private IEnumerator ActionFunction(int currentAction) {
        MovePlayer();
        float playerPos = Math.abs(playerBody.gameObject.transform.position.x);
        if(playerPos >= (interactiveObjectPositionX[currentAction] * moveDirection)) {
            playerActionEnabled = true;
            actionToDo[currentAction] = true;
            moveDirection = 0f;
            yield return new WaitForSecondsRealtime(2);
            currentAction++;
            moveDirection = interactiveObjectPositionX[currentAction];
        }
    }

}