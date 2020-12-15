using AForge.Imaging.Filters;
using ImageLayer.Collection;
using ImageLayer.DAO;
using ImageLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreasHoldWithThread
{
    public partial class Form1 : Form
    {
        BackgroundWorker workerThread = null; // Saniye Hesaplarında Kullanılır

        bool _keepRunning = false; // Ex: Saniye Durdur baslat...
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;// THREAD ÇAKIŞMASINI ENGELLER
            InstantiateWorkerThread();
        }
        private void InstantiateWorkerThread() // Vakit Kalırsa dinamik progress bar atılacak buraya
        {
            workerThread = new BackgroundWorker();
            workerThread.ProgressChanged += WorkerThread_ProgressChanged;
            workerThread.DoWork += WorkerThread_DoWork;
            workerThread.RunWorkerCompleted += WorkerThread_RunWorkerCompleted;
            workerThread.WorkerReportsProgress = true;
            workerThread.WorkerSupportsCancellation = true;
        }

        private readonly IThresholdService _ThresholdService; // Servis mantığında dll olarak channel ve pixel methodları yazılmak istendi ve serviceler ile sisteme entegre edilmesi amaclandı fakat suan icin başarısız.
        public Form1(IThresholdService ThresholdService)
        {
            this._ThresholdService = ThresholdService;
        }

        public static Bitmap Orjbitmap, Thresholdbitmap; // Yükenen OrjBitmap Return ThresholdBitmap...(T)(ThresholdBitmap) suan icin pasifte eğer vakit kalırsa resmi tek tek set pixel yapmak için
        public static List<ColorEntity> CurrentColorEntityList; // OrjBitmapin pixellerini tutar.
        public static List<ColorSizeEntity> CurrentColorSizeEntityList; // ThreadListesi MultiThreadlarda EntityById mantığı


        Image file; 
        int[] histogram = new int[256]; // Şuan için kullanılmıyor fakat vakit kalırsa ödeve histogram eşitlemesi eklenebilir.

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog(); // 
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName); //
                Orjbitmap = new Bitmap(openFileDialog1.FileName); 
                OrjBitmapImage.Image = file; // resmi bas
            }
        }


        private void BtnNotThreadOtsu_Click(object sender, EventArgs e)
        {
            lblStopWatch.Text = "Started";
            ThresholdBitmapImage.Image = null;
            StartTimer(true);

            if (Orjbitmap != null)
            {
                #region //
                //var newImage = _ThresholdService.getThresholdImage(Orjbitmap);
                //Bitmap newImage=  getThresholdImage(Orjbitmap);


                //Bitmap newImage= new ImageDAO().getThresholdImage(Orjbitmap);

                //if (newImage != null)
                //{
                //    ThresholdBitmapImage.Image = newImage; // Yeni resmi ekrana bas.
                //}

                //int ThreashHoldValue = new ImageDAO().getThresholdValue(Orjbitmap);
                //int ThreashHoldValue = new ImageDAO().getThresholdValueDemo(Orjbitmap.Height, Orjbitmap.Width, ColorOrjImageList);
                #endregion

                List<ColorEntity> ColorOrjImageList = new ImageWithMultiThreadDAO().getBitmapByAllPixel(Orjbitmap);            

                Thread SingleThread = new Thread(delegate () // Buradaki Thread tek baska threadlerdan bağımsız sistemin cekirdek sayısı yetersiz gelirse manuel sayacla yapılabilir.Amacı manuel olarak color listesini cekmek ve bunların averagını hesaplamak kaliteli resimlerde cok uzun sürüyor.Dao Mantığı ile yapıldığı için sistem saniye threadları cakisiyor ve saniyeler suan ki gibi sürekli değişen labelara sahip olamıyor.
                {
                    Thread.Sleep(1000);

                    ThredHoldValueCalcature(Orjbitmap.Height, Orjbitmap.Width, ColorOrjImageList);
                });
                SingleThread.Name = "OnlyThread";
                //t1.IsBackground = true;
                Console.WriteLine("Is thread {1} is alive : {0}", SingleThread.IsAlive, SingleThread.Name);
                SingleThread.Start();
                Console.WriteLine("Is thread {1} is alive : {0}", SingleThread.IsAlive, SingleThread.Name);
            }
            else
            {
                StopTimer(true);
                MessageBox.Show("Resim Bulunamadı. !!!");
            }

        }

        public void ThredHoldValueCalcature(int Height, int Width, List<ColorEntity> orjBitmapColorList) // Vakit Kalırsa Resmin Pixelleri sürekli set edilecek 
        {
            var ThresholdList = new List<int>();
            int AveragePixel = 0;
            Color newColor;

            for (int i = 0; i < Width; i++)
            {
                var FilterColorByWeightList = orjBitmapColorList.Where(x => x.Weight == i).ToList();

                for (int j = 0; j < Height; j++)
                {
                    var CheckColor = FilterColorByWeightList.Where(x => x.Height == j && x.Weight == i).FirstOrDefault();
                    if (CheckColor != null)
                    {
                        newColor = CheckColor.ColorByPixel;
                        ThresholdList.Add((int)(newColor.R * 0.299 + newColor.G * 0.578 + newColor.B * 0.114)); //esik değeri formul https://en.wikipedia.org/wiki/Otsu%27s_method
                    }
                }
            }
            AveragePixel = (ThresholdList != null && ThresholdList.Count() > 0) ? ThresholdList.Sum() / ThresholdList.Count : 0;

            var extractRGBchannelFilter = new ExtractChannel(); // Bu kanal resmin Thresholdni almak için kullanılıyor.vakit kalırsa DAO larda manuel olarak yapılabilir.
            extractRGBchannelFilter.Channel = AForge.Imaging.RGB.G;//RGB.R RGB.B
            var redChannel = extractRGBchannelFilter.Apply(Orjbitmap); // suan icin sadece kırmızı 
            var threshold = new Threshold(AveragePixel);
            var thresholdedImage = threshold.Apply(redChannel); // Resmi Apply etmek için kendi dll ini kullanıyor.Threadlarde cakisma oluyor araştırılacak....

            var replacedFilter = new ReplaceChannel(AForge.Imaging.RGB.G, thresholdedImage);
            var replacedImage = replacedFilter.Apply(Orjbitmap);
            ThresholdBitmapImage.Image = replacedImage;
            StopTimer(true);
        }



        private void BtnWithThreadOtsu_Click(object sender, EventArgs e)
        {
            lblStopWatch.Text = "Started";
            ThresholdBitmapImage.Image = null;
            StartTimer(true);
            if (Orjbitmap != null)
            {
                #region // 
                //Bitmap newImage = new ImageWithMultiThreadDAO().getThresholdImage(Orjbitmap);

                //if (newImage != null)
                //{
                //    ThresholdBitmapImage.Image = newImage; // Yeni resmi ekrana bas.
                //}

                //int ThreashHoldValue = new ImageWithMultiThreadDAO().getThresholdValuewithThread(Orjbitmap);
                //int ThreashHoldValue = new ImageWithMultiThreadDAO().getThresholdValuewithThreadDemo(Orjbitmap);
                #endregion
                List<ColorEntity> ColorOrjImageList = new ImageWithMultiThreadDAO().getBitmapByAllPixel(Orjbitmap); // Color Listesini Cashe bas
                if (CurrentColorEntityList == null || CurrentColorEntityList.Count <= 0) { CurrentColorEntityList = new List<ColorEntity>(); }
                if (ColorOrjImageList != null && ColorOrjImageList.Count > 0) { CurrentColorEntityList = ColorOrjImageList; }

                //int ThreadsCount = Process.GetCurrentProcess().Threads.Count; // Sistemde Kaç thread calisiyorsa 

                int ThreadsCount = 0; // Pc deki Core Sayısı
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
                {
                    ThreadsCount += int.Parse(item["NumberOfCores"].ToString());
                }
                Console.WriteLine("Number Of Cores: {0}", ThreadsCount);

                var LocalColorSizeEntityList = new List<ColorSizeEntity>();


                if (ThreadsCount > 0)
                {
                    int tidH = Orjbitmap.Height / ThreadsCount; // threadlara cekirdek sayısı kadar size bas.
                    int tidW = Orjbitmap.Width / ThreadsCount;



                    for (var ThreadById = 0; ThreadById < ThreadsCount; ThreadById++) // Bu Listenin İki kere dönmesinin sebebi şuan için thread baslatma fonksiyonu yazılmadı sistemin cekirdek sayısı kadar sizeEntity oluşturulup cashe atılmalı. Eğer threadları start ederken yapılırsa threadlarda cakisma meydana gelir.Idler karışır.
                    {
                        var EntityColorSize = new ColorSizeEntity();
                        EntityColorSize.Height = tidH * (ThreadById + 1);
                        EntityColorSize.Width = tidW * (ThreadById + 1);
                        EntityColorSize.StartHeight = (ThreadById == 0) ? 1 : tidH * (ThreadById);
                        EntityColorSize.StartWidth = (ThreadById == 0) ? 1 : tidW * (ThreadById);
                        EntityColorSize.ThreadId = ThreadById + 1;
                        EntityColorSize.ThreadsCount = ThreadsCount;
                        EntityColorSize.IsLast = ThreadsCount == (ThreadById + 1) ? true : false;
                        LocalColorSizeEntityList.Add(EntityColorSize);
                    }

                    if (CurrentColorSizeEntityList == null || CurrentColorSizeEntityList.Count <= 0) { CurrentColorSizeEntityList = new List<ColorSizeEntity>(); }
                    if (LocalColorSizeEntityList != null && LocalColorSizeEntityList.Count > 0) { CurrentColorSizeEntityList = LocalColorSizeEntityList; }

                    for (var ThreadById = 0; ThreadById < ThreadsCount; ThreadById++) // Çekirdek sayısı kadar Thread Oluştur.
                    {

                        Thread thread = new Thread(p =>
                        {
                            int RThreadResizeCount = 0;
                            //Thread.Sleep(1000);
                            //int ThreadAvarega = ThredHoldValueCalcature(RThreadResizeCount, Rheight + 10, Rweight + 10, RstartH - 10, RstartW - 10, ColorOrjImageList, RIsLast);
                            int.TryParse(p.ToString(), out RThreadResizeCount);
                            RThreadResizeCount++;
                            ThredHoldValueCalcatureWithThread(RThreadResizeCount);
                        });

                        thread.Name = $"{ThreadById}.MultiThread";
                        //t1.IsBackground = true;
                        Console.WriteLine("Is thread {1} is alive : {0} of thresmanageId {2}", thread.IsAlive, ThreadById,thread.ManagedThreadId);

                        //String arg = ThreadResizeCount.ToString() + "," + height.ToString() + "," + weight.ToString() + "," + startH.ToString() + "," + startW.ToString() + "," + IsLast.ToString();

                        String arg = ThreadById.ToString();
                        thread.Start(arg);

                        Console.WriteLine("Is thread {1} is alive : {0}", thread.IsAlive, ThreadById);
                        //wait for t1 to fimish
                        //thread.Join();
                    }
                }
            }
            else
            {
                StopTimer(true);
                MessageBox.Show("Resim Bulunamadı. !!!");

            }
        }





        public ColorSizeEntity getThreadByColorSizeEntity(int ThreadId) // Tüm thread Color Size listesi localde cash mantığı ile tutuluyor.Burada İlgili Id ye sahib size entity varsa bulup döner.
        {
            var EntityColorSize = new ColorSizeEntity();
            if (CurrentColorSizeEntityList != null && CurrentColorSizeEntityList.Count > 0)
            {
                var ReturnCheckEntity = CurrentColorSizeEntityList.Where(x => x.ThreadId == ThreadId).FirstOrDefault();
                if (ReturnCheckEntity != null)
                {
                    EntityColorSize = ReturnCheckEntity;
                }
            }
            return EntityColorSize;
        }



        #region//setThresholdAverageValue
        public void setThresHoldAverageValue() // Vakit Kalırsa şayet DAO larda  get methoları hazır olan resmin sürekli pixellerini değiştirmek için kullanılacak.
        {
            int newAveragePixel = 0;
            if (BeforeThresHoldAverageValue != null && BeforeThresHoldAverageValue.Count > 0)
            {
                newAveragePixel = BeforeThresHoldAverageValue.Sum() / BeforeThresHoldAverageValue.Count();
            }

            //BeforeThresHoldAverageValue = new List<int>();
            //var extractRGBchannelFilter = new ExtractChannel();
            //extractRGBchannelFilter.Channel = AForge.Imaging.RGB.G;//RGB.R RGB.B
            //var redChannel = extractRGBchannelFilter.Apply(Orjbitmap);
            //var threshold = new Threshold(newAveragePixel);
            //var thresholdedImage = threshold.Apply(redChannel);

            //var replacedFilter = new ReplaceChannel(AForge.Imaging.RGB.G, thresholdedImage);
            //var replacedImage = replacedFilter.Apply(Orjbitmap);
            //ThresholdBitmapImage.Image = replacedImage;

        }
        #endregion



        public static List<int> BeforeThresHoldAverageValue { get; set; } // Buradaki static değişken sadece alt taraftaki threald hesaplamalarında kullanılır.
        /// <summary>
        /// /////////İmage Size Calcature
        /// </summary>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="startH"></param>
        /// <param name="startW"></param>
        /// <param name="orjBitmapColorList"></param>
        /// <returns></returns>
        /// 
        #region //
        //public int ThredHoldValueCalcature(int ThreadByCount, int height, int weight, int startH, int startW, List<ColorEntity> orjBitmapColorList, bool IsLast)
        #endregion

        public void ThredHoldValueCalcatureWithThread(int ThreadByCount)
        {
            var CurrentColorList = CurrentColorEntityList; // Localde orjinal bitmapin color listesi mevcut 

            int ThreadsCount = 0;
            int height = 01; // Gelecekte long tutulup orjinel setpixeli bitmape basılabilir....
            int weight = 01;
            int startH = 01;
            int startW = 01;

            var SizeEntity = getThreadByColorSizeEntity(ThreadByCount); // Threadlar oluşturulurken Id verilir Color Size localdeki listede bulursa Size değerlerini  güncelle 
            if (SizeEntity != null)
            {
                height = SizeEntity.Height;
                weight = SizeEntity.Width;
                startW = SizeEntity.StartWidth;
                startH = SizeEntity.StartHeight;
                ThreadsCount = SizeEntity.ThreadsCount;
            }

            var FilterColorList = CurrentColorList.Where(x =>
              x.Height >= startH - 10 && x.Height <= height + 10
          && x.Weight >= startW - 10 && x.Weight <= weight + 10
          ).ToList(); // Tüm color listesini işleme sokamayız winform bu kadar hızlı cevap veremez

            var ThresholdList = new List<int>();
            Color newColor;
            for (int i = startW; i < weight; i++)
            {
                var FilterColorByWeightList = FilterColorList.Where(x => x.Weight == i).ToList(); // İlgili Weight eşit olanları getir.Filtrelenecek datayı azalt.

                for (int j = startH; j < height; j++)
                {
                    var CheckColor = FilterColorByWeightList.Where(x => x.Height == j && x.Weight == i).FirstOrDefault(); // Eğer bulduysa formule bastır 
                    if (CheckColor != null)
                    {
                        newColor = CheckColor.ColorByPixel;
                        ThresholdList.Add((int)(newColor.R * 0.299 + newColor.G * 0.578 + newColor.B * 0.114)); //otsu formule
                    }
                }
            }
            int AveragePixel = 0; // Localdeki average pixel eğer
            AveragePixel = (ThresholdList != null && ThresholdList.Count() > 0) ? ThresholdList.Sum() / ThresholdList.Count : 0; // İlgili liste null değilse bas
            if (BeforeThresHoldAverageValue == null || BeforeThresHoldAverageValue.Count <= 0) { BeforeThresHoldAverageValue = new List<int>(); }
            BeforeThresHoldAverageValue.Add(AveragePixel);

            int newAveragePixel = 0;

            if (BeforeThresHoldAverageValue.Count == ThreadsCount) // Pc deki cekirdek sayısına eşit işe average listesi ....            //Dynmic Change Image Threshold Eğer vakit kalırsa burası ekranda sürekli pixelleri değişen bir resim formatına getirilecek get methodları daolarda hazır
            {
                if (BeforeThresHoldAverageValue != null && BeforeThresHoldAverageValue.Count > 0)
                {
                    newAveragePixel = BeforeThresHoldAverageValue.Sum() / BeforeThresHoldAverageValue.Count();
                }

                BeforeThresHoldAverageValue = new List<int>(); // Static değişkeni cikinca kapat

                var extractRGBchannelFilter = new ExtractChannel();
                extractRGBchannelFilter.Channel = AForge.Imaging.RGB.G;//RGB.R RGB.B
                var redChannel = extractRGBchannelFilter.Apply(Orjbitmap);
                var threshold = new Threshold(newAveragePixel);
                var thresholdedImage = threshold.Apply(redChannel);

                var replacedFilter = new ReplaceChannel(AForge.Imaging.RGB.G, thresholdedImage);
                var replacedImage = replacedFilter.Apply(Orjbitmap);

                ThresholdBitmapImage.Image = replacedImage; // Resmi bas timeri kapat
                StopTimer(true);

            }
            //setThresHoldAverageValue();
        }








        /// <summary>
        /// Saniye Hesaplarında Kullanılan Threadlar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void WorkerThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStopWatch.Text = e.UserState.ToString();
        }

        private void WorkerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //lblStopWatch.Text = (e.Cancelled) ? "Canceled":"Stopped";

            if (e.Cancelled)
            {
                lblStopWatch.Text = "Cancelled";
            }          
        }

        private void WorkerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime startTime = DateTime.Now;

            _keepRunning = true;

            while (_keepRunning)
            {
                Thread.Sleep(1000);
                
                string timeElapsedInstring = (DateTime.Now - startTime).ToString(@"hh\:mm\:ss");

                workerThread.ReportProgress(0, timeElapsedInstring);

                if (workerThread.CancellationPending)
                {
                    // this is important as it set the cancelled property of RunWorkerCompletedEventArgs to true
                    e.Cancel = true;
                    break;
                }
            }
        }
        /// <summary>
        /// //////Timer Labeli thread süresini baslamat//////////
        /// </summary>
        /// <param name="start"></param>
        private void StartTimer(bool start)
        {

            if (!workerThread.IsBusy)
                workerThread.RunWorkerAsync();
            else
            {
                CancelRunProject(true);
                MessageBox.Show("Calisan Zaman Fonksiyonu bir daha cağrılamaz.Threadların işlem Süreleri Hesaplanamadı");
            }
            //workerThread.RunWorkerAsync();
        }

        private void StopTimer(bool stop)
        {
            _keepRunning = false;
        }

        private void CancelRunProject(bool cancel)
        {
            workerThread.CancelAsync();
        }







    }
}
