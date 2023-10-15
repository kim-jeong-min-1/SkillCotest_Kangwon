using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bloom : MonoBehaviour
{
    public Material bloomMaterial;
    [Range(0, 1)]
    public float bloomIntensity;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (bloomMaterial == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        bloomMaterial.SetFloat("_Intensity", bloomIntensity);

        Graphics.Blit(source, destination, bloomMaterial);
    }
}