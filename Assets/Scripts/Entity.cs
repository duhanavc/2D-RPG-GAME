using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    [Header("Collision �nfo")]
    public Transform attackCheck;
    public float attackRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackPower;
    protected bool isKnocked;
    [SerializeField] protected float knockbackDuration;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update ()
    { 

    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        Debug.Log(gameObject.name + " " + "Damaged! ");
        
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;

        
        rb.velocity = new Vector2(knockbackPower.x * -facingDir, knockbackPower.y);
        
        yield return new WaitForSeconds(knockbackDuration);
        
        isKnocked = false;
    }

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    
    #endregion

    #region Collision
    public virtual bool isWallDedected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    public virtual bool isGroundDedected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {

        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }

    public virtual void FlipController(float _x)
    {

        if (_x > 0 && !facingRight)
            Flip();

        else if (_x < 0 && facingRight)
            Flip();

    }
    #endregion

    #region Velocity
    public void SetZeroVelocity() {


        if (isKnocked)
            return;

        rb.velocity = new Vector2(0, 0); 

    } 

    public void setVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);

        

    }
    #endregion

}
