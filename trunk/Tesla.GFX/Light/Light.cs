// Light.cs created with MonoDevelop
// User: topfs at 4:48 PM 4/1/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using Tesla.Common;

namespace Tesla.GFX
{
	public enum CalculationMode
	{
		Linear,
		Exponential,
		InverseExponential,
		AnotherExponential
	}
	
	public class Light
	{
		private Vector3f position;
		private Color4f  color;
		private float    start, end, len;
		private CalculationMode lightCalculation;

		public Light(Vector3f position, Color4f color, float start, float end, CalculationMode lightCalculation)
		{
			this.position = position;
			this.color    = color;
			this.start    = start;
			this.end      = end;
			this.len      = end - start;
			this.lightCalculation = lightCalculation;
		}
		
		public Color4f calculateColor(Vector3f v)
		{
			float d = position.length(v);
			
			if (d < start)
				return color.copy();
			else if (d > end)
				return new Color4f(0.0f, 0.0f, 0.0f, 0.0f);
			else
				return color.copy().multiply(calculate((d - start) / len));
		}
		
		private float calculate(float d)
		{
			switch (lightCalculation)
			{
			case CalculationMode.Linear:
				return 1.0f - d;
			case CalculationMode.Exponential:
				return 1.0f - d * d;
			case CalculationMode.InverseExponential:
				return 1.0f - (float)Math.Sqrt(d);
			case CalculationMode.AnotherExponential:
				return (1.0f - d) * (1.0f - d);

			default:
				return 1.0f - d;
			}
		}
	}
}
