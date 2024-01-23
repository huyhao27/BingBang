using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repel : MonoBehaviour
{
    private bool isClick = false;
    private bool hadRepel = false; 
    public bool CanRepel = true;
    private bool isMoving = false;

    public float repelRadius = 5f; // Bán kính vùng đẩy
    public float repelForce = 10f; // Lực đẩy

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && CanRepel)
        {
            isClick = true;
        }
        if (isClick)
        {
            CheckMove();
        }
    }
    void ActiveRepel()
    {
        if (!hadRepel)
        {
            Debug.Log("da chay");
            CanRepel = false;
            isClick = false;
            hadRepel = true;
            // đóng băng X Y Z
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
                             RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            // Tìm các vật xung quanh player trong bán kính attractRadius
            Collider[] colliders = Physics.OverlapSphere(transform.position, repelRadius);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        // Tính vector hướng từ player đến vật
                        Vector3 direction =- transform.position + rb.transform.position;
                        // Áp dụng lực kéo về player
                        rb.AddForce(direction.normalized * repelForce, ForceMode.Impulse);
                    }
                }
            }
            StartCoroutine(PerformRepelAfterDelay()); // Gọi coroutine
        }
    }

    void CheckMove()
    {

        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.magnitude) > 0.01f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            if (!hadRepel)
            {
                StartCoroutine(RepelDelay()); // Gọi coroutine
            }
        }
    }
    IEnumerator PerformRepelAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Đợi 1 giây

        // Thực hiện hành động sau khi đợi
        Debug.Log("doi xong 1s");

        // Mở lại constraints và cho phép di chuyển tự do
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    IEnumerator RepelDelay()
    {
        yield return new WaitForSeconds(3f); // Đợi 2 giây

        // Thực hiện hành động sau khi đợi
        Debug.Log("da doi xong 2s");

        ActiveRepel();
    }
}
