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

        playerTransform.DOMove(transform.position + new Vector3(0, 0, 5), playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
         {
             playerController.enabled = true;
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
}
