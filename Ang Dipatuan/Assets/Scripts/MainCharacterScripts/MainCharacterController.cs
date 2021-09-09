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

			if (Input.GetButton("Shift"))
			{
				anim.SetBool("isRunning", true);
				speed = 20f;
			}
			else
			{
				anim.SetBool("isRunning", false);
				speed = 6f;
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




		//JUMP
		groundedPlayer = controller.isGrounded;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.Play("Jump");

			anim.SetBool("isJumping", false);
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
