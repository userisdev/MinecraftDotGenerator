using System;
using System.Drawing;

namespace MinecraftDotGenerator
{
    /// <summary> MCColorDefine class. </summary>
    internal sealed class MCColorDefine
    {
        /// <summary> Initializes a new instance of the <see cref="MCColorDefine" /> class. </summary>
        /// <param name="color"> The color. </param>
        /// <param name="red"> The red. </param>
        /// <param name="green"> The green. </param>
        /// <param name="blue"> The blue. </param>
        /// <param name="blocks"> The blocks. </param>
        public MCColorDefine(MCColor color, byte red, byte green, byte blue, params string[] blocks)
        {
            Mode = MCColorMode.Normal;

            Color = color;
            Red = red;
            Green = green;
            Blue = blue;
            Blocks = blocks;
        }

        /// <summary> Initializes a new instance of the <see cref="MCColorDefine" /> class. </summary>
        /// <param name="mode"> The mode. </param>
        /// <param name="color"> The color. </param>
        /// <param name="red"> The red. </param>
        /// <param name="green"> The green. </param>
        /// <param name="blue"> The blue. </param>
        /// <param name="blocks"> The blocks. </param>
        private MCColorDefine(MCColorMode mode, MCColor color, byte red, byte green, byte blue, params string[] blocks)
        {
            Mode = mode;

            Color = color;
            Red = red;
            Green = green;
            Blue = blue;
            Blocks = blocks;
        }

        /// <summary> Gets the blocks. </summary>
        /// <value> The blocks. </value>
        public string[] Blocks { get; }

        /// <summary> Gets the blue. </summary>
        /// <value> The blue. </value>
        public byte Blue { get; }

        /// <summary> Gets the color. </summary>
        /// <value> The color. </value>
        public MCColor Color { get; }

        /// <summary> Gets the green. </summary>
        /// <value> The green. </value>
        public byte Green { get; }

        /// <summary> Gets the mode. </summary>
        /// <value> The mode. </value>
        public MCColorMode Mode { get; }

        /// <summary> Gets the red. </summary>
        /// <value> The red. </value>
        public byte Red { get; }

        /// <summary> Converts to drawincolor. </summary>
        /// <returns> </returns>
        public Color ToDrawinColor()
        {
            return System.Drawing.Color.FromArgb(Red, Green, Blue);
        }

        /// <summary> Converts to mode180. </summary>
        /// <returns> </returns>
        /// <exception cref="System.InvalidOperationException"> ToMode180 </exception>
        public MCColorDefine ToMode180()
        {
            if (Mode != MCColorMode.Normal)
            {
                throw new InvalidOperationException(nameof(ToMode180));
            }

            byte r = (byte)(1.0 * Red * 180 / 255);
            byte g = (byte)(1.0 * Green * 180 / 255);
            byte b = (byte)(1.0 * Blue * 180 / 255);
            return new MCColorDefine(MCColorMode.Mode180, Color, r, g, b, Blocks);
        }

        /// <summary> Converts to mode220. </summary>
        /// <returns> </returns>
        /// <exception cref="System.InvalidOperationException"> ToMode220 </exception>
        public MCColorDefine ToMode220()
        {
            if (Mode != MCColorMode.Normal)
            {
                throw new InvalidOperationException(nameof(ToMode220));
            }

            byte r = (byte)(1.0 * Red * 220 / 255);
            byte g = (byte)(1.0 * Green * 220 / 255);
            byte b = (byte)(1.0 * Blue * 220 / 255);
            return new MCColorDefine(MCColorMode.Mode220, Color, r, g, b, Blocks);
        }
    }
}
