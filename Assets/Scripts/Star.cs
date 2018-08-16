using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
	[SerializeField]
	string name;

	private void Start()
	{
		if (PlayerPrefs.HasKey(name))
		{
			if (PlayerPrefs.GetInt(name) == 1)
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
			pc.CollectStar(name);
			GetComponent<CapsuleCollider>().enabled = false;
			Destroy(gameObject);
		}
	}
}
