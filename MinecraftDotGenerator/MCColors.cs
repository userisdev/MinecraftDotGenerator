using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MinecraftDotGenerator
{
    /// <summary> MCColors class. </summary>
    internal static class MCColors
    {
        /// <summary> The color map </summary>
        private static readonly Dictionary<Color, MCColorDefine> colorMap;

        /// <summary> The map </summary>
        private static readonly Dictionary<MCColor, MCColorDefine> map;

        /// <summary> Initializes the <see cref="MCColors" /> class. </summary>
        static MCColors()
        {
            map = new Dictionary<MCColor, MCColorDefine>
            {
                { MCColor.GRASS, new MCColorDefine(MCColor.GRASS, 127, 178, 56) },
                { MCColor.SAND, new MCColorDefine(MCColor.SAND, 247, 233, 163) },
                { MCColor.WOOL, new MCColorDefine(MCColor.WOOL, 199, 199, 199) },
                { MCColor.FIRE, new MCColorDefine(MCColor.FIRE, 255, 0, 0) },
                { MCColor.ICE, new MCColorDefine(MCColor.ICE, 160, 160, 255) },
                { MCColor.METAL, new MCColorDefine(MCColor.METAL, 167, 167, 167) },
                { MCColor.PLANT, new MCColorDefine(MCColor.PLANT, 0, 124, 0) },
                { MCColor.SNOW, new MCColorDefine(MCColor.SNOW, 255, 255, 255) },
                { MCColor.CLAY, new MCColorDefine(MCColor.CLAY, 164, 168, 184) },
                { MCColor.DIRT, new MCColorDefine(MCColor.DIRT, 151, 109, 77) },
                { MCColor.STONE, new MCColorDefine(MCColor.STONE, 112, 112, 112) },
                { MCColor.WATER, new MCColorDefine(MCColor.WATER, 64, 64, 255) },
                { MCColor.WOOD, new MCColorDefine(MCColor.WOOD, 143, 119, 72) },
                { MCColor.QUARTZ, new MCColorDefine(MCColor.QUARTZ, 255, 252, 245) },
                { MCColor.COLOR_ORANGE, new MCColorDefine(MCColor.COLOR_ORANGE, 216, 127, 51) },
                { MCColor.COLOR_MAGENTA, new MCColorDefine(MCColor.COLOR_MAGENTA, 178, 76, 216) },
                { MCColor.COLOR_LIGHT_BLUE, new MCColorDefine(MCColor.COLOR_LIGHT_BLUE, 102, 153, 216) },
                { MCColor.COLOR_YELLOW, new MCColorDefine(MCColor.COLOR_YELLOW, 229, 229, 51) },
                { MCColor.COLOR_LIGHT_GREEN, new MCColorDefine(MCColor.COLOR_LIGHT_GREEN, 127, 204, 25) },
                { MCColor.COLOR_PINK, new MCColorDefine(MCColor.COLOR_PINK, 242, 127, 165) },
                { MCColor.COLOR_GRAY, new MCColorDefine(MCColor.COLOR_GRAY, 76, 76, 76) },
                { MCColor.COLOR_LIGHT_GRAY, new MCColorDefine(MCColor.COLOR_LIGHT_GRAY, 153, 153, 153) },
                { MCColor.COLOR_CYAN, new MCColorDefine(MCColor.COLOR_CYAN, 76, 127, 153) },
                { MCColor.COLOR_PURPLE, new MCColorDefine(MCColor.COLOR_PURPLE, 127, 63, 178) },
                { MCColor.COLOR_BLUE, new MCColorDefine(MCColor.COLOR_BLUE, 51, 76, 178) },
                { MCColor.COLOR_BROWN, new MCColorDefine(MCColor.COLOR_BROWN, 102, 76, 51) },
                { MCColor.COLOR_GREEN, new MCColorDefine(MCColor.COLOR_GREEN, 102, 127, 51) },
                { MCColor.COLOR_RED, new MCColorDefine(MCColor.COLOR_RED, 153, 51, 51) },
                { MCColor.COLOR_BLACK, new MCColorDefine(MCColor.COLOR_BLACK, 25, 25, 25) },
                { MCColor.GOLD, new MCColorDefine(MCColor.GOLD, 250, 238, 77) },
                { MCColor.DIAMOND, new MCColorDefine(MCColor.DIAMOND, 92, 219, 213) },
                { MCColor.LAPIS, new MCColorDefine(MCColor.LAPIS, 74, 128, 255) },
                { MCColor.EMERALD, new MCColorDefine(MCColor.EMERALD, 0, 217, 58) },
                { MCColor.PODZOL, new MCColorDefine(MCColor.PODZOL, 129, 86, 49) },
                { MCColor.NETHER, new MCColorDefine(MCColor.NETHER, 112, 2, 0) },
                { MCColor.TERRACOTTA_WHITE, new MCColorDefine(MCColor.TERRACOTTA_WHITE, 209, 177, 161) },
                { MCColor.TERRACOTTA_ORANGE, new MCColorDefine(MCColor.TERRACOTTA_ORANGE, 159, 82, 36) },
                { MCColor.TERRACOTTA_MAGENTA, new MCColorDefine(MCColor.TERRACOTTA_MAGENTA, 149, 87, 108) },
                { MCColor.TERRACOTTA_LIGHT_BLUE, new MCColorDefine(MCColor.TERRACOTTA_LIGHT_BLUE, 112, 108, 138) },
                { MCColor.TERRACOTTA_YELLOW, new MCColorDefine(MCColor.TERRACOTTA_YELLOW, 186, 133, 36) },
                { MCColor.TERRACOTTA_LIGHT_GREEN, new MCColorDefine(MCColor.TERRACOTTA_LIGHT_GREEN, 103, 117, 53) },
                { MCColor.TERRACOTTA_PINK, new MCColorDefine(MCColor.TERRACOTTA_PINK, 160, 77, 78) },
                { MCColor.TERRACOTTA_GRAY, new MCColorDefine(MCColor.TERRACOTTA_GRAY, 57, 41, 35) },
                { MCColor.TERRACOTTA_LIGHT_GRAY, new MCColorDefine(MCColor.TERRACOTTA_LIGHT_GRAY, 135, 107, 98) },
                { MCColor.TERRACOTTA_CYAN, new MCColorDefine(MCColor.TERRACOTTA_CYAN, 87, 92, 92) },
                { MCColor.TERRACOTTA_PURPLE, new MCColorDefine(MCColor.TERRACOTTA_PURPLE, 122, 73, 88) },
                { MCColor.TERRACOTTA_BLUE, new MCColorDefine(MCColor.TERRACOTTA_BLUE, 76, 62, 92) },
                { MCColor.TERRACOTTA_BROWN, new MCColorDefine(MCColor.TERRACOTTA_BROWN, 76, 50, 35) },
                { MCColor.TERRACOTTA_GREEN, new MCColorDefine(MCColor.TERRACOTTA_GREEN, 76, 82, 42) },
                { MCColor.TERRACOTTA_RED, new MCColorDefine(MCColor.TERRACOTTA_RED, 142, 60, 46) },
                { MCColor.TERRACOTTA_BLACK, new MCColorDefine(MCColor.TERRACOTTA_BLACK, 37, 22, 16) },
                { MCColor.CRIMSON_NYLIUM, new MCColorDefine(MCColor.CRIMSON_NYLIUM, 189, 48, 49) },
                { MCColor.CRIMSON_STEM, new MCColorDefine(MCColor.CRIMSON_STEM, 148, 63, 97) },
                { MCColor.CRIMSON_HYPHAE, new MCColorDefine(MCColor.CRIMSON_HYPHAE, 92, 25, 29) },
                { MCColor.WARPED_NYLIUM, new MCColorDefine(MCColor.WARPED_NYLIUM, 22, 126, 134) },
                { MCColor.WARPED_STEM, new MCColorDefine(MCColor.WARPED_STEM, 58, 142, 140) },
                { MCColor.WARPED_HYPHAE, new MCColorDefine(MCColor.WARPED_HYPHAE, 86, 44, 62) },
                { MCColor.WARPED_WART_BLOCK, new MCColorDefine(MCColor.WARPED_WART_BLOCK, 20, 180, 133) },
                { MCColor.DEEPSLATE, new MCColorDefine(MCColor.DEEPSLATE, 100, 100, 100) },
                { MCColor.RAW_IRON, new MCColorDefine(MCColor.RAW_IRON, 216, 175, 147) },
                { MCColor.GLOW_LICHEN, new MCColorDefine(MCColor.GLOW_LICHEN, 127, 167, 150) }
            };

            colorMap = FullColors.ToDictionary(e => e.ToDrawinColor(), e => e);
        }

        /// <summary> Gets the colors. </summary>
        /// <value> The colors. </value>
        public static MCColorDefine[] Colors => map.Values.ToArray();

        /// <summary> Gets the full colors. </summary>
        /// <value> The full colors. </value>
        public static MCColorDefine[] FullColors => map.Values.Select(e => new[] { e, e.ToMode180(), e.ToMode220() }).SelectMany(e => e).ToArray();

        /// <summary> Gets the color define. </summary>
        /// <param name="color"> The color. </param>
        /// <returns> </returns>
        public static MCColorDefine GetColorDefine(MCColor color)
        {
            return map[color];
        }

        /// <summary> Gets the color define. </summary>
        /// <param name="color"> The color. </param>
        /// <returns> </returns>
        public static MCColorDefine GetColorDefine(Color color)
        {
            return !colorMap.ContainsKey(color) ? default : colorMap[color];
        }
    }
}
