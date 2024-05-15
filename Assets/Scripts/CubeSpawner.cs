using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const int MinSplitNumber = 0;
    private const int MaxSplitNumber = 100;

    [SerializeField] private CubeExplosion _cubeExplosion;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _minCubeCount;
    [SerializeField] private int _maxCubeCount;

    private Ray _ray;

    private float _localScale = 1f;
    private float _scaleDigit = 2f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.TryGetComponent(out Cube cube))
                {
                    _localScale = hit.transform.localScale.x;
                    TrySpawnCubes(cube);
                    Destroy(cube.gameObject);
                }
            }
        }
    }

    private void TrySpawnCubes(Cube cube)
    {
        if (IsGetSplitChance(cube))
        {
            int random = Random.Range(_minCubeCount, _maxCubeCount);
            _localScale /= _scaleDigit;
            List<Cube> cubes = new List<Cube>();

            for (int i = 0; i < random; i++)
            {
                Cube temperaryCube = Instantiate(cube, cube.transform.position, Quaternion.identity);
                temperaryCube.transform.localScale = Vector3.one * _localScale;
                temperaryCube.TakeSplitChance();
                cubes.Add(temperaryCube);
                SetCubeColors(temperaryCube);
            }

            _cubeExplosion.ExplodeCubes(cubes);
        }
    }

    private void SetCubeColors(Cube cube)
    {
        cube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    private bool IsGetSplitChance(Cube cube)
    {
        int splitRandomNumber = Random.Range(MinSplitNumber, MaxSplitNumber);

        return splitRandomNumber <= cube.TakeSplitChance();
    }
}