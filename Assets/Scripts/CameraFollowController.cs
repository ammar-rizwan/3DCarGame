using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour {

	
	public void LookAtTarget()
	{
		Vector3 _lookDirection = objectToFollow.position - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
		// transform.position = Vector3.Lerp(transform.position, _lookDirection, followSpeed * Time.deltaTime);

	}

	public void MoveToTarget()
	{
		Vector3 _targetPos = objectToFollow.position + 
							 objectToFollow.forward * offset.z + 
							 objectToFollow.right * offset.x + 
							 objectToFollow.up * offset.y;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
		
	}

	void TOP(){

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(30f, 0f, 0f), Time.deltaTime * 2f);

		Vector3 targetPosition = new Vector3(0f, objectToFollow.position.y, objectToFollow.position.z);
		targetPosition -= transform.rotation * Vector3.forward * 5f;
		targetPosition = new Vector3(objectToFollow.position.x, 8f, objectToFollow.position.z);
		// transform.position = SmoothApproach( pastFollowerPosition, pastTargetPosition, targetPosition, (speed / 10f) * Mathf.Clamp(Time.timeSinceLevelLoad - 1.5f, 0f, .85f) );
		// targetFieldOfView = topFOV;
		transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);


	}

	private void FixedUpdate()
	{
		LookAtTarget();
		MoveToTarget();
		// TOP();
        transform.LookAt(objectToFollow);

	}



	public Transform objectToFollow;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;
}