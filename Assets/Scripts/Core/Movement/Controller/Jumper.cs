using Assets.Scripts.Core.Movement.Data;
using UnityEngine;

namespace Assets.Scripts.Core.Movement.Controller
{
    public class Jumper
    {
        private readonly JumpData _jumpData;
        private readonly Rigidbody2D _rigidbody;

        public bool IsOnGroundVar { get; set; }
        public bool IsJumping { get; set; }
        public int Counter { get; set; }

        public Jumper(Rigidbody2D rigidbody, JumpData jumpData)
        {
            _rigidbody = rigidbody;
            _jumpData = jumpData;
        }

        public void Jump()
        {
            if (!IsOnGroundVar)
            {
                return;
            }
            IsOnGroundVar = false;
            IsJumping = true;
            _rigidbody.AddForce(Vector2.up * _jumpData._jumpForce);
            _rigidbody.gravityScale = _jumpData._gravityScale;
        }

        public bool IsOnGround()
        {
            return Physics2D.OverlapCircle(_jumpData._groundCheck.position, _jumpData._groundCheckRadius, _jumpData._groundLayer);
        }

        public void UpdateJump()
        {
            if (IsOnGroundVar && Counter > 2)
            {
                ResetJump();
                return;
            }
            IsOnGroundVar = IsOnGround();
        }

        private void ResetJump()
        {
            IsJumping = false;
            Counter = 0;
        }
    }
}
