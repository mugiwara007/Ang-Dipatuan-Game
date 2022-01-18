using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCharacterController : MonoBehaviour
{
	CharacterController controller;
	Animator anim;

	//Sound FX Variables (Movements)
	public AudioSource Run;
	public AudioSource Walk;
	public AudioSource Jmp;
	public AudioSource SSword;
	public AudioSource USword;
	public AudioSource Crch;
	public AudioSource Hit;

	//Sound FX Variables (Attack)
	public AudioSource SA1;
	public AudioSource SA2;
	public AudioSource SA3;
	public AudioSource SA4;
	public AudioSource JmpAtk;
	public AudioSource Stab;

	//Heavy Atks
	public AudioSource HA1;
	public AudioSource HA2;
	public AudioSource HA3;
	//Skills
	public AudioSource Brvry;
	public AudioSource SloMo;
	public AudioSource DMode;

	private float speed = 6f;

	public float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;

	public bool stun;

	private Transform cam;

	public bool isRunning;

	//For Attacks

	//for light attack
	int comboStep;
	public bool comboPossible,isLightAttacking,isBlocking;

	//for heavy attack
	int heavyAttackcomboStep;
	public bool heavyAttackcomboPossible, isHeavyAttacking;

	private bool heavyAttackPossible = true, lightAttackPossible = true;
	//Attack End


	//For Jumping
	public bool groundedPlayer;
	private Vector3 playerVelocity;
	private float jumpHeight = 2.6f;
	private float gravityValue = -9.81f;
	private bool isJumping;
	private float groundedTimer;     // to allow jumping when going down ramps

	//For Crouching
	private GameObject followCamera;
	private float followCamPosition = 1.51f;
	public bool isCrouching;
	public bool canCrouch;

	//For Sword
	GameObject kampilan,scabbard,scabbardWithSword;
	private bool isHoldingSword;
	private float timeToSheathe;
	private bool isSwordRecentlyUsed;

	//For Camera Shake When Jump Drop Down Attack
	private CinemachineFreeLook cinemachineFreeLookCam;
	private float amplitudeGain = 2f;
	private float frequemcyGain = 1f;
	private float shakeDuration = 0.1f;
	NoiseSettings JumpAttackShake,DefaultCamShake;

	PlayerBar playerBars;

	//For Damage and Death
	public bool isTakingDamage = false;

	// Start is called before the first frame update
	void Awake()
	{
		StartVariables();
		stun = false;

		//Movements Sound FX
		Run.playOnAwake = false;
		Walk.playOnAwake = false;
		Jmp.playOnAwake = false;
		SSword.playOnAwake = false;
		USword.playOnAwake = false;
		Crch.playOnAwake = false;
		Hit.playOnAwake = false;

		//Attack Sound FX
		SA1.playOnAwake = false;
		SA2.playOnAwake = false;
		SA3.playOnAwake = false;
		SA4.playOnAwake = false;
		JmpAtk.playOnAwake = false;
		Stab.playOnAwake = false;
		HA1.playOnAwake = false;
		HA2.playOnAwake = false;
		HA3.playOnAwake = false;

		//Skills Sound FX
		Brvry.playOnAwake = false;
		SloMo.playOnAwake = false;
		DMode.playOnAwake = false;
	}

	public void StartVariables()
    {
		controller = GetComponent<CharacterController>();
		anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

		cam = GameObject.FindGameObjectWithTag("MainCamera").transform;

		followCamera = GameObject.FindGameObjectWithTag("followCamera");

		kampilan = GameObject.FindGameObjectWithTag("KampilanArmed");
		scabbardWithSword = GameObject.FindGameObjectWithTag("KampilanUnarmed");
		scabbard = GameObject.FindGameObjectWithTag("Scabbard");

		kampilan.gameObject.SetActive(false);
		scabbard.gameObject.SetActive(false);

		cinemachineFreeLookCam = GameObject.FindGameObjectWithTag("cinemachineFreeLook").GetComponent<CinemachineFreeLook>();
		JumpAttackShake = Resources.Load("JumpAttackShake") as NoiseSettings;
		DefaultCamShake = Resources.Load("DefaultCamShake") as NoiseSettings;

		playerBars = GetComponent<PlayerBar>();

		canCrouch = true;
	}


	// Update is called once per frame
	void Update()
	{
		if (stun == false)
        {
			if(!isTakingDamage)
			{
				Jump();

				Movement();

				LightAttackActionListener();

				HeavyAttackActionListener();

				Block();

				Crouch();

				Sheathe();

				JumpDropDownAttack();
			}
        }
		else
        {
			anim.SetBool("isRunning", false);
			anim.SetBool("isWalking", false);
			isRunning = false;
		}
	}


	public void Jump()
    {
		//JUMP
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

		if (Input.GetButtonUp("Shift"))
		{
			jumpHeight = 2.6f;
			isRunning = false;
		}

		playerVelocity.y += gravityValue * Time.deltaTime; //Creates Gravity to Player

		controller.Move(playerVelocity * Time.deltaTime);
		//END JUMP
	}




	public void Movement()
    {
		//For Movement
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);

			if (isLightAttacking == false && isHeavyAttacking == false && isBlocking == false)
			{
				Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
				controller.Move(moveDir.normalized * speed * Time.deltaTime);
			}

			anim.SetBool("isWalking", true);


			//JUMP WHEN WALKING
			if (isJumping)
			{
				anim.SetBool("isJumping", true);
			}

			//RUNNING
			if (Input.GetButton("Shift") && playerBars.stamina > 0 && isCrouching == false && isBlocking == false)
			{
				isRunning = true;
				anim.SetBool("isRunning", true);
				speed = 13f;
				jumpHeight = 3f;
				

				//JUMP WHEN RUNNING
				if (isJumping)
				{
					anim.SetBool("isJumping", true);
					speed = 4f;
				}

				
			}
			else
			{
				anim.SetBool("isRunning", false);
				speed = 4f;
				jumpHeight = 2.6f;
				isRunning = false;
			}	
		}
		else
		{
			anim.SetBool("isRunning", false);
			anim.SetBool("isWalking", false);
			isRunning = false;
		}

		//End Movement
	}



	public void Block()
    {
		//Block
		if (Input.GetButton("Left Ctrl") && isCrouching == false && isJumping == false)
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
		//END Block
	}

	public void Crouch()
    {
		//Crouch
		if (Input.GetButton("Crouch") && isJumping == false && canCrouch)
		{
			isCrouching = true;
			
			//Adjust Crouch Layer in player Animator
			var currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Crouch"));
			anim.SetLayerWeight(anim.GetLayerIndex("Crouch"), Mathf.Lerp(currentWeight, 1.0f, 7f * Time.deltaTime));
			speed = 3f;

			//Makes Camera to go down a little bit when crouching
			Vector3 NewPos = new Vector3(followCamera.transform.localPosition.x, followCamPosition - 0.5f, followCamera.transform.localPosition.z);
			followCamera.transform.localPosition = Vector3.Lerp(followCamera.transform.localPosition, NewPos, 7f * Time.deltaTime);
		}
		else
		{
			isCrouching = false;
			var currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Crouch"));
			anim.SetLayerWeight(anim.GetLayerIndex("Crouch"), Mathf.Lerp(currentWeight, 0f, 7f * Time.deltaTime));
			Vector3 NewPos = new Vector3(followCamera.transform.localPosition.x, followCamPosition, followCamera.transform.localPosition.z);
			followCamera.transform.localPosition = Vector3.Lerp(followCamera.transform.localPosition, NewPos, 7f * Time.deltaTime);
		}
		//END CROUCHING
	}

	public void Sheathe()
    {
		
		//Show Kampilan on Hand when Attacking
		if (isLightAttacking || isHeavyAttacking || isBlocking )
		{
			kampilan.gameObject.SetActive(true);
			timeToSheathe = 0f;
		}
		else
		{
			if (isSwordRecentlyUsed)
            {
				timeToSheathe += Time.deltaTime;
			}
			
			if (timeToSheathe >= 5f)
            {
				USword.Play();
				StartCoroutine("SheatheSword");
				anim.SetBool("isSheathing", true);
				kampilan.gameObject.transform.localPosition = new Vector3(0.1094f, -0.0691f, -0.396f);
				kampilan.gameObject.transform.localRotation = Quaternion.Euler(-77.863f, -148.234f, 319.526f);
				timeToSheathe = 0f;
				isSwordRecentlyUsed = false;
			}
            else
            {
				kampilan.gameObject.transform.localPosition = new Vector3(0.177f, 0.0628f, -0.349f);
				kampilan.gameObject.transform.localRotation = Quaternion.Euler(-37.614f, -100.303f, 259.229f);
				anim.SetBool("isSheathing", false);
			}

		}


	}

	IEnumerator SheatheSword()
    {
		yield return new WaitForSeconds(0.5f);
		kampilan.gameObject.SetActive(false);
		isHoldingSword = false;
		
		scabbard.gameObject.SetActive(false);
		scabbardWithSword.gameObject.SetActive(true);
	}

	//LIGHT ATTACK
	public void LightAttackActionListener()
    {
		//For LightAttack
		if (Input.GetKeyDown(KeyCode.Mouse0) && lightAttackPossible == true && isBlocking == false && isCrouching == false && groundedPlayer)
		{
			//if player is not holding the sword then withdraw the sword
			if (!isHoldingSword)
			{
				SSword.Play();
				anim.SetBool("withdrawSword", true);
				StartCoroutine("ShowSwordOnHand");
				StartCoroutine("SetWithdrawSwordAnimationToFalse");
				StartCoroutine("canAttack");
			}
            else
            {
				Attack();
				heavyAttackPossible = false;
			}
			
		}
	}

	public void Attack()
    {
		isLightAttacking = true;
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
		if (comboStep == 2)
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
		isLightAttacking = false;
		heavyAttackPossible = true;

		isSwordRecentlyUsed = true;
	}

	public void DeductStamina(float deductPoints)
    {
		//deduct to stamina
		playerBars.DecreaseStamina(deductPoints);
	}

	//HEAVY ATTACK
	public void HeavyAttackActionListener()
	{
		//For Heavy Attack
		if (Input.GetKeyDown(KeyCode.Mouse1) && playerBars.stamina >= 5f && heavyAttackPossible == true && isBlocking == false && isCrouching == false && groundedPlayer)
		{
			//if player is not holding the sword then withdraw the sword
			if (!isHoldingSword)
			{
				anim.SetBool("withdrawSword", true);
				StartCoroutine("ShowSwordOnHand");
				StartCoroutine("SetWithdrawSwordAnimationToFalse");
				StartCoroutine("canAttack");
			}
			else
            {
				HeavyAttack();
				lightAttackPossible = false;
			}
		}
	}

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

		isSwordRecentlyUsed = true;
	}

	//This is called whenever player receives Damage
	public void AttackReset()
	{
		comboPossible = false;
		comboStep = 0;
		isLightAttacking = false;
		heavyAttackPossible = true;

		heavyAttackcomboPossible = false;
		heavyAttackcomboStep = 0;
		isHeavyAttacking = false;
		lightAttackPossible = true;

	}

	public void ChangeKampilanonHandPosition()
    {
		kampilan.gameObject.transform.localPosition = new Vector3(0.179f, -0.122f, -0.281f);
		kampilan.gameObject.transform.localRotation = Quaternion.Euler(-35.251f, -128.959f, 297.612f);
	}

	IEnumerator SetWithdrawSwordAnimationToFalse()
    {
		yield return new WaitForSeconds(0.3f);
		anim.SetBool("withdrawSword", false);
	}

	IEnumerator ShowSwordOnHand()
	{
		yield return new WaitForSeconds(0.7f);
		kampilan.gameObject.SetActive(true);
		isSwordRecentlyUsed = true;

		scabbard.gameObject.SetActive(true);
		scabbardWithSword.gameObject.SetActive(false);
	}
	IEnumerator canAttack()
	{
		yield return new WaitForSeconds(0.8f);
		isHoldingSword = true;
	}

	public void JumpDropDownAttack()
    {
		if (isJumping)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0) && playerBars.stamina >= 25f)
			{
				kampilan.gameObject.transform.localPosition = new Vector3(0.179f, -0.122f, -0.281f);
				kampilan.gameObject.transform.localRotation = Quaternion.Euler(-35.251f, -128.959f, 297.612f);

				scabbard.gameObject.SetActive(true);
				scabbardWithSword.gameObject.SetActive(false);

				anim.SetBool("isJumpAttack", true);
				isHeavyAttacking = true;
				isHoldingSword = true;
				isSwordRecentlyUsed = true;
				lightAttackPossible = false;
				heavyAttackPossible = false;
				//Movement Delay after Jump Drop Down Attack
				StartCoroutine("movementDelay");
			}
			else
			{
				anim.SetBool("isJumpAttack", false);
			}
		}
	}

	//Movement Delay after Jump Drop Down Attack
	IEnumerator movementDelay()
    {
		yield return new WaitForSeconds(1.3f);
		isHeavyAttacking = false;
		lightAttackPossible = true;
		heavyAttackPossible = true;
	}

	//Shake when Jump Drop Down Attack
	public void DoShake()
	{
		StartCoroutine(Shake());
	}
	public IEnumerator Shake()
	{
		cinemachineFreeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = JumpAttackShake;
		Noise(amplitudeGain, frequemcyGain);

		yield return new WaitForSeconds(shakeDuration);

		cinemachineFreeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = DefaultCamShake;
		Noise(0.7f, 1f);
	}
	void Noise(float amplitude, float frequency)
	{
		cinemachineFreeLookCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
		cinemachineFreeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;

		cinemachineFreeLookCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
		cinemachineFreeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
	}
	//End Jump Drop Down Attack
	

	//Sound FX Functions
	public void SwordAtk1FX()
    {
		SA1.Play();
    }
	public void SwordAtk2FX()
	{
		SA2.Play();
	}
	public void JmpAtkFX()
	{
		JmpAtk.Play();
	}
	public void SwordAtk4FX()
	{
		SA4.Play();
	}
	public void SwordAtk3FX()
	{
		SA3.Play();
	}
	public void StabFX()
	{
		Stab.Play();
	}
	public void HeavyAtk1FX()
	{
		HA1.Play();
	}
	public void HeavyAtk2FX()
	{
		HA2.Play();
	}
	public void HeavyAtk3FX()
	{
		HA3.Play();
	}
	public void HitFX()
	{
		Hit.Play();
	}

	//Movements
	public void WalkFX()
    {
		//START
		if(!Walk.isPlaying)
        {
			Walk.Play();
        }
		
	}

	public void WalkFIX()
    {
		//STOP
		if (Run.isPlaying)
		{
			Run.Stop();
		}
		if (Crch.isPlaying)
		{
			Crch.Stop();
		}
		if (Jmp.isPlaying)
		{
			Jmp.Stop();
		}
	}
	public void RunFX()
	{
		//START
		if (!Run.isPlaying)
		{
			Run.Play();
		}
	}
	
	public void RunFIX()
    {
		//STOP
		if (Walk.isPlaying)
		{
			Walk.Stop();
		}
		if (Crch.isPlaying)
		{
			Crch.Stop();
		}
		if (Jmp.isPlaying)
		{
			Jmp.Stop();
		}
	}
	public void CrouchFX()
	{
		//START
		if (!Crch.isPlaying)
		{
			Crch.Play();
		}
	}

	public void CrouchFIX()
    {
		//STOP
		if (Run.isPlaying)
		{
			Run.Stop();
		}
		if (Walk.isPlaying)
		{
			Walk.Stop();
		}
		if (Jmp.isPlaying)
		{
			Jmp.Stop();
		}
	}

	public void JumpFX()
    {
		//START
		if (!Jmp.isPlaying)
		{
			Jmp.Play();
		}
	}

	public void JumpFIX()
    {
		//STOP
		if (Run.isPlaying)
		{
			Run.Stop();
		}
		if (Walk.isPlaying)
		{
			Walk.Stop();
		}
		if (Crch.isPlaying)
		{
			Crch.Stop();
		}
	}

	public void IdleFX()
    {
		Run.Stop();
		Walk.Stop();
		Crch.Stop();
		Jmp.Stop();
	}

	//Skills
	public void BraveryFX()
    {
		Brvry.Play();
    }
	public void SlowMoFX()
    {
		SloMo.Play();
    }
	public void DipatuanModeFX()
    {
		DMode.Play();
	}

	//END Sound FX Functions
}
