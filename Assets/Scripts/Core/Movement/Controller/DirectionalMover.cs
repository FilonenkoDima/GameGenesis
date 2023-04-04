﻿using UnityEngine;
using Core.Enums;
using InputReader.Movement.Data;
using StatsSystem;
using StatsSystem.Enum;

namespace InputReader.Movement.Controller
{
    public class DirectionalMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly DirectionalMovementData _directionalMovementData;
        private readonly float _sizeModificator;
        private readonly IStatValueGiver _statValueGiver;

        private Vector2 _movement;

        public Direction Direction { get; private set; }
        public bool IsMoving => _movement.magnitude > 0;

        public DirectionalMover(Rigidbody2D rigidbody2D, DirectionalMovementData directionalMovementData,
            IStatValueGiver statValueGiver)
        {
            _rigidbody = rigidbody2D;
            _transform = rigidbody2D.transform;
            _directionalMovementData = directionalMovementData;
            _statValueGiver = statValueGiver;
            float positionDifference = _directionalMovementData.MaxVerticalPosition -
                                       _directionalMovementData.MinVerticalPosition;
            float sizeDifference = _directionalMovementData.MaxSize - _directionalMovementData.MinSize;
            _sizeModificator = sizeDifference / positionDifference;

            UpdateSize();
        }


        public void MoveHorizontally(float direction)
        {
            _movement.x = direction;
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _statValueGiver.GetStatValue(StatType.Speed);
            _rigidbody.velocity = velocity;
        }

        public void MoveVertically(float direction)
        {
            _movement.y = direction;
            Vector2 velocity = _rigidbody.velocity;
            velocity.y = direction * _statValueGiver.GetStatValue(StatType.Speed) / 2;
            _rigidbody.velocity = velocity;

            if (direction == 0)
                return;

            float verticalPosition = Mathf.Clamp(_transform.position.y, _directionalMovementData.MinVerticalPosition,
                _directionalMovementData.MaxVerticalPosition);
            _rigidbody.position = new Vector2(_rigidbody.position.x, verticalPosition);
            UpdateSize();
        }

        private void UpdateSize()
        {
            float verticalDelta = _directionalMovementData.MaxVerticalPosition - _transform.position.y;
            float currentSizeModificator = _directionalMovementData.MinSize + _sizeModificator * verticalDelta;
            _transform.localScale = Vector2.one * currentSizeModificator;
        }

        private void SetDirection(float direction)
        {
            if ((Direction == Direction.Right && direction < 0) ||
                (Direction == Direction.Left && direction > 0))
                Flip();
        }

        private void Flip()
        {
            _transform.Rotate(0, 180, 0);
            Direction = Direction == Direction.Right ? Direction.Left : Direction.Right;
        }
    }
}