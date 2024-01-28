namespace Courseework
{
    partial class AdminForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.RegisterNewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitButton = new System.Windows.Forms.Button();
            this.LoadDepsButton = new System.Windows.Forms.Button();
            this.LoadEmployeesButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LoadUsersButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(148, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(573, 263);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RegisterNewUser});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // RegisterNewUser
            // 
            this.RegisterNewUser.BackColor = System.Drawing.Color.Transparent;
            this.RegisterNewUser.Name = "RegisterNewUser";
            this.RegisterNewUser.Size = new System.Drawing.Size(238, 20);
            this.RegisterNewUser.Text = "Зарегестрировать нового пользователя";
            this.RegisterNewUser.Click += new System.EventHandler(this.RegisterNewUser_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(651, 404);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(137, 34);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // LoadDepsButton
            // 
            this.LoadDepsButton.Location = new System.Drawing.Point(148, 333);
            this.LoadDepsButton.Name = "LoadDepsButton";
            this.LoadDepsButton.Size = new System.Drawing.Size(166, 33);
            this.LoadDepsButton.TabIndex = 3;
            this.LoadDepsButton.Text = "Загрузить отделы";
            this.LoadDepsButton.UseVisualStyleBackColor = true;
            this.LoadDepsButton.Click += new System.EventHandler(this.LoadDepsButton_Click);
            // 
            // LoadEmployeesButton
            // 
            this.LoadEmployeesButton.Location = new System.Drawing.Point(336, 333);
            this.LoadEmployeesButton.Name = "LoadEmployeesButton";
            this.LoadEmployeesButton.Size = new System.Drawing.Size(150, 33);
            this.LoadEmployeesButton.TabIndex = 4;
            this.LoadEmployeesButton.Text = "Загрузить сотрудников";
            this.LoadEmployeesButton.UseVisualStyleBackColor = true;
            this.LoadEmployeesButton.Click += new System.EventHandler(this.LoadEmployeesButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(197, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Вы зашли под учётной записью администратора";
            // 
            // LoadUsersButton
            // 
            this.LoadUsersButton.Location = new System.Drawing.Point(518, 333);
            this.LoadUsersButton.Name = "LoadUsersButton";
            this.LoadUsersButton.Size = new System.Drawing.Size(203, 33);
            this.LoadUsersButton.TabIndex = 6;
            this.LoadUsersButton.Text = "Загрузить пользоватаелей системы";
            this.LoadUsersButton.UseVisualStyleBackColor = true;
            this.LoadUsersButton.Click += new System.EventHandler(this.LoadUsersButton_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LoadUsersButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoadEmployeesButton);
            this.Controls.Add(this.LoadDepsButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem RegisterNewUser;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button LoadDepsButton;
        private System.Windows.Forms.Button LoadEmployeesButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoadUsersButton;
    }
}