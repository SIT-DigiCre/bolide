using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bolide
{
    public partial class MainWindow : Form
    {
        Connection connection;
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void startConnectionBtn_Click(object sender, EventArgs e)
        {
            if(roomNameTextBox.Text == "")
            {
                roomNameTextBox.BackColor = Color.Orange;
                return;
            }
            if (connection != null) return;
            connection = new Connection(Program.testMode,roomNameTextBox.Text,"https://bolide.digicre.net/","wss://bolide.digicre.net/");
            connection.ConnectionErrorHandler += (sender, e) =>
            {
                switch (e.erorrKind)
                {
                    case Connection.ErorrKind.WrongRoomID:
                        MessageBox.Show("部屋名が間違っています","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    case Connection.ErorrKind.WebsocketClosed:
                        startConnectionBtn.Text = "接続終了済";
                        startConnectionBtn.BackColor = Color.Blue;
                        return;
                    case Connection.ErorrKind.WebsocketError:
                        MessageBox.Show("WebSocket接続エラー", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                MessageBox.Show("不明なエラー", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            connection.ConnectionStartHandler += (sender, e) =>
            {
                startConnectionBtn.Text = "接続済";
                startConnectionBtn.BackColor = Color.Green;
            };
            connection.CommentEventHandler += RunComment;
            connection.StartConnection();
        }
        private void RunComment(object sender, Connection.CommentEventArgs commentEventArgs)
        {
            if (!allowFlowCheckBox.Checked) return;
            AnimateText(commentEventArgs);
        }
        private void AnimateText(Connection.CommentEventArgs commentEventArgs)
        {
            _ = Task.Run(() =>
            {
                using (var form = new CommentWindow())
                {
                    form.Left = Screen.PrimaryScreen.Bounds.Width;
                    form.Top = yPosRandom.GetNotNearlyRandomValue() * 40;
                    //form.BackColor = colorPickers[colorRandom.GetRandomValue()].selectedColor;
                    form.commentLabel.Text = commentEventArgs.comment;
                    form.commentLabel.ForeColor = colorPickers[colorRandom.GetRandomValue()].selectedColor;
                    form.Size = new(form.commentLabel.Size.Width, 130);
                    form.TransparencyKey = form.BackColor;
                    double animationTimeScale = 1.0 + form.commentLabel.Width / Screen.PrimaryScreen.Bounds.Width;

                    form.Load += (sender, e) =>
                    {
                        //form.BringToFront();

                        Animator.Animate((int)(6000 * animationTimeScale), (frame, frequency) =>
                        {
                            if (!form.Visible || form.IsDisposed) return false;
                            form.Left = (int)(Screen.PrimaryScreen.Bounds.Width - Screen.PrimaryScreen.Bounds.Width * (1.0 + form.commentLabel.Width / Screen.PrimaryScreen.Bounds.Width) * frame / frequency);
                            if (frame == frequency) form.Close();
                            return true;
                        });
                    };
                    form.Shown += (sender, e) =>
                    {
                        form.TopMost = true;
                        form.TopLevel = true;
                    };
                    form.ShowDialog();
                }
            });
            
        }

        
        List<ColorPicker> colorPickers;
        Random colorRandom, yPosRandom;
        private void MainWindow_Load(object sender, EventArgs e)
        {
            colorPickers = 
                new() { colorPicker1, colorPicker2, colorPicker3,
                    colorPicker4, colorPicker5, colorPicker6 };
            List<Color> colors = 
                new() { Color.Red,Color.Orange,Color.SkyBlue,Color.SpringGreen,
                    Color.MediumPurple,Color.MediumBlue};
            for (int i = 0; i < colorPickers.Count; i++) colorPickers[i].selectedColor = colors[i];
            colorRandom = new(1000,0,colorPickers.Count);
            yPosRandom = new(1000, 1, 7);

        }
    }
    public class Random
    {
        private System.Random random;
        private int start,end;
        private int lastValue = 0;
        
        public Random(int seed,int start,int end)
        {
            random = new System.Random(seed);
            this.start = start;
            this.end = end;
        }
        public int GetRandomValue()
        {
            int value = random.Next(start, end);
            while(value==lastValue) value = random.Next(start, end);
            lastValue = value;
            return value;
        }
        public int GetNotNearlyRandomValue()
        {
            int value = random.Next(start, end);
            while (MathF.Abs(value - lastValue) < 2) value = random.Next(start, end);
            lastValue = value;
            return value;
        }
    }
}
