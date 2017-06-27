using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static int score;
	public static int strikesAllowed;
	public static int coinsCollected;
	public static int strikes;
	public static float totalPauseTime;
	public static GameObject BlackHole_GameObj;
	public static GameObject Canon_GameObj;
    public static bool pause;
	public static float startPauseTime,endPauseTime;
	public GameObject GameStatus;
	public GameObject MainStatus;
	public GameObject GameEndPanel;
	public GameObject scoreText_GameObj;
	public GameObject coinsText_GameObj;
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
		if (coinsCollected >= 60) 
		{
			coinsCollected = coinsCollected - 60;
			strikesAllowed = strikesAllowed + 1;
		} 
		else 
		{
			GameStatus.GetComponent<UnityEngine.UI.Text> ().text = "Coins are less than 60";
		}
		StartGameWithExtraLife();
	}

	void StartGameWithExtraLife()
	{
		GameManager.score = 0;
		GameManager.strikes = 0;
		MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Shoot Speed Decreased";
		GameEndPanel.SetActive (false);
		GameManager.StartOrPauseGame ();
	}	
	public static void StartOrPauseGame()
	{
		if (!pause)
		{
			BlackHole_GameObj.GetComponent<TouchManager>().enabled=false;
			Canon_GameObj.GetComponent<CanonBall> ().enabled = false;
			Canon_GameObj.GetComponent<CanonRotatiion> ().enabled = false;
			Canon_GameObj.GetComponent<Rigidbody2D> ().Sleep ();
			pause = true;
			startPauseTime = Time.time;
			Time.timeScale = 0;
		}
		else 
		{
			BlackHole_GameObj.GetComponent<TouchManager>().enabled=true;
			Canon_GameObj.GetComponent<CanonBall>().enabled=true;
			Canon_GameObj.GetComponent<CanonRotatiion>().enabled=true;
			pause = false;
			endPauseTime = Time.time;
			Time.timeScale = 1;
		}
	}

}
