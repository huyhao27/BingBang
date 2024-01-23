using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contact : MonoBehaviour
{
    public float Stickiness = 100f; // Độ dính của quả bóng

    // Biến để kiểm tra xem có quả bóng nào đã dính vào không
    private bool isBallStuck = false;

    // Biến để lưu tham chiếu đến quả bóng đã dính
    private GameObject stuckBall;

    // Hàm này được gọi khi có va chạm xảy ra
    void OnCollisionEnter(Collision other)
    {
        // Kiểm tra xem quả bóng chưa được dính và đối tượng va chạm có tag "Ball" không
        if (!isBallStuck && other.gameObject.CompareTag("Player"))
        {
            // Đánh dấu rằng quả bóng đã được dính
            isBallStuck = true;

            // Lưu tham chiếu đến quả bóng đã dính vào biến stuckBall
            stuckBall = other.gameObject;

            // Áp dụng lực dính vào quả bóng
            Rigidbody ballRigidbody = stuckBall.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Tính toán và áp dụng lực dính dựa trên độ dính (Stickiness)
                // và khoảng cách giữa đối tượng Player và quả bóng
                Vector3 forceDirection = stuckBall.transform.position - transform.position;
                ballRigidbody.AddForce(Stickiness * forceDirection);
            }
        }
    }

    //...
}
