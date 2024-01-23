using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public float rotationSpeed = 20f; // Tốc độ xoay
    public teleport exitPortal; // The portal this one leads to
    private bool isTeleporting = false;
    void Start()
    {
        // Gọi coroutine để xoay cổng teleport
        StartCoroutine(RotatePortal());
    }
    IEnumerator RotatePortal()
    {
        while (true) // Lặp vô hạn để xoay cổng teleport
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            yield return null; // Chờ một frame sau khi thực hiện xoay
        }
    }
    void Update()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && exitPortal != null)
        {
            exitPortal.isTeleporting = true;

            // Teleport the object to the exit portal
            other.transform.position = exitPortal.transform.position;
            //other.transform.rotation = exitPortal.transform.rotation;

            // If the object is a Rigidbody, preserve its velocity
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = exitPortal.transform.rotation * Quaternion.Inverse(transform.rotation) * rb.velocity;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isTeleporting)
        {
            isTeleporting = false;
        }
    }
}
