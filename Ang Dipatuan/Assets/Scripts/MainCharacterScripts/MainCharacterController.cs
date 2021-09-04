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


	int comboStep;
	public bool comboPossible,isAttacking;



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

		if(direction.magnitude >= 0.1f)
        {
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (isAttacking == false)
            {
				Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
				controller.Move(moveDir.normalized * speed * Time.deltaTime);
			}
			

			anim.SetBool("isWalking", true);

        }
        else
        {
			anim.SetBool("isWalking", false);
		}


		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Attack();
		}

		/*
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
			anim.SetBool("isAttack", true);
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			anim.SetBool("isAttack", false);
		}
		*/

	}
	
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
	}

}
