using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private int WorldSize;
    [Range(0, 1f)]
    [SerializeField] private float Elevation;
    [Space]
    [SerializeField] private Transform World;
    [SerializeField] private GameObject CubePrefab;

    private float Resets = 1;

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorld();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform child in World)
            {
                Destroy(child.gameObject);
            }

            Resets = Random.Range(0, 100f);
            GenerateWorld();
        }
    }

    private float GeneratePerlinNoise(int x, int z)
    {
        float xCoord = (float)x / WorldSize + Resets;
        float zCoord = (float)z / WorldSize + Resets;

        float noise = Mathf.Clamp01(Mathf.PerlinNoise(xCoord, zCoord));
        return noise;
    }

    private void GenerateWorld()
    {
        StartCoroutine(_GenerateWorld());
    }

    private IEnumerator _GenerateWorld()
    {
        for (int y = 0; y < WorldSize; y++)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            for (int z = 0; z < WorldSize; z++)
            {              
                for (int x = 0; x < WorldSize; x++)
                {
                    float noise = GeneratePerlinNoise(x, z);
                    if (noise >= (float)y / WorldSize / Elevation)
                    {
                        Instantiate(CubePrefab, new Vector3(x, y, z), Quaternion.identity, World);
                    }                 
                }
            }
        }

        yield return new WaitForSeconds(Time.deltaTime);
    }
}
