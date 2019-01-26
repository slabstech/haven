using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage = 10;

    bool hasParent = false;
    void Start()
    {

    }

    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasParent == false && other.gameObject.name != "PlayerObject")
        {
            this.gameObject.layer = 12;

            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0f;

            this.transform.SetParent(other.gameObject.transform);

            if (other.gameObject.tag == "Enemy")
            {
                // demage enemy
                other.gameObject.GetComponent<EnemyScript>().DamageEnemy(this.damage);
            }
            hasParent = true;
        }

    }

    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(60f);
        if (this.transform.parent != null)
        {
            if (this.transform.parent.gameObject.tag != "Enemy")
            {
                Destroy(this.gameObject);
            }
        }

    }
}
