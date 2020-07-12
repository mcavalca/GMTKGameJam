using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public GameObject startMenuInfo;
    public GameObject pauseMenu;

    void Awake() {
        Time.timeScale = 0;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            StartGameFunction();
        }
    }

    public void StartGameFunction() {
        Time.timeScale = 1;
        startMenuInfo.SetActive(false);
        pauseMenu.GetComponent<BackToGame>().activatePauseMenu = true;
    }

}
