using UnityEngine;
using System.Collections;

public class SquareTheCollider : MonoBehaviour 
{
	private class ConnectoPoint
	{
		static float closeThreshold = 0.01f;

		public Vector2 point;
		public bool plusPlusOccupied = false;
		public bool plusMinusOccupied = false;
		public bool minusPlusOccupied = false;
		public bool minusMinusOccupied = false;

		public void LogOccupations()
		{
			Debug.Log (plusPlusOccupied.ToString() + plusMinusOccupied + minusPlusOccupied + minusMinusOccupied);
		}

		public bool Full()
		{
			return plusPlusOccupied && plusMinusOccupied && minusPlusOccupied && minusMinusOccupied;
		}

		public bool CloseBorder(ConnectoPoint other)
		{
			return XClose(other) && YClose(other);
		}

		public bool XClose(ConnectoPoint other)
		{
			bool xClose = Mathf.Abs (other.point.x - point.x) < closeThreshold;
			return xClose;
		}

		public bool YClose(ConnectoPoint other)
		{
			bool yClose = Mathf.Abs (other.point.y - point.y) < closeThreshold;
			return yClose;
		}
	}

	public Collider2D squareMe;
	public GameObject squarePrefab;
	public float smallestSquareScale;
	public float biggestSquareScale;

	void Start () 
	{
		SquareIt ();
		squareMe.enabled = false;
	}

	private void SquareIt()
	{
		//build first square
		float randomScale = UnityEngine.Random.Range (smallestSquareScale, biggestSquareScale);
		float firstX = squareMe.bounds.center.x;
		float firstY = squareMe.bounds.center.y;
		Vector3 randomPosition = new Vector3(firstX, firstY, squareMe.gameObject.transform.position.z);
		GameObject firstSquare = Instantiate(squarePrefab, randomPosition, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
		firstSquare.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
		firstSquare.transform.SetParent (squareMe.transform);

		//second square
		float randomDirection = UnityEngine.Random.Range (0, Mathf.PI * 2);
		randomScale = UnityEngine.Random.Range (smallestSquareScale, biggestSquareScale);
	}

	private void SquareItConnectos()
	{
		System.Collections.ArrayList connectoPoints = new ArrayList();

		//build first square
		float randomScale = UnityEngine.Random.Range (smallestSquareScale, biggestSquareScale);
		float firstX = squareMe.bounds.center.x;
		float firstY = squareMe.bounds.center.y;
		Vector3 randomPosition = new Vector3(firstX, firstY, squareMe.gameObject.transform.position.z);
		GameObject firstSquare = Instantiate(squarePrefab, randomPosition, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
		firstSquare.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
		firstSquare.transform.SetParent (squareMe.transform);

		//first square's corners are connectopoints
		ConnectoPoint firstConnectoPoint = new ConnectoPoint();
		ConnectoPoint secondConnectoPoint = new ConnectoPoint();
		firstConnectoPoint.point = firstSquare.GetComponent<BoxCollider2D> ().bounds.max;
		secondConnectoPoint.point = firstSquare.GetComponent<BoxCollider2D> ().bounds.min;
		firstConnectoPoint.minusMinusOccupied = true;
		secondConnectoPoint.plusPlusOccupied = true;

		connectoPoints.Add (firstConnectoPoint);
		connectoPoints.Add (secondConnectoPoint);

		Debug.Log ("Before starting there are " + connectoPoints.Count.ToString () + " connectoPoints.");
		//further squares are connected at connecto points
		for (int i = 0; i < 20; i++)
		{
			//choose a random connectopoint to connect
			int randomConnectoPointIndex;
			int spinCounter = 0;
			do
			{
				spinCounter++;
				if (spinCounter > 100) throw new UnityException("infinite loop 1");
				randomConnectoPointIndex = UnityEngine.Random.Range(0, connectoPoints.Count);
				Debug.Log ("Now there are " + connectoPoints.Count.ToString () + " connectoPoints.");
				Debug.Log("We chose number " + randomConnectoPointIndex.ToString());
			}
			while ((connectoPoints[randomConnectoPointIndex] as ConnectoPoint).Full());

			ConnectoPoint randomConnectoPoint = (connectoPoints[randomConnectoPointIndex] as ConnectoPoint);
			Vector3 nextSquareCornerPosition = new Vector3(randomConnectoPoint.point.x,
			                                               randomConnectoPoint.point.y,
			                                               squareMe.gameObject.transform.position.z);
			Debug.Log("This is the connecting corner position: " + nextSquareCornerPosition);

			//determine the correct direction and scale for the new square
			ArrayList directionalConnectoPoints = new ArrayList();
			foreach (ConnectoPoint connectoPoint in connectoPoints)
			{
				bool pointInPlusPlus = connectoPoint.point.x > randomConnectoPoint.point.x &&
									   connectoPoint.point.y > randomConnectoPoint.point.y;
				bool pointInPlusMinus = connectoPoint.point.x > randomConnectoPoint.point.x &&
									    connectoPoint.point.y < randomConnectoPoint.point.y;
				bool pointInMinusPlus = connectoPoint.point.x < randomConnectoPoint.point.x &&
								  	    connectoPoint.point.y > randomConnectoPoint.point.y;
				bool pointInMinusMinus = connectoPoint.point.x < randomConnectoPoint.point.x &&
									     connectoPoint.point.y < randomConnectoPoint.point.y;

				if (!
				     (
					 (connectoPoint.CloseBorder(randomConnectoPoint)) ||
				 	 (pointInPlusPlus && randomConnectoPoint.plusPlusOccupied) ||
					 (pointInPlusMinus && randomConnectoPoint.plusMinusOccupied) ||
					 (pointInMinusPlus && randomConnectoPoint.minusPlusOccupied) ||
					 (pointInMinusMinus && randomConnectoPoint.minusMinusOccupied)
				     )
				   )
				{
					directionalConnectoPoints.Add(connectoPoint);
				}
			}

			Debug.Log("Number of directionally ok connectopoints found: " + directionalConnectoPoints.Count);

			Vector3 nextSquareScale = Vector3.zero;
			bool nextSquarePositiveX = false;
			bool nextSquarePositiveY = false;
			if (directionalConnectoPoints.Count == 0)
			{
				//the case where there are no edges to connect to from this corner
				float squareScale = UnityEngine.Random.Range(smallestSquareScale, biggestSquareScale);
				nextSquareScale = new Vector3(squareScale, squareScale, squareScale);
				spinCounter = 0;
				do
				{
					spinCounter++;
					if (spinCounter > 100) throw new UnityException("infinite loop 2");
					nextSquarePositiveX = UnityEngine.Random.Range(0, 2) == 0;
					nextSquarePositiveY = UnityEngine.Random.Range(0, 2) == 0;
				}
				while (nextSquarePositiveX && nextSquarePositiveY && randomConnectoPoint.plusPlusOccupied ||
				       nextSquarePositiveX && !nextSquarePositiveY && randomConnectoPoint.plusMinusOccupied ||
				       !nextSquarePositiveX && nextSquarePositiveY && randomConnectoPoint.minusPlusOccupied ||
				       !nextSquarePositiveX && !nextSquarePositiveY && randomConnectoPoint.minusMinusOccupied);
				Debug.Log("nextSquarePositiveX: " + nextSquarePositiveX);
				Debug.Log("nextSquarePositiveY: " + nextSquarePositiveY);

				GameObject nextSquare = MakeSquare(nextSquarePositiveX, nextSquarePositiveY, randomConnectoPoint.point, nextSquareScale);
				BoxCollider2D nextSquareCollider = nextSquare.GetComponent<BoxCollider2D>();

				ConnectoPoint newConnectoPoint = new ConnectoPoint();

				if (nextSquarePositiveX && nextSquarePositiveY) 
				{
					newConnectoPoint.minusMinusOccupied = true;
					newConnectoPoint.point = nextSquareCollider.bounds.max;
				}
				if (nextSquarePositiveX && !nextSquarePositiveY)
				{
					newConnectoPoint.minusPlusOccupied = true;
					newConnectoPoint.point = new Vector3(nextSquareCollider.bounds.max.x,
					                                     nextSquareCollider.bounds.min.y,
					                                     nextSquareCollider.bounds.max.z);
				}
				if (!nextSquarePositiveX && nextSquarePositiveY)
				{
					newConnectoPoint.plusMinusOccupied = true;
					newConnectoPoint.point = new Vector3(nextSquareCollider.bounds.min.x,
					                                     nextSquareCollider.bounds.max.y,
					                                     nextSquareCollider.bounds.max.z);
				}
				if (!nextSquarePositiveX && !nextSquarePositiveY)
				{
					newConnectoPoint.plusPlusOccupied = true;
					newConnectoPoint.point = nextSquareCollider.bounds.min;
				}
				connectoPoints.Add (newConnectoPoint);

			}
			else
			{
				//the case where we want to fill up to existing edges
				float nearestXBorder = float.MaxValue;
				float nearestYBorder = float.MaxValue;
				float lowestXDistance = float.MaxValue;
				float lowestYDistance = float.MaxValue;
				foreach(ConnectoPoint connectoPoint in connectoPoints)
				{
					float xDistance = Mathf.Abs(connectoPoint.point.x - randomConnectoPoint.point.x);
					if (!connectoPoint.XClose(randomConnectoPoint) &&
					    xDistance < lowestXDistance)
					{
						nearestXBorder = connectoPoint.point.x;
						lowestXDistance = xDistance;
					}
					float yDistance = Mathf.Abs(connectoPoint.point.y - randomConnectoPoint.point.y);
					if (!connectoPoint.YClose(randomConnectoPoint) &&
						yDistance < lowestYDistance)
					{
						nearestYBorder = connectoPoint.point.y;
						lowestYDistance = yDistance;
					}
				}
				nextSquarePositiveX = nearestXBorder > randomConnectoPoint.point.x;
				nextSquarePositiveY = nearestYBorder > randomConnectoPoint.point.y;

				float xGap = nearestXBorder - randomConnectoPoint.point.x;
				float yGap = nearestYBorder - randomConnectoPoint.point.y;

				Debug.Log("x diff: " + xGap + " | y diff: " + yGap);

				nextSquareScale = new Vector3(xGap/4f, yGap/4f, 1f);

				MakeSquare(nextSquarePositiveX, nextSquarePositiveY, nextSquareCornerPosition, nextSquareScale);
			}

			if (!nextSquarePositiveX && !nextSquarePositiveY) 
			{
				randomConnectoPoint.minusMinusOccupied = true;
			}
			if (!nextSquarePositiveX && nextSquarePositiveY)
			{
				randomConnectoPoint.minusPlusOccupied = true;
			}
			if (nextSquarePositiveX && !nextSquarePositiveY)
			{
				randomConnectoPoint.plusMinusOccupied = true;
			}
			if (nextSquarePositiveX && nextSquarePositiveY)
			{
				randomConnectoPoint.plusPlusOccupied = true;
			}

			connectoPoints[randomConnectoPointIndex] = randomConnectoPoint;
			randomConnectoPoint.LogOccupations();
		}
	}

	private GameObject MakeSquare(bool nextSquarePositiveX, bool nextSquarePositiveY, Vector3 nextSquareCornerPosition, Vector3 nextSquareScale)
	{
		GameObject nextSquare = Instantiate(squarePrefab) as GameObject;
		nextSquare.transform.SetParent(squareMe.transform);

		//set scale, then based on scale and direction set position
		nextSquare.transform.localScale = nextSquareScale;

		BoxCollider2D nextSquareCollider = nextSquare.GetComponent<BoxCollider2D>();

		float nextSquareXPosition = 0;
		float nextSquareYPosition = 0;
		if (nextSquarePositiveX)
			nextSquareXPosition = nextSquareCornerPosition.x + nextSquareCollider.bounds.extents.x;
		else
			nextSquareXPosition = nextSquareCornerPosition.x - nextSquareCollider.bounds.extents.x;
		if (nextSquarePositiveY)
			nextSquareYPosition = nextSquareCornerPosition.y + nextSquareCollider.bounds.extents.y;
		else
			nextSquareYPosition = nextSquareCornerPosition.y - nextSquareCollider.bounds.extents.y;
		
		nextSquare.transform.position = new Vector3(nextSquareXPosition, nextSquareYPosition, squareMe.transform.position.z);

		return nextSquare;
	}
}