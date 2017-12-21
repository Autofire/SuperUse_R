using UnityEngine;
using System;
using System.Collections.Generic;

namespace UniversalColors
{

	static public class Colorable {

		// TODO: Write favored CC stuff
		// TODO: Write GetComponentInChildren, etc.
		static private Dictionary<Type, Type> ccToInterfacedType;

		/// <summary>
		/// Regesters newComponent, which interfaces with the component of newType. Note that the same newComponent will
		/// not be registered twice.
		/// </summary>
		static public void AddColorableType(Type newComponent, Type newType)
		{
			// Initializing the dictionary within the declaration is untrustworthy, so it's better to do it here. 
			if(ccToInterfacedType == null)
			{
				ccToInterfacedType = new Dictionary<Type, Type>();
			}

			if(!ccToInterfacedType.ContainsKey(newComponent))
			{
				ccToInterfacedType.Add(newComponent, newType);
			}
		}

		/// <summary>
		/// Fetches the first ColorableComponent on the targetObj, even if we had to create it.
		/// </summary>
		/// <returns>
		/// If the Target Object doesn't have a ColorableComponent, the function attempts to attach a compatible
		/// ColorableComponent to the object. If we were successful in attaching a ColorableComponent, then the new
		/// component is returned. Otherwise, null is returned.
		/// </returns>
		/// <param name="targetObj">Object to fetch the ColorableComponent from.</param>
		static public ColorableComponent GetComponent(GameObject targetObj)
		{
			ColorableComponent cc;

			cc = targetObj.GetComponent<ColorableComponent>();

			if(cc == null)
			{
				cc = CreateComponent(targetObj);
			}

			return cc;
		}

		/// <summary>
		/// Attempts to create a colorable component based on the renderers that the target GameObject contains. This
		/// is not meant to be called directly; use Colorable.GetComponent(targetObj) instead.
		/// </summary>
		/// <returns>The created component, or null if creation process fails.</returns>
		/// <param name="target">Object to search.</param>
		static public ColorableComponent CreateComponent(GameObject target)
		{
			foreach(KeyValuePair<Type, Type> ccRendererPair in ccToInterfacedType)
			{
				if(target.GetComponent(ccRendererPair.Value) != null)
				{
					ColorableComponent newComponent = target.AddComponent(ccRendererPair.Key) as ColorableComponent;

					return newComponent;
				}
			}

			// Runs only if we didn't add any components.
			#if UNITY_EDITOR
			Debug.LogWarning("No suitable colorable component was found for " + target.name);
			#endif

			return null;
		}
	}

}