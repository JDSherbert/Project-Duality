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
    using UnityEngine.U2D;
    using UnityEngine.Events;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Two dimensional controller for a side scroller.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_PlayerController2D : MonoBehaviour
    {
        [System.Serializable]
        public class PlayerControllerSettings
        {
            public enum State
            {
                Idle, Moving, Stop
            }

            public State state = State.Idle;

            [System.Serializable]
            public class InputSettings
            {
                public string HorizontalAxis = "Horizontal";
                public float AXIS_HORIZONTAL;
                public string VerticalAxis = "Vertical";
                public float AXIS_VERTICAL;
                public string JumpAxis = "Jump";
                public float AXIS_JUMP;
            }
            public InputSettings input = new InputSettings();

            [System.Serializable]
            public class LocomotionSettings
            {
                public float stepSize = 0.1f;
                public float speed = 15.0f;
                public float acceleration = 0.01f;
                public Vector3 nextMoveCommand;

                public Vector3 start, end;
                public Vector2 currentVelocity; //! Must be Vec2 for smoothdamp
                public float startTime;
                public float distance;
                public float velocity;

                [Tooltip("Play the sprite's LEFT animation but flipped when walking.")]
                public bool useFlipX = false;
            }
            public LocomotionSettings locomotion = new LocomotionSettings();

            [System.Serializable]
            public class Components
            {
                public Animator animator;
                public Rigidbody2D rigidbody2D;
                public SpriteRenderer spriteRenderer;
                public PixelPerfectCamera pixelPerfectCamera;
            }
            public Components component = new Components();

            [System.Serializable]
            public class Events
            {
                public UnityEvent OnWalkUp;
                public UnityEvent OnWalkDown;
                public UnityEvent OnWalkLeft;
                public UnityEvent OnWalkRight;
                public UnityEvent OnJump;
            }
            public Events events = new Events();
        }

        public PlayerControllerSettings player = new PlayerControllerSettings();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            Init();
        }

        void Update()
        {
            if(player.state != PlayerControllerSettings.State.Stop) InputHandler();
            PlayerStateMachine();
        }

        void LateUpdate()
        {
            CameraHandler();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public virtual void InputHandler()
        {
            player.input.AXIS_HORIZONTAL = Input.GetAxis(player.input.HorizontalAxis);
            player.input.AXIS_VERTICAL = Input.GetAxis(player.input.VerticalAxis);
            player.input.AXIS_JUMP = Input.GetAxis(player.input.JumpAxis);

            if (player.input.AXIS_VERTICAL > 0)
                player.locomotion.nextMoveCommand = Vector3.up * player.locomotion.stepSize;
            else if (player.input.AXIS_VERTICAL < 0)
                player.locomotion.nextMoveCommand = Vector3.down * player.locomotion.stepSize;
            else if (player.input.AXIS_HORIZONTAL < 0)
                player.locomotion.nextMoveCommand = Vector3.left * player.locomotion.stepSize;
            else if (player.input.AXIS_HORIZONTAL > 0)
                player.locomotion.nextMoveCommand = Vector3.right * player.locomotion.stepSize;
            else
                player.locomotion.nextMoveCommand = Vector3.zero;
        }

        void PlayerStateMachine()
        {
            switch (player.state)
            {
                case PlayerControllerSettings.State.Idle:
                    IdleState();
                    break;
                case PlayerControllerSettings.State.Moving:
                    MoveState();
                    break;
                case PlayerControllerSettings.State.Stop:
                    StopActions();
                    break;
                
            }
        }

        public virtual void IdleState()
        {
            if (player.locomotion.nextMoveCommand != Vector3.zero)
            {
                player.locomotion.start = transform.position;
                player.locomotion.end = player.locomotion.start + player.locomotion.nextMoveCommand;
                player.locomotion.distance = (player.locomotion.end - player.locomotion.start).magnitude;
                player.locomotion.velocity = 0;

                UpdateAnimator(player.locomotion.nextMoveCommand);
                player.locomotion.nextMoveCommand = Vector3.zero;
                player.state = PlayerControllerSettings.State.Moving;
            }
            else
            {
                player.state = PlayerControllerSettings.State.Idle;
                player.locomotion.velocity = 0;
            }
        }

        public virtual void MoveState()
        {
            player.locomotion.velocity = Mathf.Clamp01(player.locomotion.velocity + Time.deltaTime * player.locomotion.acceleration);
            UpdateAnimator(player.locomotion.nextMoveCommand);
            SmoothStopping();

            //? Flip Sprite
            if (player.component.spriteRenderer && player.locomotion.useFlipX)
                player.component.spriteRenderer.flipX = player.locomotion.nextMoveCommand.x >= 0 ? true : false;
        }

        public void StopActions() //! Will need to be taken out of this state
        {
            player.state = PlayerControllerSettings.State.Stop;
            player.locomotion.nextMoveCommand = Vector3.zero;
            player.locomotion.velocity = 0;
            player.component.rigidbody2D.velocity = new Vector2(0,0);
            UpdateAnimator(Vector3.zero);
            SmoothStopping();
        }
        public void ResumeActions()
        {
            player.state = PlayerControllerSettings.State.Idle;
        }

        public void UpdateAnimator(Vector3 direction)
        {
            if (player.component.animator && !player.locomotion.useFlipX)
            {
                player.component.animator.SetInteger("WalkX", direction.x < 0 ? -1 : direction.x > 0 ? 1 : 0);
                player.component.animator.SetInteger("WalkY", direction.y < 0 ? -1 : direction.y > 0 ? 1 : 0);
            }
            else //? Use LEFT sprite flipped
            {
                player.component.animator.SetInteger("WalkX", direction.x < 0 ? -1 : direction.x > 0 ? -1 : 0);
                player.component.animator.SetInteger("WalkY", direction.y < 0 ? -1 : direction.y > 0 ? 1 : 0);
            }
            UpdateControllerEvents(direction);
        }

        void UpdateControllerEvents(Vector3 direction)
        {
            if (direction.x < 0) player.events.OnWalkLeft.Invoke();
            if (direction.x > 0) player.events.OnWalkRight.Invoke();
            if (direction.y < 0) player.events.OnWalkDown.Invoke();
            if (direction.y > 0) player.events.OnWalkUp.Invoke();
        }

        void CameraHandler()
        {
            if (player.component.pixelPerfectCamera)
            {
                transform.position = player.component.pixelPerfectCamera.RoundToPixel(transform.position);
            }
        }

        void SmoothStopping()
        {
            //? Smooth Stopping
            player.component.rigidbody2D.velocity = Vector2.SmoothDamp(
                player.component.rigidbody2D.velocity,
                player.locomotion.nextMoveCommand * player.locomotion.speed,
                ref player.locomotion.currentVelocity,
                player.locomotion.acceleration,
                player.locomotion.speed);
        }

        void Init()
        {
            if (!player.component.rigidbody2D && GetComponent<Rigidbody2D>()) player.component.rigidbody2D = GetComponent<Rigidbody2D>();
            else if (!player.component.rigidbody2D && !GetComponent<Rigidbody2D>()) player.component.rigidbody2D = new Rigidbody2D();

            if (!player.component.spriteRenderer && GetComponentInChildren<SpriteRenderer>()) player.component.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            else if (!player.component.spriteRenderer && !GetComponentInChildren<SpriteRenderer>()) Debug.LogWarning("No Sprite Renderer found as a child of this gameobject.");

            if (!player.component.animator && GetComponentInChildren<Animator>()) player.component.animator = GetComponentInChildren<Animator>();
            else if (!player.component.animator && !GetComponentInChildren<Animator>()) Debug.LogWarning("No animator found as a child of this gameobject.");

            if (!player.component.pixelPerfectCamera) player.component.pixelPerfectCamera = GameObject.FindObjectOfType<PixelPerfectCamera>();
            else if (!player.component.pixelPerfectCamera && !GameObject.FindObjectOfType<PixelPerfectCamera>()) Debug.LogWarning("No Pixel Perfect Camera found.");
        }
    }
}

