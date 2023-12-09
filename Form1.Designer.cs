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
            Angle = new TextBox();
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
            leftButton = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button5 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            button13 = new Button();
            RemoveEdgesCheckBox = new CheckBox();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            label14 = new Label();
            label22 = new Label();
            label23 = new Label();
            label24 = new Label();
            ((System.ComponentModel.ISupportInitialize)Canvas).BeginInit();
            SuspendLayout();
            // 
            // Canvas
            // 
            Canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Canvas.BackColor = SystemColors.Window;
            Canvas.Location = new Point(0, -1);
            Canvas.Margin = new Padding(3, 2, 3, 2);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(662, 498);
            Canvas.TabIndex = 0;
            Canvas.TabStop = false;
            Canvas.Click += Canvas_Click;
            // 
            // Button_Mirror
            // 
            Button_Mirror.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_Mirror.Location = new Point(670, 212);
            Button_Mirror.Margin = new Padding(3, 2, 3, 2);
            Button_Mirror.Name = "Button_Mirror";
            Button_Mirror.Size = new Size(110, 22);
            Button_Mirror.TabIndex = 1;
            Button_Mirror.Text = "Отразить";
            Button_Mirror.UseVisualStyleBackColor = true;
            Button_Mirror.Click += Button_Mirror_Click;
            // 
            // separator_dont_touch
            // 
            separator_dont_touch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            separator_dont_touch.BorderStyle = BorderStyle.Fixed3D;
            separator_dont_touch.Location = new Point(660, 77);
            separator_dont_touch.Name = "separator_dont_touch";
            separator_dont_touch.Size = new Size(340, 2);
            separator_dont_touch.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.Location = new Point(670, 180);
            label1.Name = "label1";
            label1.Size = new Size(110, 30);
            label1.TabIndex = 3;
            label1.Text = "Аффинные \r\nпреобразования:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(670, 6);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 4;
            label2.Text = "Проекции:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AxesList
            // 
            AxesList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AxesList.FormattingEnabled = true;
            AxesList.Location = new Point(786, 213);
            AxesList.Margin = new Padding(3, 2, 3, 2);
            AxesList.Name = "AxesList";
            AxesList.Size = new Size(38, 23);
            AxesList.TabIndex = 5;
            // 
            // Button_Scale
            // 
            Button_Scale.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_Scale.Location = new Point(670, 238);
            Button_Scale.Margin = new Padding(3, 2, 3, 2);
            Button_Scale.Name = "Button_Scale";
            Button_Scale.Size = new Size(110, 22);
            Button_Scale.TabIndex = 6;
            Button_Scale.Text = "Масштаб";
            Button_Scale.UseVisualStyleBackColor = true;
            Button_Scale.Click += Button_Scale_Click;
            // 
            // ScaleValue
            // 
            ScaleValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ScaleValue.Location = new Point(786, 238);
            ScaleValue.Margin = new Padding(3, 2, 3, 2);
            ScaleValue.Name = "ScaleValue";
            ScaleValue.Size = new Size(38, 23);
            ScaleValue.TabIndex = 7;
            ScaleValue.TextAlign = HorizontalAlignment.Center;
            // 
            // AxesList_Rt
            // 
            AxesList_Rt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AxesList_Rt.FormattingEnabled = true;
            AxesList_Rt.Location = new Point(786, 264);
            AxesList_Rt.Margin = new Padding(3, 2, 3, 2);
            AxesList_Rt.Name = "AxesList_Rt";
            AxesList_Rt.Size = new Size(38, 23);
            AxesList_Rt.TabIndex = 9;
            // 
            // Button_Rotate
            // 
            Button_Rotate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_Rotate.Location = new Point(670, 264);
            Button_Rotate.Margin = new Padding(3, 2, 3, 2);
            Button_Rotate.Name = "Button_Rotate";
            Button_Rotate.Size = new Size(110, 22);
            Button_Rotate.TabIndex = 8;
            Button_Rotate.Text = "Вращение";
            Button_Rotate.UseVisualStyleBackColor = true;
            Button_Rotate.Click += Button_Rotate_Click;
            // 
            // Button_Turn
            // 
            Button_Turn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_Turn.Location = new Point(671, 290);
            Button_Turn.Margin = new Padding(3, 2, 3, 2);
            Button_Turn.Name = "Button_Turn";
            Button_Turn.Size = new Size(110, 22);
            Button_Turn.TabIndex = 10;
            Button_Turn.Text = "Поворот°";
            Button_Turn.UseVisualStyleBackColor = true;
            Button_Turn.Click += Button_Turn_Click;
            // 
            // Angle
            // 
            Angle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Angle.Location = new Point(786, 291);
            Angle.Margin = new Padding(3, 2, 3, 2);
            Angle.Name = "Angle";
            Angle.Size = new Size(37, 23);
            Angle.TabIndex = 13;
            Angle.TextAlign = HorizontalAlignment.Center;
            // 
            // X1
            // 
            X1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            X1.Location = new Point(693, 342);
            X1.Margin = new Padding(3, 2, 3, 2);
            X1.Name = "X1";
            X1.Size = new Size(39, 23);
            X1.TabIndex = 15;
            X1.TextAlign = HorizontalAlignment.Center;
            // 
            // Y1
            // 
            Y1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Y1.Location = new Point(738, 342);
            Y1.Margin = new Padding(3, 2, 3, 2);
            Y1.Name = "Y1";
            Y1.Size = new Size(39, 23);
            Y1.TabIndex = 16;
            Y1.TextAlign = HorizontalAlignment.Center;
            // 
            // Z1
            // 
            Z1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Z1.Location = new Point(784, 342);
            Z1.Margin = new Padding(3, 2, 3, 2);
            Z1.Name = "Z1";
            Z1.Size = new Size(39, 23);
            Z1.TabIndex = 17;
            Z1.TextAlign = HorizontalAlignment.Center;
            // 
            // Z2
            // 
            Z2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Z2.Location = new Point(784, 368);
            Z2.Margin = new Padding(3, 2, 3, 2);
            Z2.Name = "Z2";
            Z2.Size = new Size(39, 23);
            Z2.TabIndex = 20;
            Z2.TextAlign = HorizontalAlignment.Center;
            // 
            // Y2
            // 
            Y2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Y2.Location = new Point(738, 368);
            Y2.Margin = new Padding(3, 2, 3, 2);
            Y2.Name = "Y2";
            Y2.Size = new Size(39, 23);
            Y2.TabIndex = 19;
            Y2.TextAlign = HorizontalAlignment.Center;
            // 
            // X2
            // 
            X2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            X2.Location = new Point(693, 368);
            X2.Margin = new Padding(3, 2, 3, 2);
            X2.Name = "X2";
            X2.Size = new Size(39, 23);
            X2.TabIndex = 18;
            X2.TextAlign = HorizontalAlignment.Center;
            // 
            // PerspectiveButton
            // 
            PerspectiveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PerspectiveButton.Location = new Point(670, 24);
            PerspectiveButton.Margin = new Padding(3, 2, 3, 2);
            PerspectiveButton.Name = "PerspectiveButton";
            PerspectiveButton.Size = new Size(154, 22);
            PerspectiveButton.TabIndex = 21;
            PerspectiveButton.Text = "Перспектива";
            PerspectiveButton.UseVisualStyleBackColor = true;
            PerspectiveButton.Click += PerspectiveButton_Click;
            // 
            // ParallelButton
            // 
            ParallelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ParallelButton.Location = new Point(670, 50);
            ParallelButton.Margin = new Padding(3, 2, 3, 2);
            ParallelButton.Name = "ParallelButton";
            ParallelButton.Size = new Size(154, 22);
            ParallelButton.TabIndex = 22;
            ParallelButton.Text = "Параллельно";
            ParallelButton.UseVisualStyleBackColor = true;
            ParallelButton.Click += ParallelButton_Click;
            // 
            // CubeButton
            // 
            CubeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CubeButton.Location = new Point(670, 100);
            CubeButton.Margin = new Padding(3, 2, 3, 2);
            CubeButton.Name = "CubeButton";
            CubeButton.Size = new Size(74, 22);
            CubeButton.TabIndex = 23;
            CubeButton.Text = "Куб";
            CubeButton.UseVisualStyleBackColor = true;
            CubeButton.Click += CubeButton_Click;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(670, 82);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 24;
            label6.Text = "Фигуры:";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.BorderStyle = BorderStyle.Fixed3D;
            label7.Location = new Point(660, 177);
            label7.Name = "label7";
            label7.Size = new Size(170, 2);
            label7.TabIndex = 25;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(670, 126);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(74, 22);
            button1.TabIndex = 26;
            button1.Text = "Октаэдр";
            button1.UseVisualStyleBackColor = true;
            button1.Click += OctahedronButton_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(749, 100);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(74, 22);
            button2.TabIndex = 27;
            button2.Text = "Тетраэдр";
            button2.UseVisualStyleBackColor = true;
            button2.Click += TetrahedronButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LoadButton.Location = new Point(840, 24);
            LoadButton.Margin = new Padding(3, 2, 3, 2);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(154, 22);
            LoadButton.TabIndex = 28;
            LoadButton.Text = "Загрузить";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SaveButton.Location = new Point(840, 50);
            SaveButton.Margin = new Padding(3, 2, 3, 2);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(154, 22);
            SaveButton.TabIndex = 29;
            SaveButton.Text = "Сохранить";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // calculateButton
            // 
            calculateButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            calculateButton.Location = new Point(840, 224);
            calculateButton.Margin = new Padding(3, 2, 3, 2);
            calculateButton.Name = "calculateButton";
            calculateButton.Size = new Size(153, 22);
            calculateButton.TabIndex = 30;
            calculateButton.Text = "Посчитать";
            calculateButton.UseVisualStyleBackColor = true;
            calculateButton.Click += calculateButton_Click;
            // 
            // formulTextBox
            // 
            formulTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            formulTextBox.Location = new Point(839, 196);
            formulTextBox.Margin = new Padding(3, 2, 3, 2);
            formulTextBox.Name = "formulTextBox";
            formulTextBox.Size = new Size(153, 23);
            formulTextBox.TabIndex = 31;
            formulTextBox.Text = "5*(cos(x*x+y*y+1)/(x*x+y*y+1)+0.1)";
            formulTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // X0TextBox
            // 
            X0TextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            X0TextBox.Location = new Point(863, 250);
            X0TextBox.Margin = new Padding(3, 2, 3, 2);
            X0TextBox.Name = "X0TextBox";
            X0TextBox.Size = new Size(39, 23);
            X0TextBox.TabIndex = 33;
            X0TextBox.Text = "-2";
            X0TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(840, 250);
            label8.Name = "label8";
            label8.Size = new Size(17, 15);
            label8.TabIndex = 32;
            label8.Text = "X:";
            // 
            // X1TextBox
            // 
            X1TextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            X1TextBox.Location = new Point(908, 250);
            X1TextBox.Margin = new Padding(3, 2, 3, 2);
            X1TextBox.Name = "X1TextBox";
            X1TextBox.Size = new Size(39, 23);
            X1TextBox.TabIndex = 34;
            X1TextBox.Text = "2";
            X1TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Y1TextBox
            // 
            Y1TextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Y1TextBox.Location = new Point(908, 276);
            Y1TextBox.Margin = new Padding(3, 2, 3, 2);
            Y1TextBox.Name = "Y1TextBox";
            Y1TextBox.Size = new Size(39, 23);
            Y1TextBox.TabIndex = 37;
            Y1TextBox.Text = "2";
            Y1TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Y0TextBox
            // 
            Y0TextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Y0TextBox.Location = new Point(863, 276);
            Y0TextBox.Margin = new Padding(3, 2, 3, 2);
            Y0TextBox.Name = "Y0TextBox";
            Y0TextBox.Size = new Size(39, 23);
            Y0TextBox.TabIndex = 36;
            Y0TextBox.Text = "-2";
            Y0TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(840, 276);
            label9.Name = "label9";
            label9.Size = new Size(17, 15);
            label9.TabIndex = 35;
            label9.Text = "Y:";
            // 
            // XDeltaTextBox
            // 
            XDeltaTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            XDeltaTextBox.Location = new Point(953, 250);
            XDeltaTextBox.Margin = new Padding(3, 2, 3, 2);
            XDeltaTextBox.Name = "XDeltaTextBox";
            XDeltaTextBox.Size = new Size(39, 23);
            XDeltaTextBox.TabIndex = 38;
            XDeltaTextBox.Text = "0,3";
            XDeltaTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // YDeltaTextBox
            // 
            YDeltaTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            YDeltaTextBox.Location = new Point(953, 276);
            YDeltaTextBox.Margin = new Padding(3, 2, 3, 2);
            YDeltaTextBox.Name = "YDeltaTextBox";
            YDeltaTextBox.Size = new Size(39, 23);
            YDeltaTextBox.TabIndex = 39;
            YDeltaTextBox.Text = "0,3";
            YDeltaTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.BorderStyle = BorderStyle.Fixed3D;
            label10.Location = new Point(830, 0);
            label10.Name = "label10";
            label10.Size = new Size(3, 500);
            label10.TabIndex = 40;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Location = new Point(840, 6);
            label11.Name = "label11";
            label11.Size = new Size(39, 15);
            label11.TabIndex = 41;
            label11.Text = "Файл:";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Location = new Point(840, 82);
            label12.Name = "label12";
            label12.Size = new Size(109, 15);
            label12.TabIndex = 42;
            label12.Text = "Фигура вращения:";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button_SolidOfRevolution
            // 
            Button_SolidOfRevolution.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_SolidOfRevolution.Location = new Point(840, 100);
            Button_SolidOfRevolution.Margin = new Padding(3, 2, 3, 2);
            Button_SolidOfRevolution.Name = "Button_SolidOfRevolution";
            Button_SolidOfRevolution.Size = new Size(110, 22);
            Button_SolidOfRevolution.TabIndex = 43;
            Button_SolidOfRevolution.Text = "Рисовать";
            Button_SolidOfRevolution.UseVisualStyleBackColor = true;
            Button_SolidOfRevolution.Click += Button_SolidOfRevolution_Click;
            // 
            // Button_SolidOfRev_Show
            // 
            Button_SolidOfRev_Show.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_SolidOfRev_Show.Location = new Point(840, 126);
            Button_SolidOfRev_Show.Margin = new Padding(3, 2, 3, 2);
            Button_SolidOfRev_Show.Name = "Button_SolidOfRev_Show";
            Button_SolidOfRev_Show.Size = new Size(110, 22);
            Button_SolidOfRev_Show.TabIndex = 44;
            Button_SolidOfRev_Show.Text = "Вывести";
            Button_SolidOfRev_Show.UseVisualStyleBackColor = true;
            Button_SolidOfRev_Show.Click += Button_SolidOfRev_Show_Click;
            // 
            // NumOfSections
            // 
            NumOfSections.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            NumOfSections.Location = new Point(955, 126);
            NumOfSections.Margin = new Padding(3, 2, 3, 2);
            NumOfSections.Name = "NumOfSections";
            NumOfSections.Size = new Size(39, 23);
            NumOfSections.TabIndex = 45;
            NumOfSections.TextAlign = HorizontalAlignment.Center;
            // 
            // AxesList_SolidOfRev
            // 
            AxesList_SolidOfRev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AxesList_SolidOfRev.FormattingEnabled = true;
            AxesList_SolidOfRev.Location = new Point(955, 100);
            AxesList_SolidOfRev.Margin = new Padding(3, 2, 3, 2);
            AxesList_SolidOfRev.Name = "AxesList_SolidOfRev";
            AxesList_SolidOfRev.Size = new Size(38, 23);
            AxesList_SolidOfRev.TabIndex = 46;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.Location = new Point(840, 160);
            label13.Name = "label13";
            label13.Size = new Size(74, 30);
            label13.TabIndex = 47;
            label13.Text = "Построение\r\nграфика:";
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.Location = new Point(749, 126);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(74, 22);
            button3.TabIndex = 48;
            button3.Text = "Икосаэдр";
            button3.UseVisualStyleBackColor = true;
            button3.Click += IcosahedronButton_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.Location = new Point(670, 152);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(74, 22);
            button4.TabIndex = 49;
            button4.Text = "Додекаэдр";
            button4.UseVisualStyleBackColor = true;
            button4.Click += DodecahedronButton_Click;
            // 
            // ShapesBox
            // 
            ShapesBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ShapesBox.FormattingEnabled = true;
            ShapesBox.Location = new Point(840, 331);
            ShapesBox.Margin = new Padding(3, 2, 3, 2);
            ShapesBox.Name = "ShapesBox";
            ShapesBox.Size = new Size(153, 23);
            ShapesBox.TabIndex = 50;
            // 
            // Button_Translation
            // 
            Button_Translation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_Translation.Location = new Point(670, 398);
            Button_Translation.Margin = new Padding(3, 2, 3, 2);
            Button_Translation.Name = "Button_Translation";
            Button_Translation.Size = new Size(154, 22);
            Button_Translation.TabIndex = 51;
            Button_Translation.Text = "Перенос";
            Button_Translation.UseVisualStyleBackColor = true;
            // 
            // Translation_Z
            // 
            Translation_Z.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Translation_Z.Location = new Point(778, 439);
            Translation_Z.Margin = new Padding(3, 2, 3, 2);
            Translation_Z.Name = "Translation_Z";
            Translation_Z.Size = new Size(46, 23);
            Translation_Z.TabIndex = 54;
            Translation_Z.TextAlign = HorizontalAlignment.Center;
            // 
            // Translation_Y
            // 
            Translation_Y.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Translation_Y.Location = new Point(723, 439);
            Translation_Y.Margin = new Padding(3, 2, 3, 2);
            Translation_Y.Name = "Translation_Y";
            Translation_Y.Size = new Size(46, 23);
            Translation_Y.TabIndex = 53;
            Translation_Y.TextAlign = HorizontalAlignment.Center;
            // 
            // Translation_X
            // 
            Translation_X.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Translation_X.Location = new Point(670, 439);
            Translation_X.Margin = new Padding(3, 2, 3, 2);
            Translation_X.Name = "Translation_X";
            Translation_X.Size = new Size(46, 23);
            Translation_X.TabIndex = 52;
            Translation_X.TextAlign = HorizontalAlignment.Center;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label15.AutoSize = true;
            label15.Location = new Point(723, 422);
            label15.Name = "label15";
            label15.Size = new Size(17, 15);
            label15.TabIndex = 56;
            label15.Text = "Y:";
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label16.AutoSize = true;
            label16.Location = new Point(778, 422);
            label16.Name = "label16";
            label16.Size = new Size(17, 15);
            label16.TabIndex = 57;
            label16.Text = "Z:";
            // 
            // Clear_Button
            // 
            Clear_Button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Clear_Button.Location = new Point(1009, 200);
            Clear_Button.Margin = new Padding(3, 2, 3, 2);
            Clear_Button.Name = "Clear_Button";
            Clear_Button.Size = new Size(130, 26);
            Clear_Button.TabIndex = 58;
            Clear_Button.Text = "Очистить";
            Clear_Button.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label17.AutoSize = true;
            label17.Location = new Point(840, 312);
            label17.Name = "label17";
            label17.Size = new Size(100, 15);
            label17.TabIndex = 59;
            label17.Text = "Текущая фигура:";
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label18.BorderStyle = BorderStyle.Fixed3D;
            label18.Location = new Point(830, 305);
            label18.Name = "label18";
            label18.Size = new Size(170, 2);
            label18.TabIndex = 60;
            // 
            // Hide_CheckBox
            // 
            Hide_CheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Hide_CheckBox.Location = new Point(840, 365);
            Hide_CheckBox.Margin = new Padding(3, 2, 3, 2);
            Hide_CheckBox.Name = "Hide_CheckBox";
            Hide_CheckBox.Size = new Size(130, 38);
            Hide_CheckBox.TabIndex = 61;
            Hide_CheckBox.Text = "Удаление невидимых граней";
            Hide_CheckBox.UseVisualStyleBackColor = true;
            Hide_CheckBox.CheckedChanged += Hide_CheckBox_CheckedChanged;
            // 
            // leftButton
            // 
            leftButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            leftButton.Location = new Point(1010, 76);
            leftButton.Margin = new Padding(3, 2, 3, 2);
            leftButton.Name = "leftButton";
            leftButton.Size = new Size(39, 26);
            leftButton.TabIndex = 62;
            leftButton.Text = "L";
            leftButton.UseVisualStyleBackColor = true;
            leftButton.Click += leftButton_Click;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button6.Location = new Point(1055, 76);
            button6.Margin = new Padding(3, 2, 3, 2);
            button6.Name = "button6";
            button6.Size = new Size(39, 26);
            button6.TabIndex = 63;
            button6.Text = "D";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button7.Location = new Point(1100, 76);
            button7.Margin = new Padding(3, 2, 3, 2);
            button7.Name = "button7";
            button7.Size = new Size(39, 26);
            button7.TabIndex = 64;
            button7.Text = "R";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(1055, 49);
            button8.Margin = new Padding(3, 2, 3, 2);
            button8.Name = "button8";
            button8.Size = new Size(39, 23);
            button8.TabIndex = 65;
            button8.Text = "U";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button5.Location = new Point(1100, 49);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(39, 23);
            button5.TabIndex = 66;
            button5.Text = "F";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button9
            // 
            button9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button9.Location = new Point(1010, 49);
            button9.Margin = new Padding(3, 2, 3, 2);
            button9.Name = "button9";
            button9.Size = new Size(39, 23);
            button9.TabIndex = 67;
            button9.Text = "B";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button10
            // 
            button10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button10.Location = new Point(1055, 126);
            button10.Margin = new Padding(3, 2, 3, 2);
            button10.Name = "button10";
            button10.Size = new Size(39, 26);
            button10.TabIndex = 68;
            button10.Text = "U";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // button11
            // 
            button11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button11.Location = new Point(1055, 156);
            button11.Margin = new Padding(3, 2, 3, 2);
            button11.Name = "button11";
            button11.Size = new Size(39, 26);
            button11.TabIndex = 69;
            button11.Text = "D";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button12
            // 
            button12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button12.Location = new Point(1010, 155);
            button12.Margin = new Padding(3, 2, 3, 2);
            button12.Name = "button12";
            button12.Size = new Size(39, 26);
            button12.TabIndex = 70;
            button12.Text = "L";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // button13
            // 
            button13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button13.Location = new Point(1100, 156);
            button13.Margin = new Padding(3, 2, 3, 2);
            button13.Name = "button13";
            button13.Size = new Size(39, 26);
            button13.TabIndex = 71;
            button13.Text = "R";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            // 
            // RemoveEdgesCheckBox
            // 
            RemoveEdgesCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RemoveEdgesCheckBox.Location = new Point(840, 406);
            RemoveEdgesCheckBox.Margin = new Padding(3, 2, 3, 2);
            RemoveEdgesCheckBox.Name = "RemoveEdgesCheckBox";
            RemoveEdgesCheckBox.Size = new Size(130, 38);
            RemoveEdgesCheckBox.TabIndex = 72;
            RemoveEdgesCheckBox.Text = "Отсечение нелицевых граней";
            RemoveEdgesCheckBox.UseVisualStyleBackColor = true;
            RemoveEdgesCheckBox.CheckedChanged += RemoveEdgesCheckBox_CheckedChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(1000, 0);
            label4.Name = "label4";
            label4.Size = new Size(3, 500);
            label4.TabIndex = 73;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(670, 316);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 74;
            label3.Text = "Прямая:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.BorderStyle = BorderStyle.Fixed3D;
            label5.Location = new Point(830, 155);
            label5.Name = "label5";
            label5.Size = new Size(170, 2);
            label5.TabIndex = 75;
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label19.AutoSize = true;
            label19.Location = new Point(1010, 6);
            label19.Name = "label19";
            label19.Size = new Size(51, 15);
            label19.TabIndex = 76;
            label19.Text = "Камера:";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            label20.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label20.AutoSize = true;
            label20.Location = new Point(670, 342);
            label20.Name = "label20";
            label20.Size = new Size(18, 15);
            label20.TabIndex = 77;
            label20.Text = "A:";
            // 
            // label21
            // 
            label21.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label21.AutoSize = true;
            label21.Location = new Point(670, 368);
            label21.Name = "label21";
            label21.Size = new Size(17, 15);
            label21.TabIndex = 78;
            label21.Text = "B:";
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label14.AutoSize = true;
            label14.Location = new Point(670, 422);
            label14.Name = "label14";
            label14.Size = new Size(17, 15);
            label14.TabIndex = 79;
            label14.Text = "X:";
            // 
            // label22
            // 
            label22.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label22.AutoSize = true;
            label22.Location = new Point(1010, 107);
            label22.Name = "label22";
            label22.Size = new Size(58, 15);
            label22.TabIndex = 80;
            label22.Text = "Поворот:";
            label22.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            label23.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label23.AutoSize = true;
            label23.Location = new Point(1010, 28);
            label23.Name = "label23";
            label23.Size = new Size(66, 15);
            label23.TabIndex = 81;
            label23.Text = "Движение:";
            label23.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            label24.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label24.BorderStyle = BorderStyle.Fixed3D;
            label24.Location = new Point(1000, 196);
            label24.Name = "label24";
            label24.Size = new Size(170, 2);
            label24.TabIndex = 82;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1150, 499);
            Controls.Add(label24);
            Controls.Add(label23);
            Controls.Add(label22);
            Controls.Add(label14);
            Controls.Add(label21);
            Controls.Add(label20);
            Controls.Add(label19);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(RemoveEdgesCheckBox);
            Controls.Add(button13);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button5);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(leftButton);
            Controls.Add(Hide_CheckBox);
            Controls.Add(label18);
            Controls.Add(label17);
            Controls.Add(Clear_Button);
            Controls.Add(label16);
            Controls.Add(label15);
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
            Controls.Add(Angle);
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
            Text = "Графика. Лабораторная 8";
            ResizeEnd += Form1_ResizeEnd;
            KeyDown += Form1_KeyPress;
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
        private TextBox Angle;
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
        private Button leftButton;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button5;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
        private CheckBox RemoveEdgesCheckBox;
        private Label label4;
        private Label label3;
        private Label label5;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label14;
        private Label label22;
        private Label label23;
        private Label label24;
    }
}