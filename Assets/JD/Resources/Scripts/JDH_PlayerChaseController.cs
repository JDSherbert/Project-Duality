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
            public Vector3 forcedMovement = new Vector3(1,0,0);
            public float verticalSpeed = 1.0f;
        }
        public ChaseControllerSettings chaser = new ChaseControllerSettings();

        void Update()
        {
            InputHandler();
            IdleState(); 
            ChaseHandler();
        }

        void ChaseHandler()
        {
            transform.position += chaser.forcedMovement * Time.deltaTime;
            player.events.OnWalkRight.Invoke();
            MoveState();
        }

        public override void InputHandler()
        {
            player.input.AXIS_VERTICAL = Input.GetAxis(player.input.VerticalAxis);

            if (player.input.AXIS_VERTICAL > 0)
            {
                transform.position += (Vector3.up * chaser.verticalSpeed) * Time.deltaTime;
                player.events.OnWalkUp.Invoke();
            }
            else if (player.input.AXIS_VERTICAL < 0) 
            {
                transform.position += (Vector3.down * chaser.verticalSpeed) * Time.deltaTime;
                player.events.OnWalkDown.Invoke();
            }
        }

        public override void IdleState()
        {
            player.locomotion.start = transform.position;
            player.locomotion.end = player.locomotion.start + player.locomotion.nextMoveCommand;
            player.locomotion.distance = (player.locomotion.end - player.locomotion.start).magnitude;
            MoveState();
        }

        public override void MoveState()
        {
            player.locomotion.velocity = Mathf.Clamp01(player.locomotion.velocity + Time.deltaTime * player.locomotion.acceleration);
            UpdateAnimator(chaser.forcedMovement);
        }
    }
}
