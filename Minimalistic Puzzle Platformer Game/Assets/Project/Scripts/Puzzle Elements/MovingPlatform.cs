using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	
	[Header("ASSIGNABLES")] 
	public float speed;

	public int startingPoint;
	private int index;
	
	public Transform[] points;


	private void Start () {
		// Set starting position of the moving platform
		transform.position = points[startingPoint].position;
	}


	private void Update () {

		if (Vector2.Distance(transform.position, points[index].position) < 0.02f) {
			index++;
			if (index == points.Length) {
				index = 0;
			}
		}

		transform.position = Vector2.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);

	}


	private void OnCollisionEnter2D (Collision2D col) {

		if (col.transform.position.y > transform.position.y) {
			col.transform.SetParent(transform);
		}

	}


	private void OnCollisionExit2D (Collision2D col) {
		
		col.transform.SetParent(null);
		
	}
}
