using UnityEngine;
using System.Collections;

namespace UniversalColors
{
	#if UNITY_EDITOR
	[UnityEditor.InitializeOnLoad]
	#endif
	public class ColorableMeshRenderer : ColorableComponent
	{
		[SerializeField] private MeshRenderer targetRenderer = null;

		#if UNITY_EDITOR
		static ColorableMeshRenderer() {
			InitColorable();
		}
		#endif

		#if UNITY_STANDALONE
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		#endif
		static void InitColorable() {
			Colorable.AddColorableType(
				typeof(ColorableMeshRenderer),	// THIS class's type
				typeof(MeshRenderer)			// Type of your target renderer
			);
		}

		override public Color color {
			get { return targetRenderer.material.color; }
			set { targetRenderer.material.color = value; }
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

