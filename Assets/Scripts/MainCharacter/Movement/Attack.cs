﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Attack : MonoBehaviour {

    float buttonLeft = Screen.width * 0.88f;
    float buttonRight = Screen.width * 0.98f;
    float buttonUp = Screen.height * 0.33f;
    float buttonDown = Screen.height * 0.19f;

    private bool isAttacking = false;
    public Combat mainCharacterCombatComponent;
    private HandleAnimations mainCharacterAnimationComponent;

    public Image glowImage;
    bool animation = false;
    private float glowImageMaxScale = 2.5f;
    private float glowImageSpeed = 0.02f;
    private float glowImageAlphaSpeed;
    private Vector2 glowImageScaleNow;

    public int enemiesNumber = 0;
    private bool imageGlowed;

    private bool quickTimeEventMode = false;

    private LurkingEnemy lurkingEnemyComponent;

    public void SetLurkingEnemyComponent(LurkingEnemy LEComponent)
    {
        lurkingEnemyComponent = LEComponent;
    }

    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }

        set
        {
            isAttacking = value;
        }
    }

    public bool ImageGlowed
    {
        get
        {
            return imageGlowed;
        }

        set
        {
            imageGlowed = value;
        }
    }

    public bool QuickTimeEventMode
    {
        get
        {
            return quickTimeEventMode;
        }
        set
        {
            quickTimeEventMode = value;
            glowImage.enabled = value;
        }
    }

    // Use this for initialization
    void Start () {
        mainCharacterAnimationComponent = mainCharacterCombatComponent.GetComponent<HandleAnimations>();
        glowImageAlphaSpeed = glowImage.color.a / ((glowImageMaxScale - glowImage.GetComponent<Transform>().localScale.x) / glowImageSpeed);
    }
	
	// Update is called once per frame
	void Update () {
	    if(GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.Z) && !mainCharacterAnimationComponent.getAttackClicked() && !quickTimeEventMode)
        {
            mainCharacterCombatComponent.MeleeAttack();
        }

        if(animation && !quickTimeEventMode)
        {
            InflateGlowImage();
        }

        if(quickTimeEventMode)
        {
            //glowImage.enabled = true;
            RepeatInflateGlowImage();
            if (GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.Z) && !mainCharacterAnimationComponent.getAttackClicked())
            {
                lurkingEnemyComponent.EnemyGotHit();
            }
        }


	}

    public void StartGlowing(int enemyTabLength)
    {
        imageGlowed = true;
        glowImage.enabled = true;
        enemiesNumber = enemyTabLength;
        animation = true;
    }

    private void InflateGlowImage()
    {
        glowImage.GetComponent<Transform>().localScale = new Vector2(glowImage.GetComponent<Transform>().localScale.x + glowImageSpeed, glowImage.GetComponent<Transform>().localScale.x + glowImageSpeed);
        glowImage.color = new Color(glowImage.color.r, glowImage.color.g, glowImage.color.b, glowImage.color.a - glowImageAlphaSpeed);
        

        if(glowImage.color.a <= 0)
        {
            animation = false;
            glowImage.enabled = false;
            glowImage.GetComponent<Transform>().localScale = new Vector2(1.0f, 1.0f);
            glowImage.color = new Color(glowImage.color.r, glowImage.color.g, glowImage.color.b, 1.0f);
        }
    }

    private void RepeatInflateGlowImage()
    {
        glowImage.GetComponent<Transform>().localScale =  new Vector2(glowImage.GetComponent<Transform>().localScale.x + 4.0f * glowImageSpeed, glowImage.GetComponent<Transform>().localScale.x + 4.0f * glowImageSpeed);
        glowImage.color = new Color(glowImage.color.r, glowImage.color.g, glowImage.color.b, glowImage.color.a - 4.0f * glowImageAlphaSpeed);


        if (glowImage.color.a <= 0)
        {
            glowImage.GetComponent<Transform>().localScale = new Vector2(1.0f, 1.0f);
            glowImage.color = new Color(glowImage.color.r, glowImage.color.g, glowImage.color.b, 1.0f);
        }
    }
}
