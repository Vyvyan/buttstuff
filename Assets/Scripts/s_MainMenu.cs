using UnityEngine;
using System.Collections;

public class s_MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("Game");
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10,10, Screen.width * .8f, Screen.height), "It's Dark! Use the muzzle flash from your gun to figure out where you and your opponent are " +
			"and kill them. One bullet will kill you, and so will falling off of the rooftop - so stay cautious. \n \n \n" +
			"Purple moves forward and backwards with W and S, and turns with A and D. Space Bar shoots and R reloads. \n \n \n" +
			"Green moves forward and backwards with the Up Arrow and Down Arrow, and turns with the Left Arrow and Right Arrow. Left-Click on the mouse shoots and " +
			"Right-Click reloads. \n \n" +
		          "press Return to start the game.");
	}
}
