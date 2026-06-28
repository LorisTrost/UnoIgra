using System.Drawing;
using System.Drawing.Drawing2D;

namespace UnoIgra;

public static class CardImageStore
{
    private const int CardWidth = 300;
    private const int CardHeight = 430;
    private static readonly Dictionary<string, Image> Cache = new();

    public static Image GetImage(Card card)
    {
        string key = card.ImageFileName;

        if (Cache.TryGetValue(key, out Image? image))
            return image;

        image = CreateCardImage(card);
        Cache[key] = image;
        return image;
    }

    public static Image GetBackImage()
    {
        const string key = "back";

        if (Cache.TryGetValue(key, out Image? image))
            return image;

        image = CreateBackImage();
        Cache[key] = image;
        return image;
    }

    private static Bitmap CreateCardImage(Card card)
    {
        Bitmap bitmap = new(CardWidth, CardHeight);

        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        graphics.Clear(Color.Transparent);

        Rectangle shadowRectangle = new(16, 18, CardWidth - 32, CardHeight - 32);
        Rectangle cardRectangle = new(10, 10, CardWidth - 32, CardHeight - 32);

        using GraphicsPath shadowPath = RoundedRectangle(shadowRectangle, 28);
        using SolidBrush shadowBrush = new(Color.FromArgb(60, 0, 0, 0));
        graphics.FillPath(shadowBrush, shadowPath);

        using GraphicsPath cardPath = RoundedRectangle(cardRectangle, 28);
        using SolidBrush cardBrush = new(GetCardColor(card.Color));
        graphics.FillPath(cardBrush, cardPath);

        using Pen borderPen = new(Color.White, 7);
        graphics.DrawPath(borderPen, cardPath);

        if (card.IsWild)
            DrawWildCard(graphics, card, cardRectangle);
        else
            DrawNormalCard(graphics, card, cardRectangle);

        return bitmap;
    }

    private static Bitmap CreateBackImage()
    {
        Bitmap bitmap = new(CardWidth, CardHeight);

        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        graphics.Clear(Color.Transparent);

        Rectangle shadowRectangle = new(16, 18, CardWidth - 32, CardHeight - 32);
        Rectangle cardRectangle = new(10, 10, CardWidth - 32, CardHeight - 32);

        using GraphicsPath shadowPath = RoundedRectangle(shadowRectangle, 28);
        using SolidBrush shadowBrush = new(Color.FromArgb(60, 0, 0, 0));
        graphics.FillPath(shadowBrush, shadowPath);

        using GraphicsPath cardPath = RoundedRectangle(cardRectangle, 28);
        using SolidBrush backBrush = new(Color.FromArgb(26, 26, 32));
        graphics.FillPath(backBrush, cardPath);

        using Pen borderPen = new(Color.White, 7);
        graphics.DrawPath(borderPen, cardPath);

        Rectangle oval = new(52, 90, 176, 220);
        DrawFourColorOval(graphics, oval);

        using Font logoFont = new("Segoe UI", 48, FontStyle.Bold);
        DrawCenteredText(graphics, "UNO", logoFont, Brushes.White, new Rectangle(30, 160, 220, 70));

        return bitmap;
    }

    private static void DrawNormalCard(Graphics graphics, Card card, Rectangle cardRectangle)
    {
        string text = GetCardText(card.Value);

        Rectangle centerOval = new(cardRectangle.X + 38, cardRectangle.Y + 82, cardRectangle.Width - 76, cardRectangle.Height - 164);
        using GraphicsPath ovalPath = new();
        ovalPath.AddEllipse(centerOval);
        using Matrix matrix = new();
        matrix.RotateAt(-18, new PointF(centerOval.X + centerOval.Width / 2f, centerOval.Y + centerOval.Height / 2f));
        ovalPath.Transform(matrix);

        using SolidBrush whiteBrush = new(Color.White);
        graphics.FillPath(whiteBrush, ovalPath);

        using SolidBrush coloredBrush = new(GetCardColor(card.Color));
        using Font centerFont = GetCenterFont(text);
        DrawCenteredText(graphics, text, centerFont, coloredBrush, new Rectangle(cardRectangle.X + 50, cardRectangle.Y + 130, cardRectangle.Width - 100, 120));

        using Font cornerFont = new("Segoe UI", 34, FontStyle.Bold);
        using SolidBrush cornerBrush = new(Color.White);
        graphics.DrawString(text, cornerFont, cornerBrush, new PointF(cardRectangle.X + 24, cardRectangle.Y + 24));

        graphics.TranslateTransform(cardRectangle.Right - 24, cardRectangle.Bottom - 24);
        graphics.RotateTransform(180);
        graphics.DrawString(text, cornerFont, cornerBrush, new PointF(0, 0));
        graphics.ResetTransform();
    }

    private static void DrawWildCard(Graphics graphics, Card card, Rectangle cardRectangle)
    {
        Rectangle oval = new(cardRectangle.X + 48, cardRectangle.Y + 74, cardRectangle.Width - 96, cardRectangle.Height - 148);
        DrawFourColorOval(graphics, oval);

        string text = card.Value == CardValue.WildDrawFour ? "+4" : "WILD";
        using Font centerFont = card.Value == CardValue.WildDrawFour
            ? new Font("Segoe UI", 68, FontStyle.Bold)
            : new Font("Segoe UI", 42, FontStyle.Bold);

        DrawCenteredText(graphics, text, centerFont, Brushes.White, new Rectangle(cardRectangle.X + 36, cardRectangle.Y + 165, cardRectangle.Width - 72, 86));

        using Font cornerFont = new("Segoe UI", 28, FontStyle.Bold);
        graphics.DrawString(text, cornerFont, Brushes.White, new PointF(cardRectangle.X + 22, cardRectangle.Y + 24));

        graphics.TranslateTransform(cardRectangle.Right - 22, cardRectangle.Bottom - 24);
        graphics.RotateTransform(180);
        graphics.DrawString(text, cornerFont, Brushes.White, new PointF(0, 0));
        graphics.ResetTransform();
    }

    private static void DrawFourColorOval(Graphics graphics, Rectangle rectangle)
    {
        using GraphicsPath ovalPath = new();
        ovalPath.AddEllipse(rectangle);
        graphics.SetClip(ovalPath);

        int halfWidth = rectangle.Width / 2;
        int halfHeight = rectangle.Height / 2;

        using SolidBrush red = new(Color.FromArgb(220, 45, 45));
        using SolidBrush yellow = new(Color.FromArgb(245, 195, 40));
        using SolidBrush green = new(Color.FromArgb(35, 155, 80));
        using SolidBrush blue = new(Color.FromArgb(35, 95, 190));

        graphics.FillRectangle(red, rectangle.X, rectangle.Y, halfWidth, halfHeight);
        graphics.FillRectangle(yellow, rectangle.X + halfWidth, rectangle.Y, rectangle.Width - halfWidth, halfHeight);
        graphics.FillRectangle(blue, rectangle.X, rectangle.Y + halfHeight, halfWidth, rectangle.Height - halfHeight);
        graphics.FillRectangle(green, rectangle.X + halfWidth, rectangle.Y + halfHeight, rectangle.Width - halfWidth, rectangle.Height - halfHeight);

        graphics.ResetClip();

        using Pen whitePen = new(Color.White, 8);
        graphics.DrawEllipse(whitePen, rectangle);
    }

    private static void DrawCenteredText(Graphics graphics, string text, Font font, Brush brush, Rectangle rectangle)
    {
        using StringFormat format = new()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        graphics.DrawString(text, font, brush, rectangle, format);
    }

    private static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
    {
        int diameter = radius * 2;
        GraphicsPath path = new();

        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }

    private static Font GetCenterFont(string text)
    {
        return text.Length switch
        {
            <= 1 => new Font("Segoe UI", 92, FontStyle.Bold),
            <= 2 => new Font("Segoe UI", 76, FontStyle.Bold),
            <= 4 => new Font("Segoe UI", 52, FontStyle.Bold),
            _ => new Font("Segoe UI", 40, FontStyle.Bold)
        };
    }

    private static string GetCardText(CardValue value)
    {
        return value switch
        {
            CardValue.Zero => "0",
            CardValue.One => "1",
            CardValue.Two => "2",
            CardValue.Three => "3",
            CardValue.Four => "4",
            CardValue.Five => "5",
            CardValue.Six => "6",
            CardValue.Seven => "7",
            CardValue.Eight => "8",
            CardValue.Nine => "9",
            CardValue.Skip => "Ø",
            CardValue.Reverse => "↺",
            CardValue.DrawTwo => "+2",
            CardValue.Wild => "WILD",
            CardValue.WildDrawFour => "+4",
            _ => "?"
        };
    }

    private static Color GetCardColor(CardColor color)
    {
        return color switch
        {
            CardColor.Red => Color.FromArgb(210, 38, 38),
            CardColor.Yellow => Color.FromArgb(235, 184, 28),
            CardColor.Green => Color.FromArgb(29, 142, 72),
            CardColor.Blue => Color.FromArgb(35, 91, 185),
            CardColor.Wild => Color.FromArgb(25, 25, 30),
            _ => Color.Gray
        };
    }
}
