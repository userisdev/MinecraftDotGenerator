using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MinecraftDotGenerator
{
    /// <summary> MCGenerator class. </summary>
    internal static class MCGenerator
    {
        /// <summary> The color map memo </summary>
        private static readonly Dictionary<Color, (MCColorDefine, double)> ColorMapMemo = new Dictionary<Color, (MCColorDefine, double)>();

        /// <summary>
        /// Gets or sets a value indicating whether [using full color].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [using full color]; otherwise, <c>false</c>.
        /// </value>
        public static bool UsingFullColor { get; set; } = true;

        /// <summary> Generates the specified work dir. </summary>
        /// <param name="workDir"> The work dir. </param>
        /// <param name="data"> The data. </param>
        /// <returns> </returns>
        public static bool Generate(DirectoryInfo workDir, byte[] data)
        {
            try
            {
                string savePath = Path.Combine(workDir.FullName, "input.png");
                FileInfo saved = Save(savePath, data);
                string resizePath = Path.Combine(workDir.FullName, "resized.png");
                FileInfo resized = Resize(saved, resizePath);
                FileInfo[] filterd = Filters(resized, workDir).ToArray();
                IEnumerable<(FileInfo, MCResult)> items = filterd.Select(e =>
                {
                    string[] s = e.Name.Split('_');
                    string tmpPath = Path.Combine(workDir.FullName, $"draw_{s[1]}");
                    return Draw(e, tmpPath);
                });

                (FileInfo, MCResult) result = Stats(items);

                string drawPath = Path.Combine(workDir.FullName, "draw.png");
                FileInfo hit = result.Item1.CopyTo(drawPath);
                _ = Recipe(hit);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary> Converts the color of the map. </summary>
        /// <param name="color"> The color. </param>
        /// <param name="colors"> The colors. </param>
        /// <returns> </returns>
        private static (MCColorDefine, double) ConvertMapColor(Color color, MCColorDefine[] colors)
        {
            if (!ColorMapMemo.ContainsKey(color))
            {
                Lab lab = Lab.FromDrawingColor(color);
                (MCColorDefine e, double) result = colors.Select(e => (e, Lab.Distance(lab, Lab.FromDrawingColor(e.ToDrawinColor())))).OrderBy(e => e.Item2).FirstOrDefault();
                ColorMapMemo[color] = result;
            }

            return ColorMapMemo[color];
        }

        /// <summary> Draws the specified source. </summary>
        /// <param name="src"> The source. </param>
        /// <param name="dst"> The DST. </param>
        /// <returns> </returns>
        private static (FileInfo, MCResult) Draw(FileInfo src, string dst)
        {
            MCColorDefine[] colors = UsingFullColor ? MCColors.FullColors : MCColors.Colors;

            List<double> list = new List<double>();
            using (Mat image = Cv2.ImRead(src.FullName, ImreadModes.Color))
            using (Mat tmp = new Mat(image.Rows, image.Cols, MatType.CV_8UC3))
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    for (int y = 0; y < image.Rows; y++)
                    {
                        Vec3b pixel = image.Get<Vec3b>(y, x);
                        Color sc = Color.FromArgb(pixel.Item2, pixel.Item1, pixel.Item0);
                        (MCColorDefine mcc, double distance) = ConvertMapColor(sc, colors);
                        Color dc = mcc.ToDrawinColor();
                        tmp.Set<Vec3b>(y, x, new Vec3b(dc.B, dc.G, dc.R));
                        list.Add(distance);
                    }
                }

                _ = tmp.SaveImage(dst);
                return (new FileInfo(dst), new MCResult(list));
            }
        }

        /// <summary> Filters the specified source. </summary>
        /// <param name="src"> The source. </param>
        /// <param name="dst"> The DST. </param>
        /// <param name="alpha"> The alpha. </param>
        /// <param name="beta"> The beta. </param>
        /// <param name="gamma"> The gamma. </param>
        /// <returns> </returns>
        private static FileInfo Filter(FileInfo src, string dst, double alpha, double beta, double gamma)
        {
            using (Mat img = Cv2.ImRead(src.FullName, ImreadModes.Color))
            using (Mat work = new Mat())
            using (Mat tmp = new Mat())
            {
                Cv2.ConvertScaleAbs(img, img, alpha, 0);
                Cv2.ConvertScaleAbs(img, img, beta, 1);

                img.ConvertTo(work, MatType.CV_32F, 1.0 / 255.0);

                Cv2.Pow(work, 1.0 / gamma, work);
                Cv2.ConvertScaleAbs(work, tmp, 255.0);

                _ = tmp.SaveImage(dst);

                return new FileInfo(dst);
            }
        }

        /// <summary> Filterses the specified source. </summary>
        /// <param name="src"> The source. </param>
        /// <param name="workDir"> The work dir. </param>
        /// <returns> </returns>
        private static IEnumerable<FileInfo> Filters(FileInfo src, DirectoryInfo workDir)
        {
            double[] alphaItems = new[] { 0.9, 1.0, 1.1 };
            double[] betaItems = new[] { 0.9, 1.0, 1.1 };
            double[] gammaItems = new[] { 0.9, 1.0, 1.1 };
            foreach ((double alpha, int ai) in alphaItems.Select((e, i) => (e, i)))
            {
                foreach ((double beta, int bi) in betaItems.Select((e, i) => (e, i)))
                {
                    foreach ((double gamma, int gi) in gammaItems.Select((e, i) => (e, i)))
                    {
                        string dst = Path.Combine(workDir.FullName, $"{Path.GetFileNameWithoutExtension(src.Name)}_{ai}{bi}{gi}{src.Extension}");
                        yield return Filter(src, dst, alpha, beta, gamma);
                    }
                }
            }
        }

        /// <summary> Recipes the specified source. </summary>
        /// <param name="src"> The source. </param>
        /// <returns> </returns>
        /// <exception cref="System.NotImplementedException"> </exception>
        private static FileInfo Recipe(FileInfo src)
        {
            throw new NotImplementedException();
        }

        /// <summary> Resizes the specified source. </summary>
        /// <param name="src"> The source. </param>
        /// <param name="dst"> The DST. </param>
        /// <returns> </returns>
        private static FileInfo Resize(FileInfo src, string dst)
        {
            using (Mat image = Cv2.ImRead(src.FullName, ImreadModes.Color))
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
                        _ = cropped.SaveImage(dst);
                        return new FileInfo(dst);
                    }
                }
            }
        }

        /// <summary> Saves the specified path. </summary>
        /// <param name="path"> The path. </param>
        /// <param name="data"> The data. </param>
        /// <returns> </returns>
        private static FileInfo Save(string path, byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            using (Bitmap src = Image.FromStream(ms) as Bitmap)
            using (Bitmap dst = new Bitmap(src.Width, src.Height))
            using (Graphics g = Graphics.FromImage(dst))
            {
                g.Clear(Color.White);
                g.DrawImageUnscaledAndClipped(src, new Rectangle(0, 0, src.Width, src.Height));
                dst.Save(path);
                return new FileInfo(path);
            }
        }

        /// <summary> Statses the specified items. </summary>
        /// <param name="items"> The items. </param>
        /// <returns> </returns>
        private static (FileInfo, MCResult) Stats(IEnumerable<(FileInfo, MCResult)> items)
        {
            (FileInfo, MCResult)[] sorted = items.OrderBy(e => e.Item2.StdDev).ToArray();
            return sorted.FirstOrDefault();
        }
    }
}
