using UnityEngine;

public class Projectile_S : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
