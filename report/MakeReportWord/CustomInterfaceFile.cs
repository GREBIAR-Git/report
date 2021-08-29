﻿using System;
using System.IO;
using System.Windows.Forms;

namespace MakeReportWord
{
    public partial class CustomInterface
    {
        void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.wordkiller;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(openFileDialog.FileName, FileMode.Open);
                StreamReader reader = new StreamReader(file);
                try
                {
                    string data = reader.ReadToEnd();
                    for (int i = 1; i < data.Length; i++)
                    {
                        if (data[i - 1] == '\r')
                        {
                            data = data.Remove(i, 1);
                        }
                    }
                    string[] lines = data.Split('\r');

                    bool readingText = false;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].StartsWith("###textstart"))
                        {
                            readingText = true;
                        }
                        else if (readingText)
                        {
                            if (lines[i].StartsWith("###textend"))
                            {
                                readingText = false;
                            }
                            else
                            {
                                text += lines[i] + "\n";
                            }
                        }
                        else
                        {
                            string[] variable_value = lines[i].Split('=');
                            if (variable_value.Length == 2)
                            {
                                if (variable_value[0].StartsWith("facultyComboBox"))
                                {
                                    facultyComboBox.SelectedItem = variable_value[1];
                                }
                                else if (variable_value[0].StartsWith("numberLabTextBox.Text"))
                                {
                                    numberLabTextBox.Text = variable_value[1];
                                }
                                else if (variable_value[0].StartsWith("themeTextBox.Text"))
                                {
                                    themeTextBox.Text = variable_value[1];
                                }
                                else if (variable_value[0].StartsWith("disciplineTextBox.Text"))
                                {
                                    disciplineTextBox.Text = variable_value[1];
                                }
                                else if (variable_value[0].StartsWith("professorTextBox.Text"))
                                {
                                    professorComboBox.Text = variable_value[1];
                                }
                                else if (variable_value[0].StartsWith("yearTextBox.Text"))
                                {
                                    yearTextBox.Text = variable_value[1];
                                }
                                else if (variable_value[0].StartsWith("heading1ComboBox"))
                                {
                                    heading1ComboBox.Items.Add(variable_value[1]);
                                }
                                else if (variable_value[0].StartsWith("heading2ComboBox"))
                                {
                                    heading2ComboBox.Items.Add(variable_value[1]);
                                }
                                else if (variable_value[0].StartsWith("listComboBox"))
                                {
                                    listComboBox.Items.Add(variable_value[1]);
                                }
                                else if (variable_value[0].StartsWith("pictureComboBox"))
                                {
                                    pictureComboBox.Items.Add(variable_value[1]);
                                }
                                else if (variable_value[0].StartsWith("tableComboBox"))
                                {
                                    tableComboBox.Items.Add(variable_value[1]);
                                }
                                else if (variable_value[0].StartsWith("codeComboBox"))
                                {
                                    codeComboBox.Items.Add(variable_value[1]);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Файл повреждён");
                }
                reader.Close();
            }
        }

        void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "|*.wordkiller;";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = File.Open(saveFileDialog.FileName, FileMode.Create);
                StreamWriter output = new StreamWriter(fileStream);

                output.WriteLine("facultyComboBox=" + facultyComboBox.SelectedItem.ToString());
                output.WriteLine("numberLabTextBox.Text=" + numberLabTextBox.Text);
                output.WriteLine("themeTextBox.Text=" + themeTextBox.Text);
                output.WriteLine("disciplineTextBox.Text=" + disciplineTextBox.Text);
                output.WriteLine("professorTextBox.Text=" + professorComboBox.Text);
                output.WriteLine("yearTextBox.Text=" + yearTextBox.Text);
                SaveCombobox(output, heading1ComboBox);
                SaveCombobox(output, heading2ComboBox);
                SaveCombobox(output, listComboBox);
                SaveCombobox(output, pictureComboBox);
                SaveCombobox(output, tableComboBox);
                SaveCombobox(output, codeComboBox);

                output.WriteLine("###textstart");
                output.WriteLine(text);
                output.WriteLine("###textend");

                output.Close();
            }
        }

        void SaveCombobox(StreamWriter output, ComboBox comboBox)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                output.WriteLine(comboBox.Name + ".Items[" + i + "]=" + comboBox.Items[i].ToString());
            }
        }
    }
}