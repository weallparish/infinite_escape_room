using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;

    private InteractionController interaction;

    private bool interacting;

    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space) && interacting)
        {
            interaction.interact();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Interaction")
        {
            interacting = true;

            if (collision.gameObject.GetComponent<InteractionController>() != null)
            {
                interaction= collision.gameObject.GetComponent<InteractionController>();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interacting = false;
    }

}
