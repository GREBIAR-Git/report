﻿using System;
using System.Collections.Generic;
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
                ClearGlobal();
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
                    List<Control> controls = new List<Control>();
                    foreach(string line in lines)
                    { 
                        if (line.StartsWith("☺TextStart☺"))
                        {
                            readingText = true;
                        }
                        else if (readingText)
                        {
                            if (line.StartsWith("☺TextEnd☺"))
                            {
                                readingText = false;
                            }
                            else
                            {
                                text += line + "\n";
                            }
                        }
                        else
                        {
                            string[] variable_value = line.Split('☺');
                            if(variable_value.Length == 2)
                            {
                                if (variable_value[0].StartsWith("TypeMenuItem"))
                                {
                                    work_Click(TypeMenuItem.DropDown.Items.Find(variable_value[1], false)[0], e);
                                    foreach (Control control in titlepagePanel.Controls)
                                    {
                                        controls.Add(control);
                                    }
                                }
                                for (int i=0;i<controls.Count;i++)
                                {
                                    if (LoadingOfTwo(variable_value, controls[i]))
                                    {
                                        controls.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                            else if (variable_value.Length == 3)
                            {
                                if (variable_value[0].StartsWith("h1ComboBox"))
                                {
                                    h1ComboBox.Items.Add(variable_value[1]);
                                    string[] str = new string[] { variable_value[1], variable_value[2]};
                                    dataComboBox.ComboBoxH1.Add(str);
                                }
                                else if (variable_value[0].StartsWith("h2ComboBox"))
                                {
                                    h2ComboBox.Items.Add(variable_value[1]);
                                    string[] str = new string[] { variable_value[1], variable_value[2] };
                                    dataComboBox.ComboBoxH2.Add(str);
                                }
                                else if (variable_value[0].StartsWith("lComboBox"))
                                {
                                    lComboBox.Items.Add(variable_value[1]);
                                    string[] str = new string[] { variable_value[1], variable_value[2] };
                                    dataComboBox.ComboBoxL.Add(str);
                                }
                                else if (variable_value[0].StartsWith("pComboBox"))
                                {
                                    pComboBox.Items.Add(variable_value[1]);
                                    string[] str = new string[] { variable_value[1], variable_value[2] };
                                    dataComboBox.ComboBoxP.Add(str);
                                }
                                else if (variable_value[0].StartsWith("tComboBox"))
                                {
                                    tComboBox.Items.Add(variable_value[1]);
                                    string[] str = new string[] { variable_value[1], variable_value[2] };
                                    dataComboBox.ComboBoxT.Add(str);
                                }
                                else if (variable_value[0].StartsWith("cComboBox"))
                                {
                                    cComboBox.Items.Add(variable_value[1]);
                                    string[] str = new string[] { variable_value[1], variable_value[2] };
                                    dataComboBox.ComboBoxC.Add(str);
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

        bool LoadingOfTwo(string[] variable_value,Control control)
        {
            if (variable_value[0].StartsWith(control.Name))
            {
                this.Controls.Find(control.Name, true)[0].Text = variable_value[1];
                return true;
            }
            return false;
        }

        void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "|*.wordkiller;";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = System.IO.File.Open(saveFileDialog.FileName, FileMode.Create);
                StreamWriter output = new StreamWriter(fileStream);
                foreach (ToolStripMenuItem item in TypeMenuItem.DropDown.Items)
                {
                    if (item.Checked)
                    {
                        output.WriteLine("TypeMenuItem☺" + item.Name.ToString());
                    }
                }
                output.WriteLine("facultyComboBox☺" + facultyComboBox.Text);
                output.WriteLine("numberTextBox☺" + numberTextBox.Text);
                output.WriteLine("themeTextBox☺" + themeTextBox.Text);
                output.WriteLine("disciplineTextBox☺" + disciplineTextBox.Text);
                output.WriteLine("professorComboBox☺" + professorComboBox.Text);
                output.WriteLine("yearTextBox☺" + yearTextBox.Text);
                output.WriteLine("shifrTextBox☺" + shifrTextBox.Text);
                output.WriteLine("studentsTextBox☺" + studentsTextBox.Text);
                SaveCombobox(output, h1ComboBox, dataComboBox.ComboBoxH1);
                SaveCombobox(output, h2ComboBox, dataComboBox.ComboBoxH2);
                SaveCombobox(output, lComboBox, dataComboBox.ComboBoxL);
                SaveCombobox(output, pComboBox, dataComboBox.ComboBoxP);
                SaveCombobox(output, tComboBox, dataComboBox.ComboBoxT);
                SaveCombobox(output, cComboBox, dataComboBox.ComboBoxC);

                output.WriteLine("☺TextStart☺");
                output.WriteLine(text);
                output.WriteLine("☺TextEnd☺");

                output.Close();
            }
        }

        void SaveCombobox(StreamWriter output, ComboBox comboBox, List<string[]> Lstr)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                output.WriteLine(comboBox.Name + "☺" + comboBox.Items[i].ToString() + "☺" + Lstr[i][1]);
            }
        }

        void ClearGlobal()
        {
            dataComboBox = new DataComboBox();
            for (int i = elementPanel.ColumnCount - 1; i < elementPanel.Controls.Count - 1; i++)
            {
                ComboBox cmbBox = (ComboBox)elementPanel.Controls[i];
                cmbBox.Items.Clear();
            }
        }
    }
}
