using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothForces
{
    private ClothSimulation sim;

    public ClothForces(ClothSimulation simulation)
    {
        sim = simulation;
    }

    public void ApplyForces()
    {
        for (int x = 0; x < sim.width; x++)
        {
            for (int y = 0; y < sim.height; y++)
            {
                Particle p = sim.particles[x, y];
                if (p.IsPinned)
                {
                    p.Force = Vector3.zero;
                    continue;
                }

                // gravity
                p.Force = sim.gravity * p.Mass;

                if (sim.enableWind)
                {
                    //Wind that pulses | uses Perlin noise and sin
                    float noise = Mathf.PerlinNoise(Time.time * sim.windPulseFrequency, 0f);
                    float gust = Mathf.PerlinNoise(Time.time * 0.2f, 100f);
                    float wave = 0.5f + noise * sim.windPulseAmplitude;
                    float finalWind = wave * (0.8f + gust);

                    //sin wave direction
                    Vector3 dir = new Vector3(Mathf.Sin(Time.time * 1.5f), 0f,Mathf.Cos(Time.time * 0.5f) * 0.2f).normalized;

                    p.Force += dir * sim.windStrength * finalWind;

                    //Line ray to show wind force direction
                    Debug.DrawRay(p.Position, dir * sim.windStrength * finalWind * 0.3f, Color.cyan);
                }
            }
        }
    }

    public void DampenVelocities()
    {
        foreach (Particle p in sim.particles)
        {
            if (!p.IsPinned)
                p.Velocity *= sim.dampingFactor;
        }
    }
}
