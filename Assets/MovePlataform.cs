using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlataform : MonoBehaviour
{
    SliderJoint2D slider;
    JointMotor2D motor;

    private void Start()
    {
        slider = GetComponent<SliderJoint2D>();
        motor = slider.motor;
    }

    private void Update()
    {
        if(slider.limitState == JointLimitState2D.LowerLimit)
        {
            motor.motorSpeed = 1;
            slider.motor = motor;
            Debug.Log("Gustavo dimas e voce");
        }

        if(slider.limitState == JointLimitState2D.UpperLimit)
        {
            motor.motorSpeed = -1;
            slider.motor = motor;
            Debug.Log("Gustavo dimas e voce tchethceretchetche");
        }
    }
}
