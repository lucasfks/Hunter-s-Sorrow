using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] Transform m_player;

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

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //    if (other.CompareTag("Player"))
    //    {
    //        _player = other.gameObject.Find(other.name);
    //        _player.Hit();
    //    }
    // }

    public void Death()
    {
        m_animator.SetTrigger("Death");
        m_isDead = true;
        Destroy(m_body2d);
        Destroy(GetComponent<BoxCollider2D>());
        attackArea.SetActive(false);
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
            if (_attacking)
            {
                _attackTimer += Time.deltaTime;

                if (_attackTimer >= 2)
                {
                    _attackTimer = 0;
                    _attacking = false;
                    attackArea.SetActive(_attacking);
                }
            }
            _endPos = m_player.position.x - m_body2d.position.x;

            if (_endPos > 0)
            {
                m_animator.SetInteger("AnimState", 2);
                m_body2d.velocity = new Vector2(1 * m_speed, m_body2d.velocity.y);
                if (_isFacingRight)
                    Flip();
            }

            if (_endPos < 0 && _endPos > -3)
            {
                m_animator.SetInteger("AnimState", 2);
                m_body2d.velocity = new Vector2(-1 * m_speed, m_body2d.velocity.y);
                if (!_isFacingRight)
                    Flip();
            }
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }

    // Update is called once per frame
    void Update2()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e"))
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }

        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }
}
