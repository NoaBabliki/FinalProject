using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
	[SerializeField] private Transform lightSource;
	[SerializeField] private Vector3 requiredPosition;
	[SerializeField] private Quaternion requiredRotation;
	[SerializeField] private GameObject successSquare;
	
	float positionDistanceRequired = 0.3f;
	float rotationAngleRequired = 5f;
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (closeEnoughPosition(lightSource.position, requiredPosition) &&
			closeEnoughRotation(lightSource.rotation, requiredRotation)	)
		{
			Debug.Log("Orientation detected!");
			MeshRenderer successSquareMesh = successSquare.GetComponent<MeshRenderer>();
			successSquareMesh.enabled = true;
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
