using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AForge.Imaging;

namespace Shear_Detection
{
    class Program
    {
        static void Main(string[] args)
        {
            string [] Files = System.IO.Directory.GetFiles("C:\\Users\\Jacob\\Pictures\\ALL_SHRED\\");
            foreach (string s in Files)
            {
                Bitmap shred = new Bitmap(s);

                int top = (int)(shred.Height * 0.25);
                int bottom = (int)(shred.Height * 0.75);

                //use function to draw blue line at point of interest
                for (int j = top; j <= bottom; j++)
                {
                    int l = getShearLength(shred, j, true);
                    int r = getShearLength(shred, j, false);
                    shred.SetPixel(l, j, Color.Blue);
                    shred.SetPixel(r, j, Color.Blue);
                }
                shred.Save(s.Replace("ALL_SHRED", "SHEAR_DETECTED"));
            }
        }

        static int getShearLength(Bitmap bmp, int yCoord, bool isLeft)
        {
            //parameters
            const int textDiff = 300;   //if color sum difference is >= 300 then we are at text
            const int paperDiff = 60;   //if color sum difference is <= 60 then we are at paper
            int i = 0;
            int edgeDist;

            if (isLeft)
            {
                //get to start of edge
                i = 0;
                edgeDist = 0;
                Color c = bmp.GetPixel(i, yCoord);
                while (c.A == 0)
                {
                    i++;
                    c = bmp.GetPixel(i, yCoord);
                }

                //find width
                bool end = false;
                int refSum = 255 * 3;
                while (!end)
                {
                    int currentSum = c.R + c.G + c.B;
                    int diff = refSum - currentSum;
                    if (diff >= textDiff || diff <= paperDiff)
                        end = true;
                    else if (edgeDist >= 20)
                    {
                        end = true;
                    }
                    else
                    {
                        edgeDist++;
                        i++;
                        c = bmp.GetPixel(i, yCoord);
                    }
                }
            }
            else
            {
                //get to start of edge
                i = bmp.Width - 1;
                Color c = bmp.GetPixel(i, yCoord);
                while (c.A == 0)
                {
                    i--;
                    c = bmp.GetPixel(i, yCoord);
                }

                //find width
                bool end = false;
                //counter to make sure we dont go too far
                edgeDist = 0;
                int refSum = 255 * 3;
                while (!end)
                {
                    int currentSum = c.R + c.G + c.B;
                    int diff = refSum - currentSum;
                    if (diff >= textDiff || diff <= paperDiff)
                        end = true;
                    else if (edgeDist >= 20)
                    {
                        end = true;
                    }
                    else
                    {
                        edgeDist++;
                        i--;
                        c = bmp.GetPixel(i, yCoord);
                    }
                }
            }
            return i;
        }
    }
}
