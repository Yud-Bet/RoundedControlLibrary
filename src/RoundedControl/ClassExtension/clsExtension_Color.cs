using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RoundedControl
{
    static class clsExtension_Color
    {
        public static Color MakeLighter(this Color thisColor, double lightnessPercent)
        {
            lightnessPercent = lightnessPercent.ForceBounds(0, 1);
            return thisColor.Blend(Color.FromRgb(255, 255, 255), lightnessPercent);
        }
        public static Color MakeDarker(this Color thisColor, double darknessPercent)
        {
            darknessPercent = darknessPercent.ForceBounds(0, 1);
            return thisColor.Blend(Color.FromRgb(0, 0, 0), darknessPercent);
        }
        public static Color Blend(this Color thisColor, Color blendToColor, double blendToPercent)
        {
            blendToPercent = (1 - blendToPercent).ForceBounds(0, 1);

            byte r = (byte)((thisColor.R * blendToPercent) + blendToColor.R * (1 - blendToPercent));
            byte g = (byte)((thisColor.G * blendToPercent) + blendToColor.G * (1 - blendToPercent));
            byte b = (byte)((thisColor.B * blendToPercent) + blendToColor.B * (1 - blendToPercent));

            return Color.FromArgb(255, r, g, b);
        }
    }
}
