using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Text amountText;

    public AudioClip impactSound;

    private int amount;

    private Player player;
    private float nextTime;

    private Color initialColor;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (player != null && nextTime < Time.time)
        {
            PlayerDamage();
        }

    }

    public void SetAmount()
    {
        gameObject.SetActive(true);
        amount = Random.Range(0, LvlController.instance.obstaclesAmount);
        if (amount <= 0)
        {
            gameObject.SetActive(false);
        }

        SetAmountText();
        SetColor();
    }

    public void SetAmountText()
    {
        amountText.text = amount.ToString();
    }

    public void SetColor()
    {
        int playerLives = FindObjectOfType<Player>().transform.childCount;
        Color newColor;

        if (amount > playerLives)
        {
            newColor = LvlController.instance.hardColor;
        }
        else if (amount > playerLives / 2)
        {
            newColor = LvlController.instance.mediumColor;
        }
        else
        {
            newColor = LvlController.instance.easyColor;
        }

        spriteRenderer.color = newColor;
        initialColor = newColor;
    }

    void PlayerDamage()
    {
        if (LvlController.instance.gameOver)
            return;

        AudioSource.PlayClipAtPoint(impactSound, Camera.main.transform.position);
        nextTime = Time.time + LvlController.instance.damageTime;
        player.TakeDamage();
        amount--;
        SetAmountText();
        if (amount <= 0)
        {
            gameObject.SetActive(false);
            player = null;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(DamageColor());
        }
    }

    IEnumerator DamageColor()
    {
        float timer = 0;
        float t = 0;

        spriteRenderer.color = initialColor;

        while (timer < LvlController.instance.damageTime)
        {
            spriteRenderer.color = Color.Lerp(initialColor, Color.white, t);
            timer += Time.deltaTime;
            t += Time.deltaTime / LvlController.instance.damageTime;
            yield return null;
        }

        spriteRenderer.color = initialColor;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            player = otherPlayer;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            player = null;
        }
    }
}
