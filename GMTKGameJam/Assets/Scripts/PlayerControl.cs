using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	private float playerSpeed = 5f;
    private float moveDirection = 0f;
    private Vector2 direction;
    public GameObject playerObject;
    private Rigidbody2D playerBody;

    private void Awake() {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Start() {

    }

    void Update() {
    }

    private void FixedUpdate() {
        moveDirection = Input.GetAxisRaw("Horizontal");  
        if (moveDirection != 0) {
            playerObject.transform.Translate(moveDirection * playerSpeed * Time.deltaTime, 0, 0);
        }
    }
}
