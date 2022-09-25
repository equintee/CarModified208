using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Garage : MonoBehaviour, IPlatformInteractable
{

    [SerializeField] private UnityEvent fixing;

    private GameObject player;
    private CinemachineStateDrivenCamera cinemachine;
    private Animator animator;
    private PlayerController playerController;
    private Transform playerTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.transform;
        cinemachine = FindObjectOfType<CinemachineStateDrivenCamera>();
    }

    public void Interact(GameObject player)
    {
        this.player = player;
        playerTransform.DOMove(transform.position, playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
        {
            animator.Play("garageFix");
            fixing.Invoke();
        });
    }
    public void BlendCamera()
    {
        cinemachine.m_DefaultBlend.m_Time = Vector3.Distance(transform.position, playerTransform.position) / playerController.movementSpeedZ;
        cinemachine.m_AnimatedTarget.Play("Garage");
    }
    public void FixExhaust()
    {
        //Alev
    }

    public void FixRim()
    {
        Material[] carMaterials = GetMaterials(player);
        Texture2D mask = new Texture2D(1, 1);
        mask.SetPixel(0, 0, Color.black);

        int[] rimMaterialsIndex = FindObjectOfType<PlayerController>().car.rimMaterialIndex;

        foreach (int index in rimMaterialsIndex)
            carMaterials[index].SetTexture("_Mask", mask);

        SetMaterials(player, carMaterials);
    }


    public void FixHeadlight()
    {
        Material[] carMaterials = GetMaterials(player);
        Texture2D mask = new Texture2D(1, 1);
        mask.SetPixel(0, 0, Color.black);

        int[] lightsIndex = FindObjectOfType<PlayerController>().car.lightsIndex;

        foreach (int index in lightsIndex)
            carMaterials[index].SetTexture("_Mask", mask);

        SetMaterials(player, carMaterials);
    }
    private Material[] GetMaterials(GameObject player)
    {
        return player.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
    }

    private void SetMaterials(GameObject player, Material[] carMaterials)
    {
        player.transform.GetChild(0).GetComponent<MeshRenderer>().materials = carMaterials;
        Invoke(nameof(ExitGarage), 2.5f);
    }

    private void ExitGarage()
    {
        cinemachine.m_DefaultBlend.m_Time = Vector3.Distance(transform.position + new Vector3(0, 0, 2), playerTransform.position) / playerController.movementSpeedZ;
        cinemachine.m_AnimatedTarget.Play("playerFollow");

        playerTransform.DOMove(transform.position + new Vector3(0, 0, 2), playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
        {
            playerController.playerEvents += playerController.playerPlatformMovement;
        });
    }
}
