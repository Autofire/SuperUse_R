using UnityEngine;
using System.Collections;

namespace UniversalColors
{
	public class ColorableMeshRendererEmission : ColorableComponent
	{
		[SerializeField] private MeshRenderer targetRenderer = null;

		override public Color color {
			get { return targetRenderer.material.GetColor("_EmissionColor"); }
			set { targetRenderer.material.SetColor("_EmissionColor", value); }
		}
				
		void Awake()
		{
			if(targetRenderer == null)
			{
				targetRenderer = GetComponent<MeshRenderer>();
				UnityEngine.Assertions.Assert.IsNotNull(targetRenderer);
			}

		}

		override public System.Type RendererType() {
			return typeof(MeshRenderer);
		}
	}

}

