using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public GameObject bullet;
    public float bulletDeath;
    
    private Transform enemyHolder;
    public float speed;
    public float fireRate = 0.990f;
    public float maxBound, minBound, goalBound;

    public GameObject m_Enemy_1;
    public GameObject m_Enemy_2;
    public GameObject m_Enemy_3;
    public GameObject m_Enemy_4;

    public AudioClip firingSound;
    
    // public int numberOfAliens = 4;
    // private int numberOfAliensLeft;
    
    public static float RepeatSpeed;
    public float tempRepeatSpeedChecker;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        RepeatSpeed = 0.5f;
        tempRepeatSpeedChecker = RepeatSpeed;
        // numberOfAliensLeft = numberOfAliens;
        InvokeRepeating("MoveEnemy", 0.1f, RepeatSpeed);
        enemyHolder = GetComponent<Transform>();
        InstantiateEnemies();
    }
    
    void MoveEnemy()
    {
        enemyHolder.position += Vector3.right * speed;
        foreach (Transform enemy in enemyHolder)
        {
            if (enemy.position.x < minBound || enemy.position.x > maxBound)
            {
                speed = -speed;
                enemyHolder.position += Vector3.down * 0.5f;
                return;
            }

            // TODO Make it so that any enemies in back of stack don't fire, only front line ones do, maybe a bool?
            if (Random.value > fireRate)
            {
                PlaySound(firingSound);
                // Debug.Log("FIRE");
                GameObject shot = Instantiate(bullet, 
                    new Vector3(enemy.position.x, enemy.position.y - 1.5f, enemy.position.z), 
                    Quaternion.identity);
                shot.GetComponent<Bullet>().amPlayerBullet = false;
                Destroy(shot, bulletDeath);
            }

            if (enemy.position.y <= goalBound)
            {
                GameOver.isPlayerDead = true;
                Time.timeScale = 0;
            }
        }

        if (tempRepeatSpeedChecker != RepeatSpeed && enemyHolder.childCount != 0)
        {
            if (RepeatSpeed <= 0.1f)
            {
                RepeatSpeed = 0.1f;
            }
            CancelInvoke();
            InvokeRepeating("MoveEnemy", 0.1f, RepeatSpeed);
            tempRepeatSpeedChecker = RepeatSpeed;
        }

        if (enemyHolder.childCount == 0)
        {
            GameOver.allEnemiesDead = true;
            Time.timeScale = 0;
        }
    }

    // TODO Maybe give the children a chance of talking back to their mom? "Help I've died increase speed"
    public void EnemyDeath()
    {
        
    }

    // TODO Expand this function
    public void InstantiateEnemies()
    {
        GameObject ToSpawnEnemy1 = GameObject.Instantiate(m_Enemy_1, transform);
        ToSpawnEnemy1.transform.localPosition = new Vector3(-6f, 5f, 0f);
        GameObject ToSpawnEnemy2 = GameObject.Instantiate(m_Enemy_2, transform);
        ToSpawnEnemy2.transform.localPosition = new Vector3(-2f, 5f, 0f);
        GameObject ToSpawnEnemy3 = GameObject.Instantiate(m_Enemy_3, transform);
        ToSpawnEnemy3.transform.localPosition = new Vector3(2f, 5f, 0f);
        GameObject ToSpawnEnemy4 = GameObject.Instantiate(m_Enemy_4, transform);
        ToSpawnEnemy4.transform.localPosition = new Vector3(6f, 5f, 0f);
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
