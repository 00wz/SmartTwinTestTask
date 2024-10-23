using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestMesh : MonoBehaviour
{
    private Mesh _mesh;
    static int cols = 10;
    static int rows = 10;
    private Vector3[] vertices;
    List<int> triangles;

    void Start()
    {
        GetComponent<MeshFilter>().mesh = _mesh = new Mesh();

        CreateMesh();

        _mesh.vertices = vertices;
        _mesh.triangles = triangles.ToArray();
    }


    private void CreateMesh()
    {
        Utils.CreateClosedMesh(rows, cols, out vertices, out triangles);

        Vector3 azimuth = Vector3.forward;
        var sideDeltaAngle = Quaternion.Euler(0f, 360f/cols, 0f);
        for (int i = 0; i < cols; i++)
        {
            Vector3 curentVertex = azimuth;
            for( int x = 0; x < rows; x++)
            {
                vertices[x * rows + i] = curentVertex;
                curentVertex += Vector3.down;
            }
            azimuth = sideDeltaAngle * azimuth;
        }

        vertices[rows * cols + 1] = Vector3.down * (rows - 1);
    }
}
