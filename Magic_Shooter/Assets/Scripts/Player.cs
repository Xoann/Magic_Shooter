using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    private Rigidbody2D rb;
    [SerializeField] Transform playerTransform;
    [SerializeField] Camera mainCamera;

    private Vector2 movement;
    private Vector2 mousePos;
    private Vector2 dashVelocity;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 20f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;


    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void OnMousePos(InputValue value)
    {
        Vector2 screenPos = value.Get<Vector2>();
        mousePos = mainCamera.ScreenToWorldPoint(screenPos);
    }

    private void OnDash()
    {
        dashVelocity = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        dashVelocity.Normalize();
        Debug.Log(dashVelocity);
        dashVelocity *= dashingPower;
        StartCoroutine(Dash());
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //if (!isDashing)
        //{
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);
        //}

        if (canDash)
        {
            rb.AddForce(dashVelocity);
            canDash = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        //isDashing = true;

        //yield return new WaitForSeconds(dashingTime);
        //isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
