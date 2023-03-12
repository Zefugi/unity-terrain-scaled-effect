using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TerrainScaledCustomVignetteEffect : TerrainScaledEffectBase
{
    [SerializeField] Volume _postProcessingVolume;
    [SerializeField, Range(0, 1)] float _smoothingEffect = .275f;

    private Vignette _vignette;
    private float _smoothDampVelocity;

    protected override void Awake()
    {
        base.Awake();

        foreach (var component in _postProcessingVolume.profile.components)
            if (component is Vignette vignette)
                _vignette = vignette;
    }

    private void Update()
    {
        var effectWeight = GetEffectWeight();

        _vignette.intensity.value = Mathf.SmoothDamp(_vignette.intensity.value, effectWeight, ref _smoothDampVelocity, _smoothingEffect);
    }
}
