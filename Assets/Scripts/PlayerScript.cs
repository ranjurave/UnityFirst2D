using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    Rigidbody2D playerRB;
    Animator playerAnimator;
    Collider2D playerCollider;

    [SerializeField] GameObject Projectile;
    [SerializeField] Transform ProjectileStartPoint;

    [HideInInspector] public int CoinsCollected = 0; // public to access it from the HUDScript class

    float PlayerSpeed = 4000;
    float JumpSpeed = 5;
    int Life = 2;
    bool IsAlive = true;

    void Start() {
        playerRB = GetComponent<Rigidbody2D>(); // getting referece of the component
        playerAnimator = GetComponent<Animator>(); // getting referece of the component
        playerCollider = GetComponent<Collider2D>(); // getting referece of the component
    }

    void Update() {
        if (IsAlive) {
            PlayerMovement();
            Jump();
            Fire();
        }
    }

    private void PlayerMovement() {
        // Getting input and moving the character
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * PlayerSpeed * Time.deltaTime, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;

        // Rotating character instead of scaling for projectile to work fine.
        if (Input.GetAxis("Horizontal") < 0) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetAxis("Horizontal") > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        bool playerHorizontalMove = Mathf.Abs(playerRB.velocity.y) > 0;

        if (playerHorizontalMove) {
            AnimationChange(playerHorizontalMove);
        }
    }

    private void AnimationChange(bool playerHorizontalMove) {
        // Changing the animator controller variable to play different animations
        playerAnimator.SetBool("CanWalk", playerHorizontalMove);
    }

    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(Projectile, ProjectileStartPoint.position, ProjectileStartPoint.rotation);
        }
    }

    private void Jump() {
        if (Input.GetButtonDown("Jump")) {
            bool IsTouchingGround = playerCollider.IsTouchingLayers(LayerMask.GetMask("Foreground"));
            if (IsTouchingGround) {
                Vector2 JumpVelocity = new Vector2(0, JumpSpeed);
                playerRB.velocity += JumpVelocity;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // If collected coin
        if (collision.gameObject.CompareTag("Coin")) {
            CoinsCollected++;
            Destroy(collision.gameObject);
            //Debug.Log(CoinsCollected);
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            Life--;
            Destroy(collision.gameObject);
            if (Life < 0) {
                //Debug.Log("DEAD.....");
                IsAlive = false;
                playerAnimator.SetTrigger("DeathTrigger");

                // If dead, then we can destroy or simply hide the player
                //gameObject.SetActive(false); // Hide player
                Destroy(gameObject, 2); // Destroy the player game object
            }
        }
    }
}
