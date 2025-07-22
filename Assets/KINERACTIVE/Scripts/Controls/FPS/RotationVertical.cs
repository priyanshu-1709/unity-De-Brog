using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationVertical : MonoBehaviour {

    [SerializeField] private float Sensitivity = 1f;

    private float verticalAxisClamp = 0f;
    private float yAxisClamp = 0f;


    private void Update ()
    {

        float verticalInput = Input.GetAxis("Mouse Y");

        float rotAmountVertical = verticalInput * Sensitivity * Time.deltaTime;

        verticalAxisClamp -= rotAmountVertical;

        Vector3 targetRot = transform.localRotation.eulerAngles;
        targetRot.x -= rotAmountVertical;
        targetRot.z = 0; //prevents cam flipping at exterme angles


        if (verticalAxisClamp > 90)
        {
            verticalAxisClamp = 90;
            targetRot.x = 90;
        }
        else if (yAxisClamp < -90)
        {
            yAxisClamp = -90;
            targetRot.y = 270;
        }

        transform.localRotation = Quaternion.Euler(targetRot);
    }

}
