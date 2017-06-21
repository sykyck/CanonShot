using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static int score;
	public static int strikesAllowed;
	public static int coinsCollected;
	public static int strikes;
	public static float totalPauseTime;
	public GameObject scoreText_GameObj;
	public GameObject coinsText_GameObj;
	public GameObject BlackHole_GameObj;
	public GameObject Canon_GameObj;
	bool pause;
	float startPauseTime,endPauseTime;
	UnityEngine.UI.Text scoreText,coinsText; 
	// Use this for initialization
	void Start () 
	{
		pause = false;
		score = 0;
		strikes = 0;
		strikesAllowed = 3;
		coinsCollected = 0;
		scoreText = scoreText_GameObj.GetComponent<UnityEngine.UI.Text> ();
		coinsText = coinsText_GameObj.GetComponent<UnityEngine.UI.Text> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		scoreText.text = System.Convert.ToString(score);
		coinsText.text = System.Convert.ToString(coinsCollected);
		if ((!pause)&&(startPauseTime!=0))
		{
			GameManager.totalPauseTime += endPauseTime - startPauseTime;
			endPauseTime = 0f;
			startPauseTime = 0f;
		}

	}
	public void GainExtraLife()
	{
		if (coinsCollected > 60) 
		{
			coinsCollected = coinsCollected - 60;
			strikesAllowed = strikesAllowed + 1;
		}
	}
	public void PauseGame()
	{
		if (!pause) {
			BlackHole_GameObj.GetComponent<TouchManager>().enabled=false;
			Canon_GameObj.GetComponent<CanonBall>().enabled=false;
			Canon_GameObj.GetComponent<CanonRotatiion>().enabled=false;
			pause = true;
			startPauseTime = Time.time;
		}
		else {
			BlackHole_GameObj.GetComponent<TouchManager>().enabled=true;
			Canon_GameObj.GetComponent<CanonBall>().enabled=true;
			Canon_GameObj.GetComponent<CanonRotatiion>().enabled=true;
			pause = false;
			endPauseTime = Time.time;
		}

	}
}
