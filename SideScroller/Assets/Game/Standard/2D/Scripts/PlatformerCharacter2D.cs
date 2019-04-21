using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wep;

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
        private bool mDead = false;
        
        private bool canDoubleJump = true;

        private Transform m_curMovingPlatform = null;

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

                //Handle death events here
                mDead = true;

                transform.position = respawnPoint;
                curHealth = 200;
                lives--;
                if (lives > -1)
                {
                    lifeDisplay.Die();
                }
                else
                {
                    //Load the Gameover scene
                    SceneManager.LoadScene(5);
                }
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

        public void AddAmmo(bool[] ammoType)
        {
            //0 = bullet
            //1 = shotgun
            //2 = rocket

            bool hadToEnableWeapon = false;
            if (ammoType[0])
            {
                //rifle ammo

                GameObject rifleObject = gameObject.transform.Find("RifleA").gameObject;
                Rifle rifle = gameObject.GetComponentInChildren<Rifle>();

                if (rifle == null)
                {
                    rifleObject.SetActive(true);
                    rifle = rifleObject.GetComponent<Rifle>();
                    hadToEnableWeapon = true;
                }

                rifle.clipCount = rifle.clipCount + 2;

                
                if (hadToEnableWeapon)
                {
                    rifleObject.SetActive(false);
                }
                else
                {
                    rifle.SetAmmoText();
                }
            }
            else if (ammoType[1])
            {
                //shotgun ammo
                GameObject shotgunObject = gameObject.transform.Find("ShotgunA").gameObject;
                Shotgun shotgun = gameObject.GetComponentInChildren<Shotgun>();

                if (shotgun == null)
                {
                    shotgunObject.SetActive(true);
                    shotgun = shotgunObject.GetComponent<Shotgun>();
                    hadToEnableWeapon = true;
                }

                shotgun.clipCount = shotgun.clipCount + 2;


                if (hadToEnableWeapon)
                {
                    shotgunObject.SetActive(false);
                }
                else
                {
                    shotgun.SetAmmoText();
                }
            }
            else if (ammoType[2])
            {
                //rocket ammo
                GameObject RLObject = gameObject.transform.Find("RocketLauncher").gameObject;
                RocketLauncher rocketLauncher = gameObject.GetComponentInChildren<RocketLauncher>();

                if (rocketLauncher == null)
                {
                    RLObject.SetActive(true);
                    rocketLauncher = RLObject.GetComponent<RocketLauncher>();
                    hadToEnableWeapon = true;
                }

                rocketLauncher.clipCount = rocketLauncher.clipCount + 2;


                if (hadToEnableWeapon)
                {
                    RLObject.SetActive(false);
                }
                else
                {
                    rocketLauncher.SetAmmoText();
                }
            }
            

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
                //EndLevelIndicator Logic

                SceneManager.LoadScene(4);
                /**GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GameObject.FindGameObjectWithTag("Finish").GetComponentInChildren<Canvas>().enabled = true;

                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(enemy);
                }**/


               
            }
            

        }

        void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == "MovingPlatformSurface") {
                m_curMovingPlatform = other.gameObject.transform;
                transform.SetParent(m_curMovingPlatform);
            }
        }

        void OnCollisionExit2D(Collision2D other) {
            if (other.gameObject.tag == "MovingPlatformSurface") {
                transform.parent = null;
                m_curMovingPlatform = null;
            }
        }
        
        private void PlayerDeath()
        {
            m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            foreach(Rigidbody2D child in this.GetComponentsInChildren<Rigidbody2D>())
            {
                child.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            Transform deathScreen = GameObject.FindGameObjectWithTag("HUD").transform.GetChild(3);
            var deathText = deathScreen.GetChild(0);
            StartCoroutine(FadeInDeath(deathScreen, deathText, 1f, 5f));
            //StartCoroutine(FadeInDeath(deathScreen, deathText, 1f, 5f));
            //Time.timeScale = 0.5f;
        }


        IEnumerator FadeInDeath(Transform material1, Transform material2, float targetOpacity, float duration)
        {
            // Cache the current color of the material, and its initiql opacity.
            Color color1 = material1.GetComponent<Image>().color;
            Color color2 = material2.GetComponent<Text>().color;
            float startOpacity1 = color1.a;
            float startOpacity2 = startOpacity1;

            // Track how many seconds we've been fading.
            float t = 0;

            while (t < duration)
            {
                // Step the fade forward one frame.
                t += Time.deltaTime;
                // Turn the time into an interpolation factor between 0 and 1.
                float blend = Mathf.Clamp01(t / duration);

                // Blend to the corresponding opacity between start & target.
                color1.a = Mathf.Lerp(startOpacity1, targetOpacity, blend);
                color2.a = Mathf.Lerp(startOpacity2, targetOpacity, blend);

                // Apply the resulting color to the material.
                material1.GetComponent<Image>().color = color1;
                material2.GetComponent<Text>().color = color2;


                // Wait one frame, and repeat.
                yield return null;
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
