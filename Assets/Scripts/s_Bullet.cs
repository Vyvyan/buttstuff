using UnityEngine;
using System.Collections;

public class s_Bullet : MonoBehaviour {

	public AudioClip metal1, metal2;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(killBullet());
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.Translate(Vector3.forward * (100 * Time.deltaTime));
	}

	IEnumerator killBullet()
	{
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			int rnd = Random.Range(1,3);
			if (rnd == 1)
			{
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(metal1);
			}
			if (rnd == 2)
			{
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(metal2);
			}
			Destroy(gameObject);
		}
	}
}
