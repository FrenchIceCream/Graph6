namespace Graph6
{
    partial class Form1
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
            Canvas = new PictureBox();
            Button_Mirror = new Button();
            separator_dont_touch = new Label();
            label1 = new Label();
            label2 = new Label();
            AxesList = new ComboBox();
            Button_Scale = new Button();
            ScaleValue = new TextBox();
            AxesList_Rt = new ComboBox();
            Button_Rotate = new Button();
            Button_Turn = new Button();
            label3 = new Label();
            label4 = new Label();
            Angle = new TextBox();
            label5 = new Label();
            X1 = new TextBox();
            Y1 = new TextBox();
            Z1 = new TextBox();
            Z2 = new TextBox();
            Y2 = new TextBox();
            X2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)Canvas).BeginInit();
            SuspendLayout();
            // 
            // Canvas
            // 
            Canvas.BackColor = SystemColors.Window;
            Canvas.Location = new Point(0, -1);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(697, 452);
            Canvas.TabIndex = 0;
            Canvas.TabStop = false;
            // 
            // Button_Mirror
            // 
            Button_Mirror.Location = new Point(720, 185);
            Button_Mirror.Name = "Button_Mirror";
            Button_Mirror.Size = new Size(94, 29);
            Button_Mirror.TabIndex = 1;
            Button_Mirror.Text = "Отразить";
            Button_Mirror.UseVisualStyleBackColor = true;
            Button_Mirror.Click += Button_Mirror_Click;
            // 
            // separator_dont_touch
            // 
            separator_dont_touch.BorderStyle = BorderStyle.Fixed3D;
            separator_dont_touch.Location = new Point(695, 130);
            separator_dont_touch.Name = "separator_dont_touch";
            separator_dont_touch.Size = new Size(200, 2);
            separator_dont_touch.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(729, 138);
            label1.Name = "label1";
            label1.Size = new Size(131, 40);
            label1.TabIndex = 3;
            label1.Text = "Аффинные \r\nпреобразования:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(750, 9);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 4;
            label2.Text = "Проекции:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AxesList
            // 
            AxesList.FormattingEnabled = true;
            AxesList.Location = new Point(831, 186);
            AxesList.Name = "AxesList";
            AxesList.Size = new Size(36, 28);
            AxesList.TabIndex = 5;
            // 
            // Button_Scale
            // 
            Button_Scale.Location = new Point(720, 229);
            Button_Scale.Name = "Button_Scale";
            Button_Scale.Size = new Size(94, 29);
            Button_Scale.TabIndex = 6;
            Button_Scale.Text = "Масштаб";
            Button_Scale.UseVisualStyleBackColor = true;
            Button_Scale.Click += Button_Scale_Click;
            // 
            // ScaleValue
            // 
            ScaleValue.Location = new Point(831, 231);
            ScaleValue.Name = "ScaleValue";
            ScaleValue.Size = new Size(36, 27);
            ScaleValue.TabIndex = 7;
            // 
            // AxesList_Rt
            // 
            AxesList_Rt.FormattingEnabled = true;
            AxesList_Rt.Location = new Point(831, 276);
            AxesList_Rt.Name = "AxesList_Rt";
            AxesList_Rt.Size = new Size(36, 28);
            AxesList_Rt.TabIndex = 9;
            // 
            // Button_Rotate
            // 
            Button_Rotate.Location = new Point(720, 275);
            Button_Rotate.Name = "Button_Rotate";
            Button_Rotate.Size = new Size(94, 29);
            Button_Rotate.TabIndex = 8;
            Button_Rotate.Text = "Вращение";
            Button_Rotate.UseVisualStyleBackColor = true;
            Button_Rotate.Click += Button_Rotate_Click;
            // 
            // Button_Turn
            // 
            Button_Turn.Location = new Point(720, 336);
            Button_Turn.Name = "Button_Turn";
            Button_Turn.Size = new Size(94, 29);
            Button_Turn.TabIndex = 10;
            Button_Turn.Text = "Поворот";
            Button_Turn.UseVisualStyleBackColor = true;
            Button_Turn.Click += Button_Turn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(827, 310);
            label3.Name = "label3";
            label3.Size = new Size(44, 20);
            label3.TabIndex = 11;
            label3.Text = "Угол:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(714, 383);
            label4.Name = "label4";
            label4.Size = new Size(20, 20);
            label4.TabIndex = 12;
            label4.Text = "1:";
            // 
            // Angle
            // 
            Angle.Location = new Point(831, 336);
            Angle.Name = "Angle";
            Angle.Size = new Size(36, 27);
            Angle.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(714, 416);
            label5.Name = "label5";
            label5.Size = new Size(20, 20);
            label5.TabIndex = 14;
            label5.Text = "2:";
            // 
            // X1
            // 
            X1.Location = new Point(740, 380);
            X1.Name = "X1";
            X1.Size = new Size(32, 27);
            X1.TabIndex = 15;
            // 
            // Y1
            // 
            Y1.Location = new Point(787, 380);
            Y1.Name = "Y1";
            Y1.Size = new Size(32, 27);
            Y1.TabIndex = 16;
            // 
            // Z1
            // 
            Z1.Location = new Point(831, 380);
            Z1.Name = "Z1";
            Z1.Size = new Size(32, 27);
            Z1.TabIndex = 17;
            // 
            // Z2
            // 
            Z2.Location = new Point(831, 413);
            Z2.Name = "Z2";
            Z2.Size = new Size(32, 27);
            Z2.TabIndex = 20;
            // 
            // Y2
            // 
            Y2.Location = new Point(787, 413);
            Y2.Name = "Y2";
            Y2.Size = new Size(32, 27);
            Y2.TabIndex = 19;
            // 
            // X2
            // 
            X2.Location = new Point(740, 413);
            X2.Name = "X2";
            X2.Size = new Size(32, 27);
            X2.TabIndex = 18;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(885, 450);
            Controls.Add(Z2);
            Controls.Add(Y2);
            Controls.Add(X2);
            Controls.Add(Z1);
            Controls.Add(Y1);
            Controls.Add(X1);
            Controls.Add(label5);
            Controls.Add(Angle);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(Button_Turn);
            Controls.Add(AxesList_Rt);
            Controls.Add(Button_Rotate);
            Controls.Add(ScaleValue);
            Controls.Add(Button_Scale);
            Controls.Add(AxesList);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(separator_dont_touch);
            Controls.Add(Button_Mirror);
            Controls.Add(Canvas);
            Name = "Form1";
            Text = "Графика. Лабораторная 6";
            ((System.ComponentModel.ISupportInitialize)Canvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox Canvas;
        private Button Button_Mirror;
        private Label separator_dont_touch;
        private Label label1;
        private Label label2;
        private ComboBox AxesList;
        private Button Button_Scale;
        private TextBox ScaleValue;
        private ComboBox AxesList_Rt;
        private Button Button_Rotate;
        private Button Button_Turn;
        private Label label3;
        private Label label4;
        private TextBox Angle;
        private Label label5;
        private TextBox X1;
        private TextBox Y1;
        private TextBox Z1;
        private TextBox Z2;
        private TextBox Y2;
        private TextBox X2;
    }
}