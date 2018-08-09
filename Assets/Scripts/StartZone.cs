using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class StartZone : MonoBehaviour, ITrackableEventHandler
{
	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		Debug.Log("I am being tracked");
		//if (newStatus == TrackableBehaviour.Status.TRACKED)
		//{
		//	rigidbody.useGravity = true;
		//}
		//else
		//{
		//	rigidbody.useGravity = false;
		//}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
