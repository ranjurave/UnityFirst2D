using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;
    [SerializeField]
    float playerSpeed = 4000;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); // getting referece of the component
        playerAnimator = GetComponent<Animator>(); // getting referece of the component
    }

    void Update()
    {
        // flipping the character to face the direction it moves
        bool playerHorizontalMove = Mathf.Abs(playerRB.velocity.y) > 0;
        if (playerHorizontalMove) {
            transform.localScale = new Vector3(Mathf.Sign(playerRB.velocity.x)*-1, 1, 1);
        }

        // Getting input and moving the character
        float controlThrow = Input.GetAxis("Horizontal") * Time.deltaTime;
        Vector2 playerVelocity = new Vector2(controlThrow * playerSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;

        // Changing the animator controller variable to play different animations
        playerAnimator.SetBool("CanWalk", playerHorizontalMove);

    }
}
