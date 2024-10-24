using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeshDraw
{
    public class ColorPanel : MonoBehaviour
    {
        [SerializeField] private ShapeInstaceBase prismInstance;

        void Start()
        {
            var buttons = GetComponentsInChildren<Button>();
            foreach(var button in buttons)
            {
                if(button.TryGetComponent<Image>(out Image image))
                {
                    button.onClick.AddListener(() => prismInstance.SetColor(image.color));
                }
            }
        }
    }
}