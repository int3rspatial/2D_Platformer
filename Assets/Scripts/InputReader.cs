using Player;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;

    private float _horizontalDirection;
    //private float _verticalDirection;

    private void Update()
    {
       _horizontalDirection = Input.GetAxisRaw("Horizontal"); //-1 0 1
       //_verticalDirection = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            _playerEntity.Jump();
        }
    }

    private void FixedUpdate()
    {
        _playerEntity.MoveHorizontally(_horizontalDirection);
        //_playerEntity.MoveVertically(_verticalDirection);
    }
}