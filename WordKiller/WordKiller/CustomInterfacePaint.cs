﻿using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WordKiller
{
    public partial class CustomInterface
    {
        void titlepagePanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0 || e.Row == 1)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 253, 219, 124)))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
            else if (e.Row == 3 || e.Row == 4)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 208, 117, 252)))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
            else if (e.Row == 6 || e.Row == 7)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 84, 213, 245)))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
        }

        void buttonText_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            int fontSize = 10;
            if (MouseIsOverControl(pb) && Control.MouseButtons != MouseButtons.Left)
            {
                fontSize = 12;
            }
            using (Font fnt = new Font(GlobalFont.GetFont().Name, fontSize))
            {
                string str;
                if (elementLabel.Text == "текст")
                {
                    str = "К подстановкам";
                }
                else
                {
                    str = "К тексту";
                }
                SizeF stringSize = e.Graphics.MeasureString(str, fnt);
                e.Graphics.DrawString(str, fnt, Brushes.Black, new Point((int)(pb.Width / 2 - stringSize.Width / 2), (int)(pb.Height / 2 - stringSize.Height / 2)));
            }
        }

        void menuPB_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Control controlPB = (Control)sender;
            int index = elementPanel.Controls.IndexOf(controlPB);
            Font fnt = GlobalFont.GetFont();

            if (MouseIsOverControl(pb) && Control.MouseButtons != MouseButtons.Left)
            {
                fnt = new Font(fnt.Name, 16);
            }
            else
            {
                fnt = new Font(fnt.Name, 14);
            }
            using (fnt)
            {
                SizeF stringSize = e.Graphics.MeasureString(menuLabels[index - 1], fnt);
                e.Graphics.DrawString(menuLabels[index - 1], fnt, Brushes.Black, new Point((int)(pb.Width / 2 - stringSize.Width / 2), (int)(pb.Height / 2 - stringSize.Height / 2)));
            }
        }

        void menuPBbtn_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            string str = pb.Name;
            Font fnt = GlobalFont.GetFont();
            int size = 10; int selected = 2;
            if (MouseIsOverControl(pb) && Control.MouseButtons != MouseButtons.Left)
            {
                fnt = new Font(fnt.Name, size + selected);
            }
            else
            {
                fnt = new Font(fnt.Name, size);
            }
            using (fnt)
            {
                SizeF stringSize = e.Graphics.MeasureString(str, fnt);
                e.Graphics.DrawString(str, fnt, Brushes.Black, new Point((int)(pb.Width / 2 - stringSize.Width / 2), (int)(pb.Height / 2 - stringSize.Height / 2)));
            }
        }

        void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            string str = TypeRichBox();
            if (dragging == 0)
            {
                if (str == AddSpecialСharacterAB("h1"))
                {
                    e.Graphics.DrawImage(Properties.Resources.Red, 0, 0, pictureBox.Width, pictureBox.Height);
                    int index = data.ComboBox["h1"].Form.SelectedIndex;
                    if (index != -1)
                    {
                        if (NumberHeadingMenuItem.Checked)
                        {
                            DrawText((index + 1).ToString() + " " + data.ComboBox["h1"].Data[index][1].ToUpper(), e);
                        }
                        else
                        {
                            DrawText(data.ComboBox["h1"].Data[index][1].ToUpper(), e);
                        }
                    }
                    else
                    {
                        DrawText("Заголовок".ToUpper(), e);
                    }
                }
                else if (str == AddSpecialСharacterAB("h2"))
                {
                    e.Graphics.DrawImage(Properties.Resources.Red, 0, 0, pictureBox.Width, pictureBox.Height);
                    int index = data.ComboBox["h2"].Form.SelectedIndex;
                    if (index != -1)
                    {
                        if (NumberHeadingMenuItem.Checked)
                        {
                            DrawText("H1." + (index + 1).ToString() + " " + data.ComboBox["h2"].Data[index][1], e);
                        }
                        else
                        {
                            DrawText(data.ComboBox["h2"].Data[index][1], e);
                        }
                    }
                    else
                    {
                        DrawText("Заголовок", e);
                    }
                }
                else if (str == AddSpecialСharacterAB("l"))
                {
                    e.Graphics.DrawImage(Properties.Resources.Red, 0, 0, pictureBox.Width, pictureBox.Height);
                    DrawText("Список", e);
                }
                else if (str == AddSpecialСharacterAB("t"))
                {
                    e.Graphics.DrawImage(Properties.Resources.Red, 0, 0, pictureBox.Width, pictureBox.Height);
                    DrawText("Таблица", e);
                }
                else if (str == AddSpecialСharacterAB("p"))
                {
                    if (fileNames == null)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Picture, 0, 0, pictureBox.Width, pictureBox.Height);
                        SizeF stringSize = e.Graphics.MeasureString("Не указан", new Font("Microsoft Sans Serif", 14));
                        e.Graphics.DrawString("Не указан", new Font("Microsoft Sans Serif", 14), new SolidBrush(Color.Black), new Point((int)(pictureBox.Width / 2 - stringSize.Width / 2), pictureBox.Height / 2 + 50));
                    }
                    else
                    {
                        try
                        {
                            e.Graphics.DrawImage(Properties.Resources.purpleBackground, 0, 0, pictureBox.Width, pictureBox.Height);
                            e.Graphics.DrawImage(Image.FromFile(fileNames), 0, 0, pictureBox.Width, pictureBox.Height);
                        }
                        catch (System.OutOfMemoryException)
                        {
                            richTextBox.Clear();
                            e.Graphics.DrawImage(Properties.Resources.DragNDrop, 0, 0, pictureBox.Width, pictureBox.Height);
                        }
                    }
                }
                else if (str == AddSpecialСharacterAB("c"))
                {
                    e.Graphics.DrawImage(Properties.Resources.Code, 0, 0, pictureBox.Width, pictureBox.Height);
                    if (fileNames == null)
                    {
                        SizeF stringSize = e.Graphics.MeasureString("Не указан", new Font("Microsoft Sans Serif", 14));
                        e.Graphics.DrawString("Не указан", new Font("Microsoft Sans Serif", 14), new SolidBrush(Color.Black), new Point((int)(pictureBox.Width / 2 - stringSize.Width / 2), pictureBox.Height / 2 + 50));
                    }
                    else
                    {
                        string nameFile = fileNames.Split('\\')[fileNames.Split('\\').Length - 1];
                        SizeF stringSize = e.Graphics.MeasureString(nameFile, new Font("Microsoft Sans Serif", 14));
                        e.Graphics.DrawString(nameFile, new Font("Microsoft Sans Serif", 14), new SolidBrush(Color.Black), new Point((int)(pictureBox.Width / 2 - stringSize.Width / 2), pictureBox.Height / 2 + 50));
                    }
                }
            }
            else if (dragging == 1)
            {
                e.Graphics.DrawImage(Properties.Resources.pictureCode_Picture, 0, 0, pictureBox.Width, pictureBox.Height);
                DragNDrop_PaintText(e, Color.White, Color.Black);
            }
            else if (dragging == 2)
            {
                e.Graphics.DrawImage(Properties.Resources.pictureCode_Code, 0, 0, pictureBox.Width, pictureBox.Height);
                DragNDrop_PaintText(e, Color.Black, Color.White);
            }
            else if (dragging == 3)
            {
                e.Graphics.DrawImage(Properties.Resources.pictureCode, 0, 0, pictureBox.Width, pictureBox.Height);
                DragNDrop_PaintText(e, Color.Black, Color.Black);
            }
        }
        void DrawText(string text, PaintEventArgs e)
        {
            SizeF stringSize = e.Graphics.MeasureString(text, new Font("Microsoft Sans Serif", 20));
            e.Graphics.DrawString(text, new Font("Microsoft Sans Serif", 20), new SolidBrush(Color.Black), new Point((int)(pictureBox.Width / 2 - stringSize.Width / 2), pictureBox.Height / 2 - 20));
        }

        void DragNDrop_PaintText(PaintEventArgs e, Color picture, Color code)
        {
            string str1 = "Для";
            SizeF stringSize = e.Graphics.MeasureString(str1, new Font("Microsoft Sans Serif", 14));
            e.Graphics.DrawString(str1, new Font("Microsoft Sans Serif", 14), new SolidBrush(picture), new Point((int)(pictureBox.Width / 4 - stringSize.Width / 2), pictureBox.Height / 2 + 30));
            str1 = "картинки";
            stringSize = e.Graphics.MeasureString(str1, new Font("Microsoft Sans Serif", 14));
            e.Graphics.DrawString(str1, new Font("Microsoft Sans Serif", 14), new SolidBrush(picture), new Point((int)(pictureBox.Width / 4 - stringSize.Width / 2), pictureBox.Height / 2 + 50));

            str1 = "Для";
            stringSize = e.Graphics.MeasureString(str1, new Font("Microsoft Sans Serif", 14));
            e.Graphics.DrawString(str1, new Font("Microsoft Sans Serif", 14), new SolidBrush(code), new Point((int)(3 * pictureBox.Width / 4 - stringSize.Width / 2), pictureBox.Height / 2 + 30));
            str1 = "кода";
            stringSize = e.Graphics.MeasureString(str1, new Font("Microsoft Sans Serif", 14));
            e.Graphics.DrawString(str1, new Font("Microsoft Sans Serif", 14), new SolidBrush(code), new Point((int)(3 * pictureBox.Width / 4 - stringSize.Width / 2), pictureBox.Height / 2 + 50));
        }

        private void menuStrip_Paint(object sender, PaintEventArgs e)
        {
            if (saveLogoVisible)
            {
                int side = (int)(Math.Round(menuStrip.Height * 3 / (float)4, MidpointRounding.AwayFromZero));
                Size saveLogoSize = new Size(side, side);
                Image saveLogo = new Bitmap(Properties.Resources.saveLogo, saveLogoSize);
                Point saveLogoPos = new Point(menuStrip.Width - 2 * saveLogo.Width, menuStrip.Height / 8);
                e.Graphics.DrawImage(saveLogo, saveLogoPos);
            }
        }

        private void CursorLocationPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle area = new Rectangle(new Point(0, 0), CursorLocationPanel.Size);
            if (this.richTextBox.SelectionStart > 0)
            {
                string str = this.richTextBox.Text.Substring(0, this.richTextBox.SelectionStart);
                int h1Count = Regex.Matches(str, AddSpecialСharacterB("h1")).Count;
                if (h1Count > 0)
                {
                    string h1 = data.ComboBox["h1"].Form.Items[h1Count - 1].ToString();
                    string h2 = "";
                    if (str.Substring(str.LastIndexOf(AddSpecialСharacterB("h1"))).Contains(AddSpecialСharacterB("h2")))
                    {
                        h2 = " : " + data.ComboBox["h2"].Form.Items[Regex.Matches(str, AddSpecialСharacterB("h2")).Count - 1].ToString();
                    }
                    label_CursorLocation.Text = h1 + h2;
                }
                else
                {
                    label_CursorLocation.Text = "До заголовков";
                }
                label_CursorLocation.Text += CursorPosExtra(str);
            }
            else
            {
                label_CursorLocation.Text = "Начало";
            }
            foreach(Control button in tableTypeInserts.Controls)
            {
                PictureBox pb = (PictureBox)button;
                string name = pb.Name.ToLower();
                int index = Regex.Matches(richTextBox.Text.Substring(0, richTextBox.SelectionStart), AddSpecialСharacterB(name)).Count;
                if (pb.Visible && index < data.ComboBox[name].Form.Items.Count && MouseIsOverControl(pb))
                {
                    string str = "Вставить " + data.ComboBox[name].Form.Items[index] + " в " + label_CursorLocation.Text;
                    if (Regex.Matches(richTextBox.Text.Substring(richTextBox.SelectionStart), AddSpecialСharacterB(name)).Count > 0)
                    {
                        str += ", последующие сместить";
                    }
                    label_CursorLocation.Text = str;
                }
            }
        }


        string CursorPosExtra(string str)
        {
            string extra = "";
            for (int i = 2; i < data.ComboBox.Keys.Count; i++)
            {
                string key = data.ComboBox.Keys.ElementAt(i);
                int count = str.Length - key.Length - 1;
                if (count>0 && str.Substring(count).StartsWith(specialBefore + key))
                {
                    int pCount = Regex.Matches(str, AddSpecialСharacterB(key)).Count;
                    extra = "  -  " + data.ComboBox[key].Form.Items[pCount - 1].ToString();
                    break;
                }
            }
            return extra;
        }

        void DrawText(PaintEventArgs e, string text, Color color, Rectangle area, int fontSize)
        {
            Point areaCenter = new Point(area.X + area.Width / 2, area.Y + area.Height / 2);
            text.Replace("\r", "");
            string[] line = text.Split('\n');
            int lineHeight = 25;
            for (int i = 0; i < line.Length; i++)
            {
                Font ourFont = new Font(GlobalFont.GetFont().Name, fontSize);
                SizeF stringSize = e.Graphics.MeasureString(line[i], ourFont);
                int middleLineIndex = (int)Math.Round((double)line.Length / 2, MidpointRounding.AwayFromZero);
                int y = areaCenter.Y + (i + 1 - middleLineIndex) * lineHeight - lineHeight / 2;
                if (line.Length % 2 == 0)
                {
                    y -= lineHeight / 2;
                }
                e.Graphics.DrawString(line[i], ourFont, new SolidBrush(color), new Point((int)(areaCenter.X - stringSize.Width / 2), y));
            }
        }
    }
}
