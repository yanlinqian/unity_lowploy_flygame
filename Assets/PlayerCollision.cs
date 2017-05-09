using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
	public Transform explosionPrefab;
	void OnCollisionEnter (Collision col)
	{
		//Destroy(col.gameObject);
		//print(gameObject.name + " and " + col.collider.name);
		ContactPoint contact = col.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		Instantiate(explosionPrefab, pos, rot);
		//Destroy(gameObject);
	}
	void OnCollisionStay (Collision col){
		
	}
	void OnCollisionExit (Collision col){
	
	}
}