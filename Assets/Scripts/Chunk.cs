using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class Chunk : MonoBehaviour
{
    public GameObject BlockGrass, BlockDirt;
    GameObject Block;
    public int DimensionX, DimensionY, DimensionZ;

    public float Smoothing, MaxY, Seed;

    GameObject[,,] chunk;

    void Start()
    {
        chunk = new GameObject[DimensionX, DimensionY, DimensionZ];

        for (int x = 0; x < DimensionX; x++)
        {
            for (int y = 0; y < DimensionY; y++)
            {
                for (int z = 0; z < DimensionZ; z++)
                {
                    if (y > 0)
                    {
                        Block = BlockDirt;
                    }
                    else
                    {
                        if (Random.Range(0f, 1f) > 0.98f)
                        {
                            Block = BlockDirt;
                        }
                        else
                        {
                            Block = BlockGrass;
                        }
                    }
                    chunk[x, y, z] = Instantiate(Block, new Vector3(x * Block.transform.localScale.x, y * Block.transform.localScale.y, z * Block.transform.localScale.z), Quaternion.identity);
                    chunk[x, y, z].name = $"Block {x}, {y}, {z}";

                }
            }
        }
        Debug.Log("Criando Relevo");
        Relevo(Seed);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Smoothing++;
            Relevo(Seed);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Smoothing--;
            Relevo(Seed);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            MaxY++;
            Relevo(Seed);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            MaxY--;
            Relevo(Seed);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Seed++;
            Relevo(Seed);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            Seed--; 
            Relevo(Seed);
        }

    }

    public void AlteraSmoothing(float arg)
    {
        Smoothing = arg;
        Relevo(Seed);
    }
    public void AlteraAltura(float arg)
    {
        MaxY = arg;
        Relevo(Seed);
    }
    public void AlteraSeed(float arg)
    {
        Seed = arg;
        Relevo(Seed);
    }

    void Relevo(float seed)
    {
        float newY;
        for (int x = 0; x < DimensionX; x++)
        {
            for (int y = 0; y < DimensionY; y++)
            {
                for (int z = 0; z < DimensionZ; z++)
                {
                    if (y == 0)
                    {
                        newY = MaxY * Mathf.PerlinNoise(x / Mathf.Sqrt(DimensionX * Smoothing) + seed, z / Mathf.Sqrt(DimensionZ * Smoothing) + seed);
                        chunk[x, y, z].transform.position = new Vector3(chunk[x, y, z].transform.position.x, Mathf.Floor(newY) * BlockGrass.transform.localScale.y, chunk[x, y, z].transform.position.z);
                    }
                    else
                    {
                        newY = chunk[x, y - 1, z].transform.position.y - (1 * BlockGrass.transform.localScale.y);
                        chunk[x, y, z].transform.position = new Vector3(chunk[x, y, z].transform.position.x, Mathf.Floor(newY), chunk[x, y, z].transform.position.z);
                    }

                    
                }
            }
            
        }
    }
}
