using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.OleDb;
using System.Data;

namespace KinectInterface.Pages
{
    public partial class QuizPage : UserControl {
        
        int no = 0;
        int []abc=new int[6];
        private OleDbDataAdapter da;
        private DataSet ds;
        public bool done=false;
        string dbPath = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + "..\\..\\Kinect.accdb";
        public QuizPage() {
            InitializeComponent();
            
            using (OleDbConnection cons = new OleDbConnection(dbPath))
            {
                 cons.Open();
                 da = new OleDbDataAdapter("select * from soal", cons);
                 ds = new DataSet();
                 da.Fill(ds, "soal");
            }
            //CreateQuestion();
           
        }

        public void CreateQuestion(){
            string []jawaban;
            txtQuestion.Text = ds.Tables["soal"].Rows[no][0].ToString();
            for (int z = 0; z < 6; z++)
            {   
                jawaban = ds.Tables["soal"].Rows[no][z].ToString().Split('|');
                ((TextBlock)QuizGrid.Children[z]).Text = jawaban[0];
            }

        }
        public void AnswerIs(int choice)
        {
            Calculate(choice);
            if (no < ds.Tables["soal"].Rows.Count - 1)
            {
                no += 1;
                CreateQuestion();
            }
            else
            {
                QuizResult();
                QuizReset();
            }
        }

        private void Calculate(int choice)
        {
            string[] jawab = ds.Tables["soal"].Rows[no][choice].ToString().Split('|');
            string[] hasil = jawab[1].ToString().Split(',');
            for (int i = 0; i < 6; i++)
            {
                abc[i] += Convert.ToInt16(hasil[i]);
            }
        }

        private void QuizResult()
        {
            int max = abc.Max();
            string result="Hasil\n";
            if (abc[0] == max) result += "TI\n";
            if (abc[1] == max) result += "SI\n";
            if (abc[2] == max) result += "MI\n";
            if (abc[3] == max) result += "KA\n";
            if (abc[4] == max) result += "AK\n";
            if (abc[5] == max) result += "MB\n";
            
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win.quizPage.Visibility = Visibility.Collapsed;
            win.homePage.Visibility = Visibility.Visible;
            win.changeState(states.Home);
            win.homePage.txtJurusan.Text = result;
        }

        public  void QuizReset()
        {
            for (int z = 0; z < 6; z++)
            {
                abc[z] = 0;
            }
            no = 0;
        }
    }
}
