namespace GOL_HappelChristopher
{
    partial class ModalDialog
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.cellColumn_UpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cellRow_UpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cellColumn_UpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellRow_UpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(237, 241);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(332, 241);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // cellColumn_UpDown1
            // 
            this.cellColumn_UpDown1.Location = new System.Drawing.Point(256, 112);
            this.cellColumn_UpDown1.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.cellColumn_UpDown1.Name = "cellColumn_UpDown1";
            this.cellColumn_UpDown1.Size = new System.Drawing.Size(120, 20);
            this.cellColumn_UpDown1.TabIndex = 2;
            // 
            // cellRow_UpDown2
            // 
            this.cellRow_UpDown2.Location = new System.Drawing.Point(256, 71);
            this.cellRow_UpDown2.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.cellRow_UpDown2.Name = "cellRow_UpDown2";
            this.cellRow_UpDown2.Size = new System.Drawing.Size(120, 20);
            this.cellRow_UpDown2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Universe X Max";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Universe Y Max";
            // 
            // ModalDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(432, 279);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cellRow_UpDown2);
            this.Controls.Add(this.cellColumn_UpDown1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ModalDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.cellColumn_UpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellRow_UpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown cellColumn_UpDown1;
        private System.Windows.Forms.NumericUpDown cellRow_UpDown2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}