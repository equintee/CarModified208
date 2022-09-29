using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    private float gasPerCollectable;
    private UIBarController fuelBar;
    private void Awake()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        gasPerCollectable = levelController.gasPerCollectable;
        fuelBar = levelController.ui.fuelBar;
    }
    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            await fuelBar.IncrementValue(gasPerCollectable, 0.2f);
            fuelBar.DecrementFuelBar();
            
        }
            
    }
}
