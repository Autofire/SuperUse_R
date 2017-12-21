using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UniversalColors
{
	#if UNITY_EDITOR
	[UnityEditor.InitializeOnLoad]
	#endif
	public class ColorableImage : ColorableComponent
	{
		[SerializeField] private Image targetRenderer = null;

		#if UNITY_EDITOR
		static ColorableImage() {
			InitColorable();
		}
		#endif

		#if UNITY_STANDALONE
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		#endif
		static void InitColorable() {
			Colorable.AddColorableType(
				typeof(ColorableImage),		// THIS class's type
				typeof(Image)				// Type of your target renderer
			);
		}

		override public Color color {
			get { return targetRenderer.color; }
			set { targetRenderer.color = value; }
		}
				
		void Awake()
		{
			if(targetRenderer == null)
			{
				targetRenderer = GetComponent<Image>();
				UnityEngine.Assertions.Assert.IsNotNull(targetRenderer);
			}

		}


		override public System.Type RendererType() {
			return typeof(Image);
		}
	}

}

