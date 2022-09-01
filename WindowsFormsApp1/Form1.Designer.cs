namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRANDOM = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSETRED = new System.Windows.Forms.Button();
            this.buttonSETBLUE = new System.Windows.Forms.Button();
            this.buttonSETGREEN = new System.Windows.Forms.Button();
            this.buttonSETFUCH = new System.Windows.Forms.Button();
            this.buttonSETMPURPLE = new System.Windows.Forms.Button();
            this.buttonSETYELLOW = new System.Windows.Forms.Button();
            this.buttonSETINDIGO = new System.Windows.Forms.Button();
            this.buttonSETGRAY = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSAVELOAD = new System.Windows.Forms.Button();
            this.buttonAUTO = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRANDOM
            // 
            this.buttonRANDOM.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRANDOM.Location = new System.Drawing.Point(14, 166);
            this.buttonRANDOM.Name = "buttonRANDOM";
            this.buttonRANDOM.Size = new System.Drawing.Size(63, 47);
            this.buttonRANDOM.TabIndex = 0;
            this.buttonRANDOM.Text = "Random";
            this.buttonRANDOM.UseVisualStyleBackColor = false;
            this.buttonRANDOM.Click += new System.EventHandler(this.buttonRANDOM_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(451, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 250);
            this.panel1.TabIndex = 2;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Set Doroppu";
            // 
            // buttonSETRED
            // 
            this.buttonSETRED.BackColor = System.Drawing.Color.Red;
            this.buttonSETRED.Location = new System.Drawing.Point(14, 24);
            this.buttonSETRED.Name = "buttonSETRED";
            this.buttonSETRED.Size = new System.Drawing.Size(30, 30);
            this.buttonSETRED.TabIndex = 4;
            this.buttonSETRED.UseVisualStyleBackColor = false;
            this.buttonSETRED.Click += new System.EventHandler(this.buttonSETRED_Click);
            // 
            // buttonSETBLUE
            // 
            this.buttonSETBLUE.BackColor = System.Drawing.Color.Blue;
            this.buttonSETBLUE.Location = new System.Drawing.Point(50, 24);
            this.buttonSETBLUE.Name = "buttonSETBLUE";
            this.buttonSETBLUE.Size = new System.Drawing.Size(30, 30);
            this.buttonSETBLUE.TabIndex = 5;
            this.buttonSETBLUE.UseVisualStyleBackColor = false;
            this.buttonSETBLUE.Click += new System.EventHandler(this.buttonSETBLUE_Click);
            // 
            // buttonSETGREEN
            // 
            this.buttonSETGREEN.BackColor = System.Drawing.Color.Green;
            this.buttonSETGREEN.Location = new System.Drawing.Point(86, 24);
            this.buttonSETGREEN.Name = "buttonSETGREEN";
            this.buttonSETGREEN.Size = new System.Drawing.Size(30, 30);
            this.buttonSETGREEN.TabIndex = 6;
            this.buttonSETGREEN.UseVisualStyleBackColor = false;
            this.buttonSETGREEN.Click += new System.EventHandler(this.buttonSETGREEN_Click);
            // 
            // buttonSETFUCH
            // 
            this.buttonSETFUCH.BackColor = System.Drawing.Color.Fuchsia;
            this.buttonSETFUCH.Location = new System.Drawing.Point(193, 24);
            this.buttonSETFUCH.Name = "buttonSETFUCH";
            this.buttonSETFUCH.Size = new System.Drawing.Size(30, 30);
            this.buttonSETFUCH.TabIndex = 9;
            this.buttonSETFUCH.UseVisualStyleBackColor = false;
            this.buttonSETFUCH.Click += new System.EventHandler(this.buttonSETFUCH_Click);
            // 
            // buttonSETMPURPLE
            // 
            this.buttonSETMPURPLE.BackColor = System.Drawing.Color.MediumPurple;
            this.buttonSETMPURPLE.Location = new System.Drawing.Point(157, 24);
            this.buttonSETMPURPLE.Name = "buttonSETMPURPLE";
            this.buttonSETMPURPLE.Size = new System.Drawing.Size(30, 30);
            this.buttonSETMPURPLE.TabIndex = 8;
            this.buttonSETMPURPLE.UseVisualStyleBackColor = false;
            this.buttonSETMPURPLE.Click += new System.EventHandler(this.buttonSETMPURPLE_Click);
            // 
            // buttonSETYELLOW
            // 
            this.buttonSETYELLOW.BackColor = System.Drawing.Color.Yellow;
            this.buttonSETYELLOW.Location = new System.Drawing.Point(121, 24);
            this.buttonSETYELLOW.Name = "buttonSETYELLOW";
            this.buttonSETYELLOW.Size = new System.Drawing.Size(30, 30);
            this.buttonSETYELLOW.TabIndex = 7;
            this.buttonSETYELLOW.UseVisualStyleBackColor = false;
            this.buttonSETYELLOW.Click += new System.EventHandler(this.buttonSETYELLOW_Click);
            // 
            // buttonSETINDIGO
            // 
            this.buttonSETINDIGO.BackColor = System.Drawing.Color.Indigo;
            this.buttonSETINDIGO.Location = new System.Drawing.Point(229, 24);
            this.buttonSETINDIGO.Name = "buttonSETINDIGO";
            this.buttonSETINDIGO.Size = new System.Drawing.Size(30, 30);
            this.buttonSETINDIGO.TabIndex = 10;
            this.buttonSETINDIGO.UseVisualStyleBackColor = false;
            this.buttonSETINDIGO.Click += new System.EventHandler(this.buttonSETINDIGO_Click);
            // 
            // buttonSETGRAY
            // 
            this.buttonSETGRAY.BackColor = System.Drawing.Color.DimGray;
            this.buttonSETGRAY.Location = new System.Drawing.Point(265, 24);
            this.buttonSETGRAY.Name = "buttonSETGRAY";
            this.buttonSETGRAY.Size = new System.Drawing.Size(30, 30);
            this.buttonSETGRAY.TabIndex = 11;
            this.buttonSETGRAY.UseVisualStyleBackColor = false;
            this.buttonSETGRAY.Click += new System.EventHandler(this.buttonSETGRAY_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 78);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Droppu types";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "Combos:";
            // 
            // buttonSAVELOAD
            // 
            this.buttonSAVELOAD.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSAVELOAD.Location = new System.Drawing.Point(301, 19);
            this.buttonSAVELOAD.Name = "buttonSAVELOAD";
            this.buttonSAVELOAD.Size = new System.Drawing.Size(81, 40);
            this.buttonSAVELOAD.TabIndex = 15;
            this.buttonSAVELOAD.Text = "SAVE/LOAD";
            this.buttonSAVELOAD.UseVisualStyleBackColor = false;
            this.buttonSAVELOAD.Click += new System.EventHandler(this.buttonSAVELOAD_Click);
            // 
            // buttonAUTO
            // 
            this.buttonAUTO.Location = new System.Drawing.Point(105, 166);
            this.buttonAUTO.Name = "buttonAUTO";
            this.buttonAUTO.Size = new System.Drawing.Size(63, 47);
            this.buttonAUTO.TabIndex = 16;
            this.buttonAUTO.Text = "AUTO";
            this.buttonAUTO.UseVisualStyleBackColor = true;
            this.buttonAUTO.Click += new System.EventHandler(this.buttonAUTO_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 317);
            this.Controls.Add(this.buttonAUTO);
            this.Controls.Add(this.buttonSAVELOAD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonSETGRAY);
            this.Controls.Add(this.buttonSETINDIGO);
            this.Controls.Add(this.buttonSETFUCH);
            this.Controls.Add(this.buttonSETMPURPLE);
            this.Controls.Add(this.buttonSETYELLOW);
            this.Controls.Add(this.buttonSETGREEN);
            this.Controls.Add(this.buttonSETBLUE);
            this.Controls.Add(this.buttonSETRED);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonRANDOM);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRANDOM;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSETRED;
        private System.Windows.Forms.Button buttonSETBLUE;
        private System.Windows.Forms.Button buttonSETGREEN;
        private System.Windows.Forms.Button buttonSETFUCH;
        private System.Windows.Forms.Button buttonSETMPURPLE;
        private System.Windows.Forms.Button buttonSETYELLOW;
        private System.Windows.Forms.Button buttonSETINDIGO;
        private System.Windows.Forms.Button buttonSETGRAY;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSAVELOAD;
        private System.Windows.Forms.Button buttonAUTO;
    }
}

