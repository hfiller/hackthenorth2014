using UnityEngine;
using System.Collections;

public class animator : MonoBehaviour {
	Animator anim;
	int jumpHash = Animator.StringToHash("hash");
	int runStateHash = Animator.StringToHash("Base Layer.Jump_001");
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {		
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if(Input.GetKeyDown(KeyCode.Space) && stateInfo.nameHash == runStateHash)
		{
			Debug.Log("test");
		}
	}
}
