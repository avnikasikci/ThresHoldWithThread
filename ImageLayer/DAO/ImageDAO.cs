using ImageLayer.Collection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLayer.DAO
{
    [Serializable]
    public class ImageDAO
    {
        public Bitmap getThresholdImage(Bitmap orjImage)
        {
            Bitmap newBitmap = new Bitmap(orjImage.Width, orjImage.Height); // Resmin Ortalamasını Aldıktak sonra set edilecek resim

            int Threshold = 0;
            Color curColor;

            for (int i = 1; i < newBitmap.Width - 1; i++)
            {
                for (int j = 1; j < newBitmap.Height - 1; j++)
                {
                    curColor = orjImage.GetPixel(i, j); //orj ilgili pixceldeki rengi alınır ve aşağıdaki sabit förmül ile set edilir. Asagıda yapılan r g b renklerine göre belli bir resim set edilir. eğer 120 den büyükse bu sayı threshold alırken ortalama renk kabul ediliyor. resmi full yapar değilse renkleri sıfırlar yani beyaz yapar.

                    Threshold = (int)(curColor.R * 0.299 + curColor.G * 0.578 + curColor.B * 0.114); // esik degeri 120 manuel oldu threshold degil.

                    Threshold = Threshold > 120 ? 255 : 0;

                    newBitmap.SetPixel(i, j, Color.FromArgb(Threshold, Threshold, Threshold));
                }
            }        


            return newBitmap;
        }


        public int getThresholdValueDemo(int Height, int Width, List<ColorEntity> orjBitmapColorList)
        {   

            var ThresholdList = new List<int>();
            int AveragePixel = 0;
            Color newColor;

            for (int i = 1; i < Width - 1; i++)
            {
                var CheckColoWeightr = orjBitmapColorList.Where(x => x.Weight == i).ToList();

                for (int j = 1; j < Height - 1; j++)
                {
                    var CheckColor = CheckColoWeightr.Where(x => x.Height == j && x.Weight == i).FirstOrDefault();
                    if (CheckColor != null)
                    {
                        newColor = CheckColor.ColorByPixel;
                        ThresholdList.Add((int)(newColor.R * 0.299 + newColor.G * 0.578 + newColor.B * 0.114)); // esik degeri 120 manuel oldu threshold degil.                   
                    }

                }
            }
            AveragePixel = (ThresholdList != null && ThresholdList.Count() > 0) ? ThresholdList.Sum() / ThresholdList.Count : 0;
            return AveragePixel;
        }



        public int getThresholdValue(Bitmap orjImage)
        {
            Bitmap newBitmap = new Bitmap(orjImage.Width, orjImage.Height); // Resmin Ortalamasını Aldıktak sonra set edilecek resim

            var ThresholdList = new List<int>();
            int AveragePixel = 0;
            Color curColor;

            for (int i = 1; i < newBitmap.Width - 1; i++)
            {
                for (int j = 1; j < newBitmap.Height - 1; j++)
                {
                    curColor = orjImage.GetPixel(i, j); //orj ilgili pixceldeki rengi alınır ve aşağıdaki sabit förmül ile set edilir. Asagıda yapılan r g b renklerine göre belli bir resim set edilir. eğer 120 den büyükse bu sayı threshold alırken ortalama renk kabul ediliyor. resmi full yapar değilse renkleri sıfırlar yani beyaz yapar.

                    ThresholdList.Add((int)(curColor.R * 0.299 + curColor.G * 0.578 + curColor.B * 0.114)); // esik degeri 120 manuel oldu threshold degil.                   
                    
                }
            }
            AveragePixel = (ThresholdList != null && ThresholdList.Count() > 0) ? ThresholdList.Sum() / ThresholdList.Count : 0;
            return AveragePixel;
        }

        public static Color GetDominantColor(Bitmap bmp)
        {
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                    total++;
                }
            }

            //Calculate average
            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb(r, g, b);
        }







    }
}
