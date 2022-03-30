using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;
    Collider2D playerCollider;

    float playerSpeed = 4000;
    float JumpSpeed = 5;

    [SerializeField]
    GameObject Projectile;
    Transform ProjectileStartPoint;

    [HideInInspector]
    public int CoinsCollected = 0; // public to access it from the HUDScript class

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); // getting referece of the component
        playerAnimator = GetComponent<Animator>(); // getting referece of the component
        playerCollider = GetComponent<Collider2D>(); // getting referece of the component
        ProjectileStartPoint = GetComponentInChildren<GameObject>().transform;
    }

    void Update() {
        bool playerHorizontalMove = PlayerMovement();
        AnimationChange(playerHorizontalMove);
        Jump();

        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("Fire Fire....");
        }

    }

    private void AnimationChange(bool playerHorizontalMove) {
        // Changing the animator controller variable to play different animations
        playerAnimator.SetBool("CanWalk", playerHorizontalMove);
    }

    private bool PlayerMovement() {
        // flipping the character to face the direction it moves
        bool playerHorizontalMove = Mathf.Abs(playerRB.velocity.y) > 0;
        if (playerHorizontalMove) {
            transform.localScale = new Vector3(Mathf.Sign(playerRB.velocity.x) * -1, 1, 1);
        }

        // Getting input and moving the character
        float controlThrow = Input.GetAxis("Horizontal") * Time.deltaTime;
        Vector2 playerVelocity = new Vector2(controlThrow * playerSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;
        return playerHorizontalMove;
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
            Debug.Log(CoinsCollected);

        }
    }
}
