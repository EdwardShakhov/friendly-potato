using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScaleParticles : MonoBehaviour {
	void Update () {
#pragma warning disable 618
		GetComponent<ParticleSystem>().startSize = transform.lossyScale.magnitude;
#pragma warning restore 618
	}
}