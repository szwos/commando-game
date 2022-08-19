using UnityEngine;
using UnityEngine.Events;

//TODO organize variables and take consistent naming convention

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	[SerializeField] private CapsuleCollider2D m_BodyCollider;              // Collider that will be disabled when passing through platforms
	[SerializeField] const float k_CeilingRadius = .17f; // Radius of the overlap circle to determine if the player can stand up
	[SerializeField] const float k_GroundedRadius = .02f; // Radius of the overlap circle to determine if grounded
	[SerializeField] private float slopeCheckDistance = 0.5f; // // Radius of the overlap circle to determine angle of ground below


	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool wasFalling;			//was the player falling after jumped
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private bool idle = true;
	private float lastX = 0;
	private Vector2 colliderSize;
	private float SlopeDownAngle;
	private Vector2 SlopeNormalPerp;
	private bool isOnSlope;
	private float SlopeDownAngleOld;
	private float SlopeSideAngle;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;
	
	

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		colliderSize = m_BodyCollider.size;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		
		if(m_Rigidbody2D.velocity.y <= 0)
			wasFalling = true;
			


		if (lastX == transform.position.x)
			idle = true;
		else
			idle = false;

		lastX = transform.position.x;
		//Debug.Log(idle);


		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject && wasFalling)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		SlopeCheck();
	}


	public void Move(float move, bool crouch, bool jump, bool down)
	{
		
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) //TODO this ovelapCircle gets triggered when player is next to a wall
			{																					   //a) make body wider than head and set overlapCircle radius between their radius
				crouch = true;																	   //b) instead of overlapCircle use something that only Checks up and down of head (overlapElipsoid??? overlapBox??? overlapCapsule???)
			}
		}



		if (m_Grounded && isOnSlope) //only if player is on the slope
		{
			Debug.Log("Moving on slope");
			//Vector3 targetVelocity = new Vector2(-move * 10f * SlopeNormalPerp.x, m_Rigidbody2D.velocity.y + 10f * SlopeNormalPerp.y * -move);
			// And then smoothing it out and applying it to the character
			//m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);\

			Vector2 newVelocity = new Vector2(-move * 10f * SlopeNormalPerp.x, m_Rigidbody2D.velocity.y + 10f * SlopeNormalPerp.y * -move);
			m_Rigidbody2D.velocity = newVelocity;

		}
		else if (m_Grounded || m_AirControl) //only control the player if grounded or airControl is turned on
		{

			// If crouching
			if (crouch)
			{

				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


			/*
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			*/

		}



			// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			wasFalling = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce)); //TODO instead of adding force modify velocity to some value, this will make character always jump the same height
		}

		//playing walking sound, only when character is grounded
		if(m_Grounded && !idle)
        {
			SoundManager.PlaySound(SoundManager.Sound.player_move);
        }

		//Disable platforms if user wants to go down
		if(m_Grounded && down)
        {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.25f);
			foreach (Collider2D collider in colliders)
            {
				if(collider.gameObject.tag == "Platform")
                {
					collider.gameObject.GetComponent<Platform>().open(m_BodyCollider);
                }
            }
		}
	}

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;


		//transform.Rotate(0f, 180f, 0f);

	}


	private void SlopeCheck()
    {

		Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);
		
		SlopeCheckVertical(checkPos);
		SlopeCheckHorizontal(checkPos);

	}

	private void SlopeCheckHorizontal(Vector2 checkPos)
    {
		RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, m_WhatIsGround);
		RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, m_WhatIsGround);

		if(slopeHitFront)
        {
			isOnSlope = true;
			SlopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }else if (slopeHitBack)
		{
			isOnSlope = true;
			SlopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
		else
        {
			SlopeSideAngle = 0.0f;
			isOnSlope = false;
        }
	}


	private void SlopeCheckVertical(Vector2 checkPos)
    {
		RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, m_WhatIsGround);

		if(hit)
        {
			SlopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

			SlopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

			if(SlopeDownAngle != SlopeDownAngleOld)
            {
				isOnSlope = true;
            }
			SlopeDownAngleOld = SlopeDownAngle;

			if (SlopeDownAngle == 0)
				isOnSlope = false;
			else
				isOnSlope = true;

			Debug.DrawRay(hit.point, SlopeNormalPerp, Color.red);
			Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }
}
