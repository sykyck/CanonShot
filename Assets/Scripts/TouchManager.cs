using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
	public bool detectCanonBalls;
	public GameObject GameStatus;
	public GameObject MainStatus;
	public GameObject GameEndPanel;
	public static bool IsTouchValid;
	System.Collections.Generic.List<int> particleSystemChoosen;
	System.Collections.Generic.List<int> particleSystemCollided;
	void Start ()
	{
		GameManager.BlackHole_GameObj = gameObject;
		detectCanonBalls = false;
		IsTouchValid = false;
		particleSystemChoosen = new System.Collections.Generic.List<int> ();
		particleSystemCollided = new System.Collections.Generic.List<int> ();
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			CanonBall.CheckIfTouchIsValid (Input.mousePosition.x, Input.mousePosition.y);
			if (IsTouchValid) 
			{
				gameObject.GetComponent<PolygonCollider2D> ().enabled = true;
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					if (!gameObject.transform.GetChild (i).gameObject.activeInHierarchy) 
					{
						gameObject.transform.GetChild (i).gameObject.SetActive (true);
						particleSystemChoosen.Add (i);
						break;
					}
				}
				gameObject.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -2f);
				detectCanonBalls = true;
			}
		}
		if (Input.GetMouseButtonUp (0))
		{
			if (IsTouchValid)
			{
				gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
				Invoke ("makeParticleSystemChoosenInactive", 1.5f);
				detectCanonBalls = false;
				IsTouchValid = false;
			}
		}
		if (Input.touchCount > 0)
		{
			CanonBall.CheckIfTouchIsValid (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y);
			if (IsTouchValid) 
			{
				switch (Input.GetTouch (0).phase) 
				{
				    case TouchPhase.Began:
					    gameObject.GetComponent<PolygonCollider2D> ().enabled = true;
						for (int i = 0; i < gameObject.transform.childCount; i++)
						{
							if (!gameObject.transform.GetChild (i).gameObject.activeInHierarchy) 
							{
								gameObject.transform.GetChild (i).gameObject.SetActive (true);
								particleSystemChoosen.Add (i);
								break;
							}
						}
					    gameObject.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position).x, Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position).y, -2f);
						detectCanonBalls = true;
						break;
				    case TouchPhase.Moved:
						break;
				    case TouchPhase.Ended:
					    gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
					    Invoke ("makeParticleSystemChoosenInactive", 1.5f);
						detectCanonBalls = false;
						IsTouchValid = false;
						break;
				}
			}
		}
	}
	void makeParticleSystemChoosenInactive()
	{
		gameObject.transform.GetChild (particleSystemChoosen [0]).gameObject.SetActive (false);
		particleSystemChoosen.RemoveAt (0);
	}
	void makeParticleSystemCollidedInactive()
	{
		gameObject.transform.GetChild (particleSystemCollided [0]).gameObject.SetActive (false);
		particleSystemCollided.RemoveAt (0);
	}
	void OnCollisionEnter2D (Collision2D collision)
	{
		if ((detectCanonBalls) && ((collision.gameObject.name == "CanonBall") || (collision.gameObject.name == "CoinPrefab"))) 
		{
			collision.gameObject.GetComponent<Rigidbody2D>().Sleep ();
			for (int i = 0; i < gameObject.transform.childCount; i++)
			{
				if (!gameObject.transform.GetChild (i).gameObject.activeInHierarchy) 
				{
					gameObject.transform.GetChild (i).gameObject.SetActive (true);
					particleSystemCollided.Add (i);
					Invoke ("makeParticleSystemCollidedInactive", 1.5f);
					break;
				}
			}
			collision.gameObject.SetActive (false);
			if (collision.gameObject.name == "CanonBall") {
				GameManager.score = GameManager.score + 1;
				CanonBall.currentBallsNumInScene = CanonBall.currentBallsNumInScene - 1;
				CanonBall.currentTotalNum = CanonBall.currentTotalNum - 1;
			}
			if (collision.gameObject.name == "CoinPrefab") {
				GameManager.coinsCollected = GameManager.coinsCollected + 1;
				CanonBall.currentCoinsNumInScene = CanonBall.currentCoinsNumInScene - 1;
				CanonBall.currentTotalNum = CanonBall.currentTotalNum - 1;
			}
		}
	}
		

	public void UseCoinsToIncreaseVortex()
	{
		if (GameManager.coinsCollected > 20) 
		{
			GameManager.coinsCollected = GameManager.coinsCollected - 20;
			for (int i = 0; i < gameObject.transform.childCount; i++) {
				gameObject.transform.GetChild (i).gameObject.GetComponent<ParticleSystem> ().startSize += 1;
			}
		} 
		else 
		{
			GameStatus.GetComponent<UnityEngine.UI.Text> ().text = "Coins are less than 20";
		}
		StartGameWithIncreaseVortexSize();
	}
	void StartGameWithIncreaseVortexSize()
	{
		GameManager.strikesAllowed = 3;
		GameManager.score = 0;
		GameManager.strikes = 0;
		CanonRotatiion.rotationSpeedFactor = 50;
		MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Vortex Size Increased";
		GameEndPanel.SetActive (false);
		GameManager.StartOrPauseGame ();
	}
		
}
