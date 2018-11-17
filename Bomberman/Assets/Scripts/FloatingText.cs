using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{

    public Animator animator;
    private Text dmgText;

    void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        dmgText = GetComponentInChildren<Text>();
    }

    public void setText(string text, Color c)
    {
        dmgText.fontSize = 4;
        dmgText.color = c;
        dmgText.text = text;

    }




}
