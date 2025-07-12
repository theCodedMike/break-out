using UnityEngine;

public class Racket : MonoBehaviour
{
    [Header("横板移动速度")]
    public float speed = 10f;

    private const float BorderWidth = 5.2f;
    

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
            if(transform.position.x < BorderWidth)
                transform.Translate(Vector3.right * (Time.deltaTime * speed));
        }
    }
}
