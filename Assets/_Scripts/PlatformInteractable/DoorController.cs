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
        interactableScript = interactable.GetComponent<IPlatformInteractable>();
    }
    public async void Interact(GameObject player)
    {
        //Sets player position and closes player input.
        PlayerController playerController = player.GetComponent<PlayerController>();
        Transform playerTransform = player.transform;

        playerController.enabled = false;
        await playerTransform.DOMove(transform.position + new Vector3(0,0,1), playerController.movementSpeedZ).SetSpeedBased().AsyncWaitForCompletion();

        interactableScript.Interact(player);
    }
    
    

}
