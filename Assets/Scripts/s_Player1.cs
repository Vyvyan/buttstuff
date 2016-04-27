using UnityEngine;
using System.Collections;

public class s_Player1 : MonoBehaviour {

	public GameObject bullet;
	public Light muzzleFlashLight;
	public GameObject firePoint;
	public bool isPlayer1;
	public bool isAlive;
	public GameObject blood;
	public Vector3 deadLocation = new Vector3(0, -500, 0);
	public string reasonForDeath;
	public string playerName;
	bool canFire;
	float fireRate = 0.45f;
	float fireRateCounter;
	int bulletsInClip;
	bool isReloading;
	public AudioClip death, gunshot, gunreloading, cheer;

	// Use this for initialization
	void Start () 
	{
		canFire = true;
		isReloading = false;
		bulletsInClip = 6;
		if (gameObject.name == "Player1")
		{
			isPlayer1 = true;
			playerName = "Purple Dood";
		}
		if (gameObject.name == "Player2")
		{
			isPlayer1 = false;
			playerName = "Green Dood";
		}
		isAlive = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//is player 1
		if (isPlayer1)
		{
			if (Input.GetKey(KeyCode.W))
			{
				gameObject.transform.Translate(Vector3.forward * (25 * Time.deltaTime));
			}
			if (Input.GetKey(KeyCode.S))
			{
				gameObject.transform.Translate(Vector3.back * (10 * Time.deltaTime));
			}
			if (Input.GetKey(KeyCode.A))
			{
				gameObject.transform.Rotate(Vector3.down * (125 * Time.deltaTime));
			}
			if (Input.GetKey(KeyCode.D))
			{
				gameObject.transform.Rotate(Vector3.up * (125 * Time.deltaTime));
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				if (!isReloading)
				{
					Debug.Log("player 1 is reloading");
					StartCoroutine(ReloadGun());
				}
			}

			// firing
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (bulletsInClip > 0)
				{
					if (canFire)
					{
						Instantiate(bullet, firePoint.gameObject.transform.position, gameObject.transform.rotation);
						StartCoroutine(MuzzleFlash());
						GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(gunshot);
						canFire = false;
						fireRateCounter = 0;
						bulletsInClip--;
					}
				}
			}

		}

		// is player 2
		if (!isPlayer1)
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				gameObject.transform.Translate(Vector3.forward * (25 * Time.deltaTime));
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				gameObject.transform.Translate(Vector3.back * (10 * Time.deltaTime));
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				gameObject.transform.Rotate(Vector3.down * (125 * Time.deltaTime));
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				gameObject.transform.Rotate(Vector3.up * (125 * Time.deltaTime));
			}

			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				if (!isReloading)
				{
					Debug.Log("player 2 is reloading");
					StartCoroutine(ReloadGun());
				}
			}

			// firing
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				if (bulletsInClip > 0)
				{
					if (canFire)
					{
						Instantiate(bullet, firePoint.gameObject.transform.position, gameObject.transform.rotation);
						StartCoroutine(MuzzleFlash());
						GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(gunshot);
						canFire = false;
						fireRateCounter = 0;
						bulletsInClip--;
					}
				}
			}
		}

		if (!isAlive)
		{
			gameObject.transform.position = deadLocation;
		}

		if (!canFire)
		{
			fireRateCounter += 1 * Time.deltaTime;
			if (fireRateCounter >= fireRate)
			{
				canFire = true;
			}
		}

		if (isReloading)
		{
			canFire = false;
		}
	}

	IEnumerator MuzzleFlash()
	{
		muzzleFlashLight.enabled = true;
		yield return new WaitForSeconds(.1f);
		muzzleFlashLight.enabled = false;
	}

	IEnumerator ReloadGun()
	{
		isReloading = true;
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(gunreloading);
		yield return new WaitForSeconds (3);
		bulletsInClip = 9;
		isReloading = false;
		canFire = true;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			isAlive = false;
			reasonForDeath = playerName + " was shot in the face.";
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(death);
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(cheer);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
			Instantiate(blood, gameObject.transform.position, Quaternion.identity);
		}
		if (other.gameObject.tag == "DeathGround")
		{
			isAlive = false;
			reasonForDeath = playerName + " fell off the roof.";
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(death);
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(cheer);
		}
	}
}
