using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    Rigidbody2D ProjectileRB;
    [SerializeField]
    float ProjectileSpeed = 25.0f;
    void Start()
    {
        ProjectileRB = GetComponent<Rigidbody2D>();

        ProjectileRB.velocity = transform.right * ProjectileSpeed;
    }

    //private void Update() {
    //    Destroy(gameObject, 1);
    //}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
