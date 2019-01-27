using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponentInParent<PlayerController>();
        startPosition = transform.position;
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

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active == true && dead == false)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 1)
            {
                // move to player
                this.transform.position -= (transform.position - player.transform.position).normalized / 20 * speed;
                float yChange = Random.Range(0, 100) - 50;
                this.transform.position += new Vector3(0, yChange / 5000, 0);

                // play Incoming Music
                playSound("enemyNearby");
            }
            else
            {
                // attack
                float xChange = Random.Range(0, 100) - 50;
                float yChange = Random.Range(0, 100) - 50;
                this.transform.position += new Vector3(yChange / 500, yChange / 500, 0);
                playerController.currentLife -= damage / 100;
                if(playerController.currentLife <= 0) {
                    playerController.Die();
                }
            }
        } else if(dead == true) {
            // die you son of a bitch
            float xChange = Random.Range(0, 100) - 50;
            this.transform.position -= new Vector3(xChange / 1000, 0.1f, 0);
            StartCoroutine("DestroyEnemy");
        }
    }


    void OnBecameVisible()
    {
        active = true;
        playSound("enemySeen");
    }

    public void playSound(string audioClipName)
    {
            AudioSource audio = gameObject.AddComponent<AudioSource >();
            AudioClip clip = (AudioClip)Resources.Load (audioClipName);
            if (clip != null) {
                audio.PlayOneShot (clip, 1.0F);
            }
            else {
            Debug.Log ("AudioResourceMissing:" + audioClipName);
            }       
    }
}
