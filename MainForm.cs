using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UnoIgra;

public partial class MainForm : Form
{
    private readonly UnoGame game = new UnoGame();

    private IContainer components = null!;

    private PictureBox topCardPicture = null!;
    private Label topCardNameLabel = null!;
    private Label activeColorLabel = null!;
    private Label currentPlayerLabel = null!;
    private Label deckLabel = null!;
    private Label playersLabel = null!;
    private FlowLayoutPanel handPanel = null!;
    private ListBox logListBox = null!;
    private Button drawButton = null!;
    private Button newGameButton = null!;
    private Panel topPanel;
    private Label titleLabel;
    private Label topCardTitleLabel;
    private Panel rightPanel;
    private Label logTitle;
    private Panel bottomPanel;
    private Label handTitle;
    private Label helpLabel;
    private ToolTip cardToolTip = null!;

    public MainForm()
    {
        InitializeComponent();

        if (!IsInDesigner())
        {
            StartNewGame();
        }
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
        topCardPicture = new PictureBox();
        topCardNameLabel = new Label();
        activeColorLabel = new Label();
        currentPlayerLabel = new Label();
        deckLabel = new Label();
        playersLabel = new Label();
        handPanel = new FlowLayoutPanel();
        logListBox = new ListBox();
        drawButton = new Button();
        newGameButton = new Button();
        cardToolTip = new ToolTip(components);
        topPanel = new Panel();
        titleLabel = new Label();
        topCardTitleLabel = new Label();
        rightPanel = new Panel();
        logTitle = new Label();
        bottomPanel = new Panel();
        handTitle = new Label();
        helpLabel = new Label();
        ((ISupportInitialize)topCardPicture).BeginInit();
        topPanel.SuspendLayout();
        rightPanel.SuspendLayout();
        bottomPanel.SuspendLayout();
        SuspendLayout();
        // 
        // topCardPicture
        // 
        topCardPicture.BackColor = Color.White;
        topCardPicture.BorderStyle = BorderStyle.FixedSingle;
        topCardPicture.Location = new Point(24, 84);
        topCardPicture.Name = "topCardPicture";
        topCardPicture.Size = new Size(108, 156);
        topCardPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        topCardPicture.TabIndex = 2;
        topCardPicture.TabStop = false;
        // 
        // topCardNameLabel
        // 
        topCardNameLabel.BackColor = Color.FromArgb(248, 248, 248);
        topCardNameLabel.BorderStyle = BorderStyle.FixedSingle;
        topCardNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        topCardNameLabel.Location = new Point(150, 95);
        topCardNameLabel.Name = "topCardNameLabel";
        topCardNameLabel.Size = new Size(205, 60);
        topCardNameLabel.TabIndex = 3;
        topCardNameLabel.Text = "Tukaj bo karta";
        topCardNameLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // activeColorLabel
        // 
        activeColorLabel.BackColor = Color.LightGray;
        activeColorLabel.BorderStyle = BorderStyle.FixedSingle;
        activeColorLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        activeColorLabel.Location = new Point(380, 95);
        activeColorLabel.Name = "activeColorLabel";
        activeColorLabel.Size = new Size(190, 60);
        activeColorLabel.TabIndex = 4;
        activeColorLabel.Text = "Aktivna barva";
        activeColorLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // currentPlayerLabel
        // 
        currentPlayerLabel.BackColor = Color.FromArgb(248, 248, 248);
        currentPlayerLabel.BorderStyle = BorderStyle.FixedSingle;
        currentPlayerLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        currentPlayerLabel.Location = new Point(590, 95);
        currentPlayerLabel.Name = "currentPlayerLabel";
        currentPlayerLabel.Size = new Size(220, 60);
        currentPlayerLabel.TabIndex = 5;
        currentPlayerLabel.Text = "Na potezi";
        currentPlayerLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // deckLabel
        // 
        deckLabel.BackColor = Color.FromArgb(248, 248, 248);
        deckLabel.BorderStyle = BorderStyle.FixedSingle;
        deckLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        deckLabel.Location = new Point(830, 95);
        deckLabel.Name = "deckLabel";
        deckLabel.Size = new Size(170, 60);
        deckLabel.TabIndex = 6;
        deckLabel.Text = "Kup za vlečenje";
        deckLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // playersLabel
        // 
        playersLabel.BackColor = Color.White;
        playersLabel.BorderStyle = BorderStyle.FixedSingle;
        playersLabel.Dock = DockStyle.Bottom;
        playersLabel.Location = new Point(10, 371);
        playersLabel.Name = "playersLabel";
        playersLabel.Padding = new Padding(8);
        playersLabel.Size = new Size(340, 140);
        playersLabel.TabIndex = 2;
        playersLabel.Text = "Igralci bodo prikazani tukaj.";
        playersLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // handPanel
        // 
        handPanel.AutoScroll = true;
        handPanel.BackColor = Color.WhiteSmoke;
        handPanel.Dock = DockStyle.Fill;
        handPanel.Location = new Point(12, 44);
        handPanel.Name = "handPanel";
        handPanel.Padding = new Padding(4);
        handPanel.Size = new Size(640, 259);
        handPanel.TabIndex = 0;
        // 
        // logListBox
        // 
        logListBox.Dock = DockStyle.Fill;
        logListBox.HorizontalScrollbar = true;
        logListBox.Location = new Point(10, 42);
        logListBox.Name = "logListBox";
        logListBox.Size = new Size(340, 329);
        logListBox.TabIndex = 0;
        // 
        // drawButton
        // 
        drawButton.Dock = DockStyle.Right;
        drawButton.Location = new Point(652, 44);
        drawButton.Name = "drawButton";
        drawButton.Size = new Size(140, 259);
        drawButton.TabIndex = 1;
        drawButton.Text = "Vleci karto";
        drawButton.UseVisualStyleBackColor = true;
        drawButton.Click += DrawButton_Click;
        // 
        // newGameButton
        // 
        newGameButton.Location = new Point(1020, 95);
        newGameButton.Name = "newGameButton";
        newGameButton.Size = new Size(120, 60);
        newGameButton.TabIndex = 7;
        newGameButton.Text = "Nova igra";
        newGameButton.UseVisualStyleBackColor = true;
        newGameButton.Click += NewGameButton_Click;
        // 
        // topPanel
        // 
        topPanel.BackColor = Color.White;
        topPanel.Controls.Add(titleLabel);
        topPanel.Controls.Add(topCardTitleLabel);
        topPanel.Controls.Add(topCardPicture);
        topPanel.Controls.Add(topCardNameLabel);
        topPanel.Controls.Add(activeColorLabel);
        topPanel.Controls.Add(currentPlayerLabel);
        topPanel.Controls.Add(deckLabel);
        topPanel.Controls.Add(newGameButton);
        topPanel.Dock = DockStyle.Top;
        topPanel.Location = new Point(0, 0);
        topPanel.Name = "topPanel";
        topPanel.Padding = new Padding(16);
        topPanel.Size = new Size(1164, 230);
        topPanel.TabIndex = 3;
        // 
        // titleLabel
        // 
        titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        titleLabel.Location = new Point(16, 12);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(360, 38);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "UNO - igra s kartami";
        // 
        // topCardTitleLabel
        // 
        topCardTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        topCardTitleLabel.Location = new Point(22, 55);
        topCardTitleLabel.Name = "topCardTitleLabel";
        topCardTitleLabel.Size = new Size(140, 25);
        topCardTitleLabel.TabIndex = 1;
        topCardTitleLabel.Text = "Zadnja karta";
        // 
        // rightPanel
        // 
        rightPanel.BackColor = Color.FromArgb(235, 235, 235);
        rightPanel.Controls.Add(logListBox);
        rightPanel.Controls.Add(logTitle);
        rightPanel.Controls.Add(playersLabel);
        rightPanel.Dock = DockStyle.Right;
        rightPanel.Location = new Point(804, 230);
        rightPanel.Name = "rightPanel";
        rightPanel.Padding = new Padding(10);
        rightPanel.Size = new Size(360, 521);
        rightPanel.TabIndex = 2;
        // 
        // logTitle
        // 
        logTitle.Dock = DockStyle.Top;
        logTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        logTitle.Location = new Point(10, 10);
        logTitle.Name = "logTitle";
        logTitle.Size = new Size(340, 32);
        logTitle.TabIndex = 1;
        logTitle.Text = "Potek igre";
        logTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // bottomPanel
        // 
        bottomPanel.BackColor = Color.White;
        bottomPanel.Controls.Add(handPanel);
        bottomPanel.Controls.Add(drawButton);
        bottomPanel.Controls.Add(handTitle);
        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Location = new Point(0, 436);
        bottomPanel.Name = "bottomPanel";
        bottomPanel.Padding = new Padding(12);
        bottomPanel.Size = new Size(804, 315);
        bottomPanel.TabIndex = 1;
        // 
        // handTitle
        // 
        handTitle.Dock = DockStyle.Top;
        handTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        handTitle.Location = new Point(12, 12);
        handTitle.Name = "handTitle";
        handTitle.Size = new Size(780, 32);
        handTitle.TabIndex = 2;
        handTitle.Text = "Tvoje karte";
        handTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // helpLabel
        // 
        helpLabel.BackColor = Color.FromArgb(245, 245, 245);
        helpLabel.Dock = DockStyle.Fill;
        helpLabel.Location = new Point(0, 230);
        helpLabel.Name = "helpLabel";
        helpLabel.Padding = new Padding(18);
        helpLabel.Size = new Size(804, 206);
        helpLabel.TabIndex = 0;
        helpLabel.Text = "Pravila: igraš karto iste barve ali iste vrednosti. Črne karte lahko odigraš vedno. Če nimaš ustrezne karte, pritisni 'Vleci karto'. Zmaga igralec, ki prvi ostane brez kart.";
        helpLabel.Click += helpLabel_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 245, 245);
        ClientSize = new Size(1164, 751);
        Controls.Add(helpLabel);
        Controls.Add(bottomPanel);
        Controls.Add(rightPanel);
        Controls.Add(topPanel);
        Font = new Font("Segoe UI", 10F);
        MinimumSize = new Size(980, 650);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "UNO - seminarska naloga";
        ((ISupportInitialize)topCardPicture).EndInit();
        topPanel.ResumeLayout(false);
        rightPanel.ResumeLayout(false);
        bottomPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private void NewGameButton_Click(object? sender, EventArgs e)
    {
        StartNewGame();
    }

    private void DrawButton_Click(object? sender, EventArgs e)
    {
        DrawCardForHuman();
    }

    private void StartNewGame()
    {
        game.StartNewGame(2);
        RefreshUi();
    }

    private void PlayHumanCard(int handIndex)
    {
        if (!game.CanHumanPlayCard(handIndex))
        {
            MessageBox.Show(
                "Te karte ne moreš odigrati.",
                "Neveljavna poteza",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            return;
        }

        Card card = game.CurrentPlayer.Hand[handIndex];
        CardColor? chosenColor = null;

        if (card.IsWild)
        {
            chosenColor = ColorChoiceForm.ChooseColor(this);

            if (chosenColor is null)
                return;
        }

        string result = game.PlayHumanCard(handIndex, chosenColor);

        if (result != "Karta je bila odigrana.")
        {
            MessageBox.Show(
                result,
                "UNO",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        game.ProcessComputerTurns();
        RefreshUi();
        ShowWinnerIfGameEnded();
    }

    private void DrawCardForHuman()
    {
        string result = game.HumanDrawCard();

        game.ProcessComputerTurns();
        RefreshUi();

        if (!result.StartsWith("Vlekel si karto", StringComparison.Ordinal))
        {
            MessageBox.Show(
                result,
                "UNO",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        ShowWinnerIfGameEnded();
    }

    private void RefreshUi()
    {
        Card topCard = game.DiscardPile.TopCard;

        topCardPicture.Image = CardImageStore.GetImage(topCard);
        topCardNameLabel.Text = topCard.ToString();

        activeColorLabel.Text = $"Aktivna barva\n{Card.ColorToSlovene(game.ActiveColor)}";
        activeColorLabel.BackColor = GetCardColor(game.ActiveColor);
        activeColorLabel.ForeColor = GetTextColor(game.ActiveColor);

        currentPlayerLabel.Text = $"Na potezi\n{game.CurrentPlayer.Name}";
        deckLabel.Text = $"Kup za vlečenje\n{game.DrawPile.Count} kart";

        playersLabel.Text = string.Join(
            Environment.NewLine,
            game.Players.Select(player => $"{player.Name}: {player.Hand.Count} kart")
        );

        logListBox.Items.Clear();

        foreach (string log in game.Log.TakeLast(120))
        {
            logListBox.Items.Add(log);
        }

        if (logListBox.Items.Count > 0)
        {
            logListBox.TopIndex = logListBox.Items.Count - 1;
        }

        RefreshHandPanel();
    }

    private void RefreshHandPanel()
    {
        handPanel.Controls.Clear();

        Player human = game.Players[0];
        bool humanTurn = !game.IsGameOver && game.CurrentPlayer == human;

        for (int i = 0; i < human.Hand.Count; i++)
        {
            Card card = human.Hand[i];
            int index = i;

            bool playable =
                humanTurn &&
                card.CanBePlayedOn(game.DiscardPile.TopCard, game.ActiveColor);

            Button button = new Button();

            button.Text = string.Empty;
            button.Width = 92;
            button.Height = 132;
            button.Margin = new Padding(6);
            button.BackColor = Color.White;
            button.BackgroundImage = CardImageStore.GetImage(card);
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.FlatStyle = FlatStyle.Flat;
            button.Enabled = playable;
            button.Cursor = playable ? Cursors.Hand : Cursors.Default;
            button.FlatAppearance.BorderSize = playable ? 3 : 1;
            button.Click += delegate
            {
                PlayHumanCard(index);
            };

            cardToolTip.SetToolTip(button, card.ToString());
            handPanel.Controls.Add(button);
        }

        drawButton.Enabled = humanTurn;
    }

    private void ShowWinnerIfGameEnded()
    {
        if (!game.IsGameOver || game.Winner is null)
            return;

        MessageBox.Show(
            $"Zmagovalec je: {game.Winner.Name}",
            "Konec igre",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }

    private static Color GetCardColor(CardColor color)
    {
        switch (color)
        {
            case CardColor.Red:
                return Color.Firebrick;

            case CardColor.Yellow:
                return Color.Gold;

            case CardColor.Green:
                return Color.SeaGreen;

            case CardColor.Blue:
                return Color.RoyalBlue;

            case CardColor.Wild:
                return Color.FromArgb(40, 40, 40);

            default:
                return Color.LightGray;
        }
    }

    private static Color GetTextColor(CardColor color)
    {
        if (color == CardColor.Yellow)
            return Color.Black;

        return Color.White;
    }

    private static bool IsInDesigner()
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return true;

        string processName = Process.GetCurrentProcess().ProcessName;

        return processName.Equals("devenv", StringComparison.OrdinalIgnoreCase);
    }

    private void helpLabel_Click(object sender, EventArgs e)
    {

    }
}