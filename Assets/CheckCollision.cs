using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D (Collision2D collision)
	{
		if ((collision.gameObject.name == "CanonBall") || (collision.gameObject.name == "CoinPrefab"))
		{
			collision.gameObject.GetComponent<Rigidbody2D>().Sleep ();
			collision.gameObject.SetActive (false);
			if (collision.gameObject.name == "CanonBall")
			{
				GameManager.score = GameManager.score + 1;
				CanonBall.currentBallsNumInScene = CanonBall.currentBallsNumInScene - 1;
				CanonBall.currentTotalNum = CanonBall.currentTotalNum - 1;
			}
			if (collision.gameObject.name == "CoinPrefab") 
			{
				GameManager.coinsCollected = GameManager.coinsCollected + 1;
				CanonBall.currentCoinsNumInScene = CanonBall.currentCoinsNumInScene - 1;
				CanonBall.currentTotalNum = CanonBall.currentTotalNum - 1;
			}
		}
	}
}
