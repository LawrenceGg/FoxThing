using UnityEngine;
using System.Collections;

public class grapple : MonoBehaviour
{


	GameObject player;
	public LineRenderer line;
	public bool swinging;
	DistanceJoint2D joint;
	Vector3 targetPos;
	RaycastHit2D hit;
	public float distance = 10f;
	public LayerMask mask;
	public float step = 0.02f;
	[SerializeField]
	[Range (1, 10)]
	public int grapplecharge = 2;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		joint = GetComponent<DistanceJoint2D>();
		joint.enabled = false;
		line.enabled = false;
	}

	public void Activate()
    {
		if (grapplecharge < 3)
		{
			grapplecharge += 1;
		}
    }

	// Update is called once per frame
	void Update()
	{
		
		//if (joint.distance > .5f)
		//	joint.distance -= step;
		//else
		//{
		//	line.enabled = false;
		//	joint.enabled = false;
//
		//}


		if (Input.GetKeyDown(KeyCode.Mouse0) && grapplecharge > 0)
		{
			targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			targetPos.z = 0;

			hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);

			if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)

			{
				joint.enabled = true;
				//	Debug.Log (hit.point - new Vector2(hit.collider.transform.position.x,hit.collider.transform.position.y);
				Vector2 connectPoint = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
				connectPoint.x = connectPoint.x / hit.collider.transform.localScale.x;
				connectPoint.y = connectPoint.y / hit.collider.transform.localScale.y;
				Debug.Log(connectPoint);
				joint.connectedAnchor = connectPoint;

				joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
				//		joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x,hit.collider.transform.position.y);
				joint.distance = Vector2.Distance(transform.position, hit.point);

				line.enabled = true;
				line.SetPosition(0, transform.position);
				line.SetPosition(1, hit.point);

				line.GetComponent<roperatio>().grabPos = hit.point;

				swinging = true;
				grapplecharge -= 1;
			}
		}
		line.SetPosition(1, joint.connectedBody.transform.TransformPoint(joint.connectedAnchor));

		if (swinging == true)
		{

			line.SetPosition(0, transform.position);
		}

		if(Input.GetKeyUp(KeyCode.Mouse0))
        {
			joint.enabled = false;
			line.enabled = false;
		}
		if (Input.GetKey(KeyCode.Space) && swinging == true)
		{
			player.GetComponent<PlayerController>().RopeJump();
			joint.enabled = false;
			line.enabled = false;
			swinging = false;
		}

	
	}
}