using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    public enum AxisPolarity
    {
        Positive,
        Negative
    }


    public class InputExtended : MonoBehaviour
    {

        public static bool GetAxisDownNegative(string axis, float previousAxisValue)
        {
            if (UnityEngine.Input.GetAxisRaw(axis) < 0 &&
                previousAxisValue == 0)
            {
                return true;
            }
            else
                return false;
        }

        public static bool GetAxisUpNegative(string axis, float previousAxisValue)
        {
            if (UnityEngine.Input.GetAxisRaw(axis) == 0 &&
                previousAxisValue < 0)
            {
                return true;
            }
            else
                return false;
        }

        public static bool GetAxisDownPositive(string axis, float previousAxisValue)
        {
            if (UnityEngine.Input.GetAxisRaw(axis) > 0 &&
                previousAxisValue == 0)
            {
                return true;
            }
            else
                return false;
        }

        public static bool GetAxisUpPositive(string axis, float previousAxisValue)
        {
            if (UnityEngine.Input.GetAxisRaw(axis) == 0 &&
                previousAxisValue > 0)
            {
                return true;
            }
            else
                return false;
        }

        public static bool GetAxisNegative(string axis)
        {
            if (UnityEngine.Input.GetAxis(axis) < 0)
                return true;
            else
                return false;
        }

        public static bool GetAxisPositive(string axis)
        {
            if (UnityEngine.Input.GetAxis(axis) > 0)
                return true;
            else
                return false;
        }
    }
}