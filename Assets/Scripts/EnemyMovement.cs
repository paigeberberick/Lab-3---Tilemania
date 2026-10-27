using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float wallCheckDistance = 0.1f;

    private LayerMask wallLayer;
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        wallLayer = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        float direction = movingRight ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (IsHittingWall(direction))
        {
            Flip();
        }
    }

    private bool IsHittingWall(float direction)
    {
        Vector2 origin = new Vector2(
            col.bounds.center.x + direction * col.bounds.extents.x,
            col.bounds.center.y
        );

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * direction, wallCheckDistance, wallLayer);
        return hit.collider != null;
    }

    private void Flip()
    {
        movingRight = !movingRight;
        spriteRenderer.flipX = !movingRight;
    }
}