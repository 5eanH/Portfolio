using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring
{
    public Particle P1;
    public Particle P2;
    public float RestLength;
    public float Stiffness;

    public Spring(Particle p1, Particle p2, float stiffness)
    {
        P1 = p1;
        P2 = p2;
        Stiffness = stiffness;
        RestLength = Vector3.Distance(P1.Position, P2.Position);
    }

    public void Solve()
    {
        //between particles
        Vector3 direction = P2.Position - P1.Position;
        float currentLength = direction.magnitude;
        if (currentLength == 0f) return;

        float difference = currentLength - RestLength;
        float forceMagnitude = difference * Stiffness;

        Vector3 correction = (direction / currentLength) * forceMagnitude;

        //move particle halfway in opposite direction to fix balance
        //only apply if not pinned
        if (!P1.IsPinned)
            P1.Position += correction * 0.5f;

        if (!P2.IsPinned)
            P2.Position -= correction * 0.5f;
    }
}