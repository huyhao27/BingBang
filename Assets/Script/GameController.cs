using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Thêm thư viện TextMesh Pro
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private byte activeCount = 0;
    [SerializeField] private bool Moved = true;
    [SerializeField] private bool isClick = false;
    [SerializeField] private int playerCount; // Số lượng player trong trò chơi
    [SerializeField] private int playerRemaining = 0;
    public GameObject[] players; // Mảng chứa các player
    private int currentPlayerIndex = 0; // Chỉ số của player hiện tại

    public TextMeshProUGUI totalScoreEndgame;
    public TextMeshProUGUI totalScoreText; // Tham chiếu đến Text để hiển thị tổng điểm
    private int totalScore = 0; // Biến để lưu tổng điểm
    public Button playAgainButton;
    public Canvas canvas; // Tham chiếu đến Canvas cần hiển thị


    // Hàm này để cập nhật tổng điểm
    public void UpdateTotalScore(int playerScore)
    {
        totalScore += playerScore;
        UpdateTotalScoreUI();
    }

    // Hàm này để cập nhật hiển thị tổng điểm trên UI
    private void UpdateTotalScoreUI()
    {
        if (totalScoreText != null)
        {
            totalScoreText.text = "Tổng Điểm: " + totalScore.ToString();
        }
    }
    void Start()
    {
        playerRemaining = playerCount;
        // Ẩn tất cả các player trừ player đầu tiên
        for (int i = 1; i < playerCount; i++)
        {
            players[i].SetActive(false);
        }
    }
    void Update()
    {
        if(Input.GetMouseButtonUp(0)) 
        {
            playerRemaining--;
            StartCoroutine(DelayActive());
            if (playerRemaining == 0)
            {
                StartCoroutine(DelayRemaining());
            }
        }
        CheckMove();
        // Kiểm tra nếu player hiện tại đã dừng lại
        if (!Moved&& isClick  )
        {
            ActivateNextPlayer();
        }
    }

    void ActivateNextPlayer()
    {
        if (activeCount < playerCount - 1)
        {
            activeCount++;
        }
        // Vô hiệu hóa player hiện tại
        //  players[currentPlayerIndex].SetActive(false);

        // Tăng chỉ số của player lên 1, nếu vượt quá số lượng player thì trở về player đầu tiên
        currentPlayerIndex = activeCount;

        // Kích hoạt player tiếp theo
        players[currentPlayerIndex].SetActive(true);
        isClick = false;
    }
    void CheckMove()
    {
        if (activeCount < playerCount)
        {
            if (Mathf.Abs(players[activeCount].GetComponent<Rigidbody>().velocity.magnitude) > 0.05f)
            {
                Moved = true;
            }
            else
            {
                Moved = false;
            }
        }
        

    }
    IEnumerator DelayActive()
    {
        yield return new WaitForSeconds(0.5f); // Delay 0.5s
        isClick = true;
    }
    IEnumerator DelayRemaining()
    {
        yield return new WaitForSeconds(3f); // Delay 3s
                                             // Dừng tất cả các GameObject đang hoạt động
        totalScoreText.gameObject.SetActive(false);
        // Hiển thị Canvas
        canvas.gameObject.SetActive(true);
        totalScoreEndgame.text = "Tổng Điểm: " + totalScore.ToString();
        playAgainButton.onClick.AddListener(OnPlayAgain);

    }
    void OnPlayAgain()
    {
        // Tải lại Scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
