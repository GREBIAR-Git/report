﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace WordKiller
{
    public partial class CustomInterface
    {
        void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.wkr;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenWordKiller(openFileDialog.FileName);
            }
        }

        void OpenWordKiller(string fileName)
        {
            data.Text = String.Empty;
            saveFileName = fileName;
            ClearGlobal();
            FileStream file = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            try
            {
                string text = reader.ReadToEnd();
                if (text[0] == '1' && text[1] == '\r' && text[2] == '\n')
                {
                    text = EncodingDecoding.MegaConvertD(text.Substring(3));
                }
                else if (text[0] == '0' && text[1] == '\r' && text[2] == '\n')
                {
                    text = text.Substring(3);
                    text = text.Replace("\n", "\r\n");
                }
                for (int i = 1; i < text.Length; i++)
                {
                    if (text[i - 1] == '\r')
                    {
                        text = text.Remove(i, 1);
                    }
                }
                string[] lines = text.Split('\r');

                bool readingText = false;
                List<Control> controls = new List<Control>();
                foreach (string line in lines)
                {
                    if (line.StartsWith(AddSpecialСharacterAB("Menu")))
                    {
                        string[] menuItem = line.Remove(0, 6).Split('!');
                        Work_Click(TypeMenuItem.DropDown.Items.Find(menuItem[0], false)[0], new EventArgs());
                        if (menuItem[0] != "DefaultDocumentMenuItem")
                        {
                            foreach (Control control in titlepagePanel.Controls)
                            {
                                if (control.GetType().ToString() != "System.Windows.Forms.Label")
                                {
                                    controls.Add(control);
                                }
                            }
                        }
                        NumberHeadingMenuItem.Checked = bool.Parse(menuItem[1]);
                    }

                    if (line.StartsWith(AddSpecialСharacterAB("TextStart")))
                    {
                        readingText = true;
                    }
                    else if (readingText)
                    {
                        if (line.StartsWith(AddSpecialСharacterAB("TextEnd")))
                        {
                            readingText = false;
                        }
                        else
                        {
                            data.Text += line + "\n";
                        }
                    }
                    else
                    {
                        string[] variable_value = line.Split(new char[] { specialBefore, specialAfter });
                        if (variable_value.Length == 2)
                        {
                            for (int i = 0; i < controls.Count; i++)
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
                            foreach (KeyValuePair<string, ElementComboBox> comboBox in data.ComboBox)
                            {
                                if (variable_value[0].StartsWith(comboBox.Key + "ComboBox"))
                                {
                                    comboBox.Value.Form.Items.Add(variable_value[1]);
                                    string[] str = new string[] { variable_value[1], variable_value[2] };
                                    comboBox.Value.Data.Add(str);
                                    break;
                                }
                            }
                        }
                    }
                    this.Text = Path.GetFileName(fileName);
                }
                if (data.Text.Length > 0)
                {
                    data.Text = data.Text.Remove(data.Text.Length - 1);
                }
            }
            catch
            {
                MessageBox.Show("Файл повреждён");
            }
            if (DownPanelMI == TextMenuItem)
            {
                richTextBox.Text = data.Text;
                UpdateTypeButton();
            }
            reader.Close();
        }

        bool LoadingOfTwo(string[] variable_value, Control control)
        {
            if (variable_value[0].StartsWith(control.Name))
            {
                control.Text = variable_value[1];
                return true;
            }
            return false;
        }

        void Save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(saveFileName))
            {
                SaveWordKiller(saveFileName);
            }
            else
            {
                SaveAsMenuItem_Click(sender, e);
            }
        }

        void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "|*.wkr;";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveFileDialog.FileName;
                SaveWordKiller(saveFileName);
            }
        }

        void SaveWordKiller(string nameFile)
        {
            FileStream fileStream = System.IO.File.Open(nameFile, FileMode.Create);
            StreamWriter output = new StreamWriter(fileStream);
            string save = string.Empty;
            foreach (ToolStripItem item in TypeMenuItem.DropDown.Items)
            {
                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    if (((ToolStripMenuItem)item).Checked)
                    {
                        save += AddSpecialСharacterAB("Menu") + item.Name.ToString() + "!" + NumberHeadingMenuItem.Checked.ToString() + "\n";
                    }
                }
            }
            save += AddSpecialСharacterA("facultyComboBox") + facultyComboBox.Text + "\n";
            save += AddSpecialСharacterA("numberTextBox") + numberTextBox.Text + "\n";
            save += AddSpecialСharacterA("themeTextBox") + themeTextBox.Text + "\n";
            save += AddSpecialСharacterA("disciplineTextBox") + disciplineTextBox.Text + "\n";
            save += AddSpecialСharacterA("professorComboBox") + professorComboBox.Text + "\n";
            save += AddSpecialСharacterA("yearTextBox") + yearTextBox.Text + "\n";
            save += AddSpecialСharacterA("shifrTextBox") + shifrTextBox.Text + "\n";
            save += AddSpecialСharacterA("studentsTextBox") + studentsTextBox.Text + "\n";
            foreach (KeyValuePair<string, ElementComboBox> comboBox in data.ComboBox)
            {
                save += SaveCombobox(output, comboBox.Value, comboBox.Key);
            }
            save += AddSpecialСharacterAB("TextStart") + "\n";
            save += data.Text + "\n";
            save += AddSpecialСharacterAB("TextEnd") + "\n";
            if (Encoding0MenuItem.Checked)
            {
                output.Write("0\r\n" + save);
            }
            else if (Encoding1MenuItem.Checked)
            {
                output.Write("1\r\n" + EncodingDecoding.MegaConvertE(save));
            }
            output.Close();
            this.Text = Path.GetFileName(nameFile);

            ShowSaveLogo();
        }
        void ShowSaveLogo()
        {
            saveLogoVisible = true;
            this.saveTimer.Stop();
            this.saveTimer.Start();
            this.menuStrip.Refresh();
        }

        void HideSaveLogo(Object source, ElapsedEventArgs e)
        {
            saveLogoVisible = false;
            this.saveTimer.Stop();
            this.menuStrip.Invoke((MethodInvoker)delegate
            {
                this.menuStrip.Refresh();
            });
        }

        string SaveCombobox(StreamWriter output, ElementComboBox comboBox, string name)
        {
            string comboBoxSave = string.Empty;
            for (int i = 0; i < comboBox.Form.Items.Count; i++)
            {
                comboBoxSave += name + "ComboBox" + AddSpecialСharacterAB(comboBox.Form.Items[i].ToString()) + comboBox.Data[i][1] + "\n";
            }
            return comboBoxSave;
        }

        void ClearGlobal()
        {
            data = new DataComboBox(h1ComboBox, h2ComboBox, lComboBox, pComboBox, tComboBox, cComboBox);
            for (int i = elementPanel.ColumnCount - 1; i < elementPanel.Controls.Count - 1; i++)
            {
                ComboBox cmbBox = (ComboBox)elementPanel.Controls[i];
                cmbBox.Items.Clear();
            }
        }

        bool NeedSave()
        {
            DialogResult result = MessageBox.Show("Нужно ли сохранить?", "Нужно ли сохранить?", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(saveFileName))
                {
                    SaveWordKiller(saveFileName);
                }
                else
                {
                    SaveAsMenuItem_Click(0, new EventArgs());
                }
                return true;
            }
            return false;
        }
    }
}
