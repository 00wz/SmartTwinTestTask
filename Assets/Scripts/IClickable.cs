using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshDraw
{
    public interface IClickable
    {
        public void OnClick(Vector3 clickWorldPosition, Vector3 clickScreenPosition);
    }
}