using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
	public GameObject GameStatus;
	public GameObject MainStatus;
	public GameObject GameEndPanel;
	public GameObject GameResumePanel;
	public GameObject LowerPanel;
	public GameObject Pause;
	public GameObject Lives;
	public Sprite AliveImage;
	public Sprite DeadImage;
	public static bool IsTouchValid;
	bool IsParticleSystemChoosen;
    System.Collections.Generic.List<int> particleSystemChoosen;
	void Start ()
	{
		GameManager.BlackHole_GameObj = gameObject;
		IsTouchValid = false;
		IsParticleSystemChoosen = true;
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
					if (i == (gameObject.transform.childCount - 1)) {
						IsParticleSystemChoosen = false;
					}
				}
			}
		}
		if (Input.GetMouseButtonUp (0))
		{
			if ((IsTouchValid)&&(IsParticleSystemChoosen))
			{
				Invoke ("makeParticleSystemInactive",1f);
				IsTouchValid = false;
			}
			if (!IsParticleSystemChoosen) {
				IsParticleSystemChoosen = true;
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
							if (i == (gameObject.transform.childCount - 1)) {
								IsParticleSystemChoosen = false;
							}
						}
						break;
				    case TouchPhase.Moved:
						break;
				case TouchPhase.Ended:
						if ((IsTouchValid) && (IsParticleSystemChoosen)) {
							IsTouchValid = false;
							Invoke ("makeParticleSystemInactive", 1f);
						}
						if (!IsParticleSystemChoosen) {
							IsParticleSystemChoosen = true;
						}
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
		if (GameManager.coinsCollected >= 20) 
		{
			if (GameEndPanel.activeInHierarchy) 
			{
				GameManager.coinsCollected = GameManager.coinsCollected - 20;
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					gameObject.transform.GetChild (i).gameObject.transform.localScale += new Vector3 (1f, 1f, 0f);
					gameObject.transform.GetChild (i).gameObject.GetComponent<ParticleSystem> ().startSize += 1;
				}
				Invoke ("DecreaseVortexSizeAfterFixedTime", 30f);
				StartGameWithIncreaseVortexSize ();
			} 
			else 
			{
				GameManager.StartOrPauseGame ();
				GameManager.coinsCollected = GameManager.coinsCollected - 20;
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					gameObject.transform.GetChild (i).gameObject.transform.localScale += new Vector3 (0.2f, 0.2f, 0f);
					gameObject.transform.GetChild (i).gameObject.GetComponent<ParticleSystem> ().startSize += 1;
				}
				Invoke ("DecreaseVortexSizeAfterFixedTime", 30f);
				StartGameWithIncreaseVortexSize ();
			}
		} 
		else 
		{
			if (GameStatus.activeInHierarchy) 
			{
				GameStatus.GetComponent<UnityEngine.UI.Text> ().text = "Coins are less than 20";
			}
			else
			{
				MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Coins are less than 20";
			}
		}
	}
	void DecreaseVortexSizeAfterFixedTime()
	{
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			gameObject.transform.GetChild (i).gameObject.transform.localScale = new Vector3 (0.3f, 0.3f, 1f);
			gameObject.transform.GetChild (i).gameObject.GetComponent<ParticleSystem> ().startSize = 3;
		}
	}
	void StartGameWithIncreaseVortexSize()
	{
		if (GameEndPanel.activeInHierarchy) 
		{
			GameManager.strikesAllowed = 3;
			GameManager.score = 0;
			GameManager.strikes = 0;
			CanonRotatiion.rotationSpeedFactor = 50;
			CanonBall.forceMultiplier = 100;
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Vortex Size Increased";
			Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
			GameResumePanel.SetActive (true);
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
			for (int j = 0; j < GameManager.BlackHole_GameObj.transform.childCount; j++) {
				GameManager.BlackHole_GameObj.transform.GetChild (j).gameObject.SetActive (false);
			}
			GameEndPanel.SetActive (false);
		} 
		else
		{
			MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Vortex Size Increased";
			Pause.GetComponent<UnityEngine.UI.Button> ().enabled = false;
			GameResumePanel.SetActive (true);
			LowerPanel.SetActive (false);
		}
	}
		
}
