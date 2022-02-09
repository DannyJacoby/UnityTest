using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float timeRemaining;
    public float totalTime = 5f;
    public static bool _timerIsRunning;

    public static bool result;
    
    private AudioSource _audioSource;
    
    public AudioClip winSound;
    public AudioClip loseSound;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        timeRemaining = totalTime;
        _timerIsRunning = true;
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        if (result)
        {
            PlaySound(winSound);   
        }
        else
        {
            PlaySound(loseSound);
        }
        timeRemaining = totalTime;
        _timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(timeRemaining + " - " + Time.deltaTime);
        if (timeRemaining > 0 && _timerIsRunning)
        {
            timeRemaining -= (Time.deltaTime + 0.1f);
            // Debug.Log("TIME " + Math.Floor(timeRemaining));
            
        }
        else if(_timerIsRunning)
        {
            timeRemaining = 0;
            _timerIsRunning = false;
        }

        if (!_timerIsRunning)
        {
            SceneManager.LoadScene("SpaceInvadersUI");
        }
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
