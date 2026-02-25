using UnityEngine;

public class Invaders_S : MonoBehaviour
{
    public System.Action killed;
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    private SpriteRenderer SpriteR;
    private int animationFrame;

    private void Awake()
    {
        SpriteR = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);

    }

    private void AnimateSprite()
    {
        animationFrame++;

        if (animationFrame >= this.animationSprites.Length)
        {
            animationFrame = 0;
        }

        SpriteR.sprite = this.animationSprites[animationFrame];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("laser"))
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);

        }
    }
}
