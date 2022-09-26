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
        playerTransform.DOMoveZ(transform.position.z + 3, playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
        {
             playerController.playerEvents += playerController.playerPlatformMovement;
        });

        Material[] carMaterials = player.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
        carMaterials[^1].SetTexture("_MainTexture", paint);
        player.transform.GetChild(0).GetComponent<MeshRenderer>().materials = carMaterials;

    }
    public void BlendCamera()
    {
        return;
    }
}
