using System.Linq;
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

    private string _punchInput;

    public Collider2D punchCollider;

    public GameObject otherPlayer;

    private void Start()
    {
        _horizontalInput = $"J{playerNumber}Horizontal";
        _jumpInput = $"J{playerNumber}Jump";
        _punchInput = $"J{playerNumber}Punch";
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw(_horizontalInput) * runSpeed;
        _jump = Input.GetButtonDown(_jumpInput);

        if (Input.GetButtonDown(_punchInput))
        {
            Punch();
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }

    private void Punch()
    {
        var colliders = otherPlayer.GetComponents<Collider2D>();
        var touching = colliders.Any(coll => punchCollider.IsTouching(coll));

        if (!touching) return;

        Debug.Log("One PUUUUNCH");
        var otherRigid = otherPlayer.GetComponent<Rigidbody2D>();
        var vec = controller.FacingRight ? Vector2.right : Vector2.left;

        otherRigid.AddForce(new Vector2(2000, 5000));
    }
}