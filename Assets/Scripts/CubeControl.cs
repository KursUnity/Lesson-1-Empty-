using UnityEngine;

public class CubeControl : MonoBehaviour
{
    [SerializeField] private CubeExplosion _cubeExplosion;
    [SerializeField] private Camera _camera;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minCubeCount;
    [SerializeField] private int _maxCubeCount;

    private Ray _ray;

    private float _localScale = 1f;
    private float _scaleDigit = 2f;

    private int _splitChance = 100;
    private int _splitDigit = 2;
    private int _splitRandomNumber;
    private int _minSplitNumber = 0;
    private int _maxSplitNumber = 100;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
            {
                Transform objectHit = hit.transform;
                _localScale = hit.transform.localScale.x;
                TrySpawnCubes(objectHit);
                _cubeExplosion.Explode(objectHit);
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void TrySpawnCubes(Transform hitObject)
    {
        if (IsGetSplitChance())
        {
            int random = Random.Range(_minCubeCount, _maxCubeCount);
            _localScale /= _scaleDigit;

            while (random > 0)
            {
                random--;
                Cube temperaryCube = Instantiate(_cubePrefab, hitObject.position, Quaternion.identity);
                temperaryCube.transform.localScale = Vector3.one * _localScale;
                _splitChance /= _splitDigit;

                SetCubeColors(temperaryCube);
            }
        }
    }

    private void SetCubeColors(Cube cube)
    {
        cube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    private bool IsGetSplitChance()
    {
        _splitRandomNumber = Random.Range(_minSplitNumber, _maxSplitNumber);

        for (int i = 0; i < _splitChance; i++)
        {
            if (i == _splitRandomNumber)
            {
                return true;
            }
        }

        return false;
    }
}