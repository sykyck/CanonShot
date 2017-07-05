using UnityEngine;
using System.Collections;
using System;

public class CanonBall : MonoBehaviour {
	public GameObject Balls;
	public GameObject Coins;
	public GameObject GameStatus;
	public GameObject MainStatus;
	public GameObject GameEndPanel;
	public GameObject GameResumePanel;
	public GameObject LowerPanel;
	public GameObject Lives;
	public GameObject Pause;
	public GameObject BlackHole_GameObj;
	public Sprite AliveImage;
	Vector3 initCanonPos;
	bool disableIncreaseInForceMultiplier;
	int shootIntervalMinLimit,shootIntervalMaxLimit,numberSelected,counter,optionToSelect,coinToDisappear,maxNumInScene;
	public static int currentTotalNum,currentBallsNumInScene,currentCoinsNumInScene,forceMultiplier;
	bool IsCanonRotating,IsCoinPresentInRandomPlayArea;
    GameObject BallReference,CoinReference;
	System.Random randomObj;
	System.Random Option;
	System.Random SpawnPointObj;

	// Use this for initialization
	void Start () {
		randomObj = new System.Random();
		Option = new System.Random();
		SpawnPointObj= new System.Random();
		forceMultiplier = 100;
		maxNumInScene = 20;
		counter = 0;
		shootIntervalMinLimit = 5;
		shootIntervalMaxLimit = 30;
		disableIncreaseInForceMultiplier = false;
		InvokeRepeating ("IncreaseForceMultiplier", 0f, 15f);
		numberSelected = randomObj.Next (shootIntervalMinLimit, shootIntervalMaxLimit);
		optionToSelect = Option.Next (1, 3);
		IsCanonRotating = true;
		initCanonPos = transform.position;
		IsCoinPresentInRandomPlayArea = false;
		GameManager.Canon_GameObj = gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if ((IsCanonRotating)&&(CanonRotatiion.canCanonShoot)&&(!IsCoinPresentInRandomPlayArea))
		{
			counter = counter + 1;
			if ((currentTotalNum<maxNumInScene)&&(counter == numberSelected))
			{
				counter = shootIntervalMinLimit-1;
				numberSelected = randomObj.Next (shootIntervalMinLimit, shootIntervalMaxLimit);
				optionToSelect = Option.Next (1, 4);
				switch (optionToSelect)
				{
				     case 1:
						StopCanonRotation ();
						break;
				     case 2:
						StopCanonRotation ();
						break;
				     case 3:
						PlaceCoinAtRandomPlayarea ();
						break;
				}
				CanonRotatiion.canCanonShoot = false;
			}
			if (currentBallsNumInScene == Balls.transform.childCount) {
				for (int i = 0; i < Balls.transform.childCount; i++) {
					Balls.transform.GetChild (i).gameObject.SetActive (false);
					currentTotalNum = currentTotalNum - currentBallsNumInScene;
					currentBallsNumInScene = 0;
				}
			}
			if (currentCoinsNumInScene == Coins.transform.childCount) {
				for (int i = 0; i < Coins.transform.childCount; i++) {
					Coins.transform.GetChild (i).gameObject.SetActive (false);
					currentTotalNum = currentTotalNum - currentCoinsNumInScene;
					currentCoinsNumInScene = 0;
				}
			}
		}
	}
	void IncreaseForceMultiplier()
	{
		if ((forceMultiplier < 1000)&&(!disableIncreaseInForceMultiplier)) {
			forceMultiplier += 50;
		}
	}
	void shootBall()
	{
		for(int i=0;i<Balls.transform.childCount;i++)
		{
			if (!Balls.transform.GetChild (i).gameObject.activeInHierarchy)
			{
				BallReference = Balls.transform.GetChild (i).gameObject;
				BallReference.transform.position = transform.position;
				BallReference.SetActive (true);
				currentBallsNumInScene = currentBallsNumInScene + 1;
				currentTotalNum = currentTotalNum + 1;
				BallReference.GetComponent<Rigidbody2D> ().AddForce (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * (Vector3.up) * 1 * forceMultiplier);
				gameObject.GetComponent<Rigidbody2D> ().AddForce (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * (Vector3.up) * -1 * forceMultiplier);
				Invoke ("PlaceCanonAtInitPos", 0.3f);
				break;
			}
			if(i==Balls.transform.childCount-1)
			{
				for (int j = 0; j < Balls.transform.childCount; j++)
				{
					Balls.transform.GetChild (j).gameObject.SetActive (false);
				}
				BallReference = Balls.transform.GetChild (0).gameObject;
				BallReference.transform.position = transform.position;
				BallReference.SetActive (true);
				currentTotalNum = currentTotalNum - currentBallsNumInScene + 1;
				currentBallsNumInScene = 1;
				BallReference.GetComponent<Rigidbody2D> ().AddForce (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * (Vector3.up) * 1 * forceMultiplier);
				gameObject.GetComponent<Rigidbody2D> ().AddForce (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * (Vector3.up) * -1 * forceMultiplier);
				Invoke ("PlaceCanonAtInitPos", 0.3f);
			}
		}
		Invoke("StartCanonRotation",1f);
	}
	void PlaceCanonAtInitPos()
	{
		gameObject.GetComponent<Rigidbody2D> ().Sleep ();
		gameObject.transform.position = Vector3.MoveTowards (transform.position, initCanonPos,Vector3.Distance(transform.position,initCanonPos));
	}
	void shootCoin()
	{
		for(int i=0;i<Coins.transform.childCount;i++)
		{
			if (!Coins.transform.GetChild (i).gameObject.activeInHierarchy)
			{
				CoinReference = Coins.transform.GetChild (i).gameObject;
				CoinReference.transform.position = transform.position;
				CoinReference.SetActive (true);
				currentCoinsNumInScene = currentCoinsNumInScene + 1;
				currentTotalNum = currentTotalNum + 1;
				CoinReference.GetComponent<Rigidbody2D> ().AddForce (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * (Vector3.up) * 1 * forceMultiplier);
				gameObject.GetComponent<Rigidbody2D> ().AddForce (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * (Vector3.up) * -1 * forceMultiplier);
				Invoke ("PlaceCanonAtInitPos", 0.3f);
				break;
			}
			if(i==Coins.transform.childCount-1)
			{
				for (int j = 0; j < Coins.transform.childCount; j++)
				{
					Coins.transform.GetChild (j).gameObject.SetActive (false);
				}
				CoinReference = Coins.transform.GetChild (0).gameObject;
				CoinReference.transform.position = transform.position;
				CoinReference.SetActive (true);
				currentTotalNum = currentTotalNum - currentCoinsNumInScene + 1;
				currentCoinsNumInScene = 1;
				CoinReference.GetComponent<Rigidbody2D> ().AddForce (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * (Vector3.up) * 1 * forceMultiplier);
				gameObject.GetComponent<Rigidbody2D> ().AddForce (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * (Vector3.up) * -1 * forceMultiplier);
				Invoke ("PlaceCanonAtInitPos", 0.3f);
			}
		}
		Invoke("StartCanonRotation",1f);
	}
    void PlaceCoinAtRandomPlayarea()
	{
		float x=SpawnPointObj.Next(0,Screen.width);
		float y=SpawnPointObj.Next(0,Screen.height);
		RaycastHit hit;
		Vector3 pointerPosition=Camera.main.ScreenToWorldPoint(new Vector3(x,y,0));
		Ray ray = new Ray(pointerPosition, Vector3.forward);
		if (Physics.Raycast (ray, out hit))
		{
			if (hit.collider.name == "CanonCollider") 
			{
				TouchManager.IsTouchValid = false;
				PlaceCoinAtRandomPlayarea ();
				return;
			}
			if(hit.collider.name=="CircleBackGround")
			{
				IsCoinPresentInRandomPlayArea = true;
				pointerPosition.z = -2f;
				for(int i=0;i<Coins.transform.childCount;i++)
				{
					if (!Coins.transform.GetChild (i).gameObject.activeInHierarchy)
					{
						CoinReference = Coins.transform.GetChild (i).gameObject;
						CoinReference.transform.position = pointerPosition;
						CoinReference.SetActive (true);
						currentCoinsNumInScene = currentCoinsNumInScene + 1;
						currentTotalNum = currentTotalNum + 1;
						coinToDisappear = i;
						Invoke ("MakeCoinDisappear", 1f);
						break;
					}
					if(i==Coins.transform.childCount-1)
					{
						for (int j = 0; j < Coins.transform.childCount; j++)
						{
							Coins.transform.GetChild (j).gameObject.SetActive (false);
						}
						CoinReference = Coins.transform.GetChild (0).gameObject;
						CoinReference.transform.position = pointerPosition;
						CoinReference.SetActive (true);
						currentTotalNum = currentTotalNum - currentCoinsNumInScene + 1;
						currentCoinsNumInScene = 1;
						coinToDisappear = 0;
						Invoke ("MakeCoinDisappear", 1f);
					}
				}
			}
		}

	}
	void MakeCoinDisappear()
	{
		IsCoinPresentInRandomPlayArea = false;
		MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "";
		if (Coins.transform.GetChild (coinToDisappear).gameObject.activeInHierarchy) {
			Coins.transform.GetChild (coinToDisappear).gameObject.SetActive (false);
			currentCoinsNumInScene = currentCoinsNumInScene - 1;
			currentTotalNum = currentTotalNum - 1;
		}
	}
	void StopCanonRotation()
	{
		gameObject.GetComponent<CanonRotatiion>().enabled = false;
		IsCanonRotating = false;
		if (optionToSelect == 1) {
			Invoke ("shootBall", 0.1f);
		}
		if (optionToSelect == 2) {
			optionToSelect = Option.Next (1, 4);
			if (optionToSelect == 2) {
				Invoke ("shootCoin", 0.1f);
			} else {
				Invoke ("shootBall", 0.1f);
			}
		}
	}
	void StartCanonRotation()
	{
		gameObject.GetComponent<CanonRotatiion>().enabled = true;
		IsCanonRotating = true;
	}
	public static void CheckIfTouchIsValid(float clickPosition_x,float clickPosition_y)
	{
		RaycastHit hit;
		RaycastHit[] hits;
		Vector3 pointerPosition=Camera.main.ScreenToWorldPoint(new Vector3(clickPosition_x,clickPosition_y,0));
		Ray ray = new Ray(pointerPosition, Vector3.forward);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.name == "CanonCollider") 
			{
				TouchManager.IsTouchValid = false;
				return;
			}
			if (hit.collider.name =="CircleBackGround")
			{
				hits = Physics.RaycastAll (pointerPosition, Vector3.forward, Mathf.Infinity);
				for (int i = 0; i < hits.Length; i++)
				{
					if ((hits [i].collider.name == "CanonCollider")||(hits [i].collider.name == "ParticleSystem"))
					{
						TouchManager.IsTouchValid = false;
						break;
					}
					if (i == hits.Length - 1) 
					{
						TouchManager.IsTouchValid = true;
					}
				}
			}
		}
	}
	public void ShootwithLessSpeed()
	{
		if (GameManager.coinsCollected >= 30)
		{
			if (GameEndPanel.activeInHierarchy)
			{
				GameManager.coinsCollected = GameManager.coinsCollected - 30;
				disableIncreaseInForceMultiplier = true;
				Invoke ("enableIncreaseInForceMultiplier", 40f);
				forceMultiplier = 100;
				StartGameWithLessShootSpeed ();
			}
			else 
			{
				GameManager.StartOrPauseGame ();
				GameManager.coinsCollected = GameManager.coinsCollected - 30;
				disableIncreaseInForceMultiplier = true;
				Invoke ("enableIncreaseInForceMultiplier", 40f);
				forceMultiplier = 100;
				StartGameWithLessShootSpeed ();
			}
		} 
		else
		{
			if (GameEndPanel.activeInHierarchy)
			{
				GameStatus.GetComponent<UnityEngine.UI.Text> ().text = "Coins are less than 30";
			} 
			else
			{
				MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Coins are less than 30";
			}
		}
	}
	void enableIncreaseInForceMultiplier()
	{
		disableIncreaseInForceMultiplier = false;
	}
		
	void StartGameWithLessShootSpeed()
	{
		if (GameEndPanel.activeInHierarchy)
		{
			GameManager.strikesAllowed = 3;
			GameManager.score = 0;
			GameManager.strikes = 0;
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Shoot Speed Decreased";
			GameEndPanel.SetActive (false);
			Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
			GameResumePanel.SetActive(true);
			for (int i = 0; i < GameManager.maxstrikesAllowed; i++) 
			{
				if (i < (GameManager.strikesAllowed))
				{
					Lives.transform.GetChild (i).gameObject.SetActive(true);
					Lives.transform.GetChild (i).gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = AliveImage;
				}
				if (i >= (GameManager.strikesAllowed)) 
				{
					Lives.transform.GetChild (i).gameObject.SetActive (false);
				}
			}
			for (int j = 0; j < GameManager.BlackHole_GameObj.transform.childCount; j++) 
			{
				GameManager.BlackHole_GameObj.transform.GetChild (j).gameObject.SetActive (false);
			}
		} 
		else
		{
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Shoot Speed Decreased";
			Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
			GameResumePanel.SetActive(true);
			LowerPanel.SetActive (false);
		}
	}
}
