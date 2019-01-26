using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgoundScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite backgroundSprite;

    public GameObject player;

    public Vector3 playerStartPosition;

    public float speed = 1;
    public float xRepeat;

    public float yOffset = 0; 

    void Start()
    {
        transform.SetParent(GameObject.Find("Main Camera").transform);
        transform.localPosition = new Vector3(0, 0, 1);
        player = GameObject.Find("PlayerObject");

        playerStartPosition = player.transform.position;

        // GameObject.Find("BGSprite1").gameObject.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
    }

    // Update is called once per frame
    void Update()
    {
        float positionX = (playerStartPosition.x - player.transform.position.x) * speed;
        positionX = positionX % xRepeat;//18.44f;
        transform.localPosition = new Vector3(positionX, yOffset, 1f);
    }
}
