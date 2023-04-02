using System;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private List<ParallaxLayer> _layers;
    [SerializeField] private Transform _target;

    private float _previousTargetPositionHorizontal;
    private float _previousTargetPositionVertical;

    private void Start()
    {
        _previousTargetPositionHorizontal = _target.position.x;
        _previousTargetPositionVertical = _target.position.y;
    }

    private void LateUpdate()
    {
        float deltaMovementHorizontal = _previousTargetPositionHorizontal - _target.position.x;
        float deltaMovementVertical = _previousTargetPositionVertical - _target.position.y;
        foreach (var layer in _layers)
        {
            Vector2 layerPosition = layer.Layer.position;
            layerPosition.x += deltaMovementHorizontal * layer.HorizontalSpeed;
            layerPosition.y += deltaMovementVertical * layer.VerticalSpeed;
            layer.Layer.position = layerPosition;
        }
        _previousTargetPositionHorizontal = _target.position.x;
        _previousTargetPositionVertical = _target.position.y;
    }

    [Serializable]
    private class ParallaxLayer
    {
        [field: SerializeField] public Transform Layer { get; private set; }
        [field: SerializeField] public float HorizontalSpeed { get; private set; }
        [field: SerializeField] public float VerticalSpeed { get; private set; }
    }
}
