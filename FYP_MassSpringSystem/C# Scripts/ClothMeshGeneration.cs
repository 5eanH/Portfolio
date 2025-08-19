using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothMeshGeneration
{
    private ClothSimulation sim;
    public ClothMeshGeneration(ClothSimulation simulation)
    {
        sim = simulation;
    }

    public void CreateMesh()
    {
        sim.mesh = new Mesh();
        sim.GetComponent<MeshFilter>().mesh = sim.mesh;
    }
    public void UpdateMesh()
    {
        int w = sim.width;
        int h = sim.height;

        Vector3[] verts = new Vector3[w * h];       //hold 3d positions for particles
        Vector2[] uvs = new Vector2[verts.Length];  //texture mapping
        List<int> tris = new List<int>();           //triangle indices to create mesh surface



        int i = 0;
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                Particle p = sim.particles[x, y];
                verts[i] = sim.transform.InverseTransformPoint(p.Position);
                uvs[i] = new Vector2(x / (float)(w - 1), y / (float)(h - 1));
                i++;
            }
        }

        //handles building triangle if springs still exist | for tearing
        bool EdgeExists(int x1, int y1, int x2, int y2) =>
            sim.connectedEdges.Contains(new Edge(new Vector2Int(x1, y1), new Vector2Int(x2, y2)));

        int VertIndex(int x, int y) => y * w + x;

        //build triangle list from connected edges
        for (int y = 0; y < h - 1; y++)
        {
            for (int x = 0; x < w - 1; x++)
            {
                if (EdgeExists(x, y, x + 1, y) && EdgeExists(x, y, x, y + 1) && EdgeExists(x + 1, y, x, y + 1))
                {
                    tris.Add(VertIndex(x, y));
                    tris.Add(VertIndex(x + 1, y));
                    tris.Add(VertIndex(x, y + 1));
                }

                if (EdgeExists(x + 1, y, x + 1, y + 1) && EdgeExists(x + 1, y + 1, x, y + 1) && EdgeExists(x + 1, y, x, y + 1))
                {
                    tris.Add(VertIndex(x + 1, y));
                    tris.Add(VertIndex(x + 1, y + 1));
                    tris.Add(VertIndex(x, y + 1));
                }
            }
        }

        sim.mesh.Clear();
        sim.mesh.vertices = verts;
        sim.mesh.uv = uvs;
        sim.mesh.triangles = tris.ToArray();
        sim.mesh.RecalculateNormals();
        sim.mesh.RecalculateBounds();
    }
    //Gizmos to make spring and particle node visable
    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var p in sim.particles)
            Gizmos.DrawSphere(p.Position, 0.2f);

        Gizmos.color = Color.blue;
        foreach (var s in sim.springs)
            Gizmos.DrawLine(s.P1.Position, s.P2.Position);
    }
}
