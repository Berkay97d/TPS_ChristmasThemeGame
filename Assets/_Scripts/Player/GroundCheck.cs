using System;
using UnityEngine;
public class GroundCheck : MonoBehaviour 
{
    
        [SerializeField] private Mesh cubeMesh;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Vector3 boxSize;
        [SerializeField, Min(0f)] private float maxDistance;

        public static bool isGrounded;
        
        private void BoxCast(Vector3 origin, Vector3 direction, Quaternion rotation)
        {
            var isHit = Physics.BoxCast(origin,
                boxSize * 0.5f, 
                direction, 
                out var hitInfo,
                rotation,
                maxDistance, 
                layerMask);

            if (isHit)
            {
                isGrounded = true;
            }

            else
            {
                isGrounded = false;
            }
        }
        
        private void Update()
        {
            var origin = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            var direction = -transform.up;
            var rotation = transform.rotation;

            BoxCast(origin, direction, rotation);
        }

        private void OnValidate()
        {
            boxSize.x = Mathf.Max(0f, boxSize.x);
            boxSize.y = Mathf.Max(0f, boxSize.y);
            boxSize.z = Mathf.Max(0f, boxSize.z);
        }
}
   

