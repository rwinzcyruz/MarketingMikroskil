using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Data.OleDb;
using System.Data;
namespace KinectInterface.Pages
{
    public class Rank
    {
        public int Position { get; private set; }
        public string Name { get; private set; }
        public int Score { get; private set; }

        public Rank(int position, string name, int score)
        {
            Position = position;
            Name = name;
            Score = score;
        }
    }

    public partial class KtypePage : UserControl
    {
        private int live;
        private int score;
        private string hrf="";
        private int time;
        private DispatcherTimer idleTimer;

        ObservableCollection<Rank> rank = new ObservableCollection<Rank>();
        public ObservableCollection<Rank> Rank { get { return rank; } }

        //db path
        private string dbPath = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + "..\\..\\Kinect.accdb";
        private OleDbDataAdapter da;
        private DataSet ds;
        //db thing

        public KtypePage()
        {
            InitializeComponent();
            ToogleScoreBoard();
            idleTimer = new DispatcherTimer();
            idleTimer.Tick += new EventHandler(this.idleTime);
            idleTimer.Interval = new TimeSpan(0, 0, 1);
            //comment out jika tanpa kinect
            this.keyboard.setBlock(txtPenampung);
        }

        public void idleTime(object sender, EventArgs e)
        {
            time -= 1;
            _timePane.Value = time.ToString();
            if (time == 0)
            {
                GameOver();
            }
        }

        public void cekJawaban(string huruf)
        {
            if (huruf.Equals(hrf))
            {
                score += 10;
                reload();
            }
            else
            {
                live--;
                if (live == 0) GameOver();
                else reload();
            }
        }

        private void txtPenampung_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)e.Source;
            cekJawaban((String)txt.Text);
        }

        public void Start()
        {
            live = 3;
            score = 0;
            time = 60;
            idleTimer.Start();
            reload();
        }

        private void reload()
        {
            hrf = hurufAcak(1);
            _livePane.Value = live.ToString();
            _scorePane.Value = score.ToString();
            _letterPane.Value = hrf.ToString();
            _timePane.Value = time.ToString();
        }

        public void GameOver()
        {
            idleTimer.Stop();
            ToogleScoreBoard();
            //testing db score
            InsertScoreBoard(score);
            ShowScoreBoard();
            //
            var win = (MainWindow)Window.GetWindow(this);
            win._ktypePage.Visibility = Visibility.Collapsed;
            win._gamePage.Visibility = Visibility.Visible;
            win.changeState(states.Game );
        }

        public String hurufAcak(int length)
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            if (hrf.Equals(result.ToString())) return hurufAcak(1);
            else return result.ToString();
        }

        public void ToogleScoreBoard()
        {
            if (_scoreboard.Visibility == Visibility.Visible)
                _scoreboard.Visibility = Visibility.Collapsed;
            else
            {
                _scoreboard.Visibility = Visibility.Visible;
            }
        }

        public void ShowScoreBoard()
        {
            using (OleDbConnection con = new OleDbConnection(dbPath))
            {
                con.Open();

                string sql = "select top 5 * from score order by score desc";

                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {   
                    da = new OleDbDataAdapter(sql, con );
                    ds = new DataSet();
                    da.Fill(ds, "score");
                }
            }

            rank.Clear();
            string nama;
            int score;
            for (int z = 0; z < ds.Tables["score"].Rows.Count - 1; z++)
            {
                nama = ds.Tables["score"].Rows[z][0].ToString();
                score = Convert .ToInt32(ds.Tables["score"].Rows[z][1]);
                rank.Add(new Rank(z + 1, nama , score));
            }

        }

        public void InsertScoreBoard(int score)
        {
            string nama,sql;
            using (OleDbConnection con = new OleDbConnection(dbPath))
            {
                con.Open();

                sql = "select top 1 nama from KinectUser order by no desc";
                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {
                    nama = (string)cmd.ExecuteScalar();
                }

                sql = "insert into score values('" + nama + "','" + score + "')";

                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
