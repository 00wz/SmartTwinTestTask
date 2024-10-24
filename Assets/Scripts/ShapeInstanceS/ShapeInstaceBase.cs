using UnityEngine;

namespace MeshDraw
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public abstract class ShapeInstaceBase : MonoBehaviour, IClickable
    {
        [SerializeField] GameObject editingWindow;

        private Mesh _mesh;
        private MeshCollider _meshCollider;
        private Shape _shape;

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
            _meshCollider = GetComponent<MeshCollider>();
            editingWindow.SetActive(false);
        }

        private void Start()
        {
            RedrawMesh();
        }

        public void SetColor(Color color)
        {
            _shape.color = color;
        }

        protected void RedrawMesh()
        {
            _shape = CreateShape();
            _shape.AssignMesh(_mesh);
            _meshCollider.sharedMesh = _mesh;
        }

        protected abstract Shape CreateShape();

        public void OnClick(Vector3 clickWorldPosition, Vector3 clickScreenPosition)
        {
            editingWindow.transform.position = clickScreenPosition;
            editingWindow.SetActive(true);
        }

        public void Delete()
        {
            Destroy(gameObject);
        }
    }
}