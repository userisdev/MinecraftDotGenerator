using System.Drawing;

namespace MinecraftDotGenerator
{
    internal sealed class MCColorDefine
    {
        public MCColorDefine(MCColor color, byte red, byte green, byte blue, params string[] blocks)
        {
            Color = color;
            Red = red;
            Green = green;
            Blue = blue;
            Blocks = blocks;
        }

        public string[] Blocks { get; }

        public byte Blue { get; }

        public MCColor Color { get; }

        public byte Green { get; }

        public byte Red { get; }

        public Color ToDrawinColor()
        {
            return System.Drawing.Color.FromArgb(Red, Green, Blue);
        }
    }
}
