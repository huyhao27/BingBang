using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainArrow;
    public GameObject auxiliaryArrow; // Arrow phụ
    public float arrowSwingAmplitude = 30.0f; // Biên độ dao động
    public float rotationSpeed = 3.0f; // Tốc độ thay đổi rotation
    public float moveSpeed = 5.0f; // tốc độ di chuyển của player

    // lực
    public float minForce = 1.0f; // Lực tối thiểu
    public float maxForce = 10.0f; // Lực tối đa
    public float chargeDuration = 2.0f; // Thời gian tụ lực
    public float forceOscillationSpeed = 1.0f;
    public float forceMagnitude;

    public Transform targetPoint; // Điểm mục tiêu
    public Transform Force;

    public byte ChargingCount = 1;
    public bool CanMove = true;

    private bool isCharging = false;
    private float chargeStartTime;
    private float currentForce;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!isCharging)
        {
            UpdateAuxiliaryArrowSwing();
        }

        if (Input.GetMouseButtonDown(0) && !isCharging && ChargingCount > 0 )
        {
            StartCharging();
        }

        if (Input.GetMouseButton(0) && isCharging && ChargingCount > 0)
        {
            UpdateCharge(); // Gọi hàm để cập nhật giá trị forceMagnitude
        }

        if (Input.GetMouseButtonUp(0) && isCharging && ChargingCount > 0)
        {
            ReleaseCharge();

        }

        if (isCharging)
        {
            UpdateArrowPosition();
        }

        if (CanMove)
        {
            MovePlayer();
        }
    }

    void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
        forceMagnitude = minForce; // Đặt giá trị khởi đầu

        auxiliaryArrow.SetActive(false);
        mainArrow.SetActive(true);
        Force.gameObject.SetActive(true);

        // Đặt vị trí của arrow chính bằng vị trí hiện tại của arrow phụ
        mainArrow.transform.rotation = auxiliaryArrow.transform.rotation;
    }

    void ReleaseCharge()
    {
        if (isCharging)
        {
            isCharging = false;

            mainArrow.SetActive(false);
            Force.gameObject.SetActive(false);
       
            rb.AddForce(mainArrow.transform.forward * forceMagnitude, ForceMode.Impulse);
            ChargingCount--;
            CanMove = false;
        }
    }

    void UpdateAuxiliaryArrowSwing()
    {
        float swingAngle = Mathf.Sin(Time.time * rotationSpeed) * arrowSwingAmplitude;
        auxiliaryArrow.transform.localRotation = Quaternion.Euler(0f, swingAngle, 0f);
    }
    void UpdateArrowPosition()
    {
        mainArrow.transform.position = auxiliaryArrow.transform.position;
    }

    void UpdateCharge()
    {
        float chargeTime = Time.time - chargeStartTime;
        float forceMultiplier = Mathf.PingPong(chargeTime * forceOscillationSpeed, 1.0f);
        forceMagnitude = Mathf.Lerp(minForce, maxForce, forceMultiplier);
        // Thay đổi Scale X của transform Force dựa trên forceMagnitude
        Vector3 newScale = Force.localScale;
        newScale.x = forceMagnitude/3;
        Force.localScale = newScale;
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);
    }
}
