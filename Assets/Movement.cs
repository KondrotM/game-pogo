using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float shrinkChange;
    [SerializeField] private float expandChange;
    private bool grounded;
    private Rigidbody2D body;
    private float upwards;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if(Input.GetKey(KeyCode.Space) && grounded){
            Jump();
        }
        if(Input.GetKey(KeyCode.E)){
            Expand();
        }
        if(Input.GetKey(KeyCode.R)){
            Rotate();
        }
        if(Input.GetKey(KeyCode.Q)){
            Shrink();
        }
        if(Input.GetKey(KeyCode.Z)){
            Debug();
        }
     
    }

    private void Rotate() {
        body.transform.Rotate(new Vector3(0,0,1));
    }

    private void Debug() {
        print(body.transform.eulerAngles[2]);
        print(body.transform.up);
        print(Mathf.Cos(body.transform.eulerAngles[2]));
    }

    private void Shrink() {
        if(body.transform.localScale[1] > 0.1) {
            body.transform.localScale += new Vector3(0,-shrinkChange,0);
        }
    }
    private void Expand() {
        if(body.transform.localScale[1] < 0.4) {
            body.transform.localScale += new Vector3(0,expandChange,0);
        }
    }

    private void Jump() {
        //body.velocity = new Vector2(body.velocity.x, body.velocity.y + speed);
        // body.velocity = new Vector2(body.velocity.x, body.velocity.y + speed * Mathf.Cos(body.transform.eulerAngles[2]));
        //body.velocity = new Vector2(body.velocity.x + speed - (speed * Mathf.Cos(body.transform.eulerAngles[2])), body.velocity.y + (speed * Mathf.Cos(body.transform.eulerAngles[2])));
        //print(body.velocity);
        body.AddForce(transform.up * speed);
        body.AddForce(transform.up * speed, ForceMode2D.Impulse);

        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }
}

