using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgoundScript : MonoBehaviour
{
    GameObject player;

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
    }

    // Update is called once per frame
    void Update()
    {
        float positionX = (playerStartPosition.x - player.transform.position.x) * speed;
        positionX = positionX % xRepeat;
        transform.localPosition = new Vector3(positionX, yOffset, 1f);
    }
}
