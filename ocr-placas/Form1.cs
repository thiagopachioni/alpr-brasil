using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using openalprnet;
using System.Reflection;
using System.IO;

namespace ocr_placas
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog;

        //TEST OPERATION
        FolderBrowserDialog openFolderDialog;
        string[] files;
        string[] result;
        int atual = 0;
        int totalAnalisadas = 0;
        int totalEncontradas = 0;
        int totalCorretas = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public static string AssemblyDirectory
        {
            get
            {
                return Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog = new OpenFileDialog();

            if (this.openFileDialog.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            this.processaImagem(this.openFileDialog.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.openFolderDialog = new FolderBrowserDialog();

            if (this.openFolderDialog.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;

            files = Directory.GetFiles(openFolderDialog.SelectedPath);
            result = new string[files.Length + 3];

            atual = 0;
            totalAnalisadas = files.Length;
            totalEncontradas = 0;
            totalCorretas = 0;

            timer_ocr.Enabled = true;
            timer_ocr.Start();
        }

        public Rectangle boundingRectangle(List<Point> points)
        {
            int x = points.Min<Point>((Func<Point, int>)(p => p.X)) > 0 ? points.Min<Point>((Func<Point, int>)(p => p.X)) : 0;
            int y = points.Min<Point>((Func<Point, int>)(p => p.Y)) > 0 ? points.Min<Point>((Func<Point, int>)(p => p.Y)) : 0;
            int num1 = points.Max<Point>((Func<Point, int>)(p => p.X)) > 0 ? points.Max<Point>((Func<Point, int>)(p => p.X)) : 0;
            int num2 = points.Max<Point>((Func<Point, int>)(p => p.Y)) > 0 ? points.Max<Point>((Func<Point, int>)(p => p.Y)) : 0;
            return new Rectangle(new Point(x, y), new Size(num1 - x, num2 - y));
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bitmap = new Bitmap(img);
            return (Image)bitmap.Clone(cropArea, bitmap.PixelFormat);
        }

        public static Bitmap combineImages(List<Image> images)
        {
            Bitmap bitmap = (Bitmap)null;
            try
            {
                int width = 0;
                int height = 0;
                foreach (Image image in images)
                {
                    width += image.Width;
                    height = image.Height > height ? image.Height : height;
                }
                bitmap = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage((Image)bitmap))
                {
                    graphics.Clear(Color.Black);
                    int x = 0;
                    foreach (Bitmap image in images)
                    {
                        graphics.DrawImage((Image)image, new Rectangle(x, 0, image.Width, image.Height));
                        x += image.Width;
                    }
                }
                return bitmap;
            }
            catch (Exception ex)
            {
                bitmap?.Dispose();
                throw ex;
            }
            finally
            {
                foreach (Image image in images)
                    image.Dispose();
            }
        }
        private string processaImagem(string filename)
        {
            try
            { 
                string ocr_encontrada = "@@@####";

                using (AlprNet alprNet = new AlprNet("br", Path.Combine(Form1.AssemblyDirectory, "openalpr.conf"), Path.Combine(Form1.AssemblyDirectory, "runtime_data")))
                {
                    alprNet.DefaultRegion = "br";
                    //alprNet.Configuration.MustMatchPattern = true;

                    original_pic.ImageLocation = filename;


                    if (!alprNet.IsLoaded())
                    {
                        return "OpenALPR Falhou ao Iniciar";
                    }
                    else
                    {
                        AlprResultsNet alprResultsNet = alprNet.Recognize(filename);
                        List<Image> imageList = new List<Image>(alprResultsNet.Plates.Count<AlprPlateResultNet>());

                        foreach (AlprPlateResultNet plate in alprResultsNet.Plates)
                        {
                            //entra em cada placa ENCONTRADA
                            Rectangle areaPlaca = this.boundingRectangle(plate.PlatePoints);
                            Image image = Form1.cropImage(Image.FromFile(filename), areaPlaca);
                            imageList.Add(image);

                            foreach (AlprPlateNet topNplate in plate.TopNPlates)
                            {
                                //entra em cada resultado para cada placa    
                                if (ocr_encontrada == "@@@####")
                                {
                                    ocr_encontrada = topNplate.Characters;
                                }
                                if (topNplate.MatchesTemplate)
                                {
                                    ocr_encontrada = topNplate.Characters;
                                    break;
                                }

                            }
                        }
                        if (!imageList.Any<Image>())
                        {
                            this.placa_pic.Image = this.placa_pic.ErrorImage;
                            if (this.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.ocr_label.Text = ocr_encontrada;
                                });
                            }
                            else
                            {
                                this.ocr_label.Text = ocr_encontrada;
                            }
                            return "Placa não encontrada";
                        }
                        this.placa_pic.Image = (Image)Form1.combineImages(imageList);

                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.ocr_label.Text = ocr_encontrada;
                            });
                        }
                        else
                        {
                            this.ocr_label.Text = ocr_encontrada;
                        }

                        return ocr_encontrada;
                    }
                }
            }
            catch(Exception ex)
            {
                return "Erro não Tratado";
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                timer_ocr.Stop();

                string file = files[atual];

                string[] dotsplit = file.Split('.');
                string fileExtension = dotsplit[dotsplit.Length - 1];

                string[] barSplit = dotsplit[0].Split('\\');
                string filename = barSplit[barSplit.Length - 1];

                string[] removeSpaces = filename.Split(' ');
                string CleanFilename = removeSpaces[0];

                if (fileExtension.ToLower() == "png" || fileExtension.ToLower() == "jpg" || fileExtension.ToLower() == "jpeg")
                {
                    string resultOCR = processaImagem(file);
                    if (resultOCR != "Placa não encontrada")
                    {
                        totalEncontradas++;
                        Boolean ocrCorreta = (CleanFilename.ToLower() == resultOCR.ToLower());
                        if (ocrCorreta)
                        {
                            totalCorretas++;
                        }
                        result[atual] = CleanFilename + " || " + resultOCR + " || " + (ocrCorreta ? "certo" : "errado");
                    }
                    else
                    {
                        result[atual] = CleanFilename + " || " + resultOCR;
                    }
                }
                else
                {
                    result[atual] = CleanFilename + " || " + "Não é Imagem";
                    totalAnalisadas--;
                }
                atual++;
                if (atual == files.Length)
                {
                    result[atual] = "Do total de " + files.Length + " arquivos, dos quais " + totalAnalisadas + " eram imagens, foi verificado o seguinte resultado :";
                    atual++;
                    result[atual] = totalEncontradas + " placas foram encontradas em " + totalAnalisadas + " imagens.";
                    atual++;
                    result[atual] = totalCorretas + " ocr's foram realizadas corretamente de um total de " + totalEncontradas + " placas encontradas. *Lembrando que é considerada correta se a leitura da OCR foi a mesma do nome do Arquivo.";
                    System.IO.File.WriteAllLines(Form1.AssemblyDirectory + "\\resultadoTeste.txt", result);
                    DialogResult dialogResult = MessageBox.Show("Pasta Varrida com sucesso. Salvo log no arquivo."+(Form1.AssemblyDirectory + "\\resultadoTeste.txt"), "Fim de Operação", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        timer_ocr.Start();
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void timer_ocr_Tick_1(object sender, EventArgs e)
        {
            worker_ocr.RunWorkerAsync();
        }
    }
}
