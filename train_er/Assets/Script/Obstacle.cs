using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] SpriteRenderer bodySpriteRenderer;
    [SerializeField] SpriteRenderer handSpriteRenderer;
    int difficulty;
    public void Init(int diff, Transform t, int layer, Sprite bodysprite, Sprite handsprite, bool isside)
    {
        difficulty = diff;
        bodySpriteRenderer.sprite = bodysprite;
        if(isside)
        {
            handSpriteRenderer.sprite = handsprite;
            handSpriteRenderer.gameObject.SetActive(true);
            handSpriteRenderer.sortingOrder = layer + 50;
        }
        bodySpriteRenderer.sortingOrder = layer;
        this.transform.position = t.position;
        this.transform.localScale = t.localScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("contact! with user");
            //Ãæµ¹
            switch (difficulty - PlayerScript.Instance.playerLevel)
            {
                case 4:
                    PlayerScript.Instance.changePlayerHealth(-50);
                    break;
                case 3:
                    PlayerScript.Instance.changePlayerHealth(-30);
                    break;
                case 2:
                    PlayerScript.Instance.changePlayerHealth(-10);
                    break;
                case 1:
                    PlayerScript.Instance.changePlayerHealth(-5);
                    break;
                default:
                    kick();
                    break;
            }

            Destroy(this.gameObject); //destroy animation
        }
    }
    void kick()
    {
        Debug.Log("kick!");
        if (difficulty - PlayerScript.Instance.playerLevel > 0)
        {
            PlayerScript.Instance.addPlayerLevel(1.5f);
        }
        else if(difficulty - PlayerScript.Instance.playerLevel < 0)
        {
            PlayerScript.Instance.addPlayerLevel(1);
        }
        else
        {
            PlayerScript.Instance.addPlayerLevel(0.5f);
        }
    }
}
