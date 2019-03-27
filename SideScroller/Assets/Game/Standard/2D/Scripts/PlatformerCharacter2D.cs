using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 5.5f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 750f;                  // Amount of force added when the player jumps.

        [SerializeField] private float doubleJumpForce = 550f;
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character


        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        public Vector3 respawnPoint;

        public float curHealth = 200;
        public float maxHealth = 200;
        public float defense = 1;

        private HealthBar healthBar;
        public float lives = 3;
        private LifeCount lifeDisplay;

        private bool canDoubleJump = true;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            respawnPoint = transform.position;
            healthBar = Transform.FindObjectOfType<HealthBar>();
            lifeDisplay = Transform.FindObjectOfType<LifeCount>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject) {
                    m_Grounded = true;
                    canDoubleJump = true;
                }
                // if (colliders[i].gameObject.tag == "GhostPlatform" && m_Rigidbody2D.velocity.y > 0) {
                //     canDoubleJump = false;
                // }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump) //bool upPressed, bool downPressed, bool rightPressed, bool leftPressed
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                /* Movement disabler when aiming up or down, not needed since aiming is now with mouse
                if (m_Grounded) {
                    if (upPressed || downPressed) {
                        // Can't move if aiming up or down
                        move = 0;
                    } else {
                        // Reduce the speed if crouching by the crouchSpeed multiplier
                        move = (crouch ? move*m_CrouchSpeed : move);
                    }
                }
                */
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                /*
                // If the input is moving the player right and the player is facing left...
                // Previously took movement into consideration: (!m_FacingRight && move > 0) 
                if (!m_FacingRight && rightPressed && !leftPressed) 
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (m_FacingRight && leftPressed && !rightPressed)
                {
                    // ... flip the player.
                    Flip();
                }
                */
                float differenceX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
                if (!m_FacingRight && differenceX > 0) {
                    Flip();
                } else if (m_FacingRight && differenceX < 0) {
                    Flip();
                }
            }

            if (jump)
            {
                if (m_Grounded && m_Anim.GetBool("Ground"))
                {
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                } else if (canDoubleJump) {
                    canDoubleJump = false;
                    move = (crouch ? move*m_CrouchSpeed : move);
                    m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, 0);
                    m_Rigidbody2D.AddForce(new Vector2(0f, doubleJumpForce));
                }
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            // Vector3 theScale = transform.localScale;
            // theScale.x *= -1;
            // transform.localScale = theScale;

            // Flip instead with rotating player on y-axis
            transform.Rotate(0, 180f, 0);
        }



        public void Damage(float[] attr)
        {
            //If defense is greater than or equal to damage taken, 1 damage is taken instead
            if (attr[0] <= (defense - attr[1]))
            {
                curHealth--;
            }
            else if (curHealth <= 0)
            {
                transform.position = respawnPoint;
                curHealth = 200;
                lives--;
                lifeDisplay.Die();
            }
            else
            {
                curHealth -= (attr[0] - Mathf.Max(0, (defense - attr[1])));
            }
            healthBar.ChangeHealth(curHealth, maxHealth);
        }

        public void GetHealthPickup(float pickupValue)
        {
            if (curHealth != maxHealth)
            {
                if (maxHealth - curHealth <= pickupValue)
                {
                    curHealth = maxHealth;
                }
                else
                {
                    curHealth += pickupValue;
                }
            }
            healthBar.ChangeHealth(curHealth, maxHealth);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "RespawnPlatform")
            {
                transform.position = respawnPoint;
            }

            //Respawns to last checkpoint once we implement that
            else if(other.tag == "Checkpoint")
            {
                respawnPoint = other.transform.position;
            }
            else if (other.tag.Equals("Finish"))
            {
               
                GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GameObject.FindGameObjectWithTag("Finish").GetComponentInChildren<Canvas>().enabled = true;

                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(enemy);
                }
               
            }
            

        }
        

        /*
        public bool getFacingDirection()
        {
            return m_FacingRight;
        }
        */
    }
}
