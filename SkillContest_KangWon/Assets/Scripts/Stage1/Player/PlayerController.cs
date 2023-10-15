using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singletone<PlayerController>, IDamagable
{
    [SerializeField] private PlayerHpBar playerHpBar;
    [SerializeField] private GaugeBar playerMpBar;
    [SerializeField] private Animator platerAnimator;
    [SerializeField] private LineRenderer attackLine;
    [SerializeField] private Transform aim;
    [SerializeField] private Transform firePos;
    [SerializeField] private Transform model;
    [Space(10)]
    [SerializeField] private float playerHp;
    [SerializeField] private float playerMp;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float playerAttackDelay;
    [SerializeField] private float playerAttackDamage;
    [SerializeField] private float playerMpRePlay;
    [Space(10)]
    [SerializeField] private PlayerDefaultAttack defaultAttack;
    public Dictionary<PlayerAttackType, PlayerAttacker> playerAttackers;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 rotate;
    private float playerMaxHp;
    private float playerMaxMp;
    private float curTime;
    private bool isPlayerDie = false;
    public bool isInvincible { get; set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerInit();
    }
    public void PlayerInit()
    {
        playerMaxHp = playerHp;
        playerMaxMp = playerMp;
        playerHpBar.Init(playerMaxHp);
        playerMpBar.Init(playerMaxMp);
        playerMp = 0f;

        playerAttackers = new();
        defaultAttack.SetAttacker();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        Rotate(rotate);
    }
    private void Update()
    {
        PlayerPlusMp(playerMpRePlay);
        aim.transform.position = Input.mousePosition;
    }

    private void Movement()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f,
            Input.GetAxisRaw("Vertical"));

        var cam = Camera.main.transform;
        if (moveInput != Vector3.zero)
        {
            rotate = -moveInput;
            rb.velocity = -moveInput * playerSpeed;

            platerAnimator.SetBool("isWalk", true);
        }
        else
        {
            rb.velocity = moveInput;
            platerAnimator.SetBool("isWalk", false);
        }
        rb.AddForce(Physics.gravity * 2f);
    }

    private void Rotate(Vector3 rotate)
    {
        if (rotate == Vector3.zero) return;
        var rot = Quaternion.LookRotation(rotate, Vector3.up);
        model.rotation = Quaternion.Slerp(model.rotation, rot, rotateSpeed);
    }

    private void PlayerAttack()
    {

    }

    public void PlayerMinusMp(float minus)
    {
        playerMp -= minus;
        playerMp = Mathf.Clamp(playerMp, 0, playerMaxMp);
        playerMpBar.SetGauge(playerMp);
    }
    public void PlayerPlusMp(float plus)
    {
        playerMp += plus;
        playerMp = Mathf.Clamp(playerMp, 0, playerMaxMp);
        playerMpBar.SetGauge(playerMp);
    }

    public void PlayerPlusHp(float plus)
    {
        playerHp += plus;
        playerMp = Mathf.Clamp(playerMp, 0, playerMaxMp);
        playerHpBar.SetHpBarOneSecond(playerHp);
    }

    public bool PlayerMpEnoughCheck(float check)
    {
        return (check > playerMp) ? false : true;
    }

    public void PlayerSpeedChange(float value)
    {
        playerSpeed += value;
    }

    public void ApplyDamage(float damage)
    {
        if (isInvincible) return;

        playerHp -= damage;
        playerHp = Mathf.Clamp(playerHp, 0, playerMaxHp);
        playerHpBar.SetHpBar(playerHp);
        PlayerMinusMp(20f);

        if (playerHp <= 0 && !isPlayerDie)
        {
            isPlayerDie = true;
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        platerAnimator.SetTrigger("Die");
        this.enabled = false;
    }
}
