using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class snapObject : UdonSharpBehaviour {
	Collider collider;
	snapPoint[] points;
	snapPoint point;
	Transform OGParent;

	void Start() {
		collider = GetComponent<Collider>();
		OGParent = transform.parent;
		points = new snapPoint[0];
	}

	void OnTriggerEnter(Collider otherCollider) {
		snapPoint temp = otherCollider.gameObject.GetComponent<snapPoint>();
		if (temp != null) {
			// add new snapPoint to points
			snapPoint[] pointsTemp = new snapPoint[points.Length+1];
			for (int i = 0; i < points.Length; i++) {
				pointsTemp[i] = points[i];
			}
			pointsTemp[pointsTemp.Length-1] = temp;
			points = pointsTemp;
			point = temp;
		}
	}

	void OnTriggerExit(Collider otherCollider) {
		if (points.Length > 0) {
			// detect which collider needs to be removed from the points var
			int indexToDel = 0;
			for (int i = 0; i < points.Length; i++) {
				if (otherCollider.gameObject.GetInstanceID() == points[i].gameObject.GetInstanceID()) {
					indexToDel = i;
					break;
				}
			}

			// remove the collider from the points var
			snapPoint[] pointsTemp = new snapPoint[points.Length-1];
			int ii = 0;
			for (int i = 0; i < points.Length; i++) {
				if (i != indexToDel) {
					pointsTemp[ii] = points[i];
					ii++;
				}
			}
			points = pointsTemp;
			if (points.Length > 0) {
				point = points[points.Length-1];
			} else {
				point = null;
			}
		}
	}

	void OnDrop() {
		if (point == null) {
			transform.parent = OGParent;
		} else {
			transform.position = point.getSnapPos(transform.position);
			// transform.rotation = point.getSnapRot(transform);
			transform.rotation = point.transform.rotation;
			transform.parent = point.transform;
		}
	}
}
