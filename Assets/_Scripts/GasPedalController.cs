using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GasPedalController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerController playerController;
    private bool isPressed = false;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPressed)
            return;

        isPressed = true;
        playerController.playerEvents += playerController.playerMovementInRacingPlatform;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        playerController.playerEvents -= playerController.playerMovementInRacingPlatform;
    }

}