using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPoint : MonoBehaviour
{
    public byte Point = 0;
    private byte previousPoint = 0; // Biến để lưu trữ giá trị Point cũ
    private bool p1,p2,p3 = false;
    private GameController GameController; // Tham chiếu đến GameController

    // Start is called before the first frame update
    private void Start()
    {
        GameController = FindObjectOfType<GameController>(); // Tìm GameController trong Scene
    }

    // Update is called once per frame
    void Update()
    {
        byte currentPoint = 0;
        if (p3 == true)
        {
            currentPoint = 3;

        }
        else if (p2 == true)
        {
            currentPoint = 2;
        }
        else if (p1 == true)
        {
            currentPoint = 1;
        }
        else
        {
            currentPoint = 0;
        }
        if (currentPoint != previousPoint)
            {
                GameController.UpdateTotalScore(currentPoint - previousPoint); // Cộng hoặc trừ điểm tương ứng
                previousPoint = currentPoint; // Cập nhật giá trị Point cũ
            }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("1point"))
        {
            p1 = true;
        }
        if (other.gameObject.CompareTag("2point"))
        {
            p2 = true;
        }
        if (other.gameObject.CompareTag("3point"))
        {
            p3 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("1point"))
        {
            p1 = false;
        }
        else if (other.gameObject.CompareTag("2point"))
        {
            p2 = false;
        }
        else if (other.gameObject.CompareTag("3point"))
        {
            p3 = false;
        }
    }
}
