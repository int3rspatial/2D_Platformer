using Assets.Scripts.Core.Movement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Movement.Controller
{
    public class HorizontalMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly HorizontalMovementData _horizontalMovementData;

        private Vector2 _movement;

        public bool FacingRight { get; private set; }
        public bool IsMoving => _movement.magnitude > 0;

        public HorizontalMover(Rigidbody2D rigidbody, HorizontalMovementData horizontalMovementData)
        {
            _rigidbody = rigidbody;
            _transform = rigidbody.transform;
        }


        public void MoveHorizontally(float direction)
        {
            _movement.x = direction;
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalMovementData.HorizontalSpeed;
            _rigidbody.velocity = velocity;
        }
        private void SetDirection(float direction)
        {
            if ((_horizontalMovementData.FaceRight && direction < 0) ||
                (!_horizontalMovementData.FaceRight && direction > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            _transform.Rotate(0, 180, 0);
            _horizontalMovementData.FaceRight = !_horizontalMovementData.FaceRight;
        }
    }
}
