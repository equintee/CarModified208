using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour, IPlatformInteractable
{
    private Vector3 interactablePosition;
    private IPlatformInteractable interactableScript;
    private void Awake()
    {
        Transform interactable = transform.GetChild(0);
        interactablePosition = interactable.position;
        interactablePosition.y = 0.2f;
        interactableScript = interactable.GetComponent<IPlatformInteractable>();
        
    }
    public async void Interact(GameObject player)
    {
        //Sets player position and closes player input.
        PlayerController playerController = player.GetComponent<PlayerController>();
        Transform playerTransform = player.transform;

        FindObjectOfType<PlayerController>().movementSpeedZ += playerController.car.movementSpeedZ * 0.5f;
        playerController.playerEvents -= playerController.playerPlatformMovement;
        playerController.StopWheelsRotation();
        playerController.levelController.ui.repairBar.IncrementValue(0.3f, 0.15f);

        interactableScript.BlendCamera();

        await playerTransform.DOMove(interactablePosition, playerController.movementSpeedZ).SetSpeedBased().AsyncWaitForCompletion();

        interactableScript.Interact(player);
    }

    public void BlendCamera()
    {
        return;
    }
}
