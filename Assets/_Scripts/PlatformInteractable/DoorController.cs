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

        playerController.playerEvents -= playerController.playerPlatformMovement;
        playerController.levelController.ui.repairBar.incrementValue(0.3f);

        interactableScript.BlendCamera();

        await playerTransform.DOMove(interactablePosition, playerController.movementSpeedZ).SetSpeedBased().AsyncWaitForCompletion();

        interactableScript.Interact(player);
    }

    public void BlendCamera()
    {
        return;
    }
}
