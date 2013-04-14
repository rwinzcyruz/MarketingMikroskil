using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
        private string hrf;
        private int time;
        private DispatcherTimer idleTimer;

        ObservableCollection<Rank> rank = new ObservableCollection<Rank>();
        public ObservableCollection<Rank> Rank { get { return rank; } }

        public KtypePage()
        {
            //ini data akan masuk ke list view, @piko: buat biar bisa generate dari database dengan looping
            rank.Add(new Rank(1, "Erwin", 1000));
            rank.Add(new Rank(2, "David", 700));

            InitializeComponent();
            ToogleScoreBoard();
            idleTimer = new DispatcherTimer();
            idleTimer.Tick += new EventHandler(this.idleTime);
            idleTimer.Interval = new TimeSpan(0, 0, 1);
            //comment out jika tanpa kinect
            //this.keyboard.setBlock(txtPenampung);
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
            
            //var win = (MainWindow)Window.GetWindow(this);
            //win.ktypePage.Visibility = Visibility.Collapsed;
            //win._homePage.Visibility = Visibility.Visible;
            //win.changeState(states.Home );
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
            return result.ToString();
        }

        public void ToogleScoreBoard()
        {
            if (_scoreboard.Visibility == Visibility.Visible)
                _scoreboard.Visibility = Visibility.Collapsed;
            else
                _scoreboard.Visibility = Visibility.Visible;
        }
    }
}
