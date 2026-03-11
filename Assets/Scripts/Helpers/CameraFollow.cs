//This script allows a camera to follow the player smoothly and without rotation

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] float smoothing = 5f;							 
	[SerializeField] Vector3 offset = new Vector3 (0f, 15f, -22f);	
	
	void FixedUpdate ()
	{
		
		Vector3 targetCamPos = GameManager.Instance.Player.transform.position + offset;
		
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}

