using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public bool amINotInScene = false;
    public int myValue;
    private GameObject gm;
    private UI_Manager m_UIManager;

    private AudioSource _audioSource;
    private Animator m_Animator;
    
    public AudioClip deathSound;
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        m_Animator = GetComponent<Animator>();
        if (!amINotInScene)
        {
            gm = GameObject.FindWithTag("UI_Manager");
            m_UIManager = gm.GetComponent<UI_Manager>();
        }
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            m_Animator.SetTrigger("Dead");
            PlaySound(deathSound);
            // Debug.Log("Ouch!");
            m_UIManager.UpdateCurrentScore(myValue);
            MotherShip.RepeatSpeed -= 0.1f;
            Destroy(gameObject, 0.1f);
        }
    }
    
    private void PlaySound(AudioClip soundClip)
    {
        _audioSource.clip = soundClip;
        _audioSource.Play();
        PauseGame(soundClip.length);
    }
    
    public void PauseGame(float pauseTime)
    {
        StartCoroutine(GamePauser(pauseTime));
    }
    public IEnumerator GamePauser(float pauseTime)
    {
        yield return new WaitForSeconds (pauseTime);
    }
}
