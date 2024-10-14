using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour // Defines a new class named playerMovement. 
                                            // Since it inherits from MonoBehaviour, it can be attached to a GameObject in Unity.
{
    [SerializeField] float boostModifier = 2f; // [SerializeField] attribute allows the variable to be editable from the Unity Inspector
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashSpeed = 10f;

    private Rigidbody2D rb = null;
    private Vector2 inputDirection = Vector2.zero;

    private bool isDashing = false;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // This allows interaction with the physics system of the GameObject.
    }

    void OnMove(InputValue value)
    {
        var movementDir = value.Get<Vector2>(); // (0,1) = Up, (0,-1) = Down, (1,0) = Left, (-1,0) = Right
        Debug.Log(movementDir);

        // rb.AddForce(movementDir * boostModifier, ForceMode2D.Impulse);

        inputDirection = movementDir;
    }

    void OnDash()
    {
        if (canDash) 
        {
            StartCoroutine(Dash());
        }
    }

    private void Update()
    {
        // The Update method is called once per frame.

        if (!isDashing)
        {
            rb.AddForce(inputDirection * boostModifier); // The Update method is called once per frame.
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector2 dashDirection = inputDirection.normalized;
        rb.velocity = dashDirection * dashSpeed;
        Debug.Log("Dashing!");

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        rb.velocity = inputDirection * boostModifier;
        canDash = true; 
    }

    private void OnCollisionEnter2D(Collision2D collsion)
    {
        // This method is called when the GameObject collides with another 2D object.
        Debug.Log("Meowch!");

        if (isDashing)
        {
            isDashing = false;
        }
    }


}
