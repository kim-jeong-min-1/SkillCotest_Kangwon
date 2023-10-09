using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
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
    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 rotate;
    private float playerMaxHp;
    private float playerMaxMp;
    private float curTime;
    private bool isPlayerDie = false;

    private void Awake()
    {
        PlayerInit();
    }
    public void PlayerInit()
    {
        playerMaxHp = playerHp;
        playerMaxMp = playerMp;
        playerHpBar.Init(playerMaxHp);
        playerMpBar.Init(playerMaxMp);
        playerMp = 0f;

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        Rotate(rotate);
    }
    private void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= playerAttackDelay)
        {
            PlayerAttack();
        }
        PlayerPlusMp(playerMpRePlay);
    }

    private void Movement()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f,
            Input.GetAxisRaw("Vertical"));

        var cam = Camera.main.transform;
        if (moveInput != Vector3.zero)
        {

            var moveDirection = cam.forward * moveInput.z + cam.right * moveInput.x;
            rotate = moveDirection;
            moveDirection.y = 0;

            rb.velocity = moveDirection * playerSpeed;

            platerAnimator.SetBool("isWalk", true);
        }
        else
        {
            rb.velocity = moveInput;
            platerAnimator.SetBool("isWalk", false);
        }
    }

    private void Rotate(Vector3 rotate)
    {
        if (rotate == Vector3.zero) return;
        var rot = Quaternion.LookRotation(rotate, Vector3.up);
        model.rotation = Quaternion.Slerp(model.rotation, rot, rotateSpeed);
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(aim.position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
            {
                curTime = 0;
                StartCoroutine(attack(hit));
            }
        }

        IEnumerator attack(RaycastHit hit)
        {
            attackLine.gameObject.SetActive(true);
            attackLine.SetPosition(0, firePos.position);
            attackLine.SetPosition(1, hit.point);

            model.rotation = Quaternion.LookRotation(hit.point, Vector3.up);
            hit.transform.GetComponent<IDamagable>().ApplyDamage(playerAttackDamage);

            //파티클 생성
            yield return new WaitForSeconds(0.1f);
            attackLine.gameObject.SetActive(false);
        }
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

    public bool PlayerMpEnoughCheck(float check)
    {
        return (check > playerMp) ? false : true;
    }

    public void ApplyDamage(float damage)
    {
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
