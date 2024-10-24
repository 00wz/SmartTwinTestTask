using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    public void OnClick(Vector3 clickWorldPosition, Vector3 clickScreenPosition);
}
