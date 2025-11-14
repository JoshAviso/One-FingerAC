using System;
using UnityEngine;

[Serializable] public struct MovementStats
{
    [Header("Move Speed")]
    public float _maxHorizontalSpeed;
    public float _maxVerticalSpeed;
    [Header("Boost Force")]
    public float _horizontalBoostForce;
    public float _verticalBoostForce;
    public float _forwardBoostForce;
    public float _reverseBoostForce;
}
