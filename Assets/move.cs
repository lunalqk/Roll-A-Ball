using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public Slider speedSlider;
    public float speed;
    public int speedInt;
    public TextMesh scoreText;
    public TextMesh winText;

    private Rigidbody rb;
    private int count;
    private Renderer rend;

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
        speedSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        rend = GetComponent<Renderer>();
        count = 0;
        SetCountText();
        TextMesh scoreText = GetComponent<TextMesh>();
        TextMesh winText = GetComponent<TextMesh>();
    }

    public void ValueChangeCheck()
    {
        speed = speedSlider.value * 10;
        Debug.Log("The speed value changed to" + speed.ToString());
        switch ((int)speed)
        {
            case 0:
                rend.material.SetColor("_Color", Color.red);
                break;
            case 5:
                rend.material.SetColor("_Color", Color.yellow);
                break;
            case 10:
                rend.material.SetColor("_Color", Color.green);
                break;

        }
    }

    private void LateUpdate()
    {

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float tiltHorizontal = Input.acceleration.x;
        float tiltVertical = Input.acceleration.y;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 movement2 = new Vector3(tiltHorizontal, 0.0f, tiltVertical);

        rb.AddForce(movement * speed);
        rb.AddForce(movement2 * speed);
       

}
private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    private void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if(count >= 12)
        {
            winText.gameObject.SetActive(true);
        }
    }
}
