using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWindow : MonoBehaviour
{
    [SerializeField] SpawnWindowButton buttonPrefab;

    public void Init(List<GameObject> prefabs, Action<GameObject> onPrefabSelect)
    {
        foreach(var pref in prefabs)
        {
            SpawnWindowButton button = Instantiate(buttonPrefab, transform);
            button.Init(
                () =>
                {
                    onPrefabSelect?.Invoke(pref);
                    HideWindow();
                }, pref.name);
        }

        //hide window button
        SpawnWindowButton exitButton = Instantiate(buttonPrefab, transform);
        exitButton.Init(
            () => HideWindow(), "close");
    }

    public void ShowWindow()
    {
        gameObject.SetActive(true);
    }

    public void HideWindow()
    {
        gameObject.SetActive(false);
    }
}
