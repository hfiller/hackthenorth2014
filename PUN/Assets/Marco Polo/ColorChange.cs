using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.SetColor ("_Color", Color.red);
		renderer.material.SetColor ("_SpecColor", Color.red);
		renderer.material.SetColor ("_Emission", Color.red);
		renderer.material.SetColor ("_ReflectColor", Color.red);

		Debug.Log ("color changed");
	}
}
