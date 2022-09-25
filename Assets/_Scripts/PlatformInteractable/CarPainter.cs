using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarPainter : MonoBehaviour, IPlatformInteractable
{
    public Material paint;


    public void Interact(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        Transform playerTransform = player.transform;

        playerTransform.DOMoveZ(transform.position.z + 3, playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
        {
             playerController.playerEvents += playerController.playerPlatformMovement;
        });

        Texture2D mask = new Texture2D(1, 1);
        if (playerController.getIsClean())
        {
            mask.SetPixel(0, 0, Color.black);
            paint.SetTexture("_Mask", mask);
        }

        Material[] carMaterials = player.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
        carMaterials[^1] = paint;
        player.transform.GetChild(0).GetComponent<MeshRenderer>().materials = carMaterials;

    }
    public void BlendCamera()
    {
        return;
    }
}
