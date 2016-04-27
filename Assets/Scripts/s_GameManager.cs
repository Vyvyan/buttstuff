using UnityEngine;
using System.Collections;

public class s_GameManager : MonoBehaviour {

	public s_Player1 player1Script;
	public s_Player1 player2Script;
	public bool roundIsPlaying;
	string stringToDisplayAtRoundEnd;
	bool showStartScreen;
	public GUIStyle goStyle;
	public Light moonLight;
	public float timeUntilNextLightningChance = 0;
	float nextLightningChance = 3;
	public bool lightningHit;
	public AudioClip lightning;

	// Use this for initialization
	void Start () 
	{
		roundIsPlaying = true;
		lightningHit = false;
		StartCoroutine(disableStartScreenAfterAFewSeconds());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player1Script.isAlive == false)
		{
			roundIsPlaying = false;
			stringToDisplayAtRoundEnd = player1Script.reasonForDeath;
		}
		if (player2Script.isAlive == false)
		{
			roundIsPlaying = false;
			stringToDisplayAtRoundEnd = player2Script.reasonForDeath;
		}
		if (player1Script.isAlive == false && player2Script.isAlive == false)
		{
			roundIsPlaying = false;
			stringToDisplayAtRoundEnd = "There were no survivors.";
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (!roundIsPlaying)
			{
				Application.LoadLevel(Application.loadedLevel);	
			}
		}

		if (!roundIsPlaying)
		{
			moonLight.enabled = true;
		}

		if (roundIsPlaying)
		{
			//timer for lightning raffle
			timeUntilNextLightningChance += 1 * Time.deltaTime;
			if (timeUntilNextLightningChance >= nextLightningChance)
			{
				int rnd = Random.Range(1,7);
				if (rnd == 6) 
				{
					lightningHit = true;
					Debug.Log ("LIGHTNING STRUCK!");
					audio.PlayOneShot(lightning);
				}
				timeUntilNextLightningChance = 0;
			}
		
			if (lightningHit)
			{
				moonLight.enabled = true;
				moonLight.intensity -= .2f * Time.deltaTime;
				if (moonLight.intensity <= 0)
				{
					moonLight.intensity = .5f;
					moonLight.enabled = false;
					lightningHit = false;
				}
			}
		}
	}

	IEnumerator disableStartScreenAfterAFewSeconds()
	{
		showStartScreen = true;
		moonLight.enabled = true;
		yield return new WaitForSeconds(2);
		showStartScreen = false;
		moonLight.enabled = false;
	}

	void OnGUI()
	{
		if (!roundIsPlaying)
		{
			GUI.Label(new Rect(Screen.width * .1f, Screen.height * .2f, Screen.width * .8f, Screen.height /2f), 
			          stringToDisplayAtRoundEnd, goStyle);
			GUI.Label(new Rect(Screen.width * .1f, Screen.height * .8f, Screen.width * .8f, Screen.height /2f), 
			          "Press Return to start the next round.", goStyle);
		}
		if (showStartScreen)
		{
			GUI.Label(new Rect(Screen.width * .1f, Screen.height * .45f, Screen.width * .8f, Screen.height /2f), 
			          "Murder Time!", goStyle);
		}
	}

}
