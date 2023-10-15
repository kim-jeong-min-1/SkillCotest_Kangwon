using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloom2 : MonoBehaviour
{
    [SerializeField] private Material bloom;
    [Range(0, 1)]
    [SerializeField] private float Intensity;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(bloom == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        bloom.SetFloat("_Intensity", Intensity);
        Graphics.Blit(source, destination, bloom);
    }
}
