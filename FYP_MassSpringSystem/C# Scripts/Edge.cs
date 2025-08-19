using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Edge
{
    public Vector2Int A;
    public Vector2Int B;

    public Edge(Vector2Int a, Vector2Int b)
    {
        //for consisten hashing to order points | will help stop duplication
        if (a.x < b.x || (a.x == b.x && a.y < b.y))
        {
            A = a;
            B = b;
        }
        else
        {
            A = b;
            B = a;
        }
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Edge)) return false;
        Edge other = (Edge)obj;
        return A.Equals(other.A) && B.Equals(other.B);
    }

    public override int GetHashCode()
    {
        return A.GetHashCode() ^ B.GetHashCode();
    }
}
