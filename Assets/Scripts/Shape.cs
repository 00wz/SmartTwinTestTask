using System.Collections.Generic;
using UnityEngine;

namespace MeshDraw
{
    public class Shape
    {
        readonly private Vector3[] _vertices;
        readonly private List<int> _triangles;
        readonly private int _parallelsCount;
        readonly private int _meridiansCount;
        readonly private int _upperPoleIndex;
        readonly private int _lowerPoleIndex;
        readonly private Color[] _colors;
        private Mesh _mesh;

        public Shape(int parallelsCount, int meridiansCount)
        {
            _parallelsCount = parallelsCount;
            _meridiansCount = meridiansCount;
            Utils.CreateClosedMesh(parallelsCount, meridiansCount, out _vertices, out _triangles);
            _upperPoleIndex = parallelsCount * meridiansCount;
            _lowerPoleIndex = _upperPoleIndex + 1;
            _colors = new Color[_vertices.Length];
        }

        public Vector3 this [int parallel, int meridian]
        {
            get => _vertices[parallel * _meridiansCount + meridian];
            set => _vertices[parallel * _meridiansCount + meridian] = value;
        }

        public Vector3 UpperPole
        {
            get => _vertices[_upperPoleIndex];
            set => _vertices[_upperPoleIndex] = value;
        }

        public Vector3 LowerPole
        {
            get => _vertices[_lowerPoleIndex];
            set => _vertices[_lowerPoleIndex] = value;
        }

        public Color color
        {
            set
            {
                for(int i = 0; i < _vertices.Length; i++)
                {
                    _colors[i] = value;
                }
                _mesh.SetColors(_colors);
            }
        }

        public void AssignMesh(Mesh mesh)
        {
            _mesh = mesh;
            RedrawMesh();
        }

        public void RedrawMesh()
        {
            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles.ToArray();
            _mesh.RecalculateNormals();
        }
    }
}