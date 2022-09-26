using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarPainter : MonoBehaviour, IPlatformInteractable
{
    public Texture2D paint;

    public void Interact(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        Transform playerTransform = player.transform;
        Material bodyWorkMaterial = playerTransform.GetChild(0).GetChild(playerController.car.bodyWorkMaterialObjectIndex).GetComponent<MeshRenderer>().materials[playerController.car.bodyWorkMaterialIndex];

        playerTransform.DOMoveZ(transform.position.z + 3, playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
        {
             playerController.playerEvents += playerController.playerPlatformMovement;
        });

        bodyWorkMaterial.SetTexture("_MainTexture", paint);

    }
    public void BlendCamera()
    {
        return;
    }
}
