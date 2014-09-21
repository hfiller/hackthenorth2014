using UnityEngine;
using System.Collections;

public class buttonblue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.renderer.material.color = Color.blue;
		rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionStay (Collision other){
		if (other.gameObject.tag == "playerblue") {
			GameObject.Find ("barrierblue").SendMessage ("openGateBlue");
		} 
	}
	
	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == "playerblue") {
			GameObject.Find ("barrierblue").SendMessage ("closeGateBlue");
		} 
	}
}
