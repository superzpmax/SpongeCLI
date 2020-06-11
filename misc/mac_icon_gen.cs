using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace test {
	public static class Program {
		
		const string src = "CCIcon.ico";
		const string dst = "CCIcon.c";
		
		static void DumpIcon(StreamWriter sw, int width, int height) {
			using (Icon icon = new Icon(src, width, height)) {
				using (Bitmap bmp = icon.ToBitmap()) {
					for (int y = 0; y < bmp.Height; y++) {
						for (int x = 0; x < bmp.Width; x++) {
							Color c = bmp.GetPixel(x, y);
							int p = (c.B << 24) | (c.G << 16) | (c.R << 8) | c.A;
							sw.Write("0x" + ((uint)p).ToString("X8") + ",");
						}
						sw.WriteLine();
					}
				}
			}
		}
		
		public static void Main(string[] args) {
			using (StreamWriter sw = new StreamWriter(dst)) {
				sw.WriteLine("const unsigned int CCIcon_Data[] = {");
				DumpIcon(sw, 64, 64);
				sw.WriteLine("};");
				sw.WriteLine("const int CCIcon_Width  = 64;");
				sw.WriteLine("const int CCIcon_Height = 64;");
			}
		}
	}
}