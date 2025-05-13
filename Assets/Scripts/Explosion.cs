using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 2.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
