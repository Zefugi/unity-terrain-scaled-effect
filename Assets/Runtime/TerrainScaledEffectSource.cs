using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainScaledEffectSource : MonoBehaviour
{
    public float GetEffectWeight(Vector3 worldPositon, int layerIndex, bool clampPosition = false)
    {
        if (layerIndex >= _terrainData.alphamapLayers)
            throw new UnityException($"The parameter {nameof(layerIndex)} is higher than the number of layers in the specified terrain.");

        if (_alphaMapsCachePosition == worldPositon && _alphaMapsCacheSeconds == Time.time)
            return _alphaMapsCache[0, 0, layerIndex];

        var alphaMapPosition = GetAlphaTileLocation(worldPositon, clampPosition);
        if (!clampPosition &&
            (alphaMapPosition.x < 0 || alphaMapPosition.x > _alphaSize.x - 1 ||
            alphaMapPosition.y < 0 || alphaMapPosition.y > _alphaSize.z - 1))
            return 0f;
        
        _alphaMapsCache = _terrainData.GetAlphamaps(alphaMapPosition.x, alphaMapPosition.y, 1, 1);
        _alphaMapsCachePosition = worldPositon;
        _alphaMapsCacheSeconds = Time.time;

        return _alphaMapsCache[0, 0, layerIndex];
    }

    public Vector2Int GetAlphaTileLocation(Vector3 worldPositon, bool clamp = false)
    {
        var relativePoisition = worldPositon - _terrain.transform.position;
        Vector3 terrainPosition = new(
            relativePoisition.x * _positionConversion.x,
            0,
            relativePoisition.z * _positionConversion.z
            );

        if (clamp)
        {
            terrainPosition.x = Mathf.Clamp(terrainPosition.x, 0, _alphaSize.x - 1);
            terrainPosition.z = Mathf.Clamp(terrainPosition.z, 0, _alphaSize.z - 1);
        }

        return new Vector2Int(Mathf.FloorToInt(terrainPosition.x), Mathf.FloorToInt(terrainPosition.z));
    }



    private Terrain _terrain;
    private TerrainData _terrainData;
    private Vector3 _alphaSize;
    private Vector3 _positionConversion;

    private float[,,] _alphaMapsCache;
    private Vector3 _alphaMapsCachePosition;
    private float _alphaMapsCacheSeconds;

    private void Awake()
    {
        _terrain = GetComponent<Terrain>();
        _terrainData = _terrain.terrainData;

        ComputeTransformationVectors();
    }

    private void ComputeTransformationVectors()
    {
        Vector3 terrainSize = new(_terrainData.size.x, 0, _terrainData.size.z);
        _alphaSize = new(_terrainData.alphamapWidth, 0, _terrainData.alphamapHeight);
        _positionConversion = new(
            (_alphaSize.x - 1f) / terrainSize.x,
            1,
            (_alphaSize.z - 1f) / terrainSize.z
            );
    }
}
