using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bullet;
  public Transform shottingOffset;
  public float bulletDeath;
  
  private Transform player;
  public float speed;
  public float maxBound, minBound;

  public AudioClip deathSound;
  public AudioClip firingSound;

  private AudioSource _audioSource;
  private Animator m_Animator;
  
  void Awake()
  {
    m_Animator = GetComponent<Animator>();
    _audioSource = GetComponent<AudioSource>();
  }
  
  void Start()
  {
    player = GetComponent<Transform>();
  }
  
    // Update is called once per frame
  void Update()
  {
    // Bounds Binding
    float h = Input.GetAxis("Horizontal");
    if (player.position.x < minBound && h < 0)
    {
      h = 0;
    } else if (player.position.x > maxBound && h > 0)
    {
      h = 0;
    }

    player.position += (h * speed) * Vector3.right;

    if (Input.GetKeyDown(KeyCode.Space))
    {
      PlaySound(firingSound);
      GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
      shot.GetComponent<Bullet>().amPlayerBullet = true;
      // Debug.Log("Bang!");

      Destroy(shot, bulletDeath);

    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
      m_Animator.SetTrigger("Death");
      PauseGame(1.0f);
      PlaySound(deathSound);
      // Debug.Log("OUCH");
      GameOver.isPlayerDead = true;
      
  }
  
  private void PlaySound(AudioClip soundClip)
  {
    _audioSource.clip = soundClip;
    _audioSource.Play();
    PauseGame(soundClip.length);
    // yield return new WaitForSeconds(soundClip.length);
  }
    
  public void PauseGame(float pauseTime)
  {
    StartCoroutine(GamePauser(pauseTime));
  }
  public IEnumerator GamePauser(float pauseTime){
    yield return new WaitForSeconds (pauseTime);
  }
}
