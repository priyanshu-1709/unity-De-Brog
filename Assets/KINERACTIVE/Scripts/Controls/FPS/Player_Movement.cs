using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using UnityEngine;

namespace Kineractive
{
    public class Player_Movement : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 5f;

        CharacterController _characterController = null;
        Repositioner _repositioner = null;

        bool overrideInput = false;

        float longitudinalInput = 0;
        float lateralInput = 0;

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            _repositioner = GetComponent<Repositioner>();
        }


        void Update()
        {

            if (!overrideInput)
                InputBasedMovement();
        }

        public void InsertVerticalInput(float verticalInput)
        {
            longitudinalInput = verticalInput;
        }

        public void InsertHorizontalInput(float horizontalInput)
        {
            lateralInput = horizontalInput;
        }

        void InputBasedMovement()
        {
            Vector3 moveLongitudinal = transform.forward * longitudinalInput * Time.deltaTime * _moveSpeed;
            Vector3 moveLateral = transform.right * lateralInput * Time.deltaTime * _moveSpeed;
            Vector3 moveVector = moveLongitudinal + moveLateral;

            _characterController.Move(moveVector);
        }

        void EnableInput()
        {
            overrideInput = false;
        }
        void DisableInput()
        {
            overrideInput = true;
        }

        void OnEnable()
        {
            _repositioner.RepoEventStart += DisableInput;
            _repositioner.RepoEventEnd += EnableInput;
        }

        void OnDisable()
        {
            _repositioner.RepoEventStart -= DisableInput;
            _repositioner.RepoEventEnd -= EnableInput;
        }


    }
}
