using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zong.Test.Util
{
    public class RotateAroundUtil : MonoBehaviour
    {
        [SerializeField] private float _turnSpeed = 2f;
        void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * _turnSpeed);
        }
    }
}