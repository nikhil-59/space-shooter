using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: 0";
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("The Game Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int lives) {
        _livesImage.sprite = _livesSprite[lives];
        if (lives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            if (_gameManager != null)
            {
                _gameManager.GameOver();
            }
            StartCoroutine(GameOverCoroutine());
            
        }
    }

    IEnumerator GameOverCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "Game Over";

        }
    }
}
