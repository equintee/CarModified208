using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarPainter : MonoBehaviour, IPlatformInteractable
{
    public Texture2D paint;

    private Animator animator;
    private List<ParticleSystem> particles;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        particles = new List<ParticleSystem>();

        foreach (Transform particleTransform in transform.GetChild(0))
            particles.Add(particleTransform.GetComponent<ParticleSystem>());
    }
    public void Interact(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        Transform playerTransform = player.transform;
        Material bodyWorkMaterial = playerTransform.GetChild(0).GetChild(playerController.car.bodyWorkMaterialObjectIndex).GetComponent<MeshRenderer>().materials[playerController.car.bodyWorkMaterialIndex];

        foreach (ParticleSystem particle in particles)
            particle.Play();

        animator.Play("CarPainterAnimation");

        Invoke(nameof(ExitPainter), 2f);

    }

    public void ExitPainter()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        Transform playerTransform = playerController.transform;
        Material bodyWorkMaterial = playerTransform.GetChild(0).GetChild(playerController.car.bodyWorkMaterialObjectIndex).GetComponent<MeshRenderer>().materials[playerController.car.bodyWorkMaterialIndex];
        playerTransform.DOMoveZ(transform.position.z + 3, playerController.movementSpeedZ).SetSpeedBased().OnComplete(() =>
        {
            playerController.playerEvents += playerController.playerPlatformMovement;
            playerController.SpinWheels();
        });

        bodyWorkMaterial.SetTexture("_MainTexture", paint);
    }
    public void BlendCamera()
    {
        return;
    }
}
