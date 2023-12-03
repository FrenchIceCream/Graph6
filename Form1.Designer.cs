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
            label6 = new Label();
            label7 = new Label();
            button1 = new Button();
            button2 = new Button();
            LoadButton = new Button();
            SaveButton = new Button();
            calculateButton = new Button();
            formulTextBox = new TextBox();
            X0TextBox = new TextBox();
            label8 = new Label();
            X1TextBox = new TextBox();
            Y1TextBox = new TextBox();
            Y0TextBox = new TextBox();
            label9 = new Label();
            XDeltaTextBox = new TextBox();
            YDeltaTextBox = new TextBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            Button_SolidOfRevolution = new Button();
            Button_SolidOfRev_Show = new Button();
            NumOfSections = new TextBox();
            AxesList_SolidOfRev = new ComboBox();
            label13 = new Label();
            button3 = new Button();
            button4 = new Button();
            RemoveEdgesButton = new Button();
            label14 = new Label();
            ShapesBox = new ComboBox();
            Button_Translation = new Button();
            Translation_Z = new TextBox();
            Translation_Y = new TextBox();
            Translation_X = new TextBox();
            label15 = new Label();
            label16 = new Label();
            Clear_Button = new Button();
            label17 = new Label();
            label18 = new Label();
            Hide_CheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)Canvas).BeginInit();
            SuspendLayout();
            // 
            // Canvas
            // 
            Canvas.BackColor = SystemColors.Window;
            Canvas.Location = new Point(0, -1);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(697, 537);
            Canvas.TabIndex = 0;
            Canvas.TabStop = false;
            Canvas.Click += Canvas_Click;
            // 
            // Button_Mirror
            // 
            Button_Mirror.Location = new Point(714, 268);
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
            separator_dont_touch.Location = new Point(695, 101);
            separator_dont_touch.Name = "separator_dont_touch";
            separator_dont_touch.Size = new Size(360, 3);
            separator_dont_touch.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(723, 221);
            label1.Name = "label1";
            label1.Size = new Size(131, 40);
            label1.TabIndex = 3;
            label1.Text = "Аффинные \r\nпреобразования:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(746, 9);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 4;
            label2.Text = "Проекции:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AxesList
            // 
            AxesList.FormattingEnabled = true;
            AxesList.Location = new Point(825, 269);
            AxesList.Name = "AxesList";
            AxesList.Size = new Size(36, 28);
            AxesList.TabIndex = 5;
            // 
            // Button_Scale
            // 
            Button_Scale.Location = new Point(714, 312);
            Button_Scale.Name = "Button_Scale";
            Button_Scale.Size = new Size(94, 29);
            Button_Scale.TabIndex = 6;
            Button_Scale.Text = "Масштаб";
            Button_Scale.UseVisualStyleBackColor = true;
            Button_Scale.Click += Button_Scale_Click;
            // 
            // ScaleValue
            // 
            ScaleValue.Location = new Point(825, 315);
            ScaleValue.Name = "ScaleValue";
            ScaleValue.Size = new Size(36, 27);
            ScaleValue.TabIndex = 7;
            ScaleValue.TextAlign = HorizontalAlignment.Center;
            // 
            // AxesList_Rt
            // 
            AxesList_Rt.FormattingEnabled = true;
            AxesList_Rt.Location = new Point(825, 357);
            AxesList_Rt.Name = "AxesList_Rt";
            AxesList_Rt.Size = new Size(36, 28);
            AxesList_Rt.TabIndex = 9;
            // 
            // Button_Rotate
            // 
            Button_Rotate.Location = new Point(714, 357);
            Button_Rotate.Name = "Button_Rotate";
            Button_Rotate.Size = new Size(94, 29);
            Button_Rotate.TabIndex = 8;
            Button_Rotate.Text = "Вращение";
            Button_Rotate.UseVisualStyleBackColor = true;
            Button_Rotate.Click += Button_Rotate_Click;
            // 
            // Button_Turn
            // 
            Button_Turn.Location = new Point(714, 420);
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
            label3.Location = new Point(821, 392);
            label3.Name = "label3";
            label3.Size = new Size(44, 20);
            label3.TabIndex = 11;
            label3.Text = "Угол:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1177, 113);
            label4.Name = "label4";
            label4.Size = new Size(20, 20);
            label4.TabIndex = 12;
            label4.Text = "1:";
            // 
            // Angle
            // 
            Angle.Location = new Point(825, 420);
            Angle.Name = "Angle";
            Angle.Size = new Size(36, 27);
            Angle.TabIndex = 13;
            Angle.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1177, 146);
            label5.Name = "label5";
            label5.Size = new Size(20, 20);
            label5.TabIndex = 14;
            label5.Text = "2:";
            // 
            // X1
            // 
            X1.Location = new Point(736, 461);
            X1.Name = "X1";
            X1.Size = new Size(31, 27);
            X1.TabIndex = 15;
            X1.TextAlign = HorizontalAlignment.Center;
            // 
            // Y1
            // 
            Y1.Location = new Point(781, 461);
            Y1.Name = "Y1";
            Y1.Size = new Size(31, 27);
            Y1.TabIndex = 16;
            Y1.TextAlign = HorizontalAlignment.Center;
            // 
            // Z1
            // 
            Z1.Location = new Point(825, 461);
            Z1.Name = "Z1";
            Z1.Size = new Size(31, 27);
            Z1.TabIndex = 17;
            Z1.TextAlign = HorizontalAlignment.Center;
            // 
            // Z2
            // 
            Z2.Location = new Point(825, 496);
            Z2.Name = "Z2";
            Z2.Size = new Size(31, 27);
            Z2.TabIndex = 20;
            Z2.TextAlign = HorizontalAlignment.Center;
            // 
            // Y2
            // 
            Y2.Location = new Point(781, 496);
            Y2.Name = "Y2";
            Y2.Size = new Size(31, 27);
            Y2.TabIndex = 19;
            Y2.TextAlign = HorizontalAlignment.Center;
            // 
            // X2
            // 
            X2.Location = new Point(736, 496);
            X2.Name = "X2";
            X2.Size = new Size(31, 27);
            X2.TabIndex = 18;
            X2.TextAlign = HorizontalAlignment.Center;
            // 
            // PerspectiveButton
            // 
            PerspectiveButton.Location = new Point(710, 32);
            PerspectiveButton.Name = "PerspectiveButton";
            PerspectiveButton.Size = new Size(155, 29);
            PerspectiveButton.TabIndex = 21;
            PerspectiveButton.Text = "Перспектива";
            PerspectiveButton.UseVisualStyleBackColor = true;
            PerspectiveButton.Click += PerspectiveButton_Click;
            // 
            // ParallelButton
            // 
            ParallelButton.Location = new Point(710, 67);
            ParallelButton.Name = "ParallelButton";
            ParallelButton.Size = new Size(155, 29);
            ParallelButton.TabIndex = 22;
            ParallelButton.Text = "Параллельно";
            ParallelButton.UseVisualStyleBackColor = true;
            ParallelButton.Click += ParallelButton_Click;
            // 
            // CubeButton
            // 
            CubeButton.Location = new Point(715, 144);
            CubeButton.Name = "CubeButton";
            CubeButton.Size = new Size(70, 29);
            CubeButton.TabIndex = 23;
            CubeButton.Text = "Куб";
            CubeButton.UseVisualStyleBackColor = true;
            CubeButton.Click += CubeButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(714, 112);
            label6.Name = "label6";
            label6.Size = new Size(65, 20);
            label6.TabIndex = 24;
            label6.Text = "Фигуры:";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.BorderStyle = BorderStyle.Fixed3D;
            label7.Location = new Point(695, 219);
            label7.Name = "label7";
            label7.Size = new Size(360, 3);
            label7.TabIndex = 25;
            // 
            // button1
            // 
            button1.Location = new Point(715, 177);
            button1.Name = "button1";
            button1.Size = new Size(70, 29);
            button1.TabIndex = 26;
            button1.Text = "Октаэдр";
            button1.UseVisualStyleBackColor = true;
            button1.Click += OctahedronButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(794, 144);
            button2.Name = "button2";
            button2.Size = new Size(71, 29);
            button2.TabIndex = 27;
            button2.Text = "Тетраэдр";
            button2.UseVisualStyleBackColor = true;
            button2.Click += TetrahedronButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Location = new Point(888, 32);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(155, 29);
            LoadButton.TabIndex = 28;
            LoadButton.Text = "Загрузить";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(889, 67);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(155, 29);
            SaveButton.TabIndex = 29;
            SaveButton.Text = "Сохранить";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // calculateButton
            // 
            calculateButton.Location = new Point(889, 312);
            calculateButton.Name = "calculateButton";
            calculateButton.Size = new Size(155, 29);
            calculateButton.TabIndex = 30;
            calculateButton.Text = "Посчитать";
            calculateButton.UseVisualStyleBackColor = true;
            // 
            // formulTextBox
            // 
            formulTextBox.Location = new Point(889, 268);
            formulTextBox.Name = "formulTextBox";
            formulTextBox.Size = new Size(155, 27);
            formulTextBox.TabIndex = 31;
            formulTextBox.Text = "5*(cos(x*x+y*y+1)/(x*x+y*y+1)+0.1)";
            formulTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // X0TextBox
            // 
            X0TextBox.Location = new Point(915, 357);
            X0TextBox.Name = "X0TextBox";
            X0TextBox.Size = new Size(36, 27);
            X0TextBox.TabIndex = 33;
            X0TextBox.Text = "-2";
            X0TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(889, 361);
            label8.Name = "label8";
            label8.Size = new Size(21, 20);
            label8.TabIndex = 32;
            label8.Text = "X:";
            // 
            // X1TextBox
            // 
            X1TextBox.Location = new Point(958, 357);
            X1TextBox.Name = "X1TextBox";
            X1TextBox.Size = new Size(36, 27);
            X1TextBox.TabIndex = 34;
            X1TextBox.Text = "2";
            X1TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Y1TextBox
            // 
            Y1TextBox.Location = new Point(958, 404);
            Y1TextBox.Name = "Y1TextBox";
            Y1TextBox.Size = new Size(36, 27);
            Y1TextBox.TabIndex = 37;
            Y1TextBox.Text = "2";
            Y1TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Y0TextBox
            // 
            Y0TextBox.Location = new Point(915, 403);
            Y0TextBox.Name = "Y0TextBox";
            Y0TextBox.Size = new Size(36, 27);
            Y0TextBox.TabIndex = 36;
            Y0TextBox.Text = "-2";
            Y0TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(889, 405);
            label9.Name = "label9";
            label9.Size = new Size(20, 20);
            label9.TabIndex = 35;
            label9.Text = "Y:";
            // 
            // XDeltaTextBox
            // 
            XDeltaTextBox.Location = new Point(1002, 357);
            XDeltaTextBox.Name = "XDeltaTextBox";
            XDeltaTextBox.Size = new Size(36, 27);
            XDeltaTextBox.TabIndex = 38;
            XDeltaTextBox.Text = "0,3";
            XDeltaTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // YDeltaTextBox
            // 
            YDeltaTextBox.Location = new Point(1002, 405);
            YDeltaTextBox.Name = "YDeltaTextBox";
            YDeltaTextBox.Size = new Size(36, 27);
            YDeltaTextBox.TabIndex = 39;
            YDeltaTextBox.Text = "0,3";
            YDeltaTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.BorderStyle = BorderStyle.Fixed3D;
            label10.Location = new Point(877, -13);
            label10.Name = "label10";
            label10.Size = new Size(3, 549);
            label10.TabIndex = 40;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(941, 8);
            label11.Name = "label11";
            label11.Size = new Size(48, 20);
            label11.TabIndex = 41;
            label11.Text = "Файл:";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(901, 108);
            label12.Name = "label12";
            label12.Size = new Size(137, 20);
            label12.TabIndex = 42;
            label12.Text = "Фигура вращения:";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button_SolidOfRevolution
            // 
            Button_SolidOfRevolution.Location = new Point(888, 131);
            Button_SolidOfRevolution.Name = "Button_SolidOfRevolution";
            Button_SolidOfRevolution.Size = new Size(91, 29);
            Button_SolidOfRevolution.TabIndex = 43;
            Button_SolidOfRevolution.Text = "Рисовать";
            Button_SolidOfRevolution.UseVisualStyleBackColor = true;
            Button_SolidOfRevolution.Click += Button_SolidOfRevolution_Click;
            // 
            // Button_SolidOfRev_Show
            // 
            Button_SolidOfRev_Show.Location = new Point(889, 179);
            Button_SolidOfRev_Show.Name = "Button_SolidOfRev_Show";
            Button_SolidOfRev_Show.Size = new Size(90, 29);
            Button_SolidOfRev_Show.TabIndex = 44;
            Button_SolidOfRev_Show.Text = "Вывести";
            Button_SolidOfRev_Show.UseVisualStyleBackColor = true;
            Button_SolidOfRev_Show.Click += Button_SolidOfRev_Show_Click;
            // 
            // NumOfSections
            // 
            NumOfSections.Location = new Point(1002, 179);
            NumOfSections.Name = "NumOfSections";
            NumOfSections.Size = new Size(36, 27);
            NumOfSections.TabIndex = 45;
            NumOfSections.TextAlign = HorizontalAlignment.Center;
            // 
            // AxesList_SolidOfRev
            // 
            AxesList_SolidOfRev.FormattingEnabled = true;
            AxesList_SolidOfRev.Location = new Point(1002, 132);
            AxesList_SolidOfRev.Name = "AxesList_SolidOfRev";
            AxesList_SolidOfRev.Size = new Size(36, 28);
            AxesList_SolidOfRev.TabIndex = 46;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(923, 225);
            label13.Name = "label13";
            label13.Size = new Size(94, 40);
            label13.TabIndex = 47;
            label13.Text = "Построение\r\nграфика:";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            button3.Location = new Point(794, 179);
            button3.Name = "button3";
            button3.Size = new Size(71, 29);
            button3.TabIndex = 48;
            button3.Text = "Икосаэдр";
            button3.UseVisualStyleBackColor = true;
            button3.Click += IcosahedronButton_Click;
            // 
            // button4
            // 
            button4.Location = new Point(781, 107);
            button4.Name = "button4";
            button4.Size = new Size(85, 29);
            button4.TabIndex = 49;
            button4.Text = "Додекаэдр";
            button4.UseVisualStyleBackColor = true;
            button4.Click += DodecahedronButton_Click;
            // 
            // RemoveEdgesButton
            // 
            RemoveEdgesButton.Location = new Point(892, 467);
            RemoveEdgesButton.Name = "RemoveEdgesButton";
            RemoveEdgesButton.Size = new Size(149, 56);
            RemoveEdgesButton.TabIndex = 50;
            RemoveEdgesButton.Text = "Отсечение нелицевых граней";
            RemoveEdgesButton.UseVisualStyleBackColor = true;
            RemoveEdgesButton.Click += RemoveEdgesButton_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BorderStyle = BorderStyle.Fixed3D;
            label14.Location = new Point(1095, 63);
            label14.Name = "label14";
            label14.Size = new Size(23, 22);
            label14.TabIndex = 55;
            label14.Text = "X:";
            // 
            // ShapesBox
            // 
            ShapesBox.FormattingEnabled = true;
            ShapesBox.Location = new Point(1095, 168);
            ShapesBox.Margin = new Padding(3, 2, 3, 2);
            ShapesBox.Name = "ShapesBox";
            ShapesBox.Size = new Size(124, 28);
            ShapesBox.TabIndex = 50;
            // 
            // Button_Translation
            // 
            Button_Translation.Location = new Point(1095, 32);
            Button_Translation.Margin = new Padding(3, 2, 3, 2);
            Button_Translation.Name = "Button_Translation";
            Button_Translation.Size = new Size(124, 29);
            Button_Translation.TabIndex = 51;
            Button_Translation.Text = "Перенос";
            Button_Translation.UseVisualStyleBackColor = true;
            // 
            // Translation_Z
            // 
            Translation_Z.Location = new Point(1185, 85);
            Translation_Z.Margin = new Padding(3, 2, 3, 2);
            Translation_Z.Name = "Translation_Z";
            Translation_Z.Size = new Size(32, 27);
            Translation_Z.TabIndex = 54;
            Translation_Z.TextAlign = HorizontalAlignment.Center;
            // 
            // Translation_Y
            // 
            Translation_Y.Location = new Point(1138, 85);
            Translation_Y.Margin = new Padding(3, 2, 3, 2);
            Translation_Y.Name = "Translation_Y";
            Translation_Y.Size = new Size(33, 27);
            Translation_Y.TabIndex = 53;
            Translation_Y.TextAlign = HorizontalAlignment.Center;
            // 
            // Translation_X
            // 
            Translation_X.Location = new Point(1093, 85);
            Translation_X.Margin = new Padding(3, 2, 3, 2);
            Translation_X.Name = "Translation_X";
            Translation_X.Size = new Size(33, 27);
            Translation_X.TabIndex = 52;
            Translation_X.TextAlign = HorizontalAlignment.Center;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(1140, 63);
            label15.Name = "label15";
            label15.Size = new Size(20, 20);
            label15.TabIndex = 56;
            label15.Text = "Y:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(1187, 63);
            label16.Name = "label16";
            label16.Size = new Size(21, 20);
            label16.TabIndex = 57;
            label16.Text = "Z:";
            // 
            // Clear_Button
            // 
            Clear_Button.Location = new Point(1088, 364);
            Clear_Button.Margin = new Padding(3, 2, 3, 2);
            Clear_Button.Name = "Clear_Button";
            Clear_Button.Size = new Size(128, 34);
            Clear_Button.TabIndex = 58;
            Clear_Button.Text = "Очистить";
            Clear_Button.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(1093, 131);
            label17.Name = "label17";
            label17.Size = new Size(123, 20);
            label17.TabIndex = 59;
            label17.Text = "Текущая фигура:";
            // 
            // label18
            // 
            label18.BorderStyle = BorderStyle.Fixed3D;
            label18.Location = new Point(877, 447);
            label18.Name = "label18";
            label18.Size = new Size(200, 2);
            label18.TabIndex = 60;
            // 
            // Hide_CheckBox
            // 
            Hide_CheckBox.Location = new Point(1095, 287);
            Hide_CheckBox.Name = "Hide_CheckBox";
            Hide_CheckBox.Size = new Size(148, 51);
            Hide_CheckBox.TabIndex = 61;
            Hide_CheckBox.Text = "Удаление невидимых граней";
            Hide_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1255, 538);
            Controls.Add(RemoveEdgesButton);
            Controls.Add(Hide_CheckBox);
            Controls.Add(label18);
            Controls.Add(label17);
            Controls.Add(Clear_Button);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(Translation_Z);
            Controls.Add(Translation_Y);
            Controls.Add(Translation_X);
            Controls.Add(Button_Translation);
            Controls.Add(ShapesBox);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label13);
            Controls.Add(AxesList_SolidOfRev);
            Controls.Add(NumOfSections);
            Controls.Add(Button_SolidOfRev_Show);
            Controls.Add(Button_SolidOfRevolution);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(YDeltaTextBox);
            Controls.Add(XDeltaTextBox);
            Controls.Add(Y1TextBox);
            Controls.Add(Y0TextBox);
            Controls.Add(label9);
            Controls.Add(X1TextBox);
            Controls.Add(X0TextBox);
            Controls.Add(label8);
            Controls.Add(formulTextBox);
            Controls.Add(calculateButton);
            Controls.Add(SaveButton);
            Controls.Add(LoadButton);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label7);
            Controls.Add(label6);
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
            Name = "Form1";
            Text = "Графика. Лабораторная 7";
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
        private Label label6;
        private Label label7;
        private Button button1;
        private Button button2;
        private Button LoadButton;
        private Button SaveButton;
        private Button calculateButton;
        private TextBox formulTextBox;
        private TextBox X0TextBox;
        private Label label8;
        private TextBox X1TextBox;
        private TextBox Y1TextBox;
        private TextBox Y0TextBox;
        private Label label9;
        private TextBox XDeltaTextBox;
        private TextBox YDeltaTextBox;
        private Label label10;
        private Label label11;
        private Label label12;
        private Button Button_SolidOfRevolution;
        private Button Button_SolidOfRev_Show;
        private TextBox NumOfSections;
        private ComboBox AxesList_SolidOfRev;
        private Label label13;
        private Button button3;
        private Button button4;
        private Button RemoveEdgesButton;
        private Label label14;
        private ComboBox ShapesBox;
        private Button Button_Translation;
        private TextBox Translation_Z;
        private TextBox Translation_Y;
        private TextBox Translation_X;
        //private Label label14;
        private Label label15;
        private Label label16;
        private Button Clear_Button;
        private Label label17;
        private Label label18;
        private CheckBox Hide_CheckBox;
    }
}