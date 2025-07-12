using System;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [Header("横板移动速度")] 
    public float speed = 10f;

    private const float BorderWidth = 5.2f;

    private Ball _ball;
    

    private void Start()
    {
        _ball = GameObject.Find("ball").GetComponent<Ball>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -BorderWidth)
                transform.Translate(Vector3.left * (Time.deltaTime * speed));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < BorderWidth)
                transform.Translate(Vector3.right * (Time.deltaTime * speed));
        }
    }

    // 道具与横板的触发逻辑
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("upgrade"))
        {
            string upgradeName = other.gameObject.GetComponent<Upgrade>().upgradeName;
            PerformUpgrade(upgradeName);
            
            Destroy(other.gameObject);
        }
    }

    // 道具生效
    private void PerformUpgrade(string propName)
    {
        print($"Upgrade: {propName}");
        GetPropAction(propName)();
    }

    // 生成不同道具对应的动作
    private Action GetPropAction(string propName)
    {
        switch (propName)
        {
            case "ball_speed_up":
                return () =>
                {
                    if (_ball.speed < 27)
                        _ball.speed += 3;
                };
            case "ball_speed_down":
                return () =>
                {
                    if (_ball.speed > 18)
                        _ball.speed -= 3;
                };
            case "paddle_size_up":
                return () =>
                {
                    Vector3 localScale = transform.localScale;
                    if (localScale.x < 8.0f)
                        transform.localScale = new Vector3(localScale.x + 0.5f, localScale.y, 1f);
                };
            case "paddle_size_down":
                return () =>
                {
                    Vector3 localScale = transform.localScale;
                    if (localScale.x > 4.0f)
                        transform.localScale = new Vector3(localScale.x - 0.5f, localScale.y, 1f);
                };
            case "paddle_speed_up":
                return () => speed += 3;
            case "paddle_speed_down":
                return () => speed -= 3;
            default: throw new UnityException($"未知道具名称{propName}");
        }
    }
}