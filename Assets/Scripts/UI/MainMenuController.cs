using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject exitButton;

#if UNITY_WEBGL
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        exitButton.SetActive(false);
    }
#else
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        exitButton.SetActive(true);
    }
#endif

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
