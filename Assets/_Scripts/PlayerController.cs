using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeedX;
    public float movementSpeedZ;

    private int platformLayerMask;
    private bool isCarClean = false;
    void Start()
    {
        platformLayerMask = LayerMask.GetMask("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, movementSpeedZ * Time.deltaTime));

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 movement = new Vector3(movementSpeedX * touch.deltaPosition.x, 0, 0);
            if (Physics.Raycast(movement + transform.position, Vector3.down, platformLayerMask))
                transform.Translate(movement);
        }
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
    
}
