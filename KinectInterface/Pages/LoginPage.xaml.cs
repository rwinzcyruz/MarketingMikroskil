using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.OleDb;

namespace KinectInterface.Pages 
{
    public partial class LoginPage : UserControl 
    {
		//oledb
        private string dbPath = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.Windows.Forms.Application.StartupPath + "\\Kinect.accdb";
        //odbc
        private string otherdbPath = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + System.Windows.Forms.Application.StartupPath + "\\Kinect.accdb";
        
		public LoginPage() {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtNama.Text.Length > 0 && txtSekolah.Text.Length > 0)
            {
                using (OleDbConnection con = new OleDbConnection(dbPath))
                {
                    con.Open();

                    string sql = "insert into KinectUser(nama,sekolah,tanggal) values('" + txtNama.Text + "','" + txtSekolah.Text + "','" + DateTime.Now + "')";

                    using (OleDbCommand cmd = new OleDbCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                        txtNama.Text = "";
                        txtSekolah.Text = "";
                        toGameQuiz();
                    }
                }
            }
        }

        public void toGameQuiz()
        {
            var win = (MainWindow)Window.GetWindow(this);
            win.loginPage.Visibility = Visibility.Collapsed;
            win.quizPage.Visibility = Visibility.Visible;
            win.changeState(states.GameQuiz);
            win.quizPage.CreateQuestion();
            win.quizPage.QuizReset();
        }
    }
}
