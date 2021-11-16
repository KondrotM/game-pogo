using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float expandChange; // 0.002
    [SerializeField] private float defaultShrinkChange; // 0.002
	[SerializeField] private float jumpIncrement; // 9e-6
	public Vector3 centerOfMass; // (0, -.65, 0)
    public float rotationOffset; // 270
	
	
    private Rigidbody2D body;
    private bool grounded;
	private float jumpMomentum;
    private float shrinkChange; 
	private Transform go;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
		// go = gameObject.GetComponentInParent<Transform>();
		// go = gameObject.transform.parent;
		body.centerOfMass = centerOfMass;
		shrinkChange = defaultShrinkChange;
		jumpMomentum = 0;
    }


    // Update is called once per frame
    void Update()
    {
		// Caracter rotates towards mouse cursor
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3 (0, 0, angle + rotationOffset));

		// Input Keys

		if(Input.GetMouseButton(0)){
			Shrink();
		} else {
			Expand();
		}
        print(body.transform.localScale);

	}
    // your puny frog legs can't hop this city
    // modern problems require modern solutions


    private void OnDrawGizmos()
    {
        body = GetComponent<Rigidbody2D>();
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + transform.rotation * body.centerOfMass , .1f);
    }
    private void Shrink() {
        if(body.transform.localScale[1] > 1/3f) {
			// Shrinks character
            body.transform.localScale += new Vector3(0,-shrinkChange,0);
            //body.centerOfMass += new Vector2(0,-shrinkChange*2.333333333f);

			// Adds jump speed the more the character shrinks
			jumpMomentum += shrinkChange;
			shrinkChange = shrinkChange - jumpIncrement;
        }
    }
    private void Expand() {
        if(body.transform.localScale[1] < 1) {
			if (grounded) {
				// Character jumps, could be done nicer?
				body.AddForce(transform.up * jumpMomentum * 10);
				body.AddForce(transform.up * jumpMomentum * 10, ForceMode2D.Impulse);
				grounded = false;
				print("Aye that was a jump");
			}

			if (jumpMomentum >= 0) {
				// Drains jump momentum if released in mid-air
				jumpMomentum -= expandChange;
				print(jumpMomentum);

			}

			// Character expands back
            body.transform.localScale += new Vector3(0,expandChange,0);
            //body.centerOfMass += new Vector2(0,expandChange*2.333333333f);
			
			// Resets shrink speed (wonky)
			shrinkChange = defaultShrinkChange;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") {
            grounded = false;
        }
    }
        
}
