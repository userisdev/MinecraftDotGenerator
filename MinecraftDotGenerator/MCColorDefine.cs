using System;
using System.Drawing;
using System.Security.Cryptography.Pkcs;

namespace MinecraftDotGenerator
{
  
    internal enum MCColorMode
    {
        Normal,
        Mode220,
        Mode180,

    }

    internal sealed class MCColorDefine
    {
        public MCColorDefine(MCColor color, byte red, byte green, byte blue, params string[] blocks)
        {
            Mode = MCColorMode.Normal;

            Color = color;
            Red = red;
            Green = green;
            Blue = blue;
            Blocks = blocks;
        }

        private MCColorDefine(MCColorMode mode, MCColor color, byte red, byte green, byte blue, params string[] blocks)
        {
            Mode = mode;

            Color = color;
            Red = red;
            Green = green;
            Blue = blue;
            Blocks = blocks;
        }

        public MCColorDefine ToMode220()
        {
            if (Mode != MCColorMode.Normal) 
            {
                throw new InvalidOperationException(nameof(ToMode220));
            }
            
            var r =(byte)(1.0* this.Red * 220 / 255);
            var g = (byte)(1.0 * this.Green * 220 / 255);
            var b = (byte)(1.0 * this.Blue * 220 / 255);
            return new MCColorDefine(MCColorMode.Mode220, this.Color,r, g, b, this.Blocks);
        }


        public MCColorDefine ToMode180()
        {
            if (Mode != MCColorMode.Normal)
            {
                throw new InvalidOperationException(nameof(ToMode180));
            }

            var r = (byte)(1.0 * this.Red * 180 / 255);
            var g = (byte)(1.0 * this.Green * 180 / 255);
            var b = (byte)(1.0 * this.Blue * 180 / 255);
            return new MCColorDefine(MCColorMode.Mode180, this.Color, r, g, b, this.Blocks);
        }

        public MCColorMode Mode { get;  }

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
