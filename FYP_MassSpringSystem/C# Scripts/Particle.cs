using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    public int gridX;
    public int gridY;

    public Vector3 Position;
    public Vector3 Velocity;
    public Vector3 Force;
    public float Mass;
    public bool IsPinned;

    public Particle(int x, int y, Vector3 initialPos, float mass, bool pinned = false)
    {
        gridX = x;
        gridY = y;

        Position = initialPos;
        Mass = mass;
        IsPinned = pinned;
        Velocity = Vector3.zero;
        Force = Vector3.zero;
    }
    //Euler integration | uses newtons law
    public void Integrate(float deltaTime)
    {
        if (IsPinned)
        {
            Velocity = Vector3.zero;
            Force = Vector3.zero;
            return;
        }

        Vector3 accel = Force / Mass; 
        Velocity += accel * deltaTime;
        Position += Velocity * deltaTime;
        Force = Vector3.zero;
    }
}