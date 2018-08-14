using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField]
    private float movespeed = 5;
    [SerializeField]
    private float jumpHeight = 10;
    [SerializeField]
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private LayerMask jumpableMask;
    [SerializeField]
    private CapsuleCollider collider;
    [SerializeField]
    private Transform gravityWell;

    private bool canJump = false;
    private Vector3 fakeGravity;

    [SerializeField]
    private Camera mainCamera;
	[SerializeField]
	private Animator playerAnimator;

	[SerializeField]
	private ToggleStickBehaviour toggleStick;
	[SerializeField]
	private Transform startPosition;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
		playerAnimator = GetComponent<Animator>();
		ResetCharacter();
    }

    // Update is called once per frame
    void Update()
    {
		if (CheckIfGrounded())
		{
			canJump = true;
			playerAnimator.SetBool("IsJumping", false);
		}
		else
		{
			canJump = false;
			playerAnimator.SetBool("IsJumping", true);
		}
        Physics.gravity = gravityWell.up * -9.8f;

        velocity = (toggleStick.Horizontal() * gravityWell.right) + (toggleStick.Vertical() * gravityWell.forward);
		if (velocity != Vector3.zero)
		{
			playerAnimator.SetBool("IsWalking", true);
		}
		else
		{
			playerAnimator.SetBool("IsWalking", false);
		}
		transform.LookAt(transform.position + velocity);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
            canJump = false;
        }
        RotateGravityWell();
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + (velocity.normalized * movespeed * Time.deltaTime));
    }

    public void Jump()
    {
        if (canJump)
        {
            rigidbody.AddForce(gravityWell.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            canJump = false;
        }
    }

	private bool CheckIfGrounded()
	{
		float radius = collider.radius * 0.9f;
		Vector3 pos = (transform.position - (Vector3.up * (collider.height / 2)) + Vector3.up * (radius * 0.9f));
		Collider[] colliders = Physics.OverlapSphere(pos, radius, jumpableMask);
		if (colliders.Length > 0)
		{
			Ground gc = colliders[0].gameObject.GetComponent<Ground>();
			if (gc != null)
			{
				gravityWell = gc.GetGravityWell();
				transform.SetParent(gc.transform);
			}
			return (true);
		}
		return (false);
	}

    private void RotateGravityWell()
    {
		Vector3 currentUp = gravityWell.up;
        //gravityWell.transform.LookAt(transform.position + transform.TransformDirection(localPos));
        gravityWell.right = mainCamera.transform.right;
		gravityWell.up = currentUp;
    }

	public void ResetCharacter()
	{
		transform.position = startPosition.position;
		transform.SetParent(startPosition);
	}

	public void CollectStar(string name)
	{
		GameManager.instance.AddStar(name);
	}
}
