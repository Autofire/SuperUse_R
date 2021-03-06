using UnityEngine;
using System.Collections;

namespace UniversalColors
{
	#if UNITY_EDITOR
	[UnityEditor.InitializeOnLoad]
	#endif
	public class ColorableSkinnedMeshRenderer : ColorableComponent
	{
		[SerializeField] SkinnedMeshRenderer targetRenderer = null;
		[SerializeField] int materialIndex = 0;
		[SerializeField] bool useEmission = false;

		#if UNITY_EDITOR
		static ColorableSkinnedMeshRenderer() {
			InitColorable();
		}
		#endif

		#if UNITY_STANDALONE
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		#endif
		static void InitColorable() {
			Colorable.AddColorableType(
				typeof(ColorableSkinnedMeshRenderer),	// THIS class's type
				typeof(SkinnedMeshRenderer)				// Type of your target renderer
			);
		}

		override public Color color {
			get {
				if(useEmission)
					return targetRenderer.materials[materialIndex].GetColor("_EmissionColor");
				else
					return targetRenderer.materials[materialIndex].color;
			}

			set {
				if(useEmission)
					targetRenderer.materials[materialIndex].SetColor("_EmissionColor", value);
				else 
					targetRenderer.materials[materialIndex].color = value;
			}
		}
				
		void Awake()
		{
			if(targetRenderer == null)
			{
				targetRenderer = GetComponent<SkinnedMeshRenderer>();
				UnityEngine.Assertions.Assert.IsNotNull(targetRenderer);
			}

		}


		override public System.Type RendererType() {
			return typeof(SkinnedMeshRenderer);
		}
	}

}

