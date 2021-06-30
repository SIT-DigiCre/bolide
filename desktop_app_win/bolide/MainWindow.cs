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
            connection = new Connection(true,roomNameTextBox.Text,"https://bolide.digicre.net/","wss://bolide.digicre.net/");
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
            Task.Run(() =>
            {
                using (var form = new Form())
                {
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.StartPosition = FormStartPosition.Manual;
                    form.Width = Screen.PrimaryScreen.Bounds.Width;
                    var graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
                    graphicsPath.AddString(commentEventArgs.comment, new FontFamily("メイリオ"), 0, 80, new Point(0, 0), StringFormat.GenericDefault);
                    form.Region = new Region(graphicsPath);
                    form.Left = Screen.PrimaryScreen.Bounds.Width;
                    form.Top = random.Next(1, 10)*20;
                    form.BackColor = colorPickers[random.Next(0, colorPickers.Count)].selectedColor;
                    form.TopMost = true;
                    form.TopLevel = true;
                    form.Load += (sender, e) =>
                    {
                        form.BringToFront();
                        Animator.Animate(6000, (frame, frequency) =>
                        {
                            if (!form.Visible || form.IsDisposed) return false;
                            form.Left = Screen.PrimaryScreen.Bounds.Width - Screen.PrimaryScreen.Bounds.Width * frame / frequency;
                            if (frame == frequency) form.Close();
                            return true;
                        });
                    };
                    form.ShowDialog();
                }
            });
            
        }

        
        List<ColorPicker> colorPickers;
        Random random;
        private void MainWindow_Load(object sender, EventArgs e)
        {
            colorPickers = 
                new() { colorPicker1, colorPicker2, colorPicker3,
                    colorPicker4, colorPicker5, colorPicker6 };
            List<Color> colors = 
                new() { Color.Red,Color.Orange,Color.SkyBlue,Color.SpringGreen,
                    Color.MediumPurple,Color.MediumBlue};
            for (int i = 0; i < colorPickers.Count; i++) colorPickers[i].selectedColor = colors[i];
            random = new();

        }
    }
}
