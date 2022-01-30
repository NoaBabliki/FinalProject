using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
	[SerializeField] private Transform lightSource;
	[SerializeField] private Transform lightSourceTarget;
	[SerializeField] private GameObject successShape;
	
	[SerializeField] private float positionDistanceRequired = 0.6f;
	[SerializeField] private float rotationAngleRequired = 10f;
	[SerializeField] private AudioSource successAudio;
	[SerializeField] private int gaslumpNumber;
	private Vector3 requiredPosition;
	private Quaternion requiredRotation;
	private bool addSuccess = true;
	MeshRenderer successShapeMesh;
	
    // Start is called before the first frame update
    void Start()
    {
		successShapeMesh = successShape.GetComponent<MeshRenderer>();
		requiredPosition = lightSourceTarget.position;
		requiredRotation = lightSourceTarget.rotation;
		successShapeMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (closeEnoughPosition(lightSource.position, requiredPosition) &&
			closeEnoughRotation(lightSource.rotation, requiredRotation)	)
		{
			Debug.Log(successShape.name + " detected!");
			successShapeMesh.enabled = true;
			if (addSuccess){
				GameManager.AddSuccess();
				successAudio.Play();
				addSuccess = false;
			}
			
		}
		if (Gaslamp.gaslightNumberOn != gaslumpNumber) {
				Debug.Log("suppose to get rid of shadow");
				successShapeMesh.enabled = false;
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
