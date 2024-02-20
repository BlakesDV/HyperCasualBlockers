using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    public float speed = 5;

    public Text livesText;

    private float mouseDistance;
    private Rigidbody2D rb;

    private float lastYPos;

    private bool sliding;
    private int dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {

        lastYPos = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xPos = worldPoint.x;

        mouseDistance = Mathf.Clamp(xPos - transform.position.x, -1, 1);

        if (transform.position.y > lastYPos + 5)
        {
            LvlController.instance.Score(10);
            lastYPos = transform.position.y;
        }

    }

    private void FixedUpdate()
    {

        if (LvlController.instance.gameOver)
            return;

        if (!sliding)
            rb.velocity = new Vector2(mouseDistance * speed, LvlController.instance.gameSpeed * LvlController.instance.multiplier);
        else
            rb.velocity = new Vector2(dir * 2.5f, LvlController.instance.gameSpeed * LvlController.instance.multiplier);
    }

    public void SetText(int amount)
    {
        livesText.text = amount.ToString();
    }

    public void TakeDamage()
    {
        if (LvlController.instance.gameOver)
            return;

        int children = transform.childCount;
        if (children <= 1)
        {
            LvlController.instance.GameOver();
        }
        else
        {
            Destroy(transform.GetChild(children - 1).gameObject);
        }

        SetText(children - 1);
    }

    public void Slide(int direction)
    {
        sliding = true;
        dir = direction;
        Invoke("SetSlideToFalse", 0.25f);
    }

    void SetSlideToFalse()
    {
        sliding = false;
    }

}