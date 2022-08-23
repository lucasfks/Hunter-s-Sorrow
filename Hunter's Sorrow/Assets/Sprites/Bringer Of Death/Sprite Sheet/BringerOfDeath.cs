using UnityEngine;
using System.Collections;

public class BringerOfDeath : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] Transform m_player;
    [SerializeField] int m_life = 20;

    public Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    public bool m_isDead = false;
    private GameObject attackArea = default;

    private bool _isFacingRight;
    private float _startPos;
    private float _endPos;
    private float m_timeSinceAttack = 0.0f;
    private bool _attacking = false;
    private float _attackTimer = 0f;
    private float _cooldown = 0f;

    public bool _moveRight = true;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        _startPos = transform.position.x;
        _isFacingRight = transform.localScale.x > 0;
        attackArea = transform.GetChild(1).gameObject;
        attackArea.SetActive(false);
    }

    public void Death()
    {
        m_animator.SetTrigger("Death");
        m_isDead = true;
        Destroy(m_body2d);
        Destroy(GetComponent<BoxCollider2D>());
        attackArea.SetActive(false);
    }

    public void Hit()
    {
        if (!m_isDead)
            m_animator.SetTrigger("Hurt");
        m_life -= 1;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        m_timeSinceAttack += Time.deltaTime;

        if (other.CompareTag("Player") && m_timeSinceAttack > 2 && !_attacking && !m_isDead)
        {
            m_animator.SetTrigger("Attack");
            _attacking = true;
            attackArea.SetActive(_attacking);
            m_timeSinceAttack = 0.0f;
        }
    }

    public void Update()
    {
        if (!m_isDead)
        {
            if (m_life <= 0)
                Death();
            _cooldown += Time.deltaTime;
            _endPos = m_player.position.x - m_body2d.position.x;
            if (_attacking)
            {
                _attackTimer += Time.deltaTime;

                if (_attackTimer >= 1)
                {
                    _attackTimer = 0;
                    _attacking = false;
                    attackArea.SetActive(_attacking);
                }
            }
            else
            {
                if (_endPos > 0.3)
                {
                    if (_cooldown >= 2)
                    {
                        //m_body2d.position = new Vector2(m_body2d.position.x - 1.5f, m_body2d.position.y);
                        m_body2d.velocity = new Vector2(-1 * m_speed, m_body2d.velocity.y);
                        if (_cooldown >= 2.5)
                            _cooldown = 0;
                    }
                    else
                    {
                        m_animator.SetInteger("AnimState", 2);
                        m_body2d.velocity = new Vector2(1 * m_speed, m_body2d.velocity.y);
                        if (_isFacingRight)
                            Flip();
                    }
                }
                else if (_endPos < -0.3 && _endPos > -3)
                {
                    if (_cooldown >= 2)
                    {
                        //m_body2d.position = new Vector2(m_body2d.position.x + 1.5f, m_body2d.position.y);
                        m_body2d.velocity = new Vector2(1 * m_speed, m_body2d.velocity.y);
                        if (_cooldown >= 2.5)
                            _cooldown = 0;
                    }
                    else
                    {
                        m_animator.SetInteger("AnimState", 2);
                        m_body2d.velocity = new Vector2(-1 * m_speed, m_body2d.velocity.y);
                        if (!_isFacingRight)
                            Flip();
                    }
                }
            }
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }
}