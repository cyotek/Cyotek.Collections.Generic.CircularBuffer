namespace Cyotek.Collections.Generic.CircularBuffer.Demo
{
  partial class DemoForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoForm));
      this.snakeTimer = new System.Windows.Forms.Timer(this.components);
      this.wrapCheckBox = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.nextButton = new System.Windows.Forms.Button();
      this.automaticCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // snakeTimer
      // 
      this.snakeTimer.Interval = 25;
      this.snakeTimer.Tick += new System.EventHandler(this.SnakeTimer_Tick);
      // 
      // wrapCheckBox
      // 
      this.wrapCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.wrapCheckBox.AutoSize = true;
      this.wrapCheckBox.BackColor = System.Drawing.Color.Transparent;
      this.wrapCheckBox.Checked = true;
      this.wrapCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.wrapCheckBox.ForeColor = System.Drawing.Color.FromArgb(8, 8, 8);
      this.wrapCheckBox.Location = new System.Drawing.Point(720, 532);
      this.wrapCheckBox.Name = "wrapCheckBox";
      this.wrapCheckBox.Size = new System.Drawing.Size(52, 17);
      this.wrapCheckBox.TabIndex = 3;
      this.wrapCheckBox.Text = "&Wrap";
      this.wrapCheckBox.UseVisualStyleBackColor = false;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.ForeColor = System.Drawing.Color.FromArgb(8, 8, 8);
      this.label1.Location = new System.Drawing.Point(12, 522);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(674, 30);
      this.label1.TabIndex = 2;
      this.label1.Text = resources.GetString("label1.Text");
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.Location = new System.Drawing.Point(697, 41);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(75, 23);
      this.nextButton.TabIndex = 1;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
      // 
      // automaticCheckBox
      // 
      this.automaticCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.automaticCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.automaticCheckBox.Checked = true;
      this.automaticCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.automaticCheckBox.Location = new System.Drawing.Point(697, 12);
      this.automaticCheckBox.Name = "automaticCheckBox";
      this.automaticCheckBox.Size = new System.Drawing.Size(75, 23);
      this.automaticCheckBox.TabIndex = 0;
      this.automaticCheckBox.Text = "&Automatic";
      this.automaticCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.automaticCheckBox.UseVisualStyleBackColor = true;
      this.automaticCheckBox.CheckedChanged += new System.EventHandler(this.AutomaticCheckBox_CheckedChanged);
      // 
      // DemoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 561);
      this.Controls.Add(this.automaticCheckBox);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.wrapCheckBox);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "DemoForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Cyotek CircularBuffer CopyTo Demonstration";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Timer snakeTimer;
    private System.Windows.Forms.CheckBox wrapCheckBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.CheckBox automaticCheckBox;
  }
}

