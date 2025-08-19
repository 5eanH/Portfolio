using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothCreator
{
    private ClothSimulation sim;

    public ClothCreator(ClothSimulation simulation)
    {
        sim = simulation;
    }

    public void CreateCloth()
    {
        int width = sim.width;
        int height = sim.height;
        float spacing = sim.spacing;
        
        sim.particles = new Particle[width, height];
        sim.springs.Clear();
        sim.connectedEdges.Clear();

        //create the grid of nodes/particles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 localPos = new Vector3(x * spacing, -y * spacing, 0f);
                Vector3 worldPos = sim.transform.TransformPoint(localPos);

                bool pinned = (sim.pinTopRow && y == 0) || (sim.pinBottomRow && y == height - 1);

                sim.particles[x, y] = new Particle(x, y, worldPos, sim.particleMass, pinned);
            }
        }

        //connect springs with neighbors
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x < width - 1) AddSpring(x, y, x + 1, y); //horizontal
                if (y < height - 1) AddSpring(x, y, x, y + 1); //vert
                if (x < width - 1 && y < height - 1)
                {
                    //diagonal
                    AddSpring(x, y, x + 1, y + 1);
                    AddSpring(x + 1, y, x, y + 1);
                }
            }
        }
    }

    private void AddSpring(int x1, int y1, int x2, int y2)
    {
        Particle p1 = sim.particles[x1, y1];
        Particle p2 = sim.particles[x2, y2];

        Spring s = new Spring(p1, p2, sim.stiffness);
        sim.springs.Add(s);
        sim.connectedEdges.Add(new Edge(new Vector2Int(x1, y1), new Vector2Int(x2, y2)));
    }

    public void IntegrateParticles(float dt)
    {
        //call to apply forces to every particle | in particle/ Euler
        foreach (Particle p in sim.particles)
            p.Integrate(dt);
    }

    public void SolveSprings()
    {
        foreach (Spring s in sim.springs)
            s.Solve();
    }
}
