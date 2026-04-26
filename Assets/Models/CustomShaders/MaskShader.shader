Shader "Masked/Mask" {
 
	SubShader {
		// Render the mask after regular geometry, but before masked geometry and
		// transparent things.
 
		Tags {"Queue" = "Transparent+1" }
 
		// Don't draw in the RGBA channels; just the depth buffer
 
 
		Pass {
			Blend Zero One
		}
	}
}