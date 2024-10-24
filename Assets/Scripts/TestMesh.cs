using System.Collections.Generic;
using UnityEngine;

namespace MeshDraw
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class TestMesh : MonoBehaviour, IClickable
    {
        [SerializeField] MeshCollider meshCollider;
        [SerializeField] GameObject editingWindow;
        private Mesh _mesh;
        static int sidesCount = 6;
        //static int rows = 10;
        float height = 5f;
        private Shape _shape;

        private void Awake()
        {
            //editingWindow.SetActive(false);
        }

        void Start()
        {
            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();

            CreateMesh();

            _shape.AssignMesh(_mesh);
            meshCollider.sharedMesh = _mesh;
        }


        private void CreateMesh()
        {
            int doubleSidesCount = sidesCount * 2;
            _shape = new(4, doubleSidesCount);

            Vector3 azimuth = Vector3.forward;
            Vector3 halfHaight = Vector3.up * height * 0.5f;
            _shape.UpperPole = halfHaight;
            _shape.LowerPole = -halfHaight;
            var sideDeltaAngle = Quaternion.Euler(0f, 360f / sidesCount, 0f);

            for (int side = 0; side < doubleSidesCount; side += 2)
            {
                Vector3 currentVertex = azimuth + halfHaight;
                _shape[0, side] = currentVertex;
                _shape[1, side] = currentVertex;
                _shape[0, side + 1] = currentVertex;
                _shape[1, side + 1] = currentVertex;

                currentVertex = azimuth - halfHaight;

                _shape[2, side] = currentVertex;
                _shape[3, side] = currentVertex;
                _shape[2, side + 1] = currentVertex;
                _shape[3, side + 1] = currentVertex;

                azimuth = sideDeltaAngle * azimuth;
            }
            /*
            string message = "";
            for(int row = 0; row < 4; row++)
            {
                for(int col = 0; col < sidesCount; col++)
                {
                    message += $"[{row}, {col}]: {_shape[row, col]}";
                }
                message += "\n";
            }
            Debug.Log(message);*/
        }

        public void OnClick(Vector3 clickWorldPosition, Vector3 clickScreenPosition)
        {
            editingWindow.transform.position = clickScreenPosition;
            editingWindow.SetActive(true);
        }
    }
}