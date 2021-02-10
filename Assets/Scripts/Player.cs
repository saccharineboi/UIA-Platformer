using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
        public float speed;
        public float jumpForce;
        public float maxRayDistance;

        Rigidbody2D rigidBody2D;
        BoxCollider2D boxCollider2D;
        Animator animator;

        void Start()
        {
                rigidBody2D = GetComponent<Rigidbody2D>();
                boxCollider2D = GetComponent<BoxCollider2D>();
                animator = GetComponent<Animator>();
        }

        void Update()
        {
                Move();
                Jump();
        }

        void Move()
        {
                float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                Vector2 movement = new Vector2(horizontal, rigidBody2D.velocity.y);
                rigidBody2D.velocity = movement;

                animator.SetFloat("speed", Mathf.Abs(horizontal));
                if (!Mathf.Approximately(horizontal, 0.0f))
                        transform.localScale = new Vector3(Mathf.Sign(horizontal), transform.localScale.y, transform.localScale.z);
        }

        void Jump()
        {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                        Vector2 origin = new Vector2(transform.position.x, transform.position.y - boxCollider2D.size.y);
                        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, maxRayDistance);
                        if (hit.collider != null)
                        {
                                rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        }
                }
        }
}
