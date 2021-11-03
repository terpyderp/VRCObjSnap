using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class snapPoint : UdonSharpBehaviour {
	public bool isSurface;
	// public uint rotationSnap;

	public Vector3 getSnapPos(Vector3 pos) {
		if (isSurface) {
			pos.y = transform.position.y;
			return pos;
		} else {
			return transform.position;
		}
	}

	public Quaternion getSnapRot(Transform trans) {
		// rot.x = 0;
		// rot.z = 0;
		// if (rotationSnap > 0) {
		// 	// float rotationSnapRad = 180 * rotationSnap;
		// 	// // Debug.Log((rot.y/rotationSnapRad) + 0.5);
		// 	Debug.Log(rot.y);
		// 	// rot.y = (float) ((int) ((rot.y/rotationSnapRad) + 0.5)) * rotationSnapRad;

		// 	rot.y = (float) ((int) (rot.y*2))/2;
		// }

		return Quaternion.FromToRotation(trans.up, transform.up);
	}
}
