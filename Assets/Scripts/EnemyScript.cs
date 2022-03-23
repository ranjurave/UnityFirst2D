using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D EnemyRB;
    float MoveSpeed = 2.0f;
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsFacingRight()) {
            EnemyRB.velocity = new Vector2(-MoveSpeed, 0);
        }
        else 
        {
            EnemyRB.velocity = new Vector2(MoveSpeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        transform.localScale = new Vector3(Mathf.Sign(EnemyRB.velocity.x), 1, 1);
    }

    bool IsFacingRight() {
        return transform.localScale.x > 0;
    }
}
