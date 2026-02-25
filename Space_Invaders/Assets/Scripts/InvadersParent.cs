
using Unity.Mathematics;
using UnityEngine;

public class InvadersParent : MonoBehaviour
{
    public Invaders_S[] prefabs;
    public int rows = 5;
    public int columns = 11;

    public Projectile_S missilePrefab;

    private Vector3 direction = Vector2.right;
    public AnimationCurve speed;

    public float missaileAttackRate = 2.0f;

    public int amountKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;

    public int amountAlive => this.totalInvaders - this.amountKilled;

    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;

    private void Awake()
    {
        for (int raw = 0; raw < this.rows; raw++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + raw * 2.0f, 0.0f);

            for (int col = 0; col < this.columns; col++)
            {

                Invaders_S invader = Instantiate(this.prefabs[raw], this.transform);
                invader.killed += invaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(missaileAttack), this.missaileAttackRate, this.missaileAttackRate);

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);

        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invaders in this.transform)
        {
            if (!invaders.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && invaders.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            }
            else if (direction == Vector3.left && invaders.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }

    }

    private void AdvanceRow()
    {
        direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;

    }

    private void invaderKilled()
    {
        this.amountKilled++;
    }

    private void missaileAttack()
    {
        foreach (Transform invaders in this.transform)
        {
            if (!invaders.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (UnityEngine.Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, invaders.position, quaternion.identity);
                break;
            }


        }

    }
}
