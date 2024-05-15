using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    public void ExplodeCubes(List<Cube> explosionObjects)
    {
        foreach (Cube hit in explosionObjects)
        {
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(Random.Range(-1f,1f), Random.Range(-1f,1f), Random.Range(-1f,1f));
            }
        }
    }
}