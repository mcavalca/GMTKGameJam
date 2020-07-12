using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{

    public GameObject startMenuInfo;
    public TextMeshProUGUI pointsText;
    public GameObject playerObject;

    void OnEnable(){
        playerObject = GameObject.Find("Player");
    }

    void Awake() {
        print(playerObject.GetComponent<PlayerControl>().playerPoints);
        pointsText.text = playerObject.GetComponent<PlayerControl>().playerPoints.ToString();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)){
            EndGameFunction();
        }
    }

    public void EndGameFunction() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
