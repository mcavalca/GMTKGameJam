using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToGame : MonoBehaviour
{
    public bool activatePauseMenu = false;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && activatePauseMenu){
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
        }
    }
    
    public void BackToMainMenu () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
