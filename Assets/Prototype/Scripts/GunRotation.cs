using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public float rotationSpeed = 10;
    public Vector3 aimInput;
    Player player;
    PlayerActions actions;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.root.GetComponent<Player>();
        actions = player.Actions;
        aimInput = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //string xAxis = "R_XAxis_" + playerID;
        //string yAxis = "R_YAxis_" + playerID;
        //float inputY = -Input.GetAxis(xAxis);
        //float inputX = -Input.GetAxis(yAxis);
        ////Debug.Log("It's " + inputY + "and " + inputX);

        //Vector3 lookDirection = new Vector3(inputX, inputY, 0);
        //if (lookDirection != Vector3.zero)
        //{
        //    Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.forward);

        //    float step = rotationSpeed * Time.deltaTime;
        //    //transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
        //    transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
        //}
        if (actions.Device == null) // Device is null if the player is using keyboard/mouse.
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            aimInput = new Vector3(mousePosition.x, mousePosition.y, screenPosition.z) - screenPosition;
        }
        else
        {
            aimInput = actions.Aim.Value;
        }
        
        if (actions.Aim)
        {
            Quaternion lookRotation = Quaternion.LookRotation(aimInput, transform.root.forward);

            float step = rotationSpeed * Time.deltaTime;
            //transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
            //Debug.Log("look rotation is " + lookRotation + " and current rotation is " + transform.rotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
        }
    }

    private void LateUpdate()
    {
        //transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
    }
}
