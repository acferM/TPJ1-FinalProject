using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battlehub.Dispatcher;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerBehaviour : MonoBehaviour
{
    public static int lightlvl = 1;
    public static float maxLightRadius = 4;
    public static float maxLightRadiusLast = 4;
    public static int oillvl = 0;
    float[] oilSpill = { 0.0046f, 0.0036f, 0.0026f };
    public static int maxLife = 3;
    public static int life = 3;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] float speed;
    [SerializeField] Text moneyTxt;
    [SerializeField] Transform light;
    public static int money = 0;
    float lastHit = 0;
    int dimas = 0;

    void Update()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        if (inputX < 0)
        {
            transform.localScale = new Vector2(1, 1);
        } 
        else if (inputX > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        var movement = new Vector2(inputX, inputY);
        movement.Normalize();
        rb.velocity = movement * speed;

        animator.SetFloat("SpeedX", Mathf.Abs(inputX));
        animator.SetFloat("SpeedY", inputY);

        if (money < 10)
        {
            Dispatcher.Current.BeginInvoke(() => {
                moneyTxt.text = "0" + money;
            });
        } else {
            Dispatcher.Current.BeginInvoke(() => {
                moneyTxt.text = money.ToString();
            });
        }

        if (light.gameObject.GetComponent<Light2D>().pointLightOuterRadius <= 0 && Time.time > lastHit)
        {
            life--;
            lastHit = Time.time + 1;
        }

        if (maxLightRadiusLast != maxLightRadius)
        {
            maxLightRadiusLast = maxLightRadius;
            light.GetComponent<Light2D>().pointLightOuterRadius = maxLightRadius;
        }


        if (life <= 0)
        {
            SceneManager.LoadScene("Lose");
        }

        if (dimas >= 12)
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void FixedUpdate()
    {
        if (light.gameObject.GetComponent<Light2D>().pointLightOuterRadius > 0)
        {
            light.gameObject.GetComponent<Light2D>().pointLightOuterRadius -= oilSpill[oillvl];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("dima"))
        {
            money += 300;
            if (light.gameObject.GetComponent<Light2D>().pointLightOuterRadius < maxLightRadius - 1)
            {
                light.gameObject.GetComponent<Light2D>().pointLightOuterRadius = maxLightRadius - 1;
            }
            dimas++;
            Destroy(collision.gameObject);
        }
    }

    public static void resetLight()
    {
        maxLightRadiusLast = 3;
    }
}
