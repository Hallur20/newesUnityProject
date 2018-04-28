using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : NetworkBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {

            CmdAnimationsExistOnServer();
                
            
        }


        private void Update()
        {
            if (!hasAuthority)
            {
                return;
            }/*
            if (!m_Jump)
            {
                CmdJump();
                
            }
            */
        }


        private void FixedUpdate()
        {

            if (!hasAuthority) {
                return;
            } else {
            CmdSendAnimationsToServer();
            }
        }

        [Command]
        void CmdAnimationsExistOnServer() {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        [Command]
        void CmdSendAnimationsToServer() {
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            RpcSendAnimationsToClient(h, crouch, m_Jump);
        }
        [ClientRpc]
        void RpcSendAnimationsToClient(float h, bool crouch, bool m_Jump) {
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
