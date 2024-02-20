using System;

namespace MinecraftDotGenerator
{
    /// <summary> Lab class. </summary>
    internal sealed class Lab
    {
        /// <summary> Initializes a new instance of the <see cref="Lab" /> class. </summary>
        /// <param name="l"> The l. </param>
        /// <param name="a"> a. </param>
        /// <param name="b"> The b. </param>
        public Lab(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }

        /// <summary> Gets a. </summary>
        /// <value> a. </value>
        public double A { get; }

        /// <summary> Gets the b. </summary>
        /// <value> The b. </value>
        public double B { get; }

        /// <summary> Gets the l. </summary>
        /// <value> The l. </value>
        public double L { get; }

        /// <summary> Distances the specified a. </summary>
        /// <param name="a"> a. </param>
        /// <param name="b"> The b. </param>
        /// <returns> </returns>
        public static double Distance(Lab a, Lab b)
        {
            return Math.Sqrt(Math.Pow(a.L - b.L, 2) + Math.Pow(a.A - b.A, 2) + Math.Pow(a.B - b.B, 2));
        }

        /// <summary> Froms the color of the drawing. </summary>
        /// <param name="color"> The color. </param>
        /// <returns> </returns>
        public static Lab FromDrawingColor(System.Drawing.Color color)
        {
            return FromRGB(color.R, color.G, color.B);
        }

        /// <summary> Froms the RGB. </summary>
        /// <param name="r"> The r. </param>
        /// <param name="g"> The g. </param>
        /// <param name="b"> The b. </param>
        /// <returns> </returns>
        public static Lab FromRGB(double r, double g, double b)
        {
            double rRatio = r / 255;
            double gRatio = g / 255;
            double bRatio = b / 255;

            double rConverted = rRatio > 0.04045 ? Math.Pow((rRatio + 0.055) / 1.055, 2.4) : rRatio / 12.92;
            double gConverted = gRatio > 0.04045 ? Math.Pow((gRatio + 0.055) / 1.055, 2.4) : gRatio / 12.92;
            double bConverted = bRatio > 0.04045 ? Math.Pow((bRatio + 0.055) / 1.055, 2.4) : bRatio / 12.92;

            double x = ((rConverted * 0.4124) + (gConverted * 0.3576) + (bConverted * 0.1805)) * 100 / 95.047;
            double y = ((rConverted * 0.2126) + (gConverted * 0.7152) + (bConverted * 0.0722)) * 100 / 100;
            double z = ((rConverted * 0.0193) + (gConverted * 0.1192) + (bConverted * 0.9505)) * 100 / 108.883;

            double xConverted = x > 0.008856 ? Math.Pow(x, 1.0 / 3.0) : (7.787 * x) + (4.0 / 29);
            double yConverted = y > 0.008856 ? Math.Pow(y, 1.0 / 3.0) : (7.787 * y) + (4.0 / 29);
            double zConverted = z > 0.008856 ? Math.Pow(z, 1.0 / 3.0) : (7.787 * z) + (4.0 / 29);

            return new Lab((116 * yConverted) - 16, 500 * (xConverted - yConverted), 200 * (yConverted - zConverted));
        }
    }
}
