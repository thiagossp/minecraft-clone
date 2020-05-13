using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureControl : MonoBehaviour
{

    public int Tamanho;

    public int FrontTextureX, FrontTextureY;
    public int TopTextureX, TopTextureY;
    public int BackTextureX, BackTextureY;
    public int BottomTextureX, BottomTextureY;
    public int LeftTextureX, LeftTextureY;
    public int RightTextureX, RightTextureY;

    private 
    // Start is called before the first frame update
    void Start()
    {
        float tamanhoBloco = 1f / Tamanho;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector2[] UVs = new Vector2[mesh.vertices.Length];
        // Front
        UVs[0] = new Vector2(FrontTextureX * tamanhoBloco, FrontTextureY * tamanhoBloco);
        UVs[1] = new Vector2(FrontTextureX * tamanhoBloco + tamanhoBloco, FrontTextureY * tamanhoBloco);
        UVs[2] = new Vector2(FrontTextureX * tamanhoBloco, FrontTextureY * tamanhoBloco + tamanhoBloco);
        UVs[3] = new Vector2(FrontTextureX * tamanhoBloco + tamanhoBloco, FrontTextureY * tamanhoBloco + tamanhoBloco);
        // Top
        UVs[8] = new Vector2(TopTextureX * tamanhoBloco, TopTextureY * tamanhoBloco);
        UVs[9] = new Vector2(TopTextureX * tamanhoBloco + tamanhoBloco, TopTextureY * tamanhoBloco);
        UVs[4] = new Vector2(TopTextureX * tamanhoBloco, TopTextureY * tamanhoBloco + tamanhoBloco);
        UVs[5] = new Vector2(TopTextureX * tamanhoBloco + tamanhoBloco, TopTextureY * tamanhoBloco + tamanhoBloco);
        // Back
        UVs[7] =  new Vector2(BackTextureX * tamanhoBloco, BackTextureY * tamanhoBloco);
        UVs[6] =  new Vector2(BackTextureX * tamanhoBloco + tamanhoBloco, BackTextureY * tamanhoBloco);
        UVs[11] = new Vector2(BackTextureX * tamanhoBloco, BackTextureY * tamanhoBloco + tamanhoBloco);
        UVs[10] = new Vector2(BackTextureX * tamanhoBloco + tamanhoBloco, BackTextureY * tamanhoBloco + tamanhoBloco);
        // Bottom
        UVs[12] = new Vector2(BottomTextureX * tamanhoBloco, BottomTextureY * tamanhoBloco);
        UVs[15] = new Vector2(BottomTextureX * tamanhoBloco + tamanhoBloco, BottomTextureY * tamanhoBloco);
        UVs[13] = new Vector2(BottomTextureX * tamanhoBloco, BottomTextureY * tamanhoBloco + tamanhoBloco);
        UVs[14] = new Vector2(BottomTextureX * tamanhoBloco + tamanhoBloco, BottomTextureY * tamanhoBloco + tamanhoBloco);
        // Left
        UVs[16] = new Vector2(LeftTextureX * tamanhoBloco, LeftTextureY * tamanhoBloco);
        UVs[19] = new Vector2(LeftTextureX * tamanhoBloco + tamanhoBloco, LeftTextureY * tamanhoBloco);
        UVs[17] = new Vector2(LeftTextureX * tamanhoBloco, LeftTextureY * tamanhoBloco + tamanhoBloco);
        UVs[18] = new Vector2(LeftTextureX * tamanhoBloco + tamanhoBloco, LeftTextureY * tamanhoBloco + tamanhoBloco);
        // Right        
        UVs[20] = new Vector2(RightTextureX * tamanhoBloco, RightTextureY * tamanhoBloco);
        UVs[23] = new Vector2(RightTextureX * tamanhoBloco + tamanhoBloco, RightTextureY * tamanhoBloco);
        UVs[21] = new Vector2(RightTextureX * tamanhoBloco, RightTextureY * tamanhoBloco + tamanhoBloco);
        UVs[22] = new Vector2(RightTextureX * tamanhoBloco + tamanhoBloco, RightTextureY * tamanhoBloco + tamanhoBloco);

        mesh.uv = UVs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
