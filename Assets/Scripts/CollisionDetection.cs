using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
	public GameObject Strikes;
	public GameObject GameEndPanel;
	public GameObject MainStatus;
	public GameObject BlackHole_GameObj;
	public GameObject Canon_GameObj;
	public GameObject Pause;
	public GameObject Lives;
	public Sprite DeadImage;
	public Sprite AliveImage;
	public GameObject LowerPanel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	 
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.gameObject.name == "CanonBall") || (collision.gameObject.name == "CoinPrefab"))
		{
			collision.gameObject.GetComponent<Rigidbody2D>().Sleep ();
			collision.gameObject.SetActive (false);
			if (collision.gameObject.name == "CanonBall") 
			{
				CanonBall.currentBallsNumInScene = CanonBall.currentBallsNumInScene - 1;
				CanonBall.currentTotalNum = CanonBall.currentTotalNum - 1;
			}
			if (collision.gameObject.name == "CoinPrefab") 
			{
				CanonBall.currentCoinsNumInScene = CanonBall.currentCoinsNumInScene - 1;
				CanonBall.currentTotalNum = CanonBall.currentTotalNum - 1;
			}
			collision.gameObject.transform.position = CanonRotatiion.Canon.transform.position;
			GameManager.strikes = GameManager.strikes + 1;
//			for (int i = 0; i < Strikes.transform.childCount; i++)
//			{
//				if (!Strikes.transform.GetChild (i).gameObject.activeInHierarchy)
//				{
//					Strikes.transform.GetChild (i).gameObject.SetActive (true);
//					if (i != (Strikes.transform.childCount - 1)) {
//						break;
//					}
//				}
//				if (i == (Strikes.transform.childCount - 1))
//				{
//					for (int j = 0; j < Strikes.transform.childCount; j++)
//					{
//						Strikes.transform.GetChild (j).gameObject.SetActive (false);
//					}
//				}
//			}
			for (int i = 0; i < GameManager.strikesAllowed; i++)
			{
				if ((Lives.transform.GetChild (i).gameObject.activeInHierarchy)&&(Lives.transform.GetChild (i).gameObject.GetComponent<UnityEngine.UI.Image>().sprite!=DeadImage))
				{
					Lives.transform.GetChild (i).gameObject.GetComponent<UnityEngine.UI.Image>().sprite=DeadImage;
					break;
				}
				if (i == (GameManager.strikesAllowed - 1))
				{
					if (GameManager.strikesAllowed == GameManager.maxstrikesAllowed) 
					{
						for (int j = 0; j < Lives.transform.childCount; j++) {
							if (j < 3) {
								Lives.transform.GetChild (j).gameObject.SetActive (true);
								Lives.transform.GetChild (j).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImage;
							} else {
								Lives.transform.GetChild (j).gameObject.SetActive (false);
								Lives.transform.GetChild (j).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImage;
							}
						}
					}
				}
			}
			if (GameManager.strikes == GameManager.strikesAllowed)
			{
				MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Game Ended";
				GameEndPanel.SetActive (true);
				LowerPanel.SetActive (false);
				Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
//				for (int i = 0; i < Strikes.transform.childCount; i++) {
//					Strikes.transform.GetChild (i).gameObject.SetActive (false);
//				}
				GameManager.StartOrPauseGame ();
			}
		}
	}

	public void BackButton()
	{
		GameManager.strikesAllowed = 3;
		GameManager.score = 0;
		GameManager.strikes = 0;
		CanonRotatiion.rotationSpeedFactor = 50;
		CanonBall.forceMultiplier = 100;
		MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Game Started Again";
		Pause.GetComponent<UnityEngine.UI.Button> ().enabled = true;
		LowerPanel.SetActive (true);
		for (int i = 0; i < GameManager.maxstrikesAllowed; i++) 
		{
			if (i < (GameManager.strikesAllowed))
			{
				Lives.transform.GetChild (i).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImage;
				Lives.transform.GetChild (i).gameObject.SetActive(true);
			}
			if (i >= (GameManager.strikesAllowed)) 
			{
				Lives.transform.GetChild (i).gameObject.SetActive (false);
			}
		}
		for (int j = 0; j < BlackHole_GameObj.transform.childCount; j++) {
			BlackHole_GameObj.transform.GetChild (j).gameObject.SetActive (false);
		}
		GameEndPanel.SetActive (false);
		GameManager.StartOrPauseGame ();
	}

}
