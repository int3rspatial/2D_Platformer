using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private Animator _animator;

        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;       
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundLayer;

        private Rigidbody2D _rigidbody;
        private bool _isOnGround;

        private Vector2 _movement;
        private AnimationType _currentAnimationType;
        private bool _isJumping;

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!_isOnGround)
            {
                UpdateJump();
            }

            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            PlayAnimation(AnimationType.Idle, true);
            PlayAnimation(AnimationType.Walk, _movement.magnitude > 0);
            PlayAnimation(AnimationType.Jump, _isJumping);
        }

        public void Jump()
        {
            if (!IsOnGround())
            {
                return;
            }
            _isOnGround = true;
            _rigidbody.AddForce(Vector2.up * _jumpForce);
            _rigidbody.gravityScale = _gravityScale;
            _isJumping = true;
        }

        private bool IsOnGround()
        {
            return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        private void UpdateJump()
        {
            _isOnGround = IsOnGround();

            if (_isOnGround)
            {
                ResetJump();
                return;
            }
        }

        private void ResetJump()
        {
            _isOnGround = false;
            _isJumping = false;
        }

        public void MoveHorizontally(float direction)
        {
            _movement.x = direction;
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
        }

        private void SetDirection(float direction)
        {
            if((_faceRight && direction < 0) || 
                (!_faceRight && direction > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }
        
        private void PlayAnimation(AnimationType animationType, bool active)
        {
            if (!active)
            {
                if (_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType)
                    return;

                _currentAnimationType = AnimationType.Idle;
                PlayAnimation(_currentAnimationType);
                return;
            }
            
            if (_currentAnimationType > animationType)
                return;

            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
        }

        private void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger(nameof(AnimationType), (int)animationType);
        }
    }
}