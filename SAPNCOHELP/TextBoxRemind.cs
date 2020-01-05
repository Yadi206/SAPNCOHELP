using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


class TextBoxRemind
{
    public int RemLines = 100;
    private string[] array = null;
    public void InitAutoCompleteCustomSource(ToolStripTextBox textBox)
    {
        array = ReadTxt();
        if (array != null && array.Length > 0)
        {
            AutoCompleteStringCollection ACSC = new AutoCompleteStringCollection();
            for (int i = 0; i < array.Length; i++)
            {
                ACSC.Add(array[i]);
            }
            textBox.AutoCompleteCustomSource = ACSC;
        }
    }
    string[] ReadTxt()
    {
        try
        {
            if (!File.Exists("Remind.txt"))
            {
                FileStream fs =
                    File.Create("Remind.txt");
                fs.Close();
                fs = null;
            }
            return File.ReadAllLines("Remind.txt", Encoding.Default);
        }
        catch
        {
            return null;
        }
    }
    public void Remind(string str)
    {
        // StreamReader reader = File.OpenText("Remind.txt");


        string[] lines_array = File.ReadAllLines("Remind.txt").ToArray();
        int lines = lines_array.Count();
        lines--;

        StreamWriter writer = null;
        try
        {
            if (array != null && !array.Contains(str))
            {
                writer = new StreamWriter("Remind.txt", false, Encoding.Default);

                for (int i = RemLines-2; i >= 0; i--)
                {
                    if (lines < 0)
                    {
                        break;
                    }
                    writer.WriteLine(lines_array[lines]);
                    lines--;
                }
                writer.WriteLine(str);
                 
                //    lines_array[4] = str;
                // writer.WriteLine(str);
            }
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }
        }
    }
}
