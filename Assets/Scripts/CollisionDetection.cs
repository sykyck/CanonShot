using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
	public GameObject Strikes;
	public GameObject GameEndPanel;
	public GameObject MainStatus;
	public GameObject BlackHole_GameObj;
	public GameObject Canon_GameObj;
	public GameObject Pause;

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
			collision.gameObject.transform.position = CanonRotatiion.Canon.transform.position;
			GameManager.strikes = GameManager.strikes + 1;
			for (int i = 0; i < Strikes.transform.childCount; i++)
			{
				if (!Strikes.transform.GetChild (i).gameObject.activeInHierarchy)
				{
					Strikes.transform.GetChild (i).gameObject.SetActive (true);
					if (i != (Strikes.transform.childCount - 1)) {
						break;
					}
				}
				if (i == (Strikes.transform.childCount - 1))
				{
					for (int j = 0; j < Strikes.transform.childCount; j++)
					{
						Strikes.transform.GetChild (j).gameObject.SetActive (false);
					}
				}
			}
			if (GameManager.strikes == GameManager.strikesAllowed)
			{
				MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Game Ended";
				GameEndPanel.SetActive (true);
				GameManager.StartOrPauseGame ();
			}
		}
	}

	public void BackButton()
	{
		GameManager.strikesAllowed = 3;
		GameManager.score = 0;
		GameManager.strikes = 0;
		MainStatus.GetComponent<UnityEngine.UI.Text> ().text = "Game Started Again";
		GameEndPanel.SetActive (false);
		GameManager.StartOrPauseGame ();
	}

}
