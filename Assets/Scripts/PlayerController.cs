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

	// Use this for initialization
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		collider = GetComponent<CapsuleCollider>();
	}

	// Update is called once per frame
	void Update()
	{
		Physics.gravity = gravityWell.up * -9.8f;
#if UNITY_EDITOR
		velocity = (Input.GetAxisRaw("Horizontal") * gravityWell.right) + (Input.GetAxisRaw("Vertical") * gravityWell.forward);
#else
		velocity = (Input.GetAxis("Horizontal") * gravityWell.right) + (Input.GetAxis("Vertical") *  gravityWell.forward);
#endif
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

	private void Jump()
	{
		rigidbody.AddForce(gravityWell.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.transform.tag == "Ground")
		{
			canJump = true;
			Ground gc = collider.gameObject.GetComponent<Ground>();
			if (gc != null)
			{
				gravityWell = gc.GetGravityWell();
			}
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.transform.tag == "Ground")
		{
			canJump = false;
		}
	}

	private void RotateGravityWell()
	{
		//gravityWell.transform.LookAt(transform.position + transform.TransformDirection(localPos));
		gravityWell.right = mainCamera.transform.right;
	}
}
