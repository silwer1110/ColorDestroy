using System.Collections.Generic;
using UnityEngine;

public class Explouder : MonoBehaviour
{
    [SerializeField] private Spawner spawner;

    private float explosionForce = 15f;
    private float explosionRadius = 20f;

    private void OnEnable()
    {
        spawner.OnSpawn += Explode;
    }

    private void OnDisable()
    {
        spawner.OnSpawn -= Explode;
    }

    private void Explode(List<Cube> cubes, Vector3 position)
    {
        foreach (Cube cube in cubes)
        {
            cube.GetRigidbody().AddExplosionForce(explosionForce, position, explosionRadius);
        }
    }
}