using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    public int playerNumber;

    private float _horizontalMove;

    private bool _jump;

    private string _horizontalInput;

    private string _jumpInput;

    private void Start()
    {
        _horizontalInput = $"J{playerNumber}Horizontal";
        _jumpInput = $"J{playerNumber}Jump";
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw(_horizontalInput) * runSpeed;
        _jump = Input.GetButtonDown(_jumpInput);
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }
}