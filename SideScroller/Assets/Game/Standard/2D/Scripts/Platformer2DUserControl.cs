using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Wep;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private Wep.Weapon m_Weapon = null;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            // Get child by index because different weapons have different names
            if (transform.childCount >= 3) {
                m_Weapon = transform.GetChild(2).gameObject.GetComponent<Wep.Weapon>();
            }
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            bool upPressed = Input.GetKey(KeyCode.W);
            bool downPressed = Input.GetKey(KeyCode.S);
            bool rightPressed = Input.GetKey(KeyCode.D);
            bool leftPressed = Input.GetKey(KeyCode.A);
            bool firing = Input.GetButtonDown("Fire1");
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump, upPressed, downPressed, rightPressed, leftPressed);
            // Pass all parameters to the weapon action function
            if (m_Weapon != null) {
                m_Weapon.Action(firing, upPressed, downPressed, rightPressed, leftPressed);
            }
            m_Jump = false;
        }
    }
}
