using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshDraw
{
    public class ParallelepipedInstance : ShapeInstaceBase
    {
        [SerializeField] private float a = 2f;
        [SerializeField] private float b = 2f;
        [SerializeField] private float c = 2f;

        public float A
        {
            get => a;
            set
            {
                a = value;
                RedrawMesh();
            }
        }

        public float B
        {
            get => b;
            set
            {
                b = value;
                RedrawMesh();
            }
        }

        public float C
        {
            get => c;
            set
            {
                c = value;
                RedrawMesh();
            }
        }
        protected override Shape CreateShape()
        {
            Shape shape = new(4, 8);

            float halfA = A * 0.5f;
            float halfB = B * 0.5f;
            float halfC = C * 0.5f;

            shape.UpperPole = halfB * Vector3.up;
            shape.LowerPole = halfB * Vector3.down;

            OneValueFourVertices((0, 0), new(halfA, halfB, -halfC));
            OneValueFourVertices((0, 2), new(-halfA, halfB, -halfC));
            OneValueFourVertices((0, 4), new(-halfA, halfB, halfC));
            OneValueFourVertices((0, 6), new(halfA, halfB, halfC));
            OneValueFourVertices((2, 0), new(halfA, -halfB, -halfC));
            OneValueFourVertices((2, 2), new(-halfA, -halfB, -halfC));
            OneValueFourVertices((2, 4), new(-halfA, -halfB, halfC));
            OneValueFourVertices((2, 6), new(halfA, -halfB, halfC));

            return shape;

            void OneValueFourVertices((int parallel,int meridian) leftUpIdx, Vector3 value)
            {
                shape[leftUpIdx.parallel, leftUpIdx.meridian] = value;
                shape[leftUpIdx.parallel + 1, leftUpIdx.meridian] = value;
                shape[leftUpIdx.parallel, leftUpIdx.meridian + 1] = value;
                shape[leftUpIdx.parallel + 1, leftUpIdx.meridian + 1] = value;
            }
        }
    }
}