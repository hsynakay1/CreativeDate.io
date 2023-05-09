using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyBlockColorSetter : MonoBehaviour
{
    public Color albedoColor;

    public int materialIndex;
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        MaterialPropertyBlock blockColor = new MaterialPropertyBlock();
        blockColor.SetColor("_Color", albedoColor);

        renderer.SetPropertyBlock(blockColor, materialIndex);
    }
}
