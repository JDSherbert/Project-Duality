/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Framework
{
    using UnityEngine;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Two dimensional controllerextending from the base2D controller for chase scenes.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_PlayerChaseController : JDH_PlayerController2D
    {
        [System.Serializable]
        public class ChaseControllerSettings
        {
            public const int MAGICNUMBER = 3;
            public Vector3 forcedMovement = new Vector3(1,0,0);
            public float verticalSpeed = 1.0f;
        }
        public ChaseControllerSettings chaser = new ChaseControllerSettings();

        void Update()
        {
            InputHandler();
            MoveState();
            ChaseHandler();
        }

        void ChaseHandler()
        {
            transform.position += (chaser.forcedMovement / ChaseControllerSettings.MAGICNUMBER) * Time.fixedDeltaTime;
            player.events.OnWalkRight.Invoke();
            MoveState();
        }

        public override void InputHandler()
        {
            player.input.AXIS_VERTICAL = Input.GetAxis(player.input.VerticalAxis);

            if (player.input.AXIS_VERTICAL > 0)
            {
                transform.position += ((Vector3.up * chaser.verticalSpeed) / ChaseControllerSettings.MAGICNUMBER) * Time.fixedDeltaTime;
                player.events.OnWalkUp.Invoke();
            }
            else if (player.input.AXIS_VERTICAL < 0) 
            {
                transform.position += ((Vector3.down * chaser.verticalSpeed)/ ChaseControllerSettings.MAGICNUMBER) * Time.fixedDeltaTime;
                player.events.OnWalkDown.Invoke();
            }
        }

        public override void IdleState()
        {
            MoveState();
        }

        public override void MoveState()
        {
            player.state = PlayerControllerSettings.State.Moving;
            UpdateAnimator(chaser.forcedMovement);
        }
    }
}
