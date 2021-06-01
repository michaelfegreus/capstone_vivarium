using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InvertedMaskImage : Image { // Thanks to nicloay on answers.unity.com for this script
	public override Material materialForRendering
	{
		get
		{
			Material result = base.materialForRendering;
			result.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
			return result;
		}
	}
}