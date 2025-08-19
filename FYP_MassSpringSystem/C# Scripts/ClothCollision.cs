using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothCollision
{
    private ClothSimulation sim;

    public ClothCollision(ClothSimulation simulation)
    {
        sim = simulation;
    }

    public void DoCollision()
    {
        float radius = sim.spacing * 1f;

        foreach (Particle p in sim.particles)
        {
            if (p.IsPinned) continue;

            Collider[] hits = Physics.OverlapSphere(p.Position, radius);
            foreach (Collider col in hits)
            {
                if (col is CharacterController) continue; //skip

                Vector3 closest = col.ClosestPoint(p.Position);
                Vector3 toParticle = p.Position - closest;
                float dist = toParticle.magnitude;
 
                if (dist < radius && dist > 0f)
                {
                    //Push particle out of way of collider
                    Vector3 pushOut = toParticle.normalized * (radius - dist);
                    p.Position += pushOut;
                    p.Velocity += pushOut.normalized * sim.bounceStrength;

                    //Check threshold to see if tears
                    float impact = pushOut.magnitude / Time.fixedDeltaTime;
                    if (impact > sim.impactTearThreshold)
                        TearClothAtPoint(p.Position, sim.tearRadius);

                    Debug.Log($"Colliding with {col.name} ({col.GetType()})");
                }
            }
        }
    }

    public void TearClothAtPoint(Vector3 impactPoint, float radius)
    {
        for (int i = sim.springs.Count - 1; i >= 0; i--)
        {
            //deletes springs and edges in radius of the tear
            Spring s = sim.springs[i];
            float d1 = Vector3.Distance(impactPoint, s.P1.Position);
            float d2 = Vector3.Distance(impactPoint, s.P2.Position);

            if (d1 <= radius || d2 <= radius)
            {
                sim.springs.RemoveAt(i);
                Vector2Int a = new Vector2Int(s.P1.gridX, s.P1.gridY);
                Vector2Int b = new Vector2Int(s.P2.gridX, s.P2.gridY);
                sim.connectedEdges.Remove(new Edge(a, b));
            }
        }
    }
}
