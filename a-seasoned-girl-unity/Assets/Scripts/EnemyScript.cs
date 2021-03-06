﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 100;

    bool active = false;

    bool dead = false;

    public float damage;

    public int speed = 1;

    Vector3 startPosition;

    GameObject player;

    PlayerController playerController;

    int startHealth = 100;

    Vector3 playerOffset = new Vector3(-1f, -0.8f, 0);

    bool enemyNear = false;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponentInParent<PlayerController>();
        startPosition = transform.position;
        startHealth = health;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0.3f;
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.dead = true;
        }
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(4f);

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Destroy(this.gameObject.transform.GetChild(i).gameObject);
        }

        active = false;
        dead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active == true && dead == false)
        {
            if (Vector3.Distance(transform.position, player.transform.position + playerOffset) > 3)
            {
                // move to player
                this.transform.position -= (transform.position - player.transform.position + playerOffset).normalized / 20 * speed;
                float yChange = Random.Range(0, 100) - 50;
                this.transform.position += new Vector3(0, yChange / 5000, 0);
            }
            else
            {
                // attack
                float xChange = Random.Range(0, 100) - 50;
                float yChange = Random.Range(0, 100) - 50;
                this.transform.position += new Vector3(yChange / 500, yChange / 500, 0);
                playerController.currentLife -= damage / 100;
                if (playerController.currentLife <= 0)
                {
                    playerController.Die();
                }
                if (enemyNear == false)
                {
                    enemyNear = true;
                    playSound("enemyNearby");

                }
            }
        }
        else if (active == true && dead == true)
        {
            // die you son of a bitch
            float xChange = Random.Range(0, 100) - 50;
            this.transform.position -= new Vector3(xChange / 1000, 0.1f, 0);
            audioSource.Stop();
            StartCoroutine("DestroyEnemy");
        }
    }

    public void Respawn()
    {
        active = false;
        dead = false;
        health = startHealth;
        transform.position = startPosition;
    }


    void OnBecameVisible()
    {
        active = true;
        playSound("enemySeen");
    }



    public void playSound(string audioClipName)
    {
        AudioClip clip = (AudioClip)Resources.Load(audioClipName);
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.Log("AudioResourceMissing:" + audioClipName);
        }
    }
}
