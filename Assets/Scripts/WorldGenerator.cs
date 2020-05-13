using Packages.Rider.Editor.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private int chunkSize = 16;
    [SerializeField] private int numberOfChunks = 20;
    [SerializeField] private int chunkSizeZ;
    [SerializeField] private int mountains;
    [SerializeField] private int smoothing;
    [SerializeField] private int seed;
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private GameObject player;

    private int ActualChunkXPosition, ActualChunkZPosition = 0;

    private GameObject[,] chunks;

    void Start()
    {
        worldGenerator(numberOfChunks);
        StartCoroutine(checkChunkActive());
        
    }

    void Update()
    {
        if (((int)Mathf.Floor(player. transform.position.x / 16) != ActualChunkXPosition) || ((int)Mathf.Floor(player.transform.position.z / 16) != ActualChunkZPosition))
        {
            ActualChunkXPosition = (int)Mathf.Floor(transform.position.x / 16);
            ActualChunkZPosition = (int)Mathf.Floor(transform.position.z / 16);
            StartCoroutine(checkChunkActive());
        }

    }

    private void worldGenerator(int worldSize)
    {
        GameObject world = new GameObject("World");
        world.transform.position = Vector3.zero;
        world.transform.rotation = Quaternion.identity;

        chunks = new GameObject[worldSize, worldSize];

        for (int chunkX = 0; chunkX < worldSize; chunkX++)
        {
            for (int chunkZ = 0; chunkZ < worldSize; chunkZ++)
            {
                chunks[chunkX, chunkZ] = new GameObject($"Chunk {chunkX}, 0, {chunkZ}");
                chunks[chunkX, chunkZ].transform.parent = world.transform;
                chunks[chunkX, chunkZ].transform.position = new Vector3(chunkX * chunkSize, 0, chunkZ * chunkSize);
                chunks[chunkX, chunkZ].SetActive(false);
                //Relevo(chunkGenerator(chunkSize, chunks[chunkX, chunkZ]), seed * chunkX, seed * chunkZ);
                chunkGenerator(chunkSize,chunkX, chunkZ, chunks[chunkX, chunkZ]);
            }
        }
        PerlinGenerator(chunks);

    }

    private void chunkGenerator(int chunkSize, int chunkX, int chunkZ, GameObject chunk)
    {
        GameObject[,,] block = new GameObject[chunkSize, chunkSize, chunkSize];

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    float positionX = transform.position.x + (x * blocks[0].transform.localScale.x);
                    float positionY = transform.position.y + (y * blocks[0].transform.localScale.y); ;
                    float positionZ = transform.position.z + (z * blocks[0].transform.localScale.z);

                    Vector3 blockPosition = new Vector3(positionX, positionY, positionZ);
                    Quaternion blockRotation = Quaternion.identity;

                    block[x,y,z] = Instantiate(blocks[0], chunk.transform.position + blockPosition, blockRotation, chunk.transform);
                    block[x, y, z].name = $"Block {x}, {y}, {z}";
                }
            }
        }
    }

    public IEnumerator checkChunkActive()
    {
        int playerChunkXPosition = (int)Mathf.Floor(player.transform.position.x / 16);
        int playerChunkZPosition = (int)Mathf.Floor(player.transform.position.z / 16);        

        for (int x = playerChunkXPosition - 2; x < playerChunkXPosition + 2; x++)
        {
            //Debug.Log($"X = {x} e Chunk Length = {chunks.GetLength(0)}");
            if (x >= 0 && x <= chunks.GetLength(0) - 1)
            {
                
                for (int z = playerChunkZPosition - 2; z < playerChunkZPosition + 2; z++)
                {
                    if (z >= 0 && z <= chunks.GetLength(1) - 1)
                    {
                        if (((z == playerChunkZPosition + 1) || (z == playerChunkZPosition - 1) || (z == playerChunkZPosition)) &&
                                ((x == playerChunkXPosition + 1) || (x == playerChunkXPosition - 1) || (x == playerChunkXPosition)))
                        {
                            chunks[x, z].gameObject.SetActive(true);
                        }
                        else
                        {
                            chunks[x, z].gameObject.SetActive(false);
                        }
                    }
                }
            }
            
        }
        yield return null;
    }

    void Relevo(GameObject[,,] chunk, int seedX, int seedZ)
    {
        float newY;
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    if (y == 0)
                    {
                        newY = mountains * Mathf.PerlinNoise(x / Mathf.Sqrt(chunkSize * smoothing) + seedX + y, z / Mathf.Sqrt(chunkSize * smoothing) + seedZ);
                        chunk[x, y, z].transform.position = new Vector3(chunk[x, y, z].transform.position.x, Mathf.Floor(newY) * blocks[0].transform.localScale.y, chunk[x, y, z].transform.position.z);
                    }
                    else
                    {
                        newY = chunk[x, y - 1, z].transform.position.y + (1 * blocks[0].transform.localScale.y);
                        chunk[x, y, z].transform.position = new Vector3(chunk[x, y, z].transform.position.x, Mathf.Floor(newY), chunk[x, y, z].transform.position.z);
                    }
                }
            }

        }
    }

    void PerlinGenerator(GameObject[,] world)
    {
        
        for (int worldX = 0; worldX < world.GetLength(0); worldX++)
        {
            for (int worldY = 0; worldY < world.GetLength(1); worldY++)
            {

                Transform[] chunk = world[worldX, worldY].GetComponentsInChildren<Transform>();

                foreach (Transform teste in chunk)
                {
                    //Debug.Log(teste.name);
                    if (teste.CompareTag("block"))
                    {
                        float newY;
                        float firstY = 0f;
                        float x = teste.transform.position.x;
                        float y = teste.transform.position.y;
                        float z = teste.transform.position.z;

                        if (y == 0)
                        {
                            
                            newY = mountains * Mathf.PerlinNoise(x / Mathf.Sqrt(chunkSize * smoothing) + seed, z / Mathf.Sqrt(chunkSize * smoothing) + seed);
                            firstY = Mathf.Floor(newY) * blocks[0].transform.localScale.y;
                            teste.transform.position = new Vector3(teste.transform.position.x, Mathf.Floor(newY) * blocks[0].transform.localScale.y, teste.transform.position.z);
                            
                        }
                        else
                        {
                            newY = firstY + y;
                            teste.transform.gameObject.SetActive(false);
                        }
                    }
                }

            }
        }
    }
}
