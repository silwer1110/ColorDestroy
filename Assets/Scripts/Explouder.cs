using System.Collections.Generic;
using UnityEngine;

public class Explouder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _baseExplosionRadius = 5f;
    [SerializeField] private float _baseExplosionForce = 500f;
    [SerializeField] private float _baseSelectiveForce = 15f;
    [SerializeField] private float _baseSelectiveRadius = 20f;
    [SerializeField] private float _baseMultiplier = 3f;

    private void OnEnable()
    {
        _spawner.Spawned += ExplodeSelective;
        _spawner.NotSpawned += ExplodeOnRadius;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= ExplodeSelective;
        _spawner.NotSpawned -= ExplodeOnRadius;
    }

    private void ExplodeOnRadius(Cube cube)
    {
        Vector3 explosionPosition = cube.transform.position;

        float explosionRadius = GetExplosionRadius(cube);
        float explosionForce = GetExplosionForce(cube);
        float distance;
        float forceMultiplier;

        List<Rigidbody> rigidbodies = GetExplodableObjects(explosionPosition, explosionRadius);

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody != null && rigidbody != cube.Rigidbody)
            {
                distance = Vector3.Distance(explosionPosition, rigidbody.position);
                forceMultiplier = Mathf.Clamp01(_baseMultiplier - (distance / explosionRadius));

                ApplyExplosion(rigidbody, explosionPosition, explosionForce * forceMultiplier, explosionRadius);
            }
        }
    }

    private void ExplodeSelective(List<Rigidbody> rigidbodies, Vector3 position)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            ApplyExplosion(rigidbody, position, _baseSelectiveForce, _baseSelectiveRadius);
        }
    }

    private void ApplyExplosion(Rigidbody rigidbody, Vector3 explosionPosition, float force, float radius)
    {
        rigidbody.AddExplosionForce(force, explosionPosition, radius);
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);

        List<Rigidbody> rigidbodies = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);
        }

        return rigidbodies;
    }

    private float GetExplosionRadius(Cube cube)
    {
        float explosionRadius;

        explosionRadius = cube.transform.localScale.x * _baseExplosionRadius;

        return explosionRadius;
    }

    private float GetExplosionForce(Cube cube)
    {
        float explosionForce;

        explosionForce = _baseExplosionForce / cube.transform.localScale.x;

        return explosionForce;
    }
}