using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Text name;
    [SerializeField] private Text explain;
    [SerializeField] private Text level;
    [SerializeField] private PlayerAttacker attacker;

    public void Init(Sprite sprite, string name, string explain, int level, PlayerAttacker obj)
    {
        icon.sprite = sprite;
        this.name.text = name;
        this.explain.text = explain;
        this.level.text = $"Level : {level}";
        attacker = obj;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerLevel.Inst.SelectEnd();
        if (PlayerController.Inst.playerAttackers.ContainsKey(this.attacker.type))
        {
            PlayerController.Inst.playerAttackers[this.attacker.type].LevelUP();
            return;
        }
        var attacker = Instantiate(this.attacker, PlayerController.Inst.transform);
        attacker.SetAttacker();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
