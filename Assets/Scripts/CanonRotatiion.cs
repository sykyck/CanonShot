using UnityEngine;
using System.Collections;

public class CanonRotatiion : MonoBehaviour {
	public float speed;
	public float rotationSpeedFactor;
	public static GameObject Canon;

	// Use this for initialization
	void Start ()
	{
		rotationSpeedFactor = 50;
		Canon = gameObject;
		InvokeRepeating ("IncreaseRotationSpeedFactor", 0f, 15f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		speed = Time.deltaTime * rotationSpeedFactor;
		transform.Rotate(Vector3.forward * speed);
	}
	public void DecreaseCannonRotationSpeed()
	{
		if (GameManager.coinsCollected > 30) {
			GameManager.coinsCollected = GameManager.coinsCollected - 30;
			rotationSpeedFactor = rotationSpeedFactor - (rotationSpeedFactor / 10);
		}
	}
	void IncreaseRotationSpeedFactor()
	{
		rotationSpeedFactor += 30;
	}
}
