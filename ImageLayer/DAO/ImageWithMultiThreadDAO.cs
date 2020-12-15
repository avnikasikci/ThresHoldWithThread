using ImageLayer.Collection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageLayer.DAO
{
    [Serializable]
    public class ImageWithMultiThreadDAO
    {
        [System.ComponentModel.Browsable(false)]
        public static bool CheckForIllegalCrossThreadCalls { get; set; }

        public ImageWithMultiThreadDAO()
        {

        }
        public static Bitmap orjBitmap, setNewBitmap;






        public List<ColorEntity> getBitmapByAllPixel(Bitmap Image)
        {
            Color curColor;
            var BitmapColorList = new List<ColorEntity>();
            for (int i = 1; i < Image.Width - 1; i++)
            {
                for (int j = 1; j < Image.Height - 1; j++)
                {
                    curColor = Image.GetPixel(i, j); //orj ilgili pixceldeki rengi alınır ve aşağıdaki sabit förmül ile set edilir. Asagıda yapılan r g b renklerine göre belli bir resim set edilir. eğer 120 den büyükse bu sayı threshold alırken ortalama renk kabul ediliyor. resmi full yapar değilse renkleri sıfırlar yani beyaz yapar.

                    var EntityColor = new ColorEntity();
                    EntityColor.Height = j;
                    EntityColor.Weight = i;
                    EntityColor.ColorByPixel = curColor;
                    BitmapColorList.Add(EntityColor);
                }
            }
            return BitmapColorList;
        }




        public Bitmap getThresholdImage(Bitmap orjImage)
        {
            Bitmap newBitmap = new Bitmap(orjImage.Width, orjImage.Height); // Resmin Ortalamasını Aldıktak sonra set edilecek resim          
            Bitmap setReturnBitmap = this.StartMultipleThread(orjImage, orjImage.Width, orjImage.Height);

            //Bitmap setReturnParelelFor = this.ToGrayscale(orjImage, newBitmap);


            //if (setReturnParelelFor != null)
            //{
            //    return setReturnParelelFor;
            //}

            if (setReturnBitmap != null)
            {
                return setReturnBitmap;
            }

            return newBitmap;

        }





        public Bitmap StartMultipleThread(Bitmap orjImage, int Weight, int Height)
        {

            // get all pixel cek diziye cevir ordan dizinin içinde oyna 
            // threadlerin bittiğini kontrol ederek yap bittiktek sonra set edilecek resmi ver kullanıcıya
            // 

            DateTime startTime = DateTime.Now;

            var tidH = Height / 4;
            var tidW = Weight / 4;

            Bitmap newBitmap = new Bitmap(orjImage.Width, orjImage.Height); // Resmin Ortalamasını Aldıktak sonra set edilecek resim            

            Bitmap orjBitmap1 = orjImage;
            Bitmap orjBitmap2 = orjImage;
            Bitmap orjBitmap3 = orjImage;
            Bitmap orjBitmap4 = orjImage;



            Thread t1 = new Thread(() =>
            {
                bool finished = false;
                int height = tidH * 1;
                int weight = tidW * 1;
                int startH = 1;
                int startW = 1;
                Color curColor1;
                int ret1 = 100;
                //while (finished)
                //{
                //}
                Thread.Sleep(1000);
                for (int i = startW; i < weight - 1; i++) // widht -1 Threshold alırken son pixel kullanılmıyor.
                {
                    for (int j = startH; j < height - 1; j++)
                    {
                        curColor1 = orjImage.GetPixel(i, j); // Pixelleri tek tek dolaşıp renklerini alırız.

                        ret1 = (int)(curColor1.R * 0.299 + curColor1.G * 0.578 + curColor1.B * 0.114); // R G B default renkleri Threshold alırken ortalama degerini kullanmak için

                        ret1 = ret1 > 120 ? 255 : 0;

                        newBitmap.SetPixel(i, j, Color.FromArgb(ret1, ret1, ret1)); // R G B renklerine göre resmi set et.

                    }
                    if (i == weight - 1)
                    {
                        finished = true;
                    }
                }

                Console.WriteLine("I ran for 5 seconds");
            });

            Thread t2 = new Thread(() =>
            {

                bool finished = false;
                int height = tidH * 2;
                int weight = tidW * 2;
                int startH = tidH;
                int startW = tidW;
                Color curColor2;
                int ret2 = 100;
                //while (!finished)
                //{ }
                Thread.Sleep(1000);
                for (int i = startW; i < weight - 1; i++) // widht -1 Threshold alırken son pixel kullanılmıyor.
                {
                    for (int j = startH; j < height - 1; j++)
                    {
                        curColor2 = orjImage.GetPixel(i, j); // Pixelleri tek tek dolaşıp renklerini alırız.

                        //ret2 = (int)(curColor2.R * 0.299 + curColor2.G * 0.578 + curColor2.B * 0.114); // R G B default renkleri Threshold alırken ortalama degerini kullanmak için
                        if (ret2 > 120)
                        {
                            ret2 = 255;
                        }
                        else
                        {
                            ret2 = 0;
                        }
                        newBitmap.SetPixel(i, j, Color.FromArgb(ret2, ret2, ret2)); // R G B renklerine göre resmi set et.

                    }
                    if (i == weight - 1)
                    {
                        finished = true;
                    }
                }



                Console.WriteLine("I ran for 8 seconds");
            });


            //parameterized thread
            Thread t3 = new Thread(p =>
            {
                int numberOfSeconds = 0;

                bool finished = false;
                int height = tidH * 4;
                int weight = tidW * 3 + 1;
                int startH = 1;
                int startW = tidW * 2;
                Color curColor;
                int ret = 100;
                while (finished)
                {
                    Thread.Sleep(1000);
                    for (int i = startW; i < weight - 1; i++) // widht -1 Threshold alırken son pixel kullanılmıyor.
                    {
                        for (int j = startH; j < height - 1; j++)
                        {
                            curColor = orjBitmap3.GetPixel(i, j); // Pixelleri tek tek dolaşıp renklerini alırız.

                            ret = (int)(curColor.R * 0.299 + curColor.G * 0.578 + curColor.B * 0.114); // R G B default renkleri Threshold alırken ortalama degerini kullanmak için
                            if (ret > 120)
                            {
                                ret = 255;
                            }
                            else
                            {
                                ret = 0;
                            }
                            newBitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret)); // R G B renklerine göre resmi set et.

                        }
                        if (i == weight - 2)
                        {
                            finished = true;
                        }
                    }
                }
                Console.WriteLine("I ran for {0} seconds", numberOfSeconds);
            });

            //parameterized thread
            Thread t4 = new Thread(p =>
            {
                int numberOfSeconds = 0;

                bool finished = false;
                int height = tidH * 4;
                int weight = tidW * 4 + 1;
                int startH = 1;
                int startW = tidW * 3;
                Color curColor;
                int ret = 100;
                while (finished)
                {
                    Thread.Sleep(1000);
                    for (int i = startW; i < weight - 1; i++) // widht -1 Threshold alırken son pixel kullanılmıyor.
                    {
                        for (int j = startH; j < height - 1; j++)
                        {
                            curColor = orjBitmap4.GetPixel(i, j); // Pixelleri tek tek dolaşıp renklerini alırız.

                            ret = (int)(curColor.R * 0.299 + curColor.G * 0.578 + curColor.B * 0.114); // R G B default renkleri Threshold alırken ortalama degerini kullanmak için
                            if (ret > 120)
                            {
                                ret = 255;
                            }
                            else
                            {
                                ret = 0;
                            }
                            newBitmap.SetPixel(i, j, Color.FromArgb(ret, ret, ret)); // R G B renklerine göre resmi set et.

                        }
                        if (i == weight - 2)
                        {
                            finished = true;
                        }
                    }
                }

                Console.WriteLine("I ran for {0} seconds", numberOfSeconds);
            });




            t1.Start();
            t2.Start();
            //passing parameter to parameterized thread
            t3.Start();
            t4.Start();

            //wait for t1 to fimish
            t1.Join();

            //wait for t2 to finish
            t2.Join();

            //wait for t3 to finish
            t3.Join();
            t4.Join();


            Console.WriteLine("All Threads Exited in {0} secods", (DateTime.Now - startTime).TotalSeconds);

            return newBitmap;

        }

        public int getThresholdValuewithThread(Bitmap orjImage)
        {
            Bitmap newBitmap = new Bitmap(orjImage.Width, orjImage.Height); // Resmin Ortalamasını Aldıktak sonra set edilecek resim    


            Color curColor;
            var orjBitmapColorList = new List<ColorEntity>();
            for (int i = 1; i < newBitmap.Width - 1; i++)
            {
                for (int j = 1; j < newBitmap.Height - 1; j++)
                {
                    curColor = orjImage.GetPixel(i, j); //orj ilgili pixceldeki rengi alınır ve aşağıdaki sabit förmül ile set edilir. Asagıda yapılan r g b renklerine göre belli bir resim set edilir. eğer 120 den büyükse bu sayı threshold alırken ortalama renk kabul ediliyor. resmi full yapar değilse renkleri sıfırlar yani beyaz yapar.

                    var EntityColor = new ColorEntity();
                    EntityColor.Height = j;
                    EntityColor.Weight = i;
                    EntityColor.ColorByPixel = curColor;
                    orjBitmapColorList.Add(EntityColor);
                }
            }

            var ThresholdList = new List<int>();


            int ThreadsCount = Process.GetCurrentProcess().Threads.Count; // Sistemde Kaç çekirdek varsa
            ThreadsCount = 4;

            if (ThreadsCount > 0)
            {
                var ThreadList = new List<Thread>();


                var tidH = orjImage.Height / ThreadsCount;
                var tidW = orjImage.Width / ThreadsCount;

                for (var ThreadByCount = 0; ThreadByCount < ThreadsCount; ThreadByCount++) // Çekirdek sayısı kadar Thread Oluştur.
                {

                    Thread t1 = new Thread(() =>
                    {
                        int height = tidH * (ThreadByCount + 1);
                        int weight = tidW * (ThreadByCount + 1);
                        int startH = (ThreadByCount == 0) ? 1 : tidH * (ThreadByCount);
                        int startW = (ThreadByCount == 0) ? 1 : tidW * (ThreadByCount);
                        Color newColor;
                        //Thread.Sleep(1000);
                        for (int i = startW; i < weight; i++)
                        {

                            var CheckColoWeightr = orjBitmapColorList.Where(x => x.Weight == i).ToList();


                            for (int j = startH; j < height; j++)
                            {

                                var CheckColor = CheckColoWeightr.Where(x => x.Height == j && x.Weight == i).FirstOrDefault();
                                if (CheckColor != null)
                                {
                                    newColor = CheckColor.ColorByPixel;
                                    ThresholdList.Add((int)(newColor.R * 0.299 + newColor.G * 0.578 + newColor.B * 0.114)); // esik degeri 120 manuel oldu threshold degil.                   
                                }

                            }
                        }

                        Console.WriteLine($"{ThreadByCount}.Thread run //  I ran for 5 seconds");
                    });
                    t1.Name = $"{ThreadByCount}.Thread";
                    //t1.IsBackground = true;
                    ThreadList.Add(t1);
                    Console.WriteLine("Is thread {1} is alive : {0}",
                                            t1.IsAlive, ThreadByCount);
                    t1.Start();
                    Console.WriteLine("Is thread {1} is alive : {0}",
                                  t1.IsAlive, ThreadByCount);
                    //wait for t1 to fimish
                    t1.Join();


                }

                //if (ThreadList != null && ThreadList.Count > 0)
                //{
                //    foreach (var _ThreadStart in ThreadList)
                //    {
                //        _ThreadStart.Start();
                //        Console.WriteLine("Is thread 1 is alive : {0} thread Name {1} ",
                //                      _ThreadStart.IsAlive,_ThreadStart.Name);
                //        //wait for t1 to fimish
                //        _ThreadStart.Join();
                //    }
                //}
            }
            int AveragePixel = 0;
            AveragePixel = (ThresholdList != null && ThresholdList.Count() > 0) ? ThresholdList.Sum() / ThresholdList.Count : 0;

            return AveragePixel;
        }






        public int getThresholdValuewithThreadDemo(Bitmap orjImage)
        {
            Bitmap newBitmap = new Bitmap(orjImage.Width, orjImage.Height); // Resmin Ortalamasını Aldıktak sonra set edilecek resim    


            Color curColor;
            var orjBitmapColorList = new List<ColorEntity>();
            for (int i = 1; i < newBitmap.Width - 1; i++)
            {
                for (int j = 1; j < newBitmap.Height - 1; j++)
                {
                    curColor = orjImage.GetPixel(i, j); //orj ilgili pixceldeki rengi alınır ve aşağıdaki sabit förmül ile set edilir. Asagıda yapılan r g b renklerine göre belli bir resim set edilir. eğer 120 den büyükse bu sayı threshold alırken ortalama renk kabul ediliyor. resmi full yapar değilse renkleri sıfırlar yani beyaz yapar.

                    var EntityColor = new ColorEntity();
                    EntityColor.Height = j;
                    EntityColor.Weight = i;
                    EntityColor.ColorByPixel = curColor;
                    orjBitmapColorList.Add(EntityColor);
                }
            }

            var ThresholdList = new List<int>();


            int ThreadsCount = Process.GetCurrentProcess().Threads.Count; // Sistemde Kaç çekirdek varsa
            ThreadsCount = 4;

            int height = 42;
            int weight = 42;
            int startH = 42;
            int startW = 42;


            if (ThreadsCount > 0)
            {
                var ThreadList = new List<Thread>();

                var tidH = orjImage.Height / ThreadsCount;
                var tidW = orjImage.Width / ThreadsCount;

                for (var ThreadByCount = 0; ThreadByCount < ThreadsCount; ThreadByCount++) // Çekirdek sayısı kadar Thread Oluştur.
                {
                    height = tidH * (ThreadByCount + 1);
                    weight = tidW * (ThreadByCount + 1);
                    startH = (ThreadByCount == 0) ? 1 : tidH * (ThreadByCount);
                    startW = (ThreadByCount == 0) ? 1 : tidW * (ThreadByCount);
                    Thread thread = new Thread(delegate ()
                    {
                        int ThreadAvarega = ThredHoldValueCalcature(height, weight, startH, startW, orjBitmapColorList);
                        if (ThreadAvarega > 0) { ThresholdList.Add(ThreadAvarega); }
                    });             

                    thread.Name = $"{ThreadByCount}.Thread";
                    //t1.IsBackground = true;
                    ThreadList.Add(thread);
                    Console.WriteLine("Is thread {1} is alive : {0}",
                                            thread.IsAlive, ThreadByCount);
                    thread.Start();
                    Console.WriteLine("Is thread {1} is alive : {0}",
                                  thread.IsAlive, ThreadByCount);
                    //wait for t1 to fimish
                    thread.Join();
                }
            }
            int AveragePixel = 0;
            AveragePixel = (ThresholdList != null && ThresholdList.Count() > 0) ? ThresholdList.Sum() / ThresholdList.Count : 0;
            return AveragePixel;

        }



        public int ThredHoldValueCalcature(int height, int weight, int startH, int startW, List<ColorEntity> orjBitmapColorList)
        {
            var ThresholdList = new List<int>();
            Color newColor;
            for (int i = startW; i < weight; i++)
            {

                var CheckColoWeightr = orjBitmapColorList.Where(x => x.Weight == i).ToList();


                for (int j = startH; j < height; j++)
                {

                    var CheckColor = CheckColoWeightr.Where(x => x.Height == j && x.Weight == i).FirstOrDefault();
                    if (CheckColor != null)
                    {
                        newColor = CheckColor.ColorByPixel;
                        ThresholdList.Add((int)(newColor.R * 0.299 + newColor.G * 0.578 + newColor.B * 0.114)); // esik degeri 120 manuel oldu threshold degil.                   
                    }

                }
            }
            int AveragePixel = 0;
            AveragePixel = (ThresholdList != null && ThresholdList.Count() > 0) ? ThresholdList.Sum() / ThresholdList.Count : 0;

            return AveragePixel;
        }

        public Bitmap ToGrayscale(Bitmap source, Bitmap newSource)
        {
            Parallel.For(1, source.Height - 1, y =>
            {
                for (int x = 1; x < source.Width - 1; x++)
                {
                    Color current = source.GetPixel(x, y);
                    int avg = (current.R + current.B + current.G) / 3;
                    Color output = Color.FromArgb(avg, avg, avg);
                    newSource.SetPixel(x, y, output);
                }
            });

            return newSource;
        }







    }
}
