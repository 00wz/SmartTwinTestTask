using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MeshDraw
{
    public class InputHundler : MonoBehaviour
    {
        [SerializeField] public UnityEvent<Vector3, Vector3> defaultClickAction;

        readonly Plane CLICKABLE_PLANE = new Plane(Vector3.forward, Vector3.zero);

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit) &&
                    hit.collider.TryGetComponent<IClickable>(out IClickable clickable))
                {
                    clickable.OnClick(hit.point, Input.mousePosition);
                }
                else
                {
                    if (CLICKABLE_PLANE.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        defaultClickAction.Invoke(hitPoint, Input.mousePosition);
                    }
                }
            }
        }
    }
}