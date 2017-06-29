using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
	public GameObject GameStatus;
	public GameObject MainStatus;
	public GameObject GameEndPanel;
	public static bool IsTouchValid;
    System.Collections.Generic.List<int> particleSystemChoosen;
	void Start ()
	{
		GameManager.BlackHole_GameObj = gameObject;
		IsTouchValid = false;
		particleSystemChoosen = new System.Collections.Generic.List<int> ();
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			CanonBall.CheckIfTouchIsValid (Input.mousePosition.x, Input.mousePosition.y);
			if (IsTouchValid) 
			{
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					if (!gameObject.transform.GetChild (i).gameObject.activeInHierarchy) 
					{
						gameObject.transform.GetChild (i).gameObject.SetActive (true);
						gameObject.transform.GetChild (i).gameObject.transform.position=new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -2f);
						particleSystemChoosen.Add (i);
						break;
					}
				}
			}
		}
		if (Input.GetMouseButtonUp (0))
		{
			if (IsTouchValid)
			{
				Invoke ("makeParticleSystemInactive",1.5f);
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
						for (int i = 0; i < gameObject.transform.childCount; i++)
						{
							if (!gameObject.transform.GetChild (i).gameObject.activeInHierarchy) 
							{
								gameObject.transform.GetChild (i).gameObject.SetActive (true);
							    gameObject.transform.GetChild (i).gameObject.transform.position=new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -2f);
							    particleSystemChoosen.Add (i);
								break;
							}
						}
						break;
				    case TouchPhase.Moved:
						break;
				    case TouchPhase.Ended:
						IsTouchValid = false;
					    Invoke ("makeParticleSystemInactive",1.5f);
						break;
				}
			}
		}
	}
	void makeParticleSystemInactive()
	{
		if (gameObject.transform.GetChild (particleSystemChoosen [0]).gameObject.activeInHierarchy)
		{
			gameObject.transform.GetChild (particleSystemChoosen [0]).gameObject.SetActive (false);
		}
		particleSystemChoosen.Remove (particleSystemChoosen [0]);
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
