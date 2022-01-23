using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
	[SerializeField] private Transform lightSource;
	[SerializeField] private Transform lightSourceTarget;
	[SerializeField] private GameObject successShape;
	
	private float positionDistanceRequired = 0.3f;
	private float rotationAngleRequired = 5f;
	private Vector3 requiredPosition;
	private Quaternion requiredRotation;
	
    // Start is called before the first frame update
    void Start()
    {
		requiredPosition = lightSourceTarget.position;
		requiredRotation = lightSourceTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (closeEnoughPosition(lightSource.position, requiredPosition) &&
			closeEnoughRotation(lightSource.rotation, requiredRotation)	)
		{
			Debug.Log(successShape.name + " detected!");
			MeshRenderer successShapeMesh = successShape.GetComponent<MeshRenderer>();
			successShapeMesh.enabled = true;
		}
    }
	
	bool closeEnoughPosition(Vector3 position1, Vector3 position2)
	{
		return (Vector3.Distance(position1, position2) < positionDistanceRequired);
	}
	
	bool closeEnoughRotation(Quaternion rotation1, Quaternion rotation2)
	{
		return (Quaternion.Angle(rotation1, rotation2) < rotationAngleRequired);
	}
}
