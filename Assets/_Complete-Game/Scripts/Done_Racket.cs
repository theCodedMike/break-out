using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Racket : MonoBehaviour {
    public float speed = 10.0f;//横板移动速度
                              // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //当球拍未超过屏幕左侧时移动球拍，否则球拍不能再移动  
                if (transform.position.x > -5.2)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
                else
                {
                    return;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < 5.2)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
                else
                {
                    return;
                }
            }
    }

    /// <summary>
    /// 道具与板接触的触发器
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "upgrade")
        {
            string name = col.gameObject.GetComponent<Done_UpGrade>().upgradeName;
            performUpgrade(name);
            Destroy(col.gameObject);
        }

    }

    /// <summary>
    /// 道具生效
    /// </summary>
    /// <param name="name"></param>
    void performUpgrade(string name)
    {
        // removing Unity-attached suffixed data to get original sprite name
        name = name.Remove(name.Length - 21);
        float x;
        Done_Ball ballController = GameObject.Find("ball").GetComponent<Done_Ball>();
        switch (name)
        {
            case "ball_speed_up":
                if (ballController.BallSpeed < 27)
                {
                    ballController.BallSpeed += 3;//当小球速度小于27，并且道具为ball_speed_up时，小球速度+3，以下类似。
                }
                break;
            case "ball_speed_down":
                if (ballController.BallSpeed > 18)
                {
                    ballController.BallSpeed -= 3;
                }
                break;

            case "paddle_size_up":
                x = this.gameObject.transform.localScale.x;
                if (x < 8.0f)
                    this.gameObject.transform.localScale = new Vector3(
                                                                    x += 0.25f,
                                                                       this.gameObject.transform.localScale.y,
                                                                       1.0f);
                break;
            case "paddle_size_down":
                x = this.gameObject.transform.localScale.x;
                if (x > 4.0f)
                    this.gameObject.transform.localScale = new Vector3(
                                                                    x -= 0.25f,
                                                                    this.gameObject.transform.localScale.y,
                                                                    1.0f);

                break;
            case "paddle_speed_up":
                speed += 3;
                break;
            case "paddle_speed_down":
                if (speed > 7)
                {
                    speed -= 3;
                }
                break;
            default:
                break;
        }
    }
}
