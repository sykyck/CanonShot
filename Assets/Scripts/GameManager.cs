using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	public static int score;
	public static int strikesAllowed;
	public static int maxstrikesAllowed;
	public static int coinsCollected;
	public static int strikes;
	public static float totalPauseTime;
	public static long leaderBoardHighScore;
	public static GameObject BlackHole_GameObj;
	public static GameObject Canon_GameObj;
    public static bool pause;
	public static float startPauseTime,endPauseTime;
	public GameObject GameStatus;
	public GameObject PurchasePanel;
	public GameObject Strikes;
	public GameObject MainStatus;
	public GameObject GameEndPanel;
	public GameObject GameResumePanel;
	public GameObject Lives;
	public GameObject Pause;
	public Sprite AliveImage;
	public Sprite DeadImage;
	public GameObject scoreText_GameObj;
	public GameObject coinsText_GameObj;
	public GameObject LowerPanel;
	UnityEngine.UI.Text scoreText,coinsText; 
	int coinsPurchaseOption;
	// Use this for initialization
	void Start () 
	{
		pause = false;
		score = 0;
		strikes = 0;
		strikesAllowed = 3;
		maxstrikesAllowed = 5;
		coinsCollected = 60;
		coinsPurchaseOption = 0;
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
		if ((PurchasePanel.activeInHierarchy) && (IAPHandler.GetInstance ().IsIAPInitialised)) {
			if (coinsPurchaseOption == 1) {
				IAPHandler.GetInstance ().controller.InitiatePurchase (IAPConstants.COINS_100);
			}
			if (coinsPurchaseOption == 2) {
				IAPHandler.GetInstance ().controller.InitiatePurchase (IAPConstants.COINS_200);
			}
			if (coinsPurchaseOption == 3) {
				IAPHandler.GetInstance ().controller.InitiatePurchase (IAPConstants.COINS_500);
			}
		}
		if((IAPHandler.GetInstance ().IsIAPInitialised)&&(IAPHandler.GetInstance ().IsIAPCompleted))
		{
			if (coinsPurchaseOption == 1) {
				coinsCollected += 100;
			}
			if (coinsPurchaseOption == 2) {
				coinsCollected += 200;
			}
			if (coinsPurchaseOption == 3) {
				coinsCollected += 500;
			}
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Purchase Completed";
			coinsPurchaseOption = 0;
			IAPHandler.GetInstance ().IsIAPCompleted = false;
			PurchasePanel.SetActive (false);
			GameResumePanel.SetActive (true);
		}
		if((IAPHandler.GetInstance ().IsIAPInitialised)&&(IAPHandler.GetInstance ().IsIAPFailed))
		{
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Purchase Failed";
			coinsPurchaseOption = 0;
			IAPHandler.GetInstance ().IsIAPFailed = false;
			PurchasePanel.SetActive (false);
			GameResumePanel.SetActive (true);
		}
	}
	public void GainExtraLife()
	{
		if (coinsCollected >= 60) 
		{
			if (GameEndPanel.activeInHierarchy)
			{
				if (strikesAllowed<maxstrikesAllowed)
				{
					coinsCollected = coinsCollected - 60;
					strikesAllowed = strikesAllowed + 1;
					StartGameWithExtraLife ();
				}
				else
				{
					GameStatus.GetComponent<Text> ().text = "Cannot Have More than 5 Lives At A Time";
				}
			}
			else 
			{
				if (strikesAllowed<maxstrikesAllowed)
				{
					coinsCollected = coinsCollected - 60;
					strikesAllowed = strikesAllowed + 1;
					GameManager.StartOrPauseGame ();
					StartGameWithExtraLife ();
				}
				else
				{
					MainStatus.GetComponent<Text> ().text = "Cannot Have More than 5 Lives At A Time";
				}
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
	public void On_100Coins_PuchaseClick()
	{
		coinsPurchaseOption = 1;
	}
	public void On_200Coins_PuchaseClick()
	{
		coinsPurchaseOption = 2;
	}
	public void On_500Coins_PuchaseClick()
	{
		coinsPurchaseOption = 3;
	}

	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady ("rewardedVideo")) {
			if (!GameEndPanel.activeInHierarchy) {
				GameManager.StartOrPauseGame ();
			}
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		} else {
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "No Ads Ready" +
				"Try Again";
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				Debug.Log ("The ad was successfully shown.");
		    	OnWatcHAdvertisementClick();
				break;
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
			    OnWatcHAdvertisementClick();
				break;
			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
			    OnWatcHAdvertisementClick();
				break;
		}
	}

   void OnWatcHAdvertisementClick()
	{
		if (GameEndPanel.activeInHierarchy)
		{
		  if(strikesAllowed<maxstrikesAllowed)
			{
				strikesAllowed = strikesAllowed + 1;
				StartGameWithExtraLife ();
			}
		   else
			{
				GameStatus.GetComponent<UnityEngine.UI.Text> ().text = "Cannot Have More than 5 Lives At A Time";
			}
		} 
		else 
		{
			if(strikesAllowed<maxstrikesAllowed)
			{
				strikesAllowed = strikesAllowed + 1;
				StartGameWithExtraLife ();
			}
		    else
			{
				MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Cannot Have More than 5 Lives At A Time";
			}
		}
	}
    void StartGameWithExtraLife()
	{
		if (GameEndPanel.activeInHierarchy) 
		{
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Extra Life Awarded";
			Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
			GameResumePanel.SetActive (true);
//			for (int j = 0; j < Strikes.transform.childCount; j++) 
//			{
//				Strikes.transform.GetChild (j).gameObject.SetActive (true);
//			}
			for (int j = 0; j < Lives.transform.childCount; j++)
			{
				if (!Lives.transform.GetChild (j).gameObject.activeInHierarchy) 
				{
					Lives.transform.GetChild (j).gameObject.SetActive (true);
					Lives.transform.GetChild (j).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImage;
					break;
				}
			}
			for (int j = 0; j < BlackHole_GameObj.transform.childCount; j++) {
				BlackHole_GameObj.transform.GetChild (j).gameObject.SetActive (false);
			}
			GameEndPanel.SetActive (false);
		} 
		else
		{
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Extra Life Awarded";
			Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
			GameResumePanel.SetActive (true);
			LowerPanel.SetActive (false);
			for (int j = 0; j < Lives.transform.childCount; j++)
			{
				if (!Lives.transform.GetChild (j).gameObject.activeInHierarchy) 
				{
					Lives.transform.GetChild (j).gameObject.SetActive (true);
					Lives.transform.GetChild (j).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImage;
					break;
				}
			}
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
		if (!pause) {
			Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
			GameResumePanel.SetActive (true);
			LowerPanel.SetActive (false);
		} else {
			Pause.GetComponent<UnityEngine.UI.Button> ().enabled = true;
			GameResumePanel.SetActive (false);
			LowerPanel.SetActive (true);
		}
		StartOrPauseGame ();
	}

	public void OnPurchaseCoinClick()
	{
		StartOrPauseGame ();
		Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
		LowerPanel.SetActive (false);
		PurchasePanel.SetActive (true);
	}
	public void OnPurchaseBackButttonClick()
	{
		StartOrPauseGame ();
		Pause.GetComponent<UnityEngine.UI.Button> ().enabled = true;
		LowerPanel.SetActive (true);
		PurchasePanel.SetActive (false);
	}
		
}
