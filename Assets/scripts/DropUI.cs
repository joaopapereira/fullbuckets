using UnityEngine;

public class DropUI : MonoBehaviour {

    private Rigidbody2D rigidBody;
    public Vector3 speed = new Vector3(20, 0, 0);

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        Debug.Log("Drop start");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(rigidBody.position);
        Vector3 position = new Vector3(transform.position.x + 20 * Time.deltaTime, transform.position.y, transform.position.z);
        rigidBody.MovePosition(transform.position + speed * Time.deltaTime);
    }

    void DropStop()
    {
        Destroy(gameObject);
    }
}
