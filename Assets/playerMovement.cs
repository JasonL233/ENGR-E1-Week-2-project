using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float boostModifier = 2f;
    Rigidbody2D rb = null;
    Vector2 inputDirection = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        var movementDir = value.Get<Vector2>();
        Debug.Log(movementDir);

        // rb.AddForce(movementDir * boostModifier, ForceMode2D.Impulse);
        
        inputDirection = movementDir;
    }

    private void Update()
    {
        rb.AddForce(inputDirection * boostModifier);
    }

    private void OnCollisionEnter2D(Collision2D collsion)
    {
        Debug.Log("Meowch!");
    }


}
