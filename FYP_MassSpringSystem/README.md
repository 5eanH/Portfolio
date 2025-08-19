# Unity Cloth Physics Library with Tearing

A modular cloth physics library for Unity, featuring realistic cloth behavior, collisions, wind, gravity, and tearing under force. Designed to be lightweight and easy to integrate into any Unity project.

---

## Features

- **Mass-Spring System**: Simulates realistic cloth movement with particles and springs.
- **Gravity & Wind**: Cloth responds naturally to forces; wind includes Perlin noise for flowy, soft movement.
- **Collision Detection**: Cloth interacts with objects, including bounce effects and wrapping.
- **Tearing Feature**: Cloth tears realistically when extreme forces are applied.
- **Pinning Support**: Top rows of cloth can be pinned (ideal for flags, banners, or hanging fabrics).
- **Modular Code**: Easily customize, extend, or integrate into other Unity projects.
- **Real-time Performance**: Optimized to run efficiently in game scenes.

<img width="604" height="284" alt="Image" src="https://github.com/user-attachments/assets/400eb150-7028-4b58-8ad3-f018395ebd2d" />

<img width="604" height="290" alt="Image" src="https://github.com/user-attachments/assets/47175e82-1b79-4771-9006-3d323b7f4537" />

<img width="603" height="289" alt="Image" src="https://github.com/user-attachments/assets/877463ab-9f05-40ad-a798-f2b5a8553726" />

---

## Installation

1. Download the package from this repositorys [Release Page](https://github.com/5eanH/Portfolio-Final-Yr-Project/releases/tag/v1.0).
2. Open your Unity project.
3. Import the package via `Assets > Import Package > Custom Package`.
4. Drag the `ClothPrefab` into your Game Scene or your own empty GameObject.
5. If you created a new GameObject add the `ClothController` script to the object.

---

## Usage

1. **Adding Cloth**: Drag the prefab into your scene.
2. **Configuring Cloth**: Adjust variables in the `ClothController` inspector, including:
   - Particle grid size
   - Spring stiffness
   - Wind strength & direction
   - Tearing threshold
3. **Pinning Cloth**: Enable the top row pinning for banners or flags.
4. **Play Scene**: The cloth will respond to physics forces, collisions, and tearing.

---

## Project Structure

- **Scripts/**
  - `ClothController.cs` – main controller handling particles, springs, collisions, wind, and tearing.
  - `Particle.cs` – particle behavior and physics.
  - `Spring.cs` – connects particles and handles elasticity.
  - `MeshUpdater.cs` – updates the mesh based on spring connectivity and tearing.
- **Prefabs/** – ready-to-use cloth objects.
- **Materials/** – placeholder materials; can be customized.

---

## Technical Highlights

- Cloth is implemented using a **mass-spring system**, with particles connected via springs.
- Wind simulation uses **Perlin noise** for smooth, natural reactions.
- **Tearing** is handled by creating a radius around collision points and removing springs within that radius.
- Mesh triangles are dynamically updated to reflect spring removal for realistic tearing visuals.

---

## Future Improvements

- **Cloth Proximity Rendering**: Render cloth only when near the camera to improve performance.
- **Multiple Material Types**: Leather, canvas, cotton, etc., with preset rigidity, flexibility, and tearing behavior.
- **Fraying Edges**: Realistic visual effect on torn cloth edges.
- Additional physics improvements and gameplay-focused features.

---

## Author

**Sean Hogan** – Software Developer | Unity Programmer  

This project was created as part of a final year project focusing on physics-based systems in Unity.
