namespace Resources
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_File = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pic_File = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Types = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pic_Res = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_File)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Res)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txt_File
            // 
            resources.ApplyResources(this.txt_File, "txt_File");
            this.txt_File.Name = "txt_File";
            this.txt_File.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pic_File
            // 
            resources.ApplyResources(this.pic_File, "pic_File");
            this.pic_File.Name = "pic_File";
            this.pic_File.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txt_Types
            // 
            resources.ApplyResources(this.txt_Types, "txt_Types");
            this.txt_Types.Name = "txt_Types";
            this.txt_Types.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // pic_Res
            // 
            resources.ApplyResources(this.pic_Res, "pic_Res");
            this.pic_Res.Name = "pic_Res";
            this.pic_Res.TabStop = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pic_Res);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Types);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pic_File);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_File);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic_File)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Res)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_File;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pic_File;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Types;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pic_Res;
    }
}

