

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputPC : MonoBehaviour
{
	[SerializeField] PlayerMovement playerMovement = null;	
	[SerializeField] PlayerAttack playerAttack = null;		
	[SerializeField] PauseMenu pauseMenu;					

	
	void Reset ()
	{
		
		playerMovement = GetComponent<PlayerMovement> ();
		playerAttack = GetComponent<PlayerAttack> ();
		
		pauseMenu = FindObjectOfType<PauseMenu>();
	}

	
#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
	void Awake()
	{
		Destroy(this);
	}
#endif

	void Update ()
	{
		//If there is a pause menu and the player presses the Cancel input axis, pause the game
		if (pauseMenu != null && Input.GetButtonDown("Cancel"))
			pauseMenu.Pause();
		//If the player cannot update, leave
		if (!CanUpdate())
			return;
		//Handle inputs for movement, attacking, and allies
		HandleMoveInput();
		HandleAttackInput();
		HandleAllyInput();
	}

	bool CanUpdate()
	{
		//If the game is paused, the player cannot update
		if (pauseMenu != null && pauseMenu.IsPaused)
			return false;
		
		if (GameManager.Instance.Player == null || GameManager.Instance.Player.transform != transform)
			return false;
		
		return true;
	}

	void HandleMoveInput()
	{
		//If there is no movement script, leave
		if (playerMovement == null)
			return;
		//Get the raw Horizontal and Vertical inputs (raw inputs have no smoothing applied)
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		
		playerMovement.MoveDirection = new Vector3(horizontal, 0, vertical);
		
		if (MouseLocation.Instance != null && MouseLocation.Instance.IsValid) {
			
			Vector3 lookPoint = MouseLocation.Instance.MousePosition - playerMovement.transform.position;
			
			playerMovement.LookDirection = lookPoint;
		}
	}

	void HandleAttackInput()
	{
		//If there is no attack script, leave
		if (playerAttack == null)
			return;

		//If the player presses the SwitchAttack input axis, tell the attack script to switch weapons
		if (Input.GetButtonDown("SwitchAttack"))
		{
			playerAttack.SwitchAttack();
		}
		//If the player presses (or holds) Fire1, start firing
		if (Input.GetButton("Fire1"))
		{
			playerAttack.Fire();
		}
		//Otherwise, stop firing
		else if(Input.GetButtonUp("Fire1"))
		{
			playerAttack.StopFiring ();		
		}
	}

	void HandleAllyInput()
	{
		
		if (Input.GetButtonDown("SummonAlly") && GameManager.Instance != null)
			GameManager.Instance.SummonAlly();

	}
}

