using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeedX;
    public float movementSpeedZ;

    private int platformLayerMask;
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
}
