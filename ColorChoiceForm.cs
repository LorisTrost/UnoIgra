using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UnoIgra;

public sealed partial class ColorChoiceForm : Form
{
    private IContainer components = null!;

    private Label instructionLabel = null!;
    private FlowLayoutPanel colorPanel = null!;
    private Button redButton = null!;
    private Button yellowButton = null!;
    private Button greenButton = null!;
    private Button blueButton = null!;

    public CardColor SelectedColor { get; private set; } = CardColor.Red;

    public ColorChoiceForm()
    {
        InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();

        instructionLabel = new Label();
        colorPanel = new FlowLayoutPanel();

        redButton = new Button();
        yellowButton = new Button();
        greenButton = new Button();
        blueButton = new Button();

        SuspendLayout();

        // 
        // ColorChoiceForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(380, 150);
        Name = "ColorChoiceForm";
        Text = "Izberi barvo";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = Color.White;
        Font = new Font("Segoe UI", 10F);

        // 
        // instructionLabel
        // 
        instructionLabel.Text = "Izberi novo aktivno barvo:";
        instructionLabel.Dock = DockStyle.Top;
        instructionLabel.Height = 45;
        instructionLabel.TextAlign = ContentAlignment.MiddleCenter;
        instructionLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        instructionLabel.BackColor = Color.White;

        // 
        // colorPanel
        // 
        colorPanel.Dock = DockStyle.Fill;
        colorPanel.FlowDirection = FlowDirection.LeftToRight;
        colorPanel.Padding = new Padding(14, 14, 14, 14);
        colorPanel.WrapContents = false;
        colorPanel.BackColor = Color.White;

        // 
        // redButton
        // 
        redButton.Text = "Rdeča";
        redButton.Width = 78;
        redButton.Height = 56;
        redButton.BackColor = Color.Firebrick;
        redButton.ForeColor = Color.White;
        redButton.FlatStyle = FlatStyle.Flat;
        redButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        redButton.Margin = new Padding(4);
        redButton.Click += RedButton_Click;

        // 
        // yellowButton
        // 
        yellowButton.Text = "Rumena";
        yellowButton.Width = 78;
        yellowButton.Height = 56;
        yellowButton.BackColor = Color.Gold;
        yellowButton.ForeColor = Color.Black;
        yellowButton.FlatStyle = FlatStyle.Flat;
        yellowButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        yellowButton.Margin = new Padding(4);
        yellowButton.Click += YellowButton_Click;

        // 
        // greenButton
        // 
        greenButton.Text = "Zelena";
        greenButton.Width = 78;
        greenButton.Height = 56;
        greenButton.BackColor = Color.SeaGreen;
        greenButton.ForeColor = Color.White;
        greenButton.FlatStyle = FlatStyle.Flat;
        greenButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        greenButton.Margin = new Padding(4);
        greenButton.Click += GreenButton_Click;

        // 
        // blueButton
        // 
        blueButton.Text = "Modra";
        blueButton.Width = 78;
        blueButton.Height = 56;
        blueButton.BackColor = Color.RoyalBlue;
        blueButton.ForeColor = Color.White;
        blueButton.FlatStyle = FlatStyle.Flat;
        blueButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        blueButton.Margin = new Padding(4);
        blueButton.Click += BlueButton_Click;

        colorPanel.Controls.Add(redButton);
        colorPanel.Controls.Add(yellowButton);
        colorPanel.Controls.Add(greenButton);
        colorPanel.Controls.Add(blueButton);

        Controls.Add(colorPanel);
        Controls.Add(instructionLabel);

        ResumeLayout(false);
    }

    public static CardColor? ChooseColor(IWin32Window owner)
    {
        using ColorChoiceForm form = new ColorChoiceForm();

        DialogResult result = form.ShowDialog(owner);

        if (result == DialogResult.OK)
        {
            return form.SelectedColor;
        }

        return null;
    }

    private void RedButton_Click(object? sender, EventArgs e)
    {
        SelectColor(CardColor.Red);
    }

    private void YellowButton_Click(object? sender, EventArgs e)
    {
        SelectColor(CardColor.Yellow);
    }

    private void GreenButton_Click(object? sender, EventArgs e)
    {
        SelectColor(CardColor.Green);
    }

    private void BlueButton_Click(object? sender, EventArgs e)
    {
        SelectColor(CardColor.Blue);
    }

    private void SelectColor(CardColor color)
    {
        SelectedColor = color;
        DialogResult = DialogResult.OK;
        Close();
    }
}