using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerrainScaledEvents : TerrainScaledEffectBase
{
    [Tooltip("This is the threshold for triggering the enter and stay events. If set to 0.1, the events will fire as soon as there is even a hint of the effect texture painted. If set to 1.0, the events will only fire if the effect texture is fully painted.")]
    [SerializeField, Range(.1f, 1f)] float _enterTreshold = 0.5f;

    [Tooltip("This is the leaniency for the exit event. If the enter threshold is set to 0.5 and the exit leaniency is set to 0.1, then the enter event will fire at 0.5, while the exit event will fire once the effect texture alpha goes below 0.4. This minimizes the tendency to bounce between enter and exit when moving along the exact threshold line.")]
    [SerializeField, Range(0f, .9f)] float _exitLeaniency = 0.1f;

    [Tooltip("This event triggers once when entering the effect area.")]
    [SerializeField] UnityEvent _onEnter;

    [Tooltip("This event triggers once when leaving the effect area.")]
    [SerializeField] UnityEvent _onExit;

    [Tooltip("This event triggers every frame while inside the effect area.")]
    [SerializeField] UnityEvent _onInside;

    [Tooltip("This event triggers each frame while outside the effect area.")]
    [SerializeField] UnityEvent _onOutside;

    private bool _hasEntered;

    private void Update()
    {
        var effectWeight = GetEffectWeight();

        if (!_hasEntered && effectWeight > _enterTreshold)
        {
            _hasEntered = true;
            _onEnter?.Invoke();
        }
        else if (_hasEntered && effectWeight < _enterTreshold - _exitLeaniency)
        {
            _hasEntered = false;
            _onExit?.Invoke();
        }
        else if (!_hasEntered)
            _onOutside?.Invoke();
        else
            _onInside?.Invoke();
    }
}
