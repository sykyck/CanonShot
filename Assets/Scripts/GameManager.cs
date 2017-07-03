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
	public GameObject Strikes;
	public GameObject MainStatus;
	public GameObject GameEndPanel;
	public GameObject Lives;
	public GameObject AliveImageReference;
	public GameObject DeadImageReference;
	public GameObject scoreText_GameObj;
	public GameObject coinsText_GameObj;
	public GameObject LowerPanel;
	UnityEngine.UI.Text scoreText,coinsText; 
	// Use this for initialization
	void Start () 
	{
		pause = false;
		score = 0;
		strikes = 0;
		strikesAllowed = 3;
		coinsCollected = 60;
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
			if (GameEndPanel.activeInHierarchy)
			{
				coinsCollected = coinsCollected - 60;
				strikesAllowed = strikesAllowed + 1;
				StartGameWithExtraLife ();
			}
			else 
			{
				coinsCollected = coinsCollected - 60;
				strikesAllowed = strikesAllowed + 1;
				GameManager.StartOrPauseGame ();
				StartGameWithExtraLife ();
			}
		} 
		else 
		{
			if (GameEndPanel.activeInHierarchy) 
			{
				GameStatus.GetComponent<UnityEngine.UI.Text> ().text = "Coins are less than 60";
			} 
			else 
			{
				MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Coins are less than 60";
			}
		}
	}

	public void OnWatcHAdvertisementClick()
	{
		strikesAllowed = strikesAllowed + 1;
		if (GameEndPanel.activeInHierarchy)
		{
			StartGameWithExtraLife ();
		} 
		else 
		{
			GameManager.StartOrPauseGame ();
			StartGameWithExtraLife ();
		}
	}
    void StartGameWithExtraLife()
	{
		LowerPanel.SetActive (true);
		if (GameEndPanel.activeInHierarchy) 
		{
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Extra Life Awarded";
			for (int j = 0; j < Strikes.transform.childCount; j++) 
			{
				Strikes.transform.GetChild (j).gameObject.SetActive (true);
			}
			for (int j = 0; j < Lives.transform.childCount; j++)
			{
				if (!Lives.transform.GetChild (j).gameObject.activeInHierarchy) 
				{
					Lives.transform.GetChild (j).gameObject.SetActive (true);
					Lives.transform.GetChild (j).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImageReference.GetComponent<UnityEngine.UI.Image> ().sprite;
					break;
				}
				if (j == (Lives.transform.childCount-1))
				{
					for (int k = 0; k < Lives.transform.childCount; k++) 
					{
						Lives.transform.GetChild (k).gameObject.SetActive (false);
					}
					Lives.transform.GetChild (0).gameObject.SetActive (true);
					Lives.transform.GetChild (0).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImageReference.GetComponent<UnityEngine.UI.Image> ().sprite;
				}
			}
			GameEndPanel.SetActive (false);
			GameManager.StartOrPauseGame ();
		} 
		else
		{
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Extra Life Awarded";
			for (int j = 0; j < Lives.transform.childCount; j++)
			{
				if (!Lives.transform.GetChild (j).gameObject.activeInHierarchy) 
				{
					Lives.transform.GetChild (j).gameObject.SetActive (true);
					Lives.transform.GetChild (j).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImageReference.GetComponent<UnityEngine.UI.Image> ().sprite;
					break;
				}
				if (j == (Lives.transform.childCount-1))
				{
					for (int k = 0; k < Lives.transform.childCount; k++) 
					{
						Lives.transform.GetChild (k).gameObject.SetActive (false);
					}
					Lives.transform.GetChild (0).gameObject.SetActive (true);
					Lives.transform.GetChild (0).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImageReference.GetComponent<UnityEngine.UI.Image> ().sprite;
				}
			}
			GameManager.StartOrPauseGame ();
		}
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

	public void OnPauseButtonClick()
	{
		StartOrPauseGame ();
	}
}
