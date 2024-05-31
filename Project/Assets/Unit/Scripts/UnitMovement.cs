using System;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;


namespace Supinfo.Project.Unit.Scripts
{
    public class UnitMovement : MonoBehaviour, IMovement
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Move(Vector3 direction, float speed)
        {
            _animator.SetBool("walk", true);
            transform.Translate(speed * Time.deltaTime * direction);
            //_animator.SetBool("walk", false);
        }
    }
}