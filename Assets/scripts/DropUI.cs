using UnityEngine;

public class DropUI : MonoBehaviour {

    private Rigidbody2D rigidBody;
    public Vector3 speed = new Vector3(20, 0, 0);
    public GameUI gameController;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}
    private void Awake()
    {
        gameController = GameObject.FindObjectOfType<GameUI>();
        gameController.AddFlyingDrop();
    }

    // Update is called once per frame
    void Update () {
        Vector3 position = new Vector3(transform.position.x + 20 * Time.deltaTime, transform.position.y, transform.position.z);
        rigidBody.MovePosition(transform.position + speed * Time.deltaTime);
    }

    void DropStop()
    {
        gameController.DiedFlyingDrop();
        Destroy(gameObject);
    }
}
