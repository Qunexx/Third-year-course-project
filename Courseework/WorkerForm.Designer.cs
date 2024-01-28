namespace Courseework
{
    partial class WorkerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.LoadExpencesButton = new System.Windows.Forms.Button();
            this.LoadExpencesTypesButton = new System.Windows.Forms.Button();
            this.LoadDepsLimitsButton = new System.Windows.Forms.Button();
            this.LoadDepsButton = new System.Windows.Forms.Button();
            this.LoadEmployeeButton = new System.Windows.Forms.Button();
            this.TaskButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(128, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(557, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Вы зашли под учётной записью сотруника бухгалтерии";
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(646, 399);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(142, 39);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(389, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(399, 182);
            this.dataGridView1.TabIndex = 2;
            // 
            // LoadExpencesButton
            // 
            this.LoadExpencesButton.Location = new System.Drawing.Point(40, 103);
            this.LoadExpencesButton.Name = "LoadExpencesButton";
            this.LoadExpencesButton.Size = new System.Drawing.Size(238, 58);
            this.LoadExpencesButton.TabIndex = 3;
            this.LoadExpencesButton.Text = "Открыть средство взаимодействия с отчётностью по расходам";
            this.LoadExpencesButton.UseVisualStyleBackColor = true;
            this.LoadExpencesButton.Click += new System.EventHandler(this.LoadExpencesButton_Click);
            // 
            // LoadExpencesTypesButton
            // 
            this.LoadExpencesTypesButton.Location = new System.Drawing.Point(40, 199);
            this.LoadExpencesTypesButton.Name = "LoadExpencesTypesButton";
            this.LoadExpencesTypesButton.Size = new System.Drawing.Size(238, 57);
            this.LoadExpencesTypesButton.TabIndex = 4;
            this.LoadExpencesTypesButton.Text = "Открыть средство взаимодействия с видами расходов";
            this.LoadExpencesTypesButton.UseVisualStyleBackColor = true;
            this.LoadExpencesTypesButton.Click += new System.EventHandler(this.LoadExpencesTypesButton_Click);
            // 
            // LoadDepsLimitsButton
            // 
            this.LoadDepsLimitsButton.Location = new System.Drawing.Point(40, 287);
            this.LoadDepsLimitsButton.Name = "LoadDepsLimitsButton";
            this.LoadDepsLimitsButton.Size = new System.Drawing.Size(238, 65);
            this.LoadDepsLimitsButton.TabIndex = 5;
            this.LoadDepsLimitsButton.Text = "Открыть средство взаимодействия с ограничениями для отделов";
            this.LoadDepsLimitsButton.UseVisualStyleBackColor = true;
            this.LoadDepsLimitsButton.Click += new System.EventHandler(this.LoadDepsLimitsButton_Click);
            // 
            // LoadDepsButton
            // 
            this.LoadDepsButton.Location = new System.Drawing.Point(596, 255);
            this.LoadDepsButton.Name = "LoadDepsButton";
            this.LoadDepsButton.Size = new System.Drawing.Size(124, 49);
            this.LoadDepsButton.TabIndex = 9;
            this.LoadDepsButton.Text = "Просмотреть таблицу отделов";
            this.LoadDepsButton.UseVisualStyleBackColor = true;
            this.LoadDepsButton.Click += new System.EventHandler(this.LoadDepsButton_Click);
            // 
            // LoadEmployeeButton
            // 
            this.LoadEmployeeButton.Location = new System.Drawing.Point(443, 255);
            this.LoadEmployeeButton.Name = "LoadEmployeeButton";
            this.LoadEmployeeButton.Size = new System.Drawing.Size(124, 49);
            this.LoadEmployeeButton.TabIndex = 10;
            this.LoadEmployeeButton.Text = "Просмотреть таблицу сотрудников";
            this.LoadEmployeeButton.UseVisualStyleBackColor = true;
            this.LoadEmployeeButton.Click += new System.EventHandler(this.LoadEmployeeButton_Click);
            // 
            // TaskButton
            // 
            this.TaskButton.Location = new System.Drawing.Point(275, 394);
            this.TaskButton.Name = "TaskButton";
            this.TaskButton.Size = new System.Drawing.Size(226, 44);
            this.TaskButton.TabIndex = 11;
            this.TaskButton.Text = "Задание";
            this.TaskButton.UseVisualStyleBackColor = true;
            this.TaskButton.Click += new System.EventHandler(this.TaskButton_Click);
            // 
            // WorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TaskButton);
            this.Controls.Add(this.LoadEmployeeButton);
            this.Controls.Add(this.LoadDepsButton);
            this.Controls.Add(this.LoadDepsLimitsButton);
            this.Controls.Add(this.LoadExpencesTypesButton);
            this.Controls.Add(this.LoadExpencesButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.label1);
            this.Name = "WorkerForm";
            this.Text = "WorkerForm";
            this.Load += new System.EventHandler(this.WorkerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button LoadExpencesButton;
        private System.Windows.Forms.Button LoadExpencesTypesButton;
        private System.Windows.Forms.Button LoadDepsLimitsButton;
        private System.Windows.Forms.Button LoadDepsButton;
        private System.Windows.Forms.Button LoadEmployeeButton;
        private System.Windows.Forms.Button TaskButton;
    }
}