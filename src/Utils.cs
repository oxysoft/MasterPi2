using Microsoft.Xna.Framework;

namespace MasterPI2
{
	public class Utils
	{
		public static Color HSV(float hue, float saturation, float value, float alpha)
        {
            while (hue > 1f) { hue -= 1f; }
            while (hue < 0f) { hue += 1f; }
            while (saturation > 1f) { saturation -= 1f; }
            while (saturation < 0f) { saturation += 1f; }
            while (value > 1f) { value -= 1f; }
            while (value < 0f) { value += 1f; }
            if (hue > 0.999f) { hue = 0.999f; }
            if (hue < 0.001f) { hue = 0.001f; }
            if (saturation > 0.999f) { saturation = 0.999f; }
            if (saturation < 0.001f) { return new Color(value * 255f, value * 255f, value * 255f); }
            if (value > 0.999f) { value = 0.999f; }
            if (value < 0.001f) { value = 0.001f; }

            float h6 = hue * 6f;
            if (h6 == 6f) { h6 = 0f; }
            int ihue = (int)(h6);
            float p = value * (1f - saturation);
            float q = value * (1f - (saturation * (h6 - ihue)));
            float t = value * (1f - (saturation * (1f - (h6 - ihue))));
            switch (ihue)
            {
                case 0:
                    return new Color(value, t, p, alpha);
                case 1:
                    return new Color(q, value, p, alpha);
                case 2:
                    return new Color(p, value, t, alpha);
                case 3:
                    return new Color(p, q, value, alpha);
                case 4:
                    return new Color(t, p, value, alpha);
                default:
                    return new Color(value, p, q, alpha);
            }
        }
	}
}

