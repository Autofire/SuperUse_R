using UnityEngine;
using VariableObjects;

public class PlayFXIfNull : MonoBehaviour {

	[SerializeField] TransformConstReference checkTarget;
	[SerializeField] FXHandler fx;

	void Update() {
		if(fx != null && checkTarget.constValue == null) {
			if(!fx.IsPlaying()) {
				fx.Play();
			}
		}
		else {
			fx.Stop();
		}
	}

}
