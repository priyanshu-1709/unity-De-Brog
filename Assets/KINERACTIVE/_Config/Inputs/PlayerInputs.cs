using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [CreateAssetMenu(menuName = "Kineractive/PlayerInputs")]
    public class PlayerInputs : ScriptableObject
    {
        [SerializeField] protected string[] buttonInputs;
        [SerializeField] protected string[] axisInputs;

        public string[] ButtonInputs
        {
            get { return buttonInputs; }
        }

        public string[] AxisInputs
        {
            get { return axisInputs; }
        }
    }
}