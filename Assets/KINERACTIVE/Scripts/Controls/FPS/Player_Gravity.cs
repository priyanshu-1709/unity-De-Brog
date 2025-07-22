using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using UnityEngine;

namespace Kineractive
{
    public class Player_Gravity : MonoBehaviour
    {
        [SerializeField] float _gravitySpeed = 9.82f;

        CharacterController _characterController = null;

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }


        void Update()
        {
            Vector3 gravity = new Vector3(0, -_gravitySpeed, 0);
            _characterController.Move(gravity * Time.deltaTime);
        }


    }
}
