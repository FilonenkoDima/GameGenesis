using InputReader.Movement.Data;
using UnityEngine;

namespace InputReader.Movement.Controller
{
    public class Jumper
    {
        private readonly JumpData _jumpData;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _maxVerticalSize;
        private readonly Transform _transform;

        private float _startJumpVerticalPos;
        
        public bool IsJumping { get; private set; }

        public Jumper(Rigidbody2D rigidbody2D, JumpData jumpData, float maxVerticalSize)
        {
            _rigidbody = rigidbody2D;
            _jumpData = jumpData;
            _maxVerticalSize = maxVerticalSize;
            _transform = _rigidbody.transform;
        }

        public void Jump()
        {
            if(IsJumping)
                return;

            IsJumping = true;
            _startJumpVerticalPos = _rigidbody.position.y;
            var jumpModificator = _transform.localScale.y / _maxVerticalSize;
            var currentJumpForce = _jumpData.JumpForce * jumpModificator;
            _rigidbody.gravityScale = _jumpData.GravityScale * jumpModificator;
            _rigidbody.AddForce(Vector2.up * currentJumpForce);
        }

        public void UpdateJump()
        {
            if (_rigidbody.velocity.y < 0 && _transform.position.y < _startJumpVerticalPos)
            {
                ResetJump();
                return;
            }
        }

        private void ResetJump()
        {
            _rigidbody.gravityScale = 0;
            _transform.position = new Vector2(_transform.position.x, _startJumpVerticalPos);

            IsJumping = false;
        }
    }
}