using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System.Linq;

public class CarWashing : MonoBehaviour, IPlatformInteractable
{

    public void Interact(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        Transform playerTransform = player.transform;

        playerController.setIsClean(true);

        playerTransform.transform.DOMoveZ(transform.position.z + 1f, playerController.movementSpeedZ).SetSpeedBased().OnComplete( () => {
            playerController.playerEvents += playerController.playerPlatformMovement;
        });

        CleanCar(1f, playerTransform.GetChild(0));
    }

    private async void CleanCar(float cleanTime, Transform carModel)
    {
        int iterationCount = 10;
        float delayBetweenIteration = cleanTime / iterationCount;

        PlayerController playerController = FindObjectOfType<PlayerController>();
        Material bodyWorkMaterial = carModel.GetChild(playerController.car.bodyWorkMaterialObjectIndex).GetComponent<MeshRenderer>().materials[playerController.car.bodyWorkMaterialIndex];

        Texture2D dirtMaskTexture = new Texture2D(iterationCount, iterationCount);
        for (int height = 0; height < iterationCount; height++)
            for (int width = 0; width < iterationCount; width++)
                dirtMaskTexture.SetPixel(width, height, Color.green);

        for(int iteration = 0; iteration < iterationCount; iteration++)
        {
            for (int width = 0; width < iterationCount; width++)
                dirtMaskTexture.SetPixel(iteration, width, new Color(0, 0, 0));

            dirtMaskTexture.Apply();
            bodyWorkMaterial.SetTexture("_Mask", dirtMaskTexture);
            await Task.Delay(System.TimeSpan.FromSeconds(delayBetweenIteration));
        }
    }
    public void BlendCamera()
    {
        return;
    }

}
