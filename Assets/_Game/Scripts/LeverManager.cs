using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverManager : MonoBehaviour
{

    private Vector3 initialPosition;

    private static LeverManager ins;
    public static LeverManager Ins => ins;

    public Transform respawnPoint;

    private void Awake()
    {
        ins = this;
    }

    public void ResetPosition() // Hàm để reset vị trí nhân vật
    {
        transform.position = initialPosition;
        // Nếu cần thiết, bạn có thể reset thêm các trạng thái khác của nhân vật ở đây
    }

    public void ReloadCurrentScene()
    {
        // Lấy tên của scene hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Load lại scene hiện tại bằng tên của nó
        SceneManager.LoadScene(currentSceneName);
    }

    public void LoadNextScene()
    {
        // Lấy index của scene hiện tại
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        // Load scene tiếp theo theo thứ tự index
        SceneManager.LoadScene(currentIndex + 1);
    }
}
