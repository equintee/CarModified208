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

    public async void Interact(GameObject player)
    {
        this.player = player;

        await playerTransform.DOMove(transform.position, playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
        {
            animator.Play("garageFix");
            fixing.Invoke();
        }).AsyncWaitForCompletion();

        await playerTransform.DOMoveY(transform.position.y + 0.7f, 0.76666666666f).SetEase(Ease.InQuad).AsyncWaitForCompletion();

        await playerTransform.DOMoveY(transform.position.y, 0.76666666666f).SetEase(Ease.InQuad).AsyncWaitForCompletion();
    }
    public void BlendCamera()
    {
        cinemachine.m_DefaultBlend.m_Time = Vector3.Distance(transform.position, playerTransform.position) / playerController.movementSpeedZ;
        cinemachine.m_AnimatedTarget.Play("Garage");
    }
    public void FixExhaust(GameObject fireEffect)
    {
        Transform modelParent = player.transform.GetChild(0);
        PlayerController playerController = FindObjectOfType<PlayerController>();

        foreach (int exhaustParentIndex in playerController.car.exhaustParentIndex)
        {
            GameObject spawnedEffect = Instantiate(fireEffect, Vector3.zero, Quaternion.identity, modelParent.GetChild(exhaustParentIndex));
            spawnedEffect.transform.localPosition = Vector3.zero;
            spawnedEffect.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
            

        Invoke(nameof(ExitGarage), 2.5f);
    }

    public void FixRim()
    {
        Dictionary<GameObject, Material[]> partMaterialDictonary = new Dictionary<GameObject, Material[]>();
        Transform modelParent = player.transform.GetChild(0);
        PlayerController playerController = FindObjectOfType<PlayerController>();

        foreach (int parentIndex in playerController.car.rimMaterialObjectsIndex)
            partMaterialDictonary.Add(modelParent.GetChild(parentIndex).gameObject, modelParent.GetChild(parentIndex).GetComponent<MeshRenderer>().materials);

        Texture2D mask = new Texture2D(1, 1);
        mask.SetPixel(0, 0, Color.black);
        foreach (var partMaterialEntry in partMaterialDictonary)
        {
            foreach (Material material in partMaterialEntry.Value)
                material.SetTexture("_Mask", mask);
            SetMaterials(partMaterialEntry.Key, partMaterialEntry.Value);
        }

        Invoke(nameof(ExitGarage), 2.5f);
    }


    public void FixLights()
    {
        GameObject lightsParent = player.transform.GetChild(0).GetChild(playerController.car.lightMaterialObjectIndex).gameObject;
        Material[] carMaterials = GetMaterials(lightsParent);
        Texture2D mask = new Texture2D(1, 1);
        mask.SetPixel(0, 0, Color.black);

        int[] lightsIndex = FindObjectOfType<PlayerController>().car.lightMaterialIndex;

        foreach (int index in lightsIndex)
            carMaterials[index].SetTexture("_Mask", mask);

        SetMaterials(lightsParent, carMaterials);
        Invoke(nameof(ExitGarage), 2.5f);
    }
    private Material[] GetMaterials(GameObject player)
    {
        return player.transform.GetComponent<MeshRenderer>().materials;
    }

    private void SetMaterials(GameObject objectParent, Material[] carMaterials)
    {
        objectParent.GetComponent<MeshRenderer>().materials = carMaterials;
    }

    private void ExitGarage()
    {
        cinemachine.m_DefaultBlend.m_Time = Vector3.Distance(transform.position + new Vector3(0, 0, 2), playerTransform.position) / playerController.movementSpeedZ;
        cinemachine.m_AnimatedTarget.Play("playerFollow");

        playerTransform.DOMove(transform.position + new Vector3(0, 0, 2), playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
        {
            playerController.playerEvents += playerController.playerPlatformMovement;
            playerController.SpinWheels();
        });
    }
}
