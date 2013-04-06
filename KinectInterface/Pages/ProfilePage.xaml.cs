using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace KinectInterface.Pages 
{
    public partial class ProfilePage : UserControl 
    {
        private int state;
        private string[] majors = {
                                      "TEKNIK INFORMATIKA (S1)",
                                      "SISTEM INFORMASI (S1)",
                                      "MANAJEMEN (S1)",
                                      "AKUNTANSI (S1)",
                                      "MANAJEMEN INFORMATIKA (D3)",
                                      "KOMPUTERISASI AKUNTANSI (D3)"
                                  };
        private string[] para = {
"Dewasa ini globalisasi informasi mengalami perkembangan pesat dan telah merambah ke berbagai aspek kehidupan manusia. Untuk itu diperlukan sumber daya manusia (SDM) yang tidak hanya menguasai aspek-aspek materi dari teknologi informasi, melainkan juga mampu menerapkan dan mengembangkan teknologi informasi di berbagai bidang.",
"Teknik informatika merupakan disiplin ilmu yang menginduk pada ilmu komputer, yang pada dasarnya merupakan kumpulan disiplin ilmu dan teknik yang secara khusus menangani masalah transformasi atau pengolahan fakta-fakta simbolik (data) dengan memanfaatkan seoptimal mungkin teknologi komputer. Transformasi itu berupa proses-proses logika dan sistematika untuk mendapatkan solusi dalam menyelesaikan berbagai masalah, sehingga dengan memilih program studi Teknik Informatika, kita menjadi terlatih berpikir secara logis dan sistematis untuk dapat dengan mudah menyesuaikan diri dengan pekerjaan apapun.",
"Seiring dengan perkembangan teknologi komputer yang sangat cepat, maka program pendidikan pada program studi Teknik Informatika diarahkan pada penguasaan ilmu dan keterampilan rekayasa informatika yang berlandaskan pada kemampuan untuk memahami, menganalisis, menilai, menerapkan, serta menciptakan piranti lunak (software) dalam pengolahan dengan komputer. Di samping itu, lulusan diharapkan memiliki kemampuan untuk merencanakan suatu jaringan dan sistem komputer, serta menguasai dasar-dasar ilmu dan tenologi informasi sebagai landasan untuk pengembangan studi lanjutan.",
"Menimbang hal di atas, program studi Teknik Informatika bertujuan memenuhi kebutuhan akan SDM yang profesional di bidang teknologi informasi. Selain itu, untuk menjembatani antara kepentingan industri dan masyarakat profesi dengan kepentingan akademik, maka disusunlah kurikulum berbasis kompetensi, dimana selain muatan-muatan inti, diberikan pula muatan-muatan lokal yang mendukung basis pengetahuan terapan dan perekayasaan perangkat lunak. Diharapkan melalui program ini dapat dihasilkan lulusan yang memiliki daya saing, jiwa kewirausahaan, dan memiliki wawasan teknologi informasi yang memadai sehingga tidak gagap ketika tiba waktunya untuk menerapkan ilmunya di masyarakat."
                                };

        public ProfilePage() {
            InitializeComponent();
            Generate();
            Reset();
        }

        private void Generate()
        {
            var bc = new BrushConverter();

            foreach (var i in majors)
            {
                var txt = new TextBlock();
                txt.Text = i;
                txt.Margin = new Thickness(20, 0, 0, 0);
                txt.Foreground = Brushes.White;
                txt.FontSize = (double)this.FindResource("NormalFontSize");

                var sp = new StackPanel();
                sp.Background = (Brush)this.FindResource("SolidBrush");
                DockPanel.SetDock(sp, Dock.Top);
                sp.Children.Add(txt);

                var fd = new FlowDocument();
                fd.Focusable = false;
                fd.FontSize = (double)this.FindResource("ContentFontSize");

                para.ToList().ForEach(x => fd.Blocks.Add(new Paragraph(new Run(x))));

                var fdsv = new FlowDocumentScrollViewer();
                //fdsv.Focusable = false;
                fdsv.Document = fd;

                var dp = new DockPanel();
                dp.Children.Add(sp);
                dp.Children.Add(fdsv);

                ProfileGrid.Children.Add(dp);
            }
        }

        public void Reset() 
		{
            state = 0;
            ProfileGrid.Children[0].Visibility = Visibility.Visible;
            for (int i = 1; i <= 5; i++) 
			{
                ProfileGrid.Children[i].Visibility = Visibility.Collapsed;
            }
        }

        #region Keyboard Navigation

        private void Arrow_PreviewKeyDown(object sender, RoutedEventArgs e)
        {
            if (((KeyEventArgs)e).Key == Key.Left)
            {
                if (state > 0)
                {
                    ProfileGrid.Children[state].Visibility = Visibility.Collapsed;
                    ProfileGrid.Children[--state].Visibility = Visibility.Visible;
                }
                else
                {
                    MainWindow win = (MainWindow)Window.GetWindow(this);
                    win.profilePage.Visibility = Visibility.Collapsed;
                    win.homePage.Visibility = Visibility.Visible;
                }
                e.Handled = true;
            }
            else if (((KeyEventArgs)e).Key == Key.Right)
            {
                if (state < 5)
                {
                    ProfileGrid.Children[state].Visibility = Visibility.Collapsed;
                    ProfileGrid.Children[++state].Visibility = Visibility.Visible;
                }
                else
                {
                    Reset();
                    MainWindow win = (MainWindow)Window.GetWindow(this);
                    win.profilePage.Visibility = Visibility.Collapsed;
                    win.homePage.Visibility = Visibility.Visible;
                }
                e.Handled = true;
            }
        }

        #endregion

        #region Kinect Navigation

        public void Left_Swipe(object sender, RoutedEventArgs e)
        {
            if (state > 0)
            {
                ProfileGrid.Children[state].Visibility = Visibility.Collapsed;
                ProfileGrid.Children[--state].Visibility = Visibility.Visible;
            }
            else
            {
                MainWindow win = (MainWindow)Window.GetWindow(this);
                win.profilePage.Visibility = Visibility.Collapsed;
                win.homePage.Visibility = Visibility.Visible;
				win.changeState(states.Home);
            }
        }

        public void Right_Swipe(object sender, RoutedEventArgs e)
        {
            if (state < 5)
            {
                ProfileGrid.Children[state].Visibility = Visibility.Collapsed;
                ProfileGrid.Children[++state].Visibility = Visibility.Visible;
            }
            else
            {
                Reset();
                MainWindow win = (MainWindow)Window.GetWindow(this);
                win.profilePage.Visibility = Visibility.Collapsed;
                win.homePage.Visibility = Visibility.Visible;
				win.changeState(states.Home);
            }
        }

        #endregion
    }
}
