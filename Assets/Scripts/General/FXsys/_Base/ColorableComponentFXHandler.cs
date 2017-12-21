using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniversalColors;

/// <summary>
/// This is an abstract FXHandler which uses a colorable component. It handles nice things like
/// assuming that the colorable component will be placed on the object that this FX handler
/// resides in, and so on. However, this is configurable.
/// </summary>
public abstract class ColorableComponentFXHandler : AudioFX {

	[Space(10)]
	[Tooltip("Is ignored if targetCC is true. If both are false, current object is assumed.")]
	[SerializeField] protected GameObject			targetObj;
	[Tooltip("Set to null to create on targetObj automatically.")]
	[SerializeField] protected ColorableComponent	targetCC;

	protected Color  baseColor;

	protected virtual void Awake() {
		InitTargets();
	}

	// Do this in Start() in case there is something else that sets the color upon the object's creation
	protected virtual void Start() {
		baseColor = targetCC.color;
	}

	// FIXME: ContextMenu doesn't work.
	//[ContextMenu("Initialize targetObj & targetCC")]
	protected void InitTargets() {

		if(targetCC == null) {
			if(targetObj == null) {
				targetObj = gameObject;
			}

			targetCC = Colorable.GetComponent(targetObj);
		}

		UnityEngine.Assertions.Assert.IsNotNull(
			targetCC,
			"No suitable Colorable Component was found in " + gameObject.name
			+ " to be used by " + this.GetType().FullName
		);
	}

}
