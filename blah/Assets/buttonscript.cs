using UnityEngine;
using System.Collections;

public class buttonscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision other) {
		if(other.gameObject.tag == "Player"){
			GameObject.Find("floor").SendMessage("buttonswitch");
		}
	}
}
