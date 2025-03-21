using System.Drawing;
using System.Text;

namespace SvgColourPaletteExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //https://commons.wikimedia.org/wiki/File:WikiProject_Scouting_fleur-de-lis_dark.svg
            var path = "C:\\repos\\SvgColourPaletteExtractor";
            var file = "WikiProject_Scouting_fleur-de-lis_dark.svg";
            var filePath = Path.Combine(path, file);

            var svgCode = File.ReadAllText(filePath);
            var oldColours = new List<string>();

            var startIndex = 0;
            while (true)
            {
                var indexOfColor = svgCode.IndexOf("#", startIndex);

                if (indexOfColor < 0) { break; }

                var colour = svgCode.Substring(indexOfColor + 1, 6);

                if (IsHexColour(colour))
                {
                    oldColours.Add(colour);
                }

                startIndex += 6;
            }

            oldColours = oldColours.Distinct().Order().ToList();

            var htmlPage = new StringBuilder();
            htmlPage.AppendLine("<!DOCTYPE html><html><body>");
            foreach (var colour in oldColours)
            {
                Console.WriteLine($"#{colour}");
                htmlPage.AppendLine($"<h1 style=\"background-color:#{colour};\">#{colour}</h1>");
            }
            htmlPage.AppendLine("</body></html>");


            Console.WriteLine();
            Console.WriteLine("Paste the palettes into");
            Console.WriteLine("https://www.w3schools.com/html/tryit.asp?filename=tryhtml_color_names");
            Console.WriteLine();
            Console.WriteLine("--- Old Palette ---");
            Console.WriteLine(htmlPage.ToString());

            //var fleurDeLisColour = ColorTranslator.FromHtml("#e1ab2a");
            //var shamrockColour = ColorTranslator.FromHtml("#08773f");
            //var newColourName = "Original";

            //var shamrockColour = ColorTranslator.FromHtml("#660066");
            //var fleurDeLisColour = ColorTranslator.FromHtml("#e1ab2a");
            //var newColourName = "Lilac";

            var shamrockColour = ColorTranslator.FromHtml("#003660");
            var fleurDeLisColour = ColorTranslator.FromHtml("#007fe1");
            var newColourName = "Blue";

            var newColours = new List<string>(oldColours.Count);
            var newColourhtmlPage = new StringBuilder();
            newColourhtmlPage.AppendLine("<!DOCTYPE html><html><body>");


            var c = ChangeValue(shamrockColour, 144.1-149.7, 100-93.3, 83.1-46.7);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ColorTranslator.ToHtml(Color.FromArgb(shamrockColour.ToArgb())).ToLower();
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(shamrockColour, 0, 0, 0.4);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(shamrockColour, 144.4-149.7, 93.4- 93.3, 65.1- 46.7);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);


            //new - old
            c = ColorTranslator.ToHtml(Color.FromArgb(fleurDeLisColour.ToArgb())).ToLower();
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(fleurDeLisColour, 46.4 - 42.3, 68.5 - 81.3, 93.3 - 88.2);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(fleurDeLisColour, 46.4 - 42.3, 68.2 - 81.3, 93.7 - 88.2);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(fleurDeLisColour, 48.0 - 42.3, 77.1 - 81.3, 94.1 - 88.2);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(fleurDeLisColour, 46.5 - 42.3, 68.3 - 81.3, 94.1 - 88.2);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(fleurDeLisColour, 50.0 - 42.3, 75.0 - 81.3, 94.1 - 88.2);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(fleurDeLisColour, 53.0 - 42.3, 75.0 - 81.3, 94.1 - 88.2);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(fleurDeLisColour, 52.5 - 42.3, 75.3 - 81.3, 100.0 - 88.2);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            c = ChangeValue(fleurDeLisColour, 52.8 - 42.3, 75.3 - 81.3, 100.0 - 88.2);
            newColourhtmlPage.AppendLine($"<h1 style=\"background-color:{c};\">{c}</h1>");
            newColours.Add(c);

            newColourhtmlPage.AppendLine("</body></html>");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("--- New Palette ---");
            Console.WriteLine(newColourhtmlPage.ToString());

            var newSvgCode = svgCode;
            for (int i = 0; i < oldColours.Count; i++)
            {
                newSvgCode = newSvgCode.Replace($"#{oldColours.ElementAt(i)}", $"{newColours.ElementAt(i)}");
            }

            var oldFile = new FileInfo(file);
            var newFileName = $"{Path.GetFileNameWithoutExtension(file)}-{newColourName}{oldFile.Extension}";
            var newFilePath = Path.Combine(path, newFileName);
            File.WriteAllText(newFilePath, newSvgCode);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("New SVG created at");
            Console.WriteLine(newFilePath);
            Console.WriteLine();
        }

        private static bool IsHexColour(string colour)
        {
            foreach (char c in colour)
            {
                if (c >= 48 && c <= 57) { continue; } //0-9
                if (c >= 65 && c <= 70) { continue; } //A-F
                if (c >= 97 && c <= 102) { continue; } //a-f
                return false;
            }
            return true;
        }

        private static string ChangeValue(Color colour, double hChange, double sChange, double vChange)
        {
            double hue, saturation, value;
            ColorToHSV(colour, out hue, out saturation, out value);

            value += vChange / 100;
            saturation += sChange / 100;
            hue += hChange;

            var newColour = ColorFromHSV(hue, saturation, value);
            return ColorTranslator.ToHtml(Color.FromArgb(newColour.ToArgb())).ToLower();
        }


        //https://stackoverflow.com/a/1626175/1997617
        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        //https://stackoverflow.com/a/1626175/1997617
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Math.Max(0, Math.Min(255, Convert.ToInt32(value)));
            int p = Math.Max(0, Math.Min(255, Convert.ToInt32(value * (1 - saturation))));
            int q = Math.Max(0, Math.Min(255, Convert.ToInt32(value * (1 - f * saturation))));
            int t = Math.Max(0, Math.Min(255, Convert.ToInt32(value * (1 - (1 - f) * saturation))));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}
