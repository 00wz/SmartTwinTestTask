using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshDraw
{
    public class PrismInstance : ShapeInstaceBase
    {
        [SerializeField] private int sidesCount = 5;
        [SerializeField] private float height = 5f;
        [SerializeField] private float radius = 5f;

        public float SidesCount
        {
            get => sidesCount;
            set
            {
                sidesCount = (int)value;
                RedrawMesh();
            }
        }

        public float Height
        {
            get => height;
            set
            {
                height = value;
                RedrawMesh();
            }
        }

        public float Radius
        {
            get => radius;
            set
            {
                radius = value;
                RedrawMesh();
            }
        }

        protected override Shape CreateShape()
        {
            int doubleSidesCount = sidesCount * 2;
            Shape shape = new(4, doubleSidesCount);

            Vector3 azimuth = Vector3.forward * radius;
            Vector3 halfHaight = Vector3.up * height * 0.5f;
            shape.UpperPole = halfHaight;
            shape.LowerPole = -halfHaight;
            var sideDeltaAngle = Quaternion.Euler(0f, 360f / sidesCount, 0f);

            for (int side = 0; side < doubleSidesCount; side += 2)
            {
                Vector3 currentVertex = azimuth + halfHaight;
                shape[0, side] = currentVertex;
                shape[1, side] = currentVertex;
                shape[0, side + 1] = currentVertex;
                shape[1, side + 1] = currentVertex;

                currentVertex = azimuth - halfHaight;

                shape[2, side] = currentVertex;
                shape[3, side] = currentVertex;
                shape[2, side + 1] = currentVertex;
                shape[3, side + 1] = currentVertex;

                azimuth = sideDeltaAngle * azimuth;
            }
            return shape;
        }
    }
}