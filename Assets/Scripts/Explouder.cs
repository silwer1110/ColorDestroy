using System.Collections.Generic;
using UnityEngine;

public class Explouder : MonoBehaviour
{
    [SerializeField] private Spawner spawner;

    private void OnEnable()
    {
        spawner.Spawned += ExplodeSelective;
    }

    private void OnDisable()
    {
        spawner.Spawned -= ExplodeSelective;
    }

    private void ExplodeSelective(List<Cube> cubes, Vector3 position)
    {
        float explosionForce = 15f;
        float explosionRadius = 20f;

        foreach (Cube cube in cubes)
        {
            cube.GetRigidbody().AddExplosionForce(explosionForce, position, explosionRadius);
        }
    }
}