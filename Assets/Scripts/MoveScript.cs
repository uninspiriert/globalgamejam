using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Animator animator;

    public float runSpeed = 40f;

    public float strength = 100f;

    public int playerNumber;

    public int jumpForce;

    public Collider2D punchCollider;

    public Player otherPlayer;

    public bool punched;

    public AudioClip drinkSound;

    private AudioSource _audioSource;
    
    private Rigidbody2D _rigidbody2D;

    private bool _facingRight = true;

    private bool _grounded;

    private float _horizontalMove;

    private bool _jump;

    private bool _dab;

    private bool _punch;

    private string _horizontalInput;

    private string _jumpInput;

    private string _dabInput;

    private string _punchInput;

    private string _knowUpInput;

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

        _horizontalInput = $"J{playerNumber}Horizontal";
        _jumpInput = $"J{playerNumber}Jump";
        _punchInput = $"J{playerNumber}Punch";
        _dabInput = $"J{playerNumber}Dab";
        _knowUpInput = $"J{playerNumber}KnockUp";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Collectable")) return;

        Destroy(other.gameObject);
        Debug.Log("REEE");

        if (other.gameObject.name == "Coke of Power")
        {
            _audioSource.PlayOneShot(drinkSound);
            strength += 10;
        }
        else if (other.gameObject.name == "Shoe of Agility")
        {
            runSpeed = 60f;
        }
        else if (other.gameObject.name == "Blob of Jumping")
        {
            jumpForce += 200;
        }
    }

    private void Update()
    {
        if (punched) return;
        _horizontalMove = Input.GetAxisRaw(_horizontalInput) * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (Input.GetButtonDown(_jumpInput))
        {
            _jump = true;
        }

        if (_jump)
        {
            animator.SetBool("Jump", true);
        }
        else if (_grounded)
        {
            animator.SetBool("Jump", false);
        }

        _dab = Input.GetButtonDown(_dabInput);
        if (_dab)
        {
            animator.SetBool("Dab", true);
        }

        if (Input.GetButtonUp(_dabInput))
        {
            animator.SetBool("Dab", false);
        }

        if (Input.GetButtonDown(_punchInput))
        {
            Punch();
            animator.SetTrigger("Punch");
        }

        if (Input.GetButtonDown(_knowUpInput))
        {
            KnockUp();
            animator.SetTrigger("Punch");
        }
    }

    private void FixedUpdate()
    {
        _grounded = Math.Abs(_rigidbody2D.velocity.y) < 0.0001;

        Move();

        if (_jump && _grounded)
        {
            _rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        _jump = false;
    }

    private void Move()
    {
        if (punched) return;

        var velocity = new Vector2(_horizontalMove * Time.fixedDeltaTime * runSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = velocity;
        if (velocity.x > 0 && !_facingRight || velocity.x < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Punch()
    {
        if (otherPlayer == null) return;

        var colliders = otherPlayer.GetComponents<Collider2D>();
        var touching = colliders.Any(coll => punchCollider.IsTouching(coll));

        if (!touching) return;

        var movedScript = otherPlayer.GetComponent<MoveScript>();
        movedScript.punched = true;
        StartCoroutine(movedScript.StopPunchStun(0.5f));

        Debug.Log("One Punch");
        var otherRigid = otherPlayer.GetComponent<Rigidbody2D>();
        var otherPos = otherRigid.transform.position;

        var thisPos = transform.position;
        thisPos.y -= 1;

        var dir = (otherPos - thisPos).normalized;

        Debug.Log(dir * strength);

        otherRigid.AddForce(dir * strength, ForceMode2D.Impulse);
    }

    private void KnockUp()
    {
        if (otherPlayer == null) return;

        var colliders = otherPlayer.GetComponents<Collider2D>();
        var touching = colliders.Any(coll => punchCollider.IsTouching(coll));

        if (!touching) return;

        var movedScript = otherPlayer.GetComponent<MoveScript>();
        movedScript.punched = true;
        StartCoroutine(movedScript.StopPunchStun(1f));

        Debug.Log("One Punch");
        var otherRigid = otherPlayer.GetComponent<Rigidbody2D>();

        var dir = Vector2.up.normalized;

        Debug.Log(dir * strength);

        otherRigid.AddForce(dir * strength, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private IEnumerator StopPunchStun(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        punched = false;
    }
}