using UnityEngine;

namespace MeshDraw
{
    public class CapsuleInstance : ShapeInstaceBase
    {
        [SerializeField] private int sidesCount = 5;
        [SerializeField] private float height = 5f;
        [SerializeField] private float radius = 5f;

        private const int HORIZONTAL_EDGES_HALF_NUMBER = 10;

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
            float correctHeight = Mathf.Max(height, 2f * radius);
            Shape shape = new Shape(HORIZONTAL_EDGES_HALF_NUMBER * 2, sidesCount);

            Vector3 halfHaight = Vector3.up * correctHeight * 0.5f;
            shape.UpperPole = halfHaight;
            shape.LowerPole = -halfHaight;

            Quaternion verticalDeltaAngle = 
                Quaternion.Euler(-90f / HORIZONTAL_EDGES_HALF_NUMBER, 0f, 0f);
            Quaternion horizontalDeltaAngle = 
                Quaternion.Euler(0f, 360f / sidesCount, 0f);

            Vector3 centerUpperHemisphere = (correctHeight * 0.5f - radius) * Vector3.up;
            Vector3 centerLowerHemisphere = -centerUpperHemisphere;

            Vector3 currentPosition = Vector3.up * radius;

            int parallel = 0;
            for(; parallel < HORIZONTAL_EDGES_HALF_NUMBER; parallel++)
            {
                currentPosition = verticalDeltaAngle * currentPosition;
                for(int meridian = 0; meridian < sidesCount; meridian++)
                {
                    shape[parallel, meridian] = centerUpperHemisphere + currentPosition;
                    currentPosition = horizontalDeltaAngle * currentPosition;
                }
            }

            int parallelsCount = HORIZONTAL_EDGES_HALF_NUMBER * 2;
            for(; parallel < parallelsCount; parallel++)
            {
                for (int meridian = 0; meridian < sidesCount; meridian++)
                {
                    shape[parallel, meridian] = centerLowerHemisphere + currentPosition;
                    currentPosition = horizontalDeltaAngle * currentPosition;
                }
                currentPosition = verticalDeltaAngle * currentPosition;
            }

            return shape;
        }

    }
}