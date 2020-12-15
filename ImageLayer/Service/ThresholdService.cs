using ImageLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageLayer
{
    public class ThresholdService : IThresholdService
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

                    Threshold = (int)(curColor.R * 0.299 + curColor.G * 0.578 + curColor.B * 0.114);
                    if (Threshold > 120)
                    {
                        Threshold = 255;
                    }
                    else
                    {
                        Threshold = 0;
                    }
                    newBitmap.SetPixel(i, j, Color.FromArgb(Threshold, Threshold, Threshold));
                }
            }
            return newBitmap;

        }

        //public static Bitmap getThresholdImage(Bitmap orjImage)
        //{
        //    Bitmap newBitmap = new Bitmap(orjImage.Width, orjImage.Height); // Resmin Ortalamasını Aldıktak sonra set edilecek resim

        //    int Threshold = 0;
        //    Color curColor;

        //    for (int i = 1; i < newBitmap.Width - 1; i++)
        //    {
        //        for (int j = 1; j < newBitmap.Height - 1; j++)
        //        {
        //            curColor = orjImage.GetPixel(i, j); //orj ilgili pixceldeki rengi alınır ve aşağıdaki sabit förmül ile set edilir. Asagıda yapılan r g b renklerine göre belli bir resim set edilir. eğer 120 den büyükse bu sayı threshold alırken ortalama renk kabul ediliyor. resmi full yapar değilse renkleri sıfırlar yani beyaz yapar.

        //            Threshold = (int)(curColor.R * 0.299 + curColor.G * 0.578 + curColor.B * 0.114);
        //            if (Threshold > 120)
        //            {
        //                Threshold = 255;
        //            }
        //            else
        //            {
        //                Threshold = 0;
        //            }
        //            newBitmap.SetPixel(i, j, Color.FromArgb(Threshold, Threshold, Threshold));
        //        }
        //    }
        //    return newBitmap;
        //}




    }
}
