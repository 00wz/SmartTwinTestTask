using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void CreateClosedMesh(int parallelsCount, int meridiansCount, out Vector3[] vertices,
        out List<int> triangles)
    {
        vertices = new Vector3[meridiansCount * parallelsCount + 2];
        CreateTubeShapedMesh(parallelsCount, meridiansCount, ref vertices, out triangles);

        //close the shape from above and below
        int decrementedMeridiansCount = meridiansCount - 1;
        int upperPoleIdx = vertices.Length - 2;
        int lowerPoleIdx = vertices.Length - 1;
        int tmpSum = meridiansCount * (parallelsCount - 1);
        for (int mer = 0; mer < decrementedMeridiansCount; mer++)
        {
            AppendTriangle(mer, upperPoleIdx, mer + 1, triangles);
            AppendTriangle(tmpSum + mer, tmpSum + mer + 1, lowerPoleIdx, triangles);
        }
        AppendTriangle(decrementedMeridiansCount, upperPoleIdx, 0, triangles);
        AppendTriangle(tmpSum + decrementedMeridiansCount, tmpSum, lowerPoleIdx, triangles);
    }

    /// <summary>
    /// creates a tube-shaped polygonal mesh. 
    /// if the <paramref name="vertices"/> is zero, creates a new array with the 
    /// size <paramref name="rowsCount"/> * <paramref name="colsCount"/>
    /// </summary>
    /// <param name="rowsCount">the number of rows in the mesh</param>
    /// <param name="colsCount">the number of columns in the mesh</param>
    /// <param name="vertices">array of vertices, 
    /// where the index of each vertex is (y * <paramref name="rowsCount"/> + x)</param>
    /// <param name="triangles">list of triangles</param>
    public static void CreateTubeShapedMesh(int rowsCount, int colsCount, ref Vector3[] vertices,
        out List<int> triangles)
    {
        CreateSquareMesh(rowsCount, colsCount, ref vertices, out triangles);

        //connect the edges of the checkered mesh
        int decrementedColsCount = colsCount - 1;
        int decrementedRowsCount = rowsCount - 1;
        for (int row = 0; row < decrementedRowsCount; row++)
        {
            AppendSquare(row * rowsCount + decrementedColsCount,
                        row * rowsCount,
                        (row + 1) * rowsCount,
                        (row + 1) * rowsCount + decrementedColsCount,
                        triangles);
        }
    }

    /// <summary>
    /// creates a checkered mesh. 
    /// if the <paramref name="vertices"/> is zero, creates a new array with the 
    /// size <paramref name="rowsCount"/> * <paramref name="colsCount"/>
    /// </summary>
    /// <param name="rowsCount">the number of rows in the mesh</param>
    /// <param name="colsCount">the number of columns in the mesh</param>
    /// <param name="vertices">array of vertices, 
    /// where the index of each vertex is (y * <paramref name="rowsCount"/> + x)</param>
    /// <param name="triangles">list of triangles</param>
    public static void CreateSquareMesh(int rowsCount, int colsCount, ref Vector3[] vertices,
        out List<int> triangles)
    {
        if(vertices == null)
        {
            vertices = new Vector3[colsCount * rowsCount];
        }
        else
        {
            if(vertices.Length < colsCount * rowsCount)
            {
                throw new System.IndexOutOfRangeException();
            }
        }
        triangles = new();

        //fill the space between the vertices with polygons
        int decrementedColsCount = colsCount - 1;
        int decrementedRowsCount = rowsCount - 1;
        for (int row = 0; row < decrementedRowsCount; row++)
        {
            for (int col = 0; col < decrementedColsCount; col++)
            {
                AppendSquare(row * rowsCount + col,
                             row * rowsCount + col + 1,
                             (row + 1) * rowsCount + col + 1,
                             (row + 1) * rowsCount + col,
                             triangles);
            }
        }
    }

    /// <summary>
    /// adds a square with clockwise vertices to the target list
    /// </summary>
    /// <param name="idx1">vertex index</param>
    /// <param name="idx2">vertex index</param>
    /// <param name="idx3">vertex index</param>
    /// <param name="idx4">vertex index</param>
    /// <param name="triangles">target list of triangles</param>
    public static void AppendSquare(int idx1, int idx2, int idx3, int idx4, List<int> triangles)
    {
        AppendTriangle(idx1, idx2, idx4, triangles);
        AppendTriangle(idx2, idx3, idx4, triangles);
    }

    /// <summary>
    /// adds a triangle with clockwise vertices to the target list
    /// </summary>
    /// <param name="idx1">vertex index</param>
    /// <param name="idx2">vertex index</param>
    /// <param name="idx3">vertex index</param>
    /// <param name="triangles">target list of triangles</param>
    public static void AppendTriangle(int idx1, int idx2, int idx3, List<int> triangles)
    {
        triangles.Add(idx3);
        triangles.Add(idx2);
        triangles.Add(idx1);
    }
}
