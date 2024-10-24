using System.Collections.Generic;
using UnityEngine;

public class ShapeInstanceSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private SpawnWindow spawnWindow;

    private Vector3 _spawnPosition;

    private void Awake()
    {
        spawnWindow.Init(prefabs, OnSpawnPrefabSelect);
        spawnWindow.HideWindow();
    }

    public void CallSpawnWindow(Vector3 spawnPosition, Vector3 windowPosition)
    {
        _spawnPosition = spawnPosition;
        spawnWindow.transform.position = windowPosition;
        spawnWindow.ShowWindow();
    }

    public void OnSpawnPrefabSelect(GameObject prefab)
    {
        Instantiate(prefab, _spawnPosition, Quaternion.identity, transform);
    }
}
