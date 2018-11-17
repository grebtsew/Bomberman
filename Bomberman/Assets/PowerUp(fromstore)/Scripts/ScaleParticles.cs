using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScaleParticles : MonoBehaviour {
	void Update () {
		GetComponent<ParticleSystem>().startSize = transform.lossyScale.magnitude;
	}
}