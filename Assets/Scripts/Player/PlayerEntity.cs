using Assets.Scripts.Core.Animation;
using Assets.Scripts.Core.Movement.Controller;
using Assets.Scripts.Core.Movement.Data;
using Assets.Scripts.Player;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private AnimationController _animator;

        //[Header("HorizontalMovement")]
        [SerializeField] private HorizontalMovementData _horizontalMovementData;

        [SerializeField] private JumpData _jumpData;
        //[Header("Jump")]
        //[SerializeField] private float _jumpForce;
        //[SerializeField] private float _gravityScale;       
        //[SerializeField] private Transform _groundCheck;
        //[SerializeField] private float _groundCheckRadius;
        //[SerializeField] private LayerMask _groundLayer;
        
        private Rigidbody2D _rigidbody;
        
        ////for jumping mechanics
        //private bool _isOnGround;
        //private bool _isJumping;
        //private int counter;

        //for animation
        private AnimationType _currentAnimationType;

        private HorizontalMover _horizontalMover;
        private Jumper _jumper;
        

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _horizontalMover = new HorizontalMover(_rigidbody, _horizontalMovementData);

            _jumper = new Jumper(_rigidbody, _jumpData);

            _jumper.IsOnGroundVar = _jumper.IsOnGround();
            _jumper.IsJumping = false;
            _jumper.Counter = 0;
        }

        private void Update()
        {
            if (_jumper.IsJumping)
            {
                _jumper.Counter++;
                UpdateJump();
            }

            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Idle, true);
            _animator.PlayAnimation(AnimationType.Walk, _horizontalMover.IsMoving);
            _animator.PlayAnimation(AnimationType.Jump, _jumper.IsJumping);
        }

        public void Jump()
        {
            _jumper.Jump();
        }

        private void UpdateJump()
        {
            _jumper.UpdateJump();
        }

        public void MoveHorizontally(float direction)
        {
            _horizontalMover.MoveHorizontally(direction);
        }

        public void StartAttack()
        {
            if (!_animator.PlayAnimation(AnimationType.Attack, true))
                return;

            _animator.ActionRequested += Attack;
            _animator.AnimationEnded += EndAttack;
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }

        private void EndAttack()
        {
            _animator.ActionRequested -= Attack;
            _animator.AnimationEnded -= EndAttack;
            _animator.PlayAnimation(AnimationType.Attack, false);
        }
    }
}