﻿using System;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace MakeReportWord
{
    class MakeReport
    {
        Document doc;
        Range word;
        bool pgBreak = false;
        char special = '☺';

        public void CreateReport(DataComboBox dataMainPart, bool numbering, bool content,bool title, int fromNumbering, bool numberHeading, string typeDocument, string[] dataTitle)
        {
            Beginning();
            if(title)
            {
                TitlePart(typeDocument, dataTitle);
            }
            MainPart(dataMainPart, content, numbering, fromNumbering, numberHeading);
        }

        void Beginning()
        {
            Application app = new Application();
            app.Visible = true;
            doc = app.Documents.Add();
            word = null;
        }

        void TitlePart(string typeDocument,string[] dataTitle)
        {
            Ministry(dataTitle[0]);
            if (typeDocument == "Сотворение лабораторной работы из небытия")
            {
                string text = "ОТЧЁТ";
                WriteTextWord(text);
                word.Font.Size = 16;
                word.Font.Bold = 1;

                text = "По лабораторной работе №" + dataTitle[1];
                WriteTextWord(text);
                word.Paragraphs.SpaceAfter = 10;
                word.Font.Bold = 0;

                text = "на тему: «" + dataTitle[2] + "»" + SkipLine(1) + "по дисциплине: «" + dataTitle[3] + "»";
                WriteTextWord(text);
                word.Font.Size = 14;
                word.Paragraphs.SpaceAfter = 0;

                SkipLinesSingle(8);

                text = "Выполнили: Музалевский Н.С., Аллянов М.Д." + SkipLine(1) +
                    "Институт приборостроения, автоматизации и информационных технологий" + SkipLine(1) +
                    "Направление: 09.03.04 «Программная инженерия»" + SkipLine(1) +
                    "Группа: 92ПГ";
                WriteTextWord(text);
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                text = "Проверил: " + dataTitle[4];
                WriteTextWord(text);
                word.Paragraphs.SpaceAfter = 10;

                SkipLinesSingle(1);

                text = "Отметка о зачёте: ";
                WriteTextWord(text);
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                word.Paragraphs.SpaceAfter = 0;

                text = "Дата: «____» __________ " + dataTitle[5] + "г.";
                WriteTextWord(text);
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

                SkipLinesSingle(8);
            }
            else if (typeDocument == "Сотворение практической работы из небытия")
            {
                string text = "ОТЧЁТ";
                WriteTextWord(text);
                word.Font.Size = 16;
                word.Font.Bold = 1;

                text = "По практической работе №" + dataTitle[1];
                WriteTextWord(text);
                word.Paragraphs.SpaceAfter = 10;
                word.Font.Bold = 0;

                text = "на тему: «" + dataTitle[2] + "»" + SkipLine(1) + "по дисциплине: «" + dataTitle[3] + "»";
                WriteTextWord(text);
                word.Font.Size = 14;
                word.Paragraphs.SpaceAfter = 0;

                SkipLinesSingle(8);

                text = "Выполнили: Музалевский Н.С., Аллянов М.Д." + SkipLine(1) +
                    "Институт приборостроения, автоматизации и информационных технологий" + SkipLine(1) +
                    "Направление: 09.03.04 «Программная инженерия»" + SkipLine(1) +
                    "Группа: 92ПГ";
                WriteTextWord(text);
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                text = "Проверил: " + dataTitle[4];
                WriteTextWord(text);
                word.Paragraphs.SpaceAfter = 10;

                SkipLinesSingle(1);

                text = "Отметка о зачёте: ";
                WriteTextWord(text);
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                word.Paragraphs.SpaceAfter = 0;

                text = "Дата: «____» __________ " + dataTitle[5] + "г.";
                WriteTextWord(text);
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

                SkipLinesSingle(8);
            }
            else if (typeDocument == "Сотворение курсовой работы из небытия")
            {
                string text = "Работа допущена к защите" + SkipLine(1) + "______________Руководитель" + SkipLine(1) + "«____»_____________20___г.";
                WriteTextWord(text);
                word.Font.Size = 14;
                word.Font.Bold = 0;
                word.Paragraphs.LeftIndent = CentimetersToPoints(9.5f);
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                word.Paragraphs.LineSpacingRule = WdLineSpacing.wdLineSpace1pt5;

                text = SkipLine(3) + "КУРСОВАЯ РАБОТА" + SkipLine(1);
                WriteTextWord(text);
                word.Font.Size = 14;
                word.Font.Bold = 1;
                word.Paragraphs.LeftIndent = 0;
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                word.Paragraphs.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;

                text = "по дисциплине: «" + dataTitle[1] + "»";
                WriteTextWord(text);
                word.Font.Size = 14;
                word.Font.Bold = 0;
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                word.Paragraphs.LineSpacingRule = WdLineSpacing.wdLineSpaceDouble;

                text = "на тему: «" + dataTitle[2] + "»";
                WriteTextWord(text);
                word.Paragraphs.LineSpacingRule = WdLineSpacing.wdLineSpace1pt5;

                SkipLinesSingle(2);

                text = "Студент _________________" + dataTitle[3] + SkipLine(1) +
                    "Шифр " + dataTitle[4] + SkipLine(1) +
                    "Институт приборостроения, автоматизации и информационных технологий" + SkipLine(1) +
                    "Направление: 09.03.04 «Программная инженерия»" + SkipLine(1) +
                    "Группа: 92ПГ";
                WriteTextWord(text);
                word.Paragraphs.LineSpacingRule = WdLineSpacing.wdLineSpace1pt5;
                word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                text = "Руководитель __________________" + dataTitle[5];
                WriteTextWord(text);
                word.Paragraphs.SpaceAfter = 12;

                text = "Оценка: «________________»               Дата ______________";
                WriteTextWord(text);
                word.Paragraphs.SpaceAfter = 0;

                SkipLinesSingle(2);
            }
            else if (typeDocument == "Сотворение реферата из небытия")
            {
            }
            else if (typeDocument == "Сотворение дипломной работы из небытия")
            {
            }
            else if (typeDocument == "Сотворение ВКР из небытия")
            {
            }
            else if (typeDocument == "Сотворение РГР из небытия")
            {
            }
            Orel(dataTitle[dataTitle.Length-1]);
        }

        void Ministry(string faculty)
        {
            string text = "МИНИСТЕРСТВО НАУКИ И ВЫСШЕГО ОБРАЗОВАНИЯ" + SkipLine(1) +
            "РОССИЙСКОЙ ФЕДЕРАЦИИ" + SkipLine(1) +
            "ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ" + SkipLine(1) +
            "ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ" + SkipLine(1) +
            "«ОРЛОВСКИЙ ГОСУДАРСТВЕННЫЙ УНИВЕРСИТЕТ" + SkipLine(1) +
            "ИМЕНИ И.С.ТУРГЕНЕВА»";
            WriteTextWord(text);
            word.Font.Size = 14;
            word.Font.Name = "Times New Roman";
            word.Paragraphs.SpaceAfter = 0;
            word.Paragraphs.Space1();
            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            SkipLinesSingle(2);
            text = "Кафедра " + faculty;
            WriteTextWord(text);
            SkipLinesSingle(3);
            PageMargin(2, 2, 3, 1.5f);
        }

        void Orel(string year)
        {
            string text = "Орел, " + year;
            WriteTextWord(text);
            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            word.Paragraphs.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
            PageBreak();
        }

        void MainPart(DataComboBox content, bool cont, bool numbering, int fromNumbering, bool numberHeading)
        {
            if (cont)
            {
                //Сделать содержание
            }
            if (content.Text != null)
            {
                if(numberHeading)
                {
                    ProcessContent(content);
                }
                else
                {
                    ProcessContentwithoutNumber(content);
                }
            }
            if (numbering)
            {
                if (fromNumbering > 0)
                {
                    HeaderFooter hf = doc.Sections[doc.Sections.First.Index].Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
                    if (fromNumbering != 1)
                    {
                        Range range = doc.Range().GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, fromNumbering);
                        doc.Sections.Add(range, WdSectionStart.wdSectionContinuous);
                        hf = doc.Sections[doc.Sections.Count].Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
                        hf.LinkToPrevious = false;
                        hf.PageNumbers.RestartNumberingAtSection = false;
                    }
                    WdPageNumberAlignment alignment = WdPageNumberAlignment.wdAlignPageNumberCenter;
                    hf.PageNumbers.Add(alignment, true);
                }
            }
        }

        void ProcessContent(DataComboBox content)
        {
            int h1 = 1;
            int h2 = 1;
            int h2all = 1;
            int l = 1;
            int p = 1;
            int t = 1;
            int c = 1;
            string def = string.Empty;
            for (int i = 0; i<content.Text.Length;i++)
            {
                if(content.Text[i]==special)
                {
                    if(def!=string.Empty)
                    {
                        DefaultText(def);
                        def = string.Empty;
                    }
                    if (content.Text[i+1] == 'h')
                    {
                        if (content.Text[i + 2] == '1')
                        {
                            i += 2;
                            string text = h1.ToString() + " " + ProcessSpecial(h1,"h1", content)[0];
                            Heading1(text);
                            h1++;
                            h2 = 1;
                        }
                        else if (content.Text[i + 2] == '2')
                        {
                            i += 2;
                            string text = (h1-1).ToString() + "." + h2.ToString() + " " + ProcessSpecial(h2all,"h2", content)[0];
                            Heading2(text);
                            h2all++;
                            h2++;
                        }
                    }
                    else if (content.Text[i + 1] == 'l')
                    {
                        i += 1;
                        string[] text = ProcessSpecial(l, "l", content);
                        List(text[0]);
                        l++;
                    }
                    else if (content.Text[i + 1] == 'p')
                    {
                        i += 1;
                        string[] text = ProcessSpecial(p, "p", content);
                        Picture(text[0]);
                        СaptionForPicture("Рисунок " + p + " – " + text[1]);
                        p++;
                    }
                    else if (content.Text[i + 1] == 't')
                    {
                        i += 1;
                        string[] text = ProcessSpecial(t, "t", content);
                        Table(text[0]);
                        t++;
                    }
                    else if (content.Text[i + 1] == 'c')
                    {
                        i += 1;
                        string[] text = ProcessSpecial(c, "c", content);
                        Code(text[0]);
                        c++;
                    }
                }
                else
                {
                    def += content.Text[i];
                }
            }
            if (def != string.Empty)
            {
                DefaultText(def);
                def = string.Empty;
            }
        }

        void ProcessContentwithoutNumber(DataComboBox content)
        {
            int h1 = 1;
            int h2all = 1;
            int l = 1;
            int p = 1;
            int t = 1;
            int c = 1;
            string def = string.Empty;
            for (int i = 0; i < content.Text.Length; i++)
            {
                if (content.Text[i] == special)
                {
                    if (def != string.Empty)
                    {
                        DefaultText(def);
                        def = string.Empty;
                    }
                    if (content.Text[i + 1] == 'h')
                    {
                        if (content.Text[i + 2] == '1')
                        {
                            i += 2;
                            string text = ProcessSpecial(h1, "h1", content)[0];
                            Heading1(text);
                            h1++;
                        }
                        else if (content.Text[i + 2] == '2')
                        {
                            i += 2;
                            string text = ProcessSpecial(h2all, "h2", content)[0];
                            Heading2(text);
                            h2all++;
                        }
                    }
                    else if (content.Text[i + 1] == 'l')
                    {
                        i += 1;
                        string[] text = ProcessSpecial(l, "l", content);
                        List(text[0]);
                        l++;
                    }
                    else if (content.Text[i + 1] == 'p')
                    {
                        i += 1;
                        string[] text = ProcessSpecial(p, "p", content);
                        Picture(text[0]);
                        СaptionForPicture("Рисунок " + p + " – " + text[1]);
                        p++;
                    }
                    else if (content.Text[i + 1] == 't')
                    {
                        i += 1;
                        string[] text = ProcessSpecial(t, "t", content);
                        Table(text[0]);
                        t++;
                    }
                    else if (content.Text[i + 1] == 'c')
                    {
                        i += 1;
                        string[] text = ProcessSpecial(c, "c", content);
                        Code(text[0]);
                        c++;
                    }
                }
                else
                {
                    def += content.Text[i];
                }
            }
            if (def != string.Empty)
            {
                DefaultText(def);
                def = string.Empty;
            }
        }

        string[] ProcessSpecial(int i, string special, DataComboBox content)
        {
            string[] text = new string[2];
            if (special == "h1")
            {
                text[0] = content.ComboBoxH1[i - 1][1];
            }
            else if (special == "h2")
            {
                text[0] = content.ComboBoxH2[i - 1][1];
            }
            else if (special == "l")
            {
                text[0] = content.ComboBoxL[i - 1][1];
            }
            else if (special == "p")
            {
                text[0] = content.ComboBoxP[i - 1][1];
                text[1] = content.ComboBoxP[i - 1][0];
            }
            else if (special == "t")
            {
                text[0] = content.ComboBoxT[i - 1][1];
            }
            else if (special == "c")
            {
                text[0] = content.ComboBoxC[i - 1][1];
            }
            return text;
        }

        void WriteTextWord(string text)
        {
            
            Range wordTemp = doc.Range();
            int Length = wordTemp.Text.Length;
            if (word == null)
            {
                word = doc.Range();
                word.Text = text;
            }
            else
            {
                word.Text += text;
            }
            if(pgBreak)
            {
                word = doc.Range(Length, Type.Missing);
            }
            else
            {
                word = doc.Range(Length-1, Type.Missing);
                pgBreak = true;
            }
        }

        void PageBreak()
        {
            if (pgBreak)
            {
                pgBreak = false;
                Range word1 = doc.Range();
                int Length = word1.Text.Length;
                word = doc.Range(Length - 1, Type.Missing);
                word.InsertBreak(0);
                word.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            }
        }

        void DefaultText(string text)
        {
            WriteTextWord(text);
            word.Font.AllCaps = 0;
            word.Font.Size = 14;
            word.Font.Bold = 0;
            word.Font.ColorIndex = 0;
            word.Paragraphs.Space15();
            word.Paragraphs.FirstLineIndent = CentimetersToPoints(1.5f);
            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
        }

        void Heading1(string text)
        {
            PageBreak();
            WriteTextWord(text);
            word.Paragraphs.FirstLineIndent = CentimetersToPoints(1.5f);
            word.Font.Size = 14;
            word.Font.Bold = 1;
            word.Font.AllCaps = 1;
            word.Font.ColorIndex = 0;
            word.Paragraphs.Space15();
            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        }

        void Heading2(string text)
        {
            WriteTextWord(SkipLine(1)+text);
            word.Paragraphs.FirstLineIndent = CentimetersToPoints(1.5f);
            word.Font.AllCaps = 0;
            word.Font.Size = 14;
            word.Font.Bold = 1;
            word.Font.ColorIndex = 0;
            word.Paragraphs.Space15();
            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        }

        void List(string text)
        {

        }

        void Picture(string text)
        {
            WriteTextWord("");
            object f = false;
            object t = true;
            var Picture = word.InlineShapes.AddPicture(text, ref f, ref t);
            Picture.Height = 350;
            Picture.Width = 420;

            word.Paragraphs.Space15();
            word.Paragraphs.FirstLineIndent = 0;
            word.Font.AllCaps = 0;
            word.Font.ColorIndex = 0;

            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            Range wordTemp = doc.Range();
            int Length = wordTemp.Text.Length;
            word = doc.Range(Length-1, Type.Missing);
        }

        void СaptionForPicture(string text)
        {
            WriteTextWord(text);
            word.Paragraphs.FirstLineIndent = 0;
            word.Font.AllCaps = 0;
            word.Font.Size = 14;
            word.Font.Bold = 0;
            word.Font.ColorIndex = 0;
            word.Paragraphs.SpaceAfter= 8;
            word.Paragraphs.Space15();
            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        }

        void Table(string text)
        {

        }

        void Code(string text)
        {
            FileStream file = new FileStream(text, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string data = reader.ReadToEnd();
            Heading1(text.Split('\\')[text.Split('\\').Length - 1] + SkipLine(1));
            WriteTextWord(data);
            word.Paragraphs.FirstLineIndent = CentimetersToPoints(0f);
            word.Font.AllCaps = 0;
            word.Font.Size = 12;
            word.Font.Bold = 0;
            word.Font.ColorIndex = 0;
            word.Paragraphs.Space1();
            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
        }

        void PageMargin(float top, float bottom, float left, float right)
        {
            word.PageSetup.TopMargin = CentimetersToPoints(top);
            word.PageSetup.BottomMargin = CentimetersToPoints(bottom);
            word.PageSetup.LeftMargin = CentimetersToPoints(left);
            word.PageSetup.RightMargin = CentimetersToPoints(right);
        }

        float CentimetersToPoints(float cen)
        {
            return cen * 28.3465f;
        }

        void SkipLinesSingle(int number)
        {
            if (word == null)
            {
                return;
            }
            string text = SkipLine(number-1);
            WriteTextWord(text);
            word.Paragraphs.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
            word.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            word.Paragraphs.SpaceAfter = 0;
        }

        string SkipLine(int quantity)
        {
            var str = string.Empty;
            for (var i = 0; i < quantity; i++)
            {
                str += Environment.NewLine;
            }
            return str;
        }
    }
}
