using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed *  Time.deltaTime);
        if(transform.position.y > 8)
        {
            Destroy(this.gameObject);
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
          
        }
    }

}
