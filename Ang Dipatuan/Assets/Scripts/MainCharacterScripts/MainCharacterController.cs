using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
	CharacterController controller;
	Animator anim;

	private float speed = 6f;

	public float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;

	private Transform cam;

	//for light attack
	int comboStep;
	public bool comboPossible,isAttacking,isBlocking;

	//for heavy attack
	int heavyAttackcomboStep;
	public bool heavyAttackcomboPossible, isHeavyAttacking;

	private bool heavyAttackPossible = true, lightAttackPossible = true;

	//FOR JUMPING
	public bool groundedPlayer;
	private Vector3 playerVelocity;
	private float jumpHeight = 2.6f;
	private float gravityValue = -9.81f;
	private bool isJumping;
	private float groundedTimer;     // to allow jumping when going down ramps

	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

		cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}


	// Update is called once per frame
	void Update()
	{
		groundedPlayer = controller.isGrounded;

		// slam into the ground
		if (groundedPlayer && playerVelocity.y < 0)
		{
			// hit ground
			playerVelocity.y = 0f;
		}

		if (groundedPlayer)
		{
			// cooldown interval to allow reliable jumping even whem coming down ramps
			groundedTimer = 0.5f;
		}
		if (groundedTimer > 0)
		{
			groundedTimer -= Time.deltaTime;
		}

		//JUMP
		// allow jump as long as the player is on the ground
		if (Input.GetButtonDown("Jump"))
		{
			// must have been grounded recently to allow jump
			if (groundedTimer > 0)
			{
				// no more until we recontact ground
				groundedTimer = 0;

				// Physics dynamics formula for calculating jump up velocity based on height and gravity
				playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

				anim.SetBool("isJumping", true);
				isJumping = true;
			}
		}
		else if (groundedPlayer)
		{
			anim.SetBool("isJumping", false);
			isJumping = false;
		}

		
		playerVelocity.y += gravityValue * Time.deltaTime; //Creates Gravity to Player

		controller.Move(playerVelocity * Time.deltaTime);
		//END JUMP


		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);

			if (isAttacking == false && isHeavyAttacking == false && isBlocking == false)
			{
				Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
				controller.Move(moveDir.normalized * speed * Time.deltaTime);
			}

			anim.SetBool("isWalking", true);

			//JUMP WHEN WALKING
			if (isJumping)
			{
				anim.SetTrigger("isJump");
				anim.SetBool("isJumping", true);
			}

			//RUNNING
			if (Input.GetButton("Shift"))
			{
				anim.SetBool("isRunning", true);
				speed = 13f;

				//JUMP WHEN RUNNING
				if (isJumping)
				{
					anim.SetTrigger("isJump");
					anim.SetBool("isJumping", true);
				}
			}
			else
			{
				anim.SetBool("isRunning", false);
				speed = 4f;
			}

		}
		else
		{ 			
			anim.SetBool("isRunning", false);
			anim.SetBool("isWalking", false);
		}


		if (Input.GetKeyDown(KeyCode.Mouse0) && lightAttackPossible == true && isBlocking == false)
		{
			Attack();
			heavyAttackPossible = false;
		}

		if (Input.GetKeyDown(KeyCode.Mouse1) && heavyAttackPossible == true && isBlocking == false)
		{
			HeavyAttack();
			lightAttackPossible = false;
		}



		//Block
		if (Input.GetButton("Left Ctrl"))
		{
			anim.Play("Block");
			anim.SetBool("isBlocking", true);
			isBlocking = true;
			ComboReset();
			HeavyComboReset();
		}
        else
        {
			anim.SetBool("isBlocking", false);
			isBlocking = false;
		}


	}

	//LIGHT ATTACK
	public void Attack()
    {
		isAttacking = true;
		if (comboStep == 0)
        {
			anim.Play("sword attack 1");
			comboStep = 1;
			return;
        }
        if (comboStep != 0)
        {
            if (comboPossible)
            {
				comboPossible = false;
				comboStep += 1;
            }
        }
    }

	public void ComboPossible()
    {
		comboPossible = true;
    }

	public void Combo()
    {
		if(comboStep == 2)
        {
			anim.Play("sword attack 2");
		}
		if (comboStep == 3)
		{
			anim.Play("sword attack 3");
		}
		if (comboStep == 4)
		{
			anim.Play("sword attack 4");
		}
	}

	public void ComboReset()
    {
		comboPossible = false;
		comboStep = 0;
		isAttacking = false;
		heavyAttackPossible = true;
	}

	//HEAVY ATTACK
	public void HeavyAttack()
	{
		isHeavyAttacking = true;
		if (heavyAttackcomboStep == 0)
		{
			anim.Play("heavy attack 1");
			heavyAttackcomboStep = 1;
			return;
		}
		if (heavyAttackcomboStep != 0)
		{
			if (heavyAttackcomboPossible)
			{
				heavyAttackcomboPossible = false;
				heavyAttackcomboStep += 1;
			}
		}
	}

	public void HeavyComboPossible()
	{
		heavyAttackcomboPossible = true;
	}

	public void HeavyCombo()
	{
		if (heavyAttackcomboStep == 2)
		{
			anim.Play("heavy attack 2");
		}
		if (heavyAttackcomboStep == 3)
		{
			anim.Play("heavy attack 3");
		}
	}

	public void HeavyComboReset()
	{
		heavyAttackcomboPossible = false;
		heavyAttackcomboStep = 0;
		isHeavyAttacking = false;
		lightAttackPossible = true;
	}

}
