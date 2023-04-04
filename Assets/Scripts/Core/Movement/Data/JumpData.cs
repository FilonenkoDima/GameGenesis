using System;
using UnityEngine;

namespace InputReader.Movement.Data
{
    [Serializable]
    public class JumpData
    {
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public int GravityScale { get; private set; }
    }
}