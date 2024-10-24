using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MeshDraw
{
    [RequireComponent(typeof(Button))]
    public class SpawnWindowButton : MonoBehaviour
    {
        [SerializeField] Text text;

        public void Init(UnityAction onClick, string label)
        {
            GetComponent<Button>().onClick.AddListener(onClick);
            text.text = label;
        }
    }
}