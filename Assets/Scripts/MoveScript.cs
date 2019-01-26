using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    public int playerNumber;

    private float _horizontalMove;
    
    private bool _jump;

    private string _horizontal;

    private void Start()
    {
        _horizontal = "Horizontal" + playerNumber;
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw(_horizontal) * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            _jump = true;
        }  
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }
}

