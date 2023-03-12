using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TerrainScaledEffectBase : MonoBehaviour
{
    [Tooltip("The terrain from which to sample the texture alpha for effect activation and intensity.")]
    [SerializeField] Terrain _terrain;
    
    [Tooltip("The index of the terrain layer, from which to sample the texture alpha for effect activation and intensity.")]
    [SerializeField, Range(0, 15)] int _effectLayerIndex;

    [Tooltip("If checked; the sample position will be confined to the terrain, and samples outside of the terrain will return the nearest value.\nIf unchecked; the sample position will not be confined and samples outside of the terrain will return 0.")]
    [SerializeField] bool _clampPositions;

    public float GetEffectWeight() => GetEffectWeight(transform.position);
    public float GetEffectWeight(Vector3 worldPositon) => _effectSource.GetEffectWeight(worldPositon, _effectLayerIndex, _clampPositions);

    public Vector2Int GetAlphaTileLocation() => GetAlphaTileLocation(transform.position);
    public Vector2Int GetAlphaTileLocation(Vector3 worldPositon) => _effectSource.GetAlphaTileLocation(worldPositon, _clampPositions);



    private TerrainScaledEffectSource _effectSource;
    
    protected virtual void Awake()
    {
        if (_terrain == null)
        {
            Debug.LogError($"The field {nameof(_terrain)} has not been set on this {nameof(TerrainScaledEffectBase)}. Please drag the game object containing the terrain into the {nameof(_terrain)} field on this {nameof(TerrainScaledEffectBase)} component.", this);
            enabled = false;
            return;
        }
        if (_effectLayerIndex >= _terrain.terrainData.alphamapLayers)
        {
            Debug.LogError($"The field {nameof(_effectLayerIndex)} is higher than the number of layers in the specified terrain. The {nameof(_effectLayerIndex)} is set to {_effectLayerIndex} but the terrain only contains {_terrain.terrainData.alphamapLayers} layers from index 0 to index {(_terrain.terrainData.alphamapLayers - 1)}", this);
            enabled = false;
            return;
        }

        if (!_terrain.TryGetComponent<TerrainScaledEffectSource>(out _effectSource))
            _effectSource = _terrain.gameObject.AddComponent<TerrainScaledEffectSource>();
    }
}
