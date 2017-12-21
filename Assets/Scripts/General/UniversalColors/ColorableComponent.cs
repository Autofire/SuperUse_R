using UnityEngine;
using System;
using System.Collections.Generic;

namespace UniversalColors
{
	abstract public class ColorableComponent : MonoBehaviour {

		/// <summary>A way to access the renderer's color.</summary>
		/// <value>The color used by the renderer.</value>
		/// <remarks>When writing this, ensure that the get/set functions direclty adjust the renderer's color!</remarks>
		abstract public Color color { get; set; }

		public void SetRed(float newVal)
		{
			Color tmpColor = color;
			tmpColor.r = newVal;
			color = tmpColor;
		}

		public void SetBlue(float newVal)
		{
			Color tmpColor = color;
			tmpColor.b = newVal;
			color = tmpColor;
		}

		public void SetGreen(float newVal)
		{
			Color tmpColor = color;
			tmpColor.g = newVal;
			color = tmpColor;
		}

		public void SetAlpha(float newVal)
		{
			Color tmpColor = color;
			tmpColor.a = newVal;
			color = tmpColor;
		}

		abstract public Type RendererType();
	}


}