using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TerrainScaledExampleEffect : TerrainScaledEffectBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        var effectWeight = GetEffectWeight();

        Debug.Log($"I am now {Mathf.RoundToInt(effectWeight * 100f)}% on the effect area.");
    }
}
