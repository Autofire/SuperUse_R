using UnityEngine;
using System.Collections;

namespace UniversalColors
{
	#if UNITY_EDITOR
	[UnityEditor.InitializeOnLoad]
	#endif
	public class ColorableSprite : ColorableComponent
	{
		[SerializeField] private SpriteRenderer targetRenderer = null;

		#if UNITY_EDITOR
		static ColorableSprite() {
			InitColorable();
		}
		#endif

		#if UNITY_STANDALONE
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		#endif
		static void InitColorable() {
			Colorable.AddColorableType(
				typeof(ColorableSprite),	// THIS class's type
				typeof(SpriteRenderer)		// Type of your target renderer
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
				targetRenderer = GetComponent<SpriteRenderer>();
				UnityEngine.Assertions.Assert.IsNotNull(targetRenderer);
			}

		}


		override public System.Type RendererType() {
			return typeof(SpriteRenderer);
		}
	}

}

