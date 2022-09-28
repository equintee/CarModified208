using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Car car;

    [HideInInspector] public float movementSpeedZ;
    [HideInInspector] public CinemachineStateDrivenCamera cinemachine;
    [HideInInspector] public LevelController levelController;
    private int platformLayerMask;
    private bool isCarClean = false;
    private float movementSpeedX;

    public event Action playerEvents;
    private void Awake()
    {
        cinemachine = FindObjectOfType<CinemachineStateDrivenCamera>();
        levelController = FindObjectOfType<LevelController>();
        platformLayerMask = LayerMask.GetMask("Platform");
    }
    void Start()
    {
        movementSpeedZ = car.movementSpeedZ;
        movementSpeedX = levelController.sensivity;
        playerEvents += playerPlatformMovement;
        SpinWheels();
    }

    // Update is called once per frame
    void Update()
    {
        playerEvents?.Invoke();
    }

    public void playerPlatformMovement()
    {
        transform.Translate(new Vector3(0, 0, movementSpeedZ * Time.deltaTime));

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 movement = new Vector3(movementSpeedX * touch.deltaPosition.x, 0, 0);
            if (Physics.Raycast(movement + transform.position, Vector3.down, platformLayerMask))
                transform.Translate(movement);
        }
    }

    public void SpinWheels()
    {
        foreach(int wheelIndex in car.rimMaterialObjectsIndex)
            transform.GetChild(0).GetChild(wheelIndex).DORotate(new Vector3(360,0,0), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void StopWheelsRotation()
    {
        foreach (int wheelIndex in car.rimMaterialObjectsIndex)
            DOTween.Kill(transform.GetChild(0).GetChild(wheelIndex));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
            other.transform.GetComponent<IPlatformInteractable>().Interact(gameObject);
    }

    public void setIsClean(bool value)
    {
        isCarClean = value;
    }
    public bool getIsClean()
    {
        return isCarClean;
    }
    
    public void playerMovementInRacingPlatform()
    {
        transform.Translate(0, 0, movementSpeedZ * Time.deltaTime);
    }
}
