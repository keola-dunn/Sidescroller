using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
// using Wep;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        // m_Weapon no longer needed since weapon now updates on its own
        // private Wep.Weapon m_Weapon = null;
        private int weaponCount;
        private int startingWeaponIndex = 2;
        private int currentWeaponIndex;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            // Get child by index because different weapons have different names
            weaponCount = transform.childCount - startingWeaponIndex;
            currentWeaponIndex = startingWeaponIndex;

            // if (weaponCount > 0) {
            //     m_Weapon = transform.GetChild(currentWeaponIndex).gameObject.GetComponent<Wep.Weapon>();
            // }

            // Deactivate every weapon except current
            for (int i = currentWeaponIndex + 1; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            // Switch weapons
            checkWeaponSwitch();
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);

            /* Used to pass inputs to weapon
            bool upPressed = Input.GetKey(KeyCode.W);
            bool downPressed = Input.GetKey(KeyCode.S);
            bool rightPressed = Input.GetKey(KeyCode.D);
            bool leftPressed = Input.GetKey(KeyCode.A);
            bool firing = Input.GetButton("Fire1");
            bool reloading = Input.GetButtonDown("Reload");
            */

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);

            // Pass all parameters to the weapon action function
            // if (m_Weapon != null) {
            //     m_Weapon.Action(firing, reloading, upPressed, downPressed, rightPressed, leftPressed);
            // }

            m_Jump = false;
        }

        private void checkWeaponSwitch() 
        {
            int previousWeaponIndex = currentWeaponIndex;
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                if (currentWeaponIndex < transform.childCount - 1) {
                    currentWeaponIndex++;
                    Debug.Log(currentWeaponIndex);
                } else {
                    currentWeaponIndex = startingWeaponIndex;
                }
            } else if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                if (currentWeaponIndex > startingWeaponIndex) {
                    currentWeaponIndex--;
                } else {
                    currentWeaponIndex = transform.childCount - 1;
                }
            }
            if (weaponCount > 0 && Input.GetKeyDown(KeyCode.Alpha1)) {
                currentWeaponIndex = startingWeaponIndex;
            } else if (weaponCount > 1 && Input.GetKeyDown(KeyCode.Alpha2)) {
                currentWeaponIndex = startingWeaponIndex + 1;
            } else if (weaponCount > 2 && Input.GetKeyDown(KeyCode.Alpha3)) {
                currentWeaponIndex = startingWeaponIndex + 2;
            }
            if (previousWeaponIndex != currentWeaponIndex) {
                transform.GetChild(previousWeaponIndex).gameObject.SetActive(false);
                GameObject nextWeaponGameObject = transform.GetChild(currentWeaponIndex).gameObject;
                nextWeaponGameObject.SetActive(true);
                // m_Weapon = nextWeaponGameObject.GetComponent<Wep.Weapon>();
                // m_Weapon.setFacingDirection(m_Character.getFacingDirection());
            }
        }

    }
}
