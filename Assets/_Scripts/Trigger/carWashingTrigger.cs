using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System.Linq;

public class CarWashingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GetComponent<BoxCollider>().enabled = false;

        PlayerController playerController = other.GetComponent<PlayerController>();
        Transform playerTransform = other.transform;

        playerController.enabled = false;
        playerTransform.DOMoveZ(transform.position.z - 2f, 0);
        this.enabled = true;

        playerTransform.transform.DOMoveZ(transform.position.z + 1f, 1f).OnComplete( () => {
            this.enabled = false;
            playerController.enabled = true;
        });

        CleanCar(1f, playerTransform.GetChild(0));

    }

    private async void CleanCar(float cleanTime, Transform carModel)
    {
        int iterationCount = 10;
        float delayBetweenIteration = cleanTime / iterationCount;

        Texture2D dirtMaskTexture = new Texture2D(iterationCount, iterationCount);
        
        for (int height = 0; height < iterationCount; height++)
        {
            for (int width = 0; width < iterationCount; width++)
                dirtMaskTexture.SetPixel(width, height, Color.green);
        }

        for (int iteration = 0; iteration < iterationCount; iteration++)
        {
            foreach(Material carMaterial in carModel.GetComponent<MeshRenderer>().materials)
            {
                for (int width = 0; width < iterationCount; width++)
                    dirtMaskTexture.SetPixel(iteration, width, new Color(0, 0, 0));
                
                dirtMaskTexture.Apply();
                carMaterial.SetTexture("_Mask", dirtMaskTexture);
            }

            await Task.Delay(System.TimeSpan.FromSeconds(delayBetweenIteration));

        }

    }
}
