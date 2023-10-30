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
            PerspectiveButton = new Button();
            ParallelButton = new Button();
            CubeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)Canvas).BeginInit();
            SuspendLayout();
            // 
            // Canvas
            // 
            Canvas.BackColor = SystemColors.Window;
            Canvas.Location = new Point(0, -1);
            Canvas.Margin = new Padding(3, 2, 3, 2);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(610, 339);
            Canvas.TabIndex = 0;
            Canvas.TabStop = false;
            // 
            // Button_Mirror
            // 
            Button_Mirror.Location = new Point(630, 139);
            Button_Mirror.Margin = new Padding(3, 2, 3, 2);
            Button_Mirror.Name = "Button_Mirror";
            Button_Mirror.Size = new Size(82, 22);
            Button_Mirror.TabIndex = 1;
            Button_Mirror.Text = "Отразить";
            Button_Mirror.UseVisualStyleBackColor = true;
            Button_Mirror.Click += Button_Mirror_Click;
            // 
            // separator_dont_touch
            // 
            separator_dont_touch.BorderStyle = BorderStyle.Fixed3D;
            separator_dont_touch.Location = new Point(608, 98);
            separator_dont_touch.Name = "separator_dont_touch";
            separator_dont_touch.Size = new Size(175, 2);
            separator_dont_touch.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(638, 104);
            label1.Name = "label1";
            label1.Size = new Size(101, 30);
            label1.TabIndex = 3;
            label1.Text = "Аффинные \r\nпреобразования:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(656, 7);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 4;
            label2.Text = "Проекции:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AxesList
            // 
            AxesList.FormattingEnabled = true;
            AxesList.Location = new Point(727, 140);
            AxesList.Margin = new Padding(3, 2, 3, 2);
            AxesList.Name = "AxesList";
            AxesList.Size = new Size(32, 23);
            AxesList.TabIndex = 5;
            // 
            // Button_Scale
            // 
            Button_Scale.Location = new Point(630, 172);
            Button_Scale.Margin = new Padding(3, 2, 3, 2);
            Button_Scale.Name = "Button_Scale";
            Button_Scale.Size = new Size(82, 22);
            Button_Scale.TabIndex = 6;
            Button_Scale.Text = "Масштаб";
            Button_Scale.UseVisualStyleBackColor = true;
            Button_Scale.Click += Button_Scale_Click;
            // 
            // ScaleValue
            // 
            ScaleValue.Location = new Point(727, 173);
            ScaleValue.Margin = new Padding(3, 2, 3, 2);
            ScaleValue.Name = "ScaleValue";
            ScaleValue.Size = new Size(32, 23);
            ScaleValue.TabIndex = 7;
            // 
            // AxesList_Rt
            // 
            AxesList_Rt.FormattingEnabled = true;
            AxesList_Rt.Location = new Point(727, 207);
            AxesList_Rt.Margin = new Padding(3, 2, 3, 2);
            AxesList_Rt.Name = "AxesList_Rt";
            AxesList_Rt.Size = new Size(32, 23);
            AxesList_Rt.TabIndex = 9;
            // 
            // Button_Rotate
            // 
            Button_Rotate.Location = new Point(630, 206);
            Button_Rotate.Margin = new Padding(3, 2, 3, 2);
            Button_Rotate.Name = "Button_Rotate";
            Button_Rotate.Size = new Size(82, 22);
            Button_Rotate.TabIndex = 8;
            Button_Rotate.Text = "Вращение";
            Button_Rotate.UseVisualStyleBackColor = true;
            Button_Rotate.Click += Button_Rotate_Click;
            // 
            // Button_Turn
            // 
            Button_Turn.Location = new Point(630, 252);
            Button_Turn.Margin = new Padding(3, 2, 3, 2);
            Button_Turn.Name = "Button_Turn";
            Button_Turn.Size = new Size(82, 22);
            Button_Turn.TabIndex = 10;
            Button_Turn.Text = "Поворот";
            Button_Turn.UseVisualStyleBackColor = true;
            Button_Turn.Click += Button_Turn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(724, 232);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 11;
            label3.Text = "Угол:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(625, 287);
            label4.Name = "label4";
            label4.Size = new Size(16, 15);
            label4.TabIndex = 12;
            label4.Text = "1:";
            // 
            // Angle
            // 
            Angle.Location = new Point(727, 252);
            Angle.Margin = new Padding(3, 2, 3, 2);
            Angle.Name = "Angle";
            Angle.Size = new Size(32, 23);
            Angle.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(625, 312);
            label5.Name = "label5";
            label5.Size = new Size(16, 15);
            label5.TabIndex = 14;
            label5.Text = "2:";
            // 
            // X1
            // 
            X1.Location = new Point(648, 285);
            X1.Margin = new Padding(3, 2, 3, 2);
            X1.Name = "X1";
            X1.Size = new Size(28, 23);
            X1.TabIndex = 15;
            // 
            // Y1
            // 
            Y1.Location = new Point(689, 285);
            Y1.Margin = new Padding(3, 2, 3, 2);
            Y1.Name = "Y1";
            Y1.Size = new Size(28, 23);
            Y1.TabIndex = 16;
            // 
            // Z1
            // 
            Z1.Location = new Point(727, 285);
            Z1.Margin = new Padding(3, 2, 3, 2);
            Z1.Name = "Z1";
            Z1.Size = new Size(28, 23);
            Z1.TabIndex = 17;
            // 
            // Z2
            // 
            Z2.Location = new Point(727, 310);
            Z2.Margin = new Padding(3, 2, 3, 2);
            Z2.Name = "Z2";
            Z2.Size = new Size(28, 23);
            Z2.TabIndex = 20;
            // 
            // Y2
            // 
            Y2.Location = new Point(689, 310);
            Y2.Margin = new Padding(3, 2, 3, 2);
            Y2.Name = "Y2";
            Y2.Size = new Size(28, 23);
            Y2.TabIndex = 19;
            // 
            // X2
            // 
            X2.Location = new Point(648, 310);
            X2.Margin = new Padding(3, 2, 3, 2);
            X2.Name = "X2";
            X2.Size = new Size(28, 23);
            X2.TabIndex = 18;
            // 
            // PerspectiveButton
            // 
            PerspectiveButton.Location = new Point(625, 24);
            PerspectiveButton.Margin = new Padding(3, 2, 3, 2);
            PerspectiveButton.Name = "PerspectiveButton";
            PerspectiveButton.Size = new Size(63, 22);
            PerspectiveButton.TabIndex = 21;
            PerspectiveButton.Text = "Перспектива";
            PerspectiveButton.UseVisualStyleBackColor = true;
            PerspectiveButton.Click += PerspectiveButton_Click;
            // 
            // ParallelButton
            // 
            ParallelButton.Location = new Point(699, 24);
            ParallelButton.Margin = new Padding(3, 2, 3, 2);
            ParallelButton.Name = "ParallelButton";
            ParallelButton.Size = new Size(63, 22);
            ParallelButton.TabIndex = 22;
            ParallelButton.Text = "Параллельно";
            ParallelButton.UseVisualStyleBackColor = true;
            ParallelButton.Click += ParallelButton_Click;
            // 
            // CubeButton
            // 
            CubeButton.Location = new Point(625, 50);
            CubeButton.Margin = new Padding(3, 2, 3, 2);
            CubeButton.Name = "CubeButton";
            CubeButton.Size = new Size(63, 22);
            CubeButton.TabIndex = 23;
            CubeButton.Text = "Куб";
            CubeButton.UseVisualStyleBackColor = true;
            CubeButton.Click += CubeButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 338);
            Controls.Add(CubeButton);
            Controls.Add(ParallelButton);
            Controls.Add(PerspectiveButton);
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
            Margin = new Padding(3, 2, 3, 2);
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
        private Button PerspectiveButton;
        private Button ParallelButton;
        private Button CubeButton;
    }
}