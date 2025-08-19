using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ClothSimulation : MonoBehaviour
{
    [Header("Cloth Dimensions")]
    public int width = 10;
    public int height = 10;
    public float spacing = 0.2f;
    public float particleMass = 0.2f;

    [Header("Cloth Physics")]
    public float stiffness = 1f;
    public Vector3 gravity = new Vector3(0, -9.81f, 0);
    public Vector3 windDirection = new Vector3(1f, 0f, 0f);
    public float windStrength = 1f;
    public bool enableWind = true;
    public bool pinTopRow = true;
    public bool pinBottomRow = false;

    [Header("Wind Settings")]
    public float windPulseFrequency = 1f;
    public float windPulseAmplitude = 0.5f;

    [Header("Tearing Settings")]
    public float impactTearThreshold = 10f;
    public float tearRadius = 0.5f;

    [Header("Collision Bounce Settings")]
    public float bounceStrength = 5f;

    [Header("Simulation Settings")]
    [Range(0.0f, 1.0f)] public float dampingFactor = 0.90f;
    public int solverIterations = 10;

    public Particle[,] particles;
    public List<Spring> springs = new List<Spring>();
    public HashSet<Edge> connectedEdges = new HashSet<Edge>();
    public Mesh mesh;

    private ClothCreator creator;
    private ClothForces forces;
    private ClothCollision collision;
    private ClothMeshGeneration meshGen;

    void Start()
    {
        creator = new ClothCreator(this);
        forces = new ClothForces(this);
        collision = new ClothCollision(this);
        meshGen = new ClothMeshGeneration(this);

        creator.CreateCloth();
        meshGen.CreateMesh();
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        forces.ApplyForces();
        forces.DampenVelocities();
        creator.IntegrateParticles(dt);

        //Repeat Multiple times to improve Accurate behaviour | Stops Sagging
        for (int i = 0; i < solverIterations; i++)
            creator.SolveSprings();

        collision.DoCollision();
        meshGen.UpdateMesh();
    }

    public void TearClothAtPoint(Vector3 impactPoint, float radius)
    {
        collision.TearClothAtPoint(impactPoint, radius);
    }

    void OnDrawGizmos()
    {
        if (particles == null || springs == null) return;
        meshGen.DrawGizmos();
    }
}