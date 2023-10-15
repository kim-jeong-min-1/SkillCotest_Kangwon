using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloom3 : MonoBehaviour
{
    [SerializeField] private Material bloom;

    [Range(0, 1)]
    [SerializeField] private float intensity;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(bloom == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        bloom.SetFloat("_Intensity", intensity);
        Graphics.Blit(source, destination, bloom);
    }
}
