using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class fly_control : MonoBehaviour {

	public Rigidbody NaviBase;
	public Vector3 ThrustDirection;
	public float ThrustForce;
	public bool ShowTrustMockup = true;
	public GameObject ThrustMockup;

	SteamVR_TrackedObject trackedObj;
	FixedJoint joint;
	GameObject attachedObject;
	Vector3 tempVector;

	public float rotationspeed=0.00001f;

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate() {
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		// add force
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
			tempVector = Quaternion.Euler(ThrustDirection) * Vector3.forward;

			NaviBase.AddForce(transform.rotation * tempVector * ThrustForce);
			NaviBase.maxAngularVelocity = 2f;
			//print (NaviBase.transform.position);

			//move rotation
			Vector3 direction=transform.rotation * tempVector;
			NaviBase.MoveRotation (Quaternion.Lerp (NaviBase.rotation, Quaternion.LookRotation (direction), Time.time * rotationspeed));
		}

		// show trust mockup
		if (ShowTrustMockup && ThrustMockup != null) {
			if (attachedObject == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
				attachedObject = (GameObject)GameObject.Instantiate(ThrustMockup, Vector3.zero, Quaternion.identity);
				attachedObject.transform.SetParent(this.transform, false);
				attachedObject.transform.Rotate(ThrustDirection);
			} else if (attachedObject != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
				Destroy(attachedObject);
			}
		}
	}
}
