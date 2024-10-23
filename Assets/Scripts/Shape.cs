using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshDraw
{
    public class Shape
    {
        readonly public Vector3[] _vertices;
        readonly public List<int> _triangles;
        readonly private int _parallelsCount;
        readonly private int _meridiansCount;
        readonly private int _upperPoleIndex;
        readonly private int _lowerPoleIndex;

        public Shape(int parallelsCount, int meridiansCount)
        {
            _parallelsCount = parallelsCount;
            _meridiansCount = meridiansCount;
            Utils.CreateClosedMesh(parallelsCount, meridiansCount, out _vertices, out _triangles);
            _upperPoleIndex = parallelsCount * meridiansCount;
            _lowerPoleIndex = _upperPoleIndex + 1;
        }

        public Vector3 this [int parallel, int meridian]
        {
            get => _vertices[parallel * _parallelsCount + meridian];
            set => _vertices[parallel * _parallelsCount + meridian] = value;
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

        public void AssignMesh(Mesh mesh)
        {
            mesh.Clear();
            mesh.vertices = _vertices;
            mesh.triangles = _triangles.ToArray();
            mesh.RecalculateNormals();
        }
    }
}