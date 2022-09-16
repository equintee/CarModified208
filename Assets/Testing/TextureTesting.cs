using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TextureTesting : MonoBehaviour
{
    private Texture2D texture;
    private void Start()
    {
        CleanCar(1f);        
    }

    private async void CleanCar(float cleanTime)
    {
        texture = (Texture2D)GetComponent<MeshRenderer>().material.GetTexture("_Mask");
        Material playerMaterial = GetComponent<MeshRenderer>().material;
        Texture2D dirtMaskTexture = new Texture2D(texture.width, texture.height);
        dirtMaskTexture.SetPixels(texture.GetPixels());
        
        int iterationCount = 100;
        int coloringPerIteration = dirtMaskTexture.height / iterationCount;
        float delayBetweenIteration = cleanTime / iterationCount;
        
        for(int iteration = 0; iteration < iterationCount; iteration++)
        {
            int coloringHeight = iteration == 99 ? dirtMaskTexture.height : (iteration + 1) * coloringPerIteration;
            for (int height = iteration * coloringPerIteration; height < coloringHeight; height++)
            {
                for (int width = 0; width < dirtMaskTexture.width; width++)
                    dirtMaskTexture.SetPixel(width, height, new Color(0, 0, 0));
            }

            await Task.Delay(System.TimeSpan.FromSeconds(delayBetweenIteration));
            dirtMaskTexture.Apply();
            playerMaterial.SetTexture("_Mask", dirtMaskTexture);
        }

    }
    
}
