using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D playerBody;
    public GameObject playerObject;
    public GameObject interactiveObjectsList;
    public GameObject canvasMenu;
    public GameObject menuPrefab;
    public GameObject cameraObject;

    public GameObject pauseMenu;

    public GameObject banheiroBGObject;
    public Sprite banheiroBGSujo;

    private float playerSpeed = 3f;
    private float moveDirection = 0f;
    private float playerPos;
    public int playerPoints = 0;
    private bool facingRight;
    private bool playerIsOnBanheiro;
    private bool continueRunning= true;

    private float portaQuartoPosition = -1f;
    private float portaBanheiroPosition = -49.346f;

    private bool playerActionEnabled;
    private bool isAction;
    private int totalNumberOfActions = 0;
    public int actionNumber;
    private List<bool> actionToDo = new List<bool>();
    private List<bool> actionsDone = new List<bool>();
    private List<float> interactiveObjectPositionX = new List<float>();
    private List<GameObject> interactiveObjectList = new List<GameObject>();
    private List<GameObject> interactiveObjectMenu = new List<GameObject>();
    private GameObject interactiveMenuClone;
    private float interactiveObjectPosition;
    private float threshold = 0.1f;

    private IEnumerator actionCoroutine;

    public float timeToAction = 3f;

    private void Awake() {
        playerBody = GetComponent<Rigidbody2D>();
        playerPos = playerBody.gameObject.transform.position.x;
        playerActionEnabled = false;
        playerIsOnBanheiro = false;
        banheiroBGObject = GameObject.Find("BackgroundBanheiro");
        foreach(Transform currentChild in interactiveObjectsList.transform){
            totalNumberOfActions++;
            actionToDo.Add(false);
            actionsDone.Add(false);
            interactiveObjectPositionX.Add(currentChild.transform.GetComponent<Renderer>().bounds.center.x);
            interactiveObjectList.Add(currentChild.gameObject);
        }
        foreach(Transform currentChild in canvasMenu.transform) {
            interactiveObjectMenu.Add(currentChild.gameObject);
        }
        interactiveObjectPositionX.Add(19.8f);
        moveDirection = CalculateMoveDirection(playerPos, interactiveObjectPositionX[0]);
        actionNumber = 0;
        interactiveObjectPosition = interactiveObjectPositionX[0];
        isAction = true;
        facingRight = false;
        flipCharacter(moveDirection);
    }

    void Start() {
    }

    void Update() {
        if (continueRunning) {
            if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.GetComponent<BackToGame>().activatePauseMenu){
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
        }
    }

    private void FixedUpdate() {
        if (continueRunning) {
            playerPos = playerBody.gameObject.transform.position.x;
            MovePlayer();
            MoveCamera();
            playerObject.GetComponent<Animator>().SetBool("isWalking", checkPlayerMoving(moveDirection));
            if((playerPos >= interactiveObjectPosition - threshold) && (playerPos <= interactiveObjectPosition + threshold) && (isAction)) {
                if(actionNumber == 1 || actionNumber == 2) interactiveObjectList[2].GetComponent<Animator>().SetBool("chuveiroLigado", true);
                if(totalNumberOfActions != actionNumber) {
                    isAction = false;
                    actionCoroutine = ActionFunction(actionNumber);
                    StartCoroutine(actionCoroutine);
                } else {
                    StartCoroutine(StartEndGame());
                }
            }
        }
    }

    private void MovePlayer() {
        playerBody.gameObject.transform.Translate(playerSpeed * moveDirection * Time.deltaTime, 0, 0);
    }

    private void MoveCamera() {
        cameraObject.transform.position = new Vector3(playerPos, 0, -10);
    }

    public void StopActionCoroutine() {
        StopCoroutine(actionCoroutine);
        if(actionNumber == 4) banheiroBGObject.GetComponent<SpriteRenderer>().sprite = banheiroBGSujo;
    }

    public void MoveToNextAction() {
        interactiveObjectMenu[actionNumber].SetActive(false);
        playerActionEnabled = false;
        actionNumber++;
        interactiveObjectPosition = interactiveObjectPositionX[actionNumber];
        moveDirection = CalculateMoveDirection(playerPos, interactiveObjectPosition);
        isAction = true;

    }

    private IEnumerator ActionFunction(int currentAction) {

        if (interactiveObjectList[currentAction].GetComponent<InteractivePoints>().returnPorta()){
            if (playerIsOnBanheiro) {
                playerObject.transform.position = new Vector3(portaQuartoPosition, -3.5f, 0f);
            } else {
                playerObject.transform.position = new Vector3(portaBanheiroPosition, -3.5f, 0f);
            }
            playerIsOnBanheiro = !playerIsOnBanheiro;
            yield return new WaitForSeconds(0.1f);
        } else {
            playerActionEnabled = true;
            actionToDo[currentAction] = true;
            interactiveObjectMenu[currentAction].SetActive(true);
            moveDirection = 0f;
            yield return new WaitForSeconds(timeToAction);
            if(actionNumber == 3) interactiveObjectList[2].GetComponent<Animator>().SetBool("chuveiroLigado", false);
        }
        MoveToNextAction();

    }

    public void PlayerInteracted() {
        actionsDone[actionNumber] = true;
    }

    public void AddPoint(int pointValue) {
        playerPoints += pointValue;
    }

    public bool IsPlayerAvailable() {
        return this.playerActionEnabled;
    }

    private void flipCharacter(float m) {
        if ((m >= 0 && !facingRight) || (m <= 0 && facingRight)) {
            facingRight = !facingRight;
            Vector3 theScale = playerObject.transform.localScale;
            theScale.x *= -1;
            playerObject.transform.localScale = theScale;
        }
    }

    private float CalculateMoveDirection(float currentPos, float nextPos) {
        float nextMoveDirection;
        float distance = (nextPos - currentPos);
        nextMoveDirection = distance/Math.Abs(distance);
        flipCharacter(nextMoveDirection);
        return nextMoveDirection;
    }

    private bool checkPlayerMoving(float movementValue) {
        if (movementValue != 0f) return true;
        return false;
    }

    public IEnumerator StartEndGame() {
        moveDirection = 0f;
        yield return new WaitForSeconds(1f);
        if (continueRunning) {
            continueRunning = false;
            DontDestroyOnLoad(this.gameObject.transform);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}