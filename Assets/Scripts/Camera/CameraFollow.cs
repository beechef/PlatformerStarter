using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   private new Camera camera;
   [SerializeField] private float smoothSpeed;
   [SerializeField] private Vector3 offset;

   private void Awake()
   {
      camera = Camera.main;
   }

   private void LateUpdate()
   {
      camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + offset, smoothSpeed);
   }
}
