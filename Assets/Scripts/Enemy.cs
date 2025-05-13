using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4f;
    private Player _player;
    private Animator _animator;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -4.5 && GetComponent<Collider2D>() != null)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            Player player = other.transform.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDestroyed");
            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
        if (other.CompareTag("Laser"))
        {
            // add to score
            if (_player != null)
            {
                _player.AddScore(Random.Range(5,10));
            }
            _animator.SetTrigger("OnEnemyDestroyed");
            Destroy(other.gameObject);
            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }
}
