using UnityEngine;
using System.Collections;

public class buttonred : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.renderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionStay (Collision other){
		if (other.gameObject.tag == "playerred") {
			GameObject.Find ("barriered").SendMessage ("openGate");
		} 
	}

	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == "playerred") {
			GameObject.Find ("barriered").SendMessage ("closeGate");
		} 
	}
}
