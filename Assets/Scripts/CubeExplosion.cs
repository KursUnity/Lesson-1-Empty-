using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForces;

    public void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplosionObjects())
        {
            explodableObject.AddExplosionForce(_explosionForces, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplosionObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }
}