using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
			pc.CollectStar();
			GetComponent<CapsuleCollider>().enabled = false;
			//Play some particles
			Destroy(gameObject);
		}
	}
}
