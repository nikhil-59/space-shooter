using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.1f;
    private float _nextFire = 0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _tripleShotActive = false;
    private bool _speedPowerupActive = false;
    private float _speedPowerupValue = 3.5f;
    private bool _shieldPowerupActive = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private int _score;
    private UIManager _uimanager;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _shield.SetActive(false);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.Log("The spawn manager is null");
        }
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uimanager == null)
        {
            Debug.Log("The UI Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextFire)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
        if (_tripleShotActive)
        {
            Instantiate(_tripleShotPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1f), Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 nextPosition = new Vector3(horizontalInput, verticalInput);
        if (_speedPowerupActive)
        {
            transform.Translate(nextPosition * (_speed + _speedPowerupValue) * Time.deltaTime);
        }
        else
        {
            transform.Translate(nextPosition * _speed * Time.deltaTime);
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0));

        if (transform.position.x >= 11.3f || transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(-1 * transform.position.x, transform.position.y);
        }
    }

    public void Damage()
    {
        if (_shieldPowerupActive)
        {
            _shield.SetActive(false);
            _shieldPowerupActive = false;
            return;
        }
        _lives--;
       if(_lives == 2)
       {
            _leftEngine.SetActive(true);
       }
       if(_lives == 1)
       {
            _rightEngine.SetActive(true);
       }
        _uimanager.UpdateLives(_lives);
        if( _lives == 0)
        {
            if (_spawnManager != null) {
                _spawnManager.OnPlayerDeath();
            }
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void ActivateSpeedPowerup()
    {
        _speedPowerupActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void ActivateShieldPowerup()
    {
        _shieldPowerupActive = true;
        _shield.SetActive(true);
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        while (_tripleShotActive)
        {
            yield return new WaitForSeconds(5f);
            _tripleShotActive = false;
        }
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        while (_speedPowerupActive)
        {
            yield return new WaitForSeconds(5f);
            _speedPowerupActive = false;
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uimanager.UpdateScoreText(_score);
    }
}
