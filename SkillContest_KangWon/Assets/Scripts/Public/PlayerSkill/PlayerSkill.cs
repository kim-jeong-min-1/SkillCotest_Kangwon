using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class PlayerSkill : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    [SerializeField] protected KeyCode GetKey;
    [SerializeField] protected float coolTime;
    [SerializeField] protected float minusMp;

    [SerializeField] protected Image skillCool;
    [SerializeField] protected string actvieText;
    private float curTime;

    public abstract void UseSkill();

    protected virtual void Update()
    {
        curTime += Time.deltaTime;
        skillCool.fillAmount = curTime / coolTime;

        if (Input.GetKeyDown(GetKey) && curTime >= coolTime)
        {
            if (player.PlayerMpEnoughCheck(minusMp))
            {
                curTime = 0;
                player.PlayerMinusMp(minusMp);
                UseSkill();
            }
        }
    }
}