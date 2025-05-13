using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameOver = false;

    private void Update()
    {
        if (_gameOver) {
            if (Input.GetKey(KeyCode.R))
            {
                _gameOver = false;
                SceneManager.LoadScene(1);
            }
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}
