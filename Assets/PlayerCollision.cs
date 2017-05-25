using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
	public Transform explosionPrefab;
	public AudioClip audioclip;

	void Start(){
		GetComponent<AudioSource> ().playOnAwake = false;
		GetComponent<AudioSource> ().clip = audioclip;
	}
	void OnCollisionEnter (Collision col)
	{
		//Destroy(col.gameObject);
		//print(gameObject.name + " and " + col.collider.name);
		ContactPoint contact = col.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		Instantiate(explosionPrefab, pos, rot);
		//Destroy(gameObject);

		GetComponent<AudioSource> ().PlayOneShot(audioclip,0.3f);
	}
	void OnCollisionStay (Collision col){
		
	}
	void OnCollisionExit (Collision col){
	
	}
}