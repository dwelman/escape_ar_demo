using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Ground : MonoBehaviour
{
	[SerializeField]
	private Transform gravityWell;

	void Start()
	{
		
	}

	public Transform GetGravityWell()
	{
		return (gravityWell);
	}
}
