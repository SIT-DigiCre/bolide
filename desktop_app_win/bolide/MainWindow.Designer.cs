namespace bolide
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.roomNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorPicker6 = new bolide.ColorPicker();
            this.colorPicker5 = new bolide.ColorPicker();
            this.colorPicker4 = new bolide.ColorPicker();
            this.colorPicker3 = new bolide.ColorPicker();
            this.colorPicker2 = new bolide.ColorPicker();
            this.colorPicker1 = new bolide.ColorPicker();
            this.allowFlowCheckBox = new System.Windows.Forms.CheckBox();
            this.startConnectionBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roomNameTextBox
            // 
            this.roomNameTextBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.roomNameTextBox.Location = new System.Drawing.Point(119, 10);
            this.roomNameTextBox.Name = "roomNameTextBox";
            this.roomNameTextBox.Size = new System.Drawing.Size(125, 27);
            this.roomNameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "ルーム名";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.colorPicker6);
            this.groupBox1.Controls.Add(this.colorPicker5);
            this.groupBox1.Controls.Add(this.colorPicker4);
            this.groupBox1.Controls.Add(this.colorPicker3);
            this.groupBox1.Controls.Add(this.colorPicker2);
            this.groupBox1.Controls.Add(this.colorPicker1);
            this.groupBox1.Location = new System.Drawing.Point(13, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 120);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "テキストカラー一覧";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "スライドと被る色は変更しましょう";
            // 
            // colorPicker6
            // 
            this.colorPicker6.Location = new System.Drawing.Point(152, 59);
            this.colorPicker6.Name = "colorPicker6";
            this.colorPicker6.selectedColor = System.Drawing.SystemColors.Control;
            this.colorPicker6.Size = new System.Drawing.Size(67, 27);
            this.colorPicker6.TabIndex = 1;
            // 
            // colorPicker5
            // 
            this.colorPicker5.Location = new System.Drawing.Point(79, 59);
            this.colorPicker5.Name = "colorPicker5";
            this.colorPicker5.selectedColor = System.Drawing.SystemColors.Control;
            this.colorPicker5.Size = new System.Drawing.Size(67, 27);
            this.colorPicker5.TabIndex = 1;
            // 
            // colorPicker4
            // 
            this.colorPicker4.Location = new System.Drawing.Point(6, 59);
            this.colorPicker4.Name = "colorPicker4";
            this.colorPicker4.selectedColor = System.Drawing.SystemColors.Control;
            this.colorPicker4.Size = new System.Drawing.Size(67, 27);
            this.colorPicker4.TabIndex = 1;
            // 
            // colorPicker3
            // 
            this.colorPicker3.Location = new System.Drawing.Point(152, 26);
            this.colorPicker3.Name = "colorPicker3";
            this.colorPicker3.selectedColor = System.Drawing.SystemColors.Control;
            this.colorPicker3.Size = new System.Drawing.Size(67, 27);
            this.colorPicker3.TabIndex = 1;
            // 
            // colorPicker2
            // 
            this.colorPicker2.Location = new System.Drawing.Point(79, 26);
            this.colorPicker2.Name = "colorPicker2";
            this.colorPicker2.selectedColor = System.Drawing.SystemColors.Control;
            this.colorPicker2.Size = new System.Drawing.Size(67, 27);
            this.colorPicker2.TabIndex = 1;
            // 
            // colorPicker1
            // 
            this.colorPicker1.Location = new System.Drawing.Point(6, 26);
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.selectedColor = System.Drawing.SystemColors.Control;
            this.colorPicker1.Size = new System.Drawing.Size(67, 27);
            this.colorPicker1.TabIndex = 0;
            // 
            // allowFlowCheckBox
            // 
            this.allowFlowCheckBox.AutoSize = true;
            this.allowFlowCheckBox.Checked = true;
            this.allowFlowCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allowFlowCheckBox.Location = new System.Drawing.Point(12, 184);
            this.allowFlowCheckBox.Name = "allowFlowCheckBox";
            this.allowFlowCheckBox.Size = new System.Drawing.Size(137, 24);
            this.allowFlowCheckBox.TabIndex = 3;
            this.allowFlowCheckBox.Text = "コメントを表示する";
            this.allowFlowCheckBox.UseVisualStyleBackColor = true;
            // 
            // startConnectionBtn
            // 
            this.startConnectionBtn.Location = new System.Drawing.Point(12, 261);
            this.startConnectionBtn.Name = "startConnectionBtn";
            this.startConnectionBtn.Size = new System.Drawing.Size(232, 41);
            this.startConnectionBtn.TabIndex = 4;
            this.startConnectionBtn.Text = "接続開始";
            this.startConnectionBtn.UseVisualStyleBackColor = true;
            this.startConnectionBtn.Click += new System.EventHandler(this.startConnectionBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 450);
            this.Controls.Add(this.startConnectionBtn);
            this.Controls.Add(this.allowFlowCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.roomNameTextBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(274, 497);
            this.MinimumSize = new System.Drawing.Size(274, 497);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Bolide";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox roomNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private ColorPicker colorPicker6;
        private ColorPicker colorPicker5;
        private ColorPicker colorPicker4;
        private ColorPicker colorPicker3;
        private ColorPicker colorPicker2;
        private ColorPicker colorPicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox allowFlowCheckBox;
        private System.Windows.Forms.Button startConnectionBtn;
    }
}

