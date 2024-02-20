using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BasicMovement playerActions;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerActions = new BasicMovement();
        playerActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerActions.Movement.Up.performed += Up;
        playerActions.Movement.Down.performed += Down;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Up(InputAction.CallbackContext context)
    {
        transform.Translate(Vector2.up);
    }

    private void Down(InputAction.CallbackContext context)
    {
        transform.Translate(Vector2.down);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void Damage()
    {
        //
    }
}