using UnityEngine;
using System.Collections;

public class s_Blood : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		gameObject.rigidbody.AddForce(Random.Range(-5,5),Random.Range(-5,5),Random.Range(-5,5),ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
