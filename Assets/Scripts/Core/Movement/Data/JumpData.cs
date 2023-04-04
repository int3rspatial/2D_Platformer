using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Movement.Data
{
    [Serializable]
    public class JumpData
    {
        [field: SerializeField] public float _jumpForce;
        [field: SerializeField] public float _gravityScale;
        [field: SerializeField] public Transform _groundCheck;
        [field: SerializeField] public float _groundCheckRadius;
        [field: SerializeField] public LayerMask _groundLayer;
    }
}
