using OpenCvSharp;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MinecraftDotGenerator
{
    internal static class Program
    {
        private static MCColorDefine ConvertMapColor(System.Drawing.Color color)
        {
            Lab lab = Lab.FromDrawingColor(color);
            return MCColors.Colors.OrderBy(e => Lab.Distance(lab, Lab.FromDrawingColor(e.ToDrawinColor()))).FirstOrDefault();
        }

        private static string ConvertMapDraw(string path)
        {
            using (Bitmap src = System.Drawing.Image.FromFile(path) as Bitmap)
            using (Bitmap dst = new Bitmap(src.Width, src.Height))
            {
                for (int x = 0; x < src.Width; x++)
                {
                    for (int y = 0; y < src.Height; y++)
                    {
                        Color pix = src.GetPixel(x, y);
                        MCColorDefine mcc = ConvertMapColor(pix);
                        dst.SetPixel(x, y, mcc.ToDrawinColor());

                        Console.WriteLine($"x={x},y={y} color={pix}->{mcc.ToDrawinColor()}");
                    }
                }

                string name = Path.GetFileNameWithoutExtension(path);
                dst.Save($"{name}_map.png");
                return $"{name}_map.png";
            }
        }

        private static string ConvertTo24Bit(string path, System.Drawing.Color bgColor)
        {
            using (Bitmap src = System.Drawing.Image.FromFile(path) as Bitmap)
            using (Bitmap dst = new Bitmap(src.Width, src.Height))
            using (Graphics g = Graphics.FromImage(dst))
            {
                g.Clear(bgColor);
                g.DrawImageUnscaledAndClipped(src, new Rectangle(0, 0, src.Width, src.Height));
                string name = Path.GetFileNameWithoutExtension(path);
                dst.Save($"{name}_24bit.png");
                return $"{name}_24bit.png";
            }
        }

        private static void GenerateColorSample()
        {
            MCColorDefine[] colors = MCColors.Colors;
            int h = 10 * colors.Length;
            int w = h;

            using (Bitmap bmp = new Bitmap(w, h))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.White);
                for (int i = 0; i < colors.Length; i++)
                {
                    using (SolidBrush brush = new SolidBrush(colors[i].ToDrawinColor()))
                    {
                        g.FillRectangle(brush, new Rectangle(0, 10 * i, w, 10));
                    }
                }

                bmp.Save("color_map.png");
            }
        }

        private static void Main(string[] args)
        {
            GenerateColorSample();

            string inputImagePath = args.FirstOrDefault();

            if (File.Exists(inputImagePath))
            {
                var bgColor = Color.FromArgb(255, 255, 255);
                string img24bitPath = ConvertTo24Bit(inputImagePath, bgColor);
                string resizedPath = ReseizeImage(img24bitPath);
                _ = ConvertMapDraw(resizedPath);
            }
        }

        private static string ReseizeImage(string path)
        {
            using (Mat image = Cv2.ImRead(path, ImreadModes.Color))
            {
                int shortEdge = Math.Min(image.Width, image.Height);
                double scale = 128.0 / shortEdge;

                OpenCvSharp.Size newSize = new OpenCvSharp.Size((int)(scale * image.Width), (int)(scale * image.Height));
                using (Mat resized = new Mat())
                {
                    Cv2.Resize(image, resized, newSize, 0, 0, InterpolationFlags.Area);
                    int x = (resized.Width - 128) / 2;
                    int y = (resized.Height - 128) / 2;
                    Rect roi = new Rect(x, y, 128, 128);
                    using (Mat cropped = new Mat(resized, roi))
                    {
                        string name = Path.GetFileNameWithoutExtension(path);
                        _ = cropped.SaveImage($"{name}_resized.png");
                        return $"{name}_resized.png";
                    }
                }
            }
        }
    }
}
