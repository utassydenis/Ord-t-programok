using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beadando
{
    public partial class Form1 : Form
    {
        Tuple<string, string, string> combo;
        string inputSzalag;
        int index;
        string szabaly;

        Stack<string> verem;
        public Form1()
        {
            
            InitializeComponent();
            warningLbl.Text = "";
        }

        private void inputOKBtn_Click(object sender, EventArgs e)
        {
            if (inputTxtBx.Text != "")
            {
                verem = new Stack<string>();
                verem.Push("#");
                verem.Push("E");
                index = 0;
                szabaly = "";
                string initialInput = inputTxtBx.Text;
                List<char> convertInput = new List<char>();
                for(int i = 0; i < initialInput.Length; i++)
                {
                    if (Char.IsDigit(initialInput[i]))
                    {
                        convertInput.Add('i');
                    }
                    else
                    {
                        convertInput.Add(initialInput[i]);
                    }
                }
                inputSzalag = new String(convertInput.ToArray());


                if (!inputSzalag.EndsWith("#"))
                {
                    inputSzalag += "#";
                }
                convertedInputTxtBx.Text = inputSzalag;
                combo = new Tuple<string, string, string>(inputSzalag, veremTartalom(), szabaly);
                stepsTxtBx.AppendText("\n"+combo+"\n");

                warningLbl.Text = "Input sikeresen átadva.";
            }
            else
            {
                warningLbl.Text = "Nincs input!";
            }

        }

        private void solveBtn_Click(object sender, EventArgs e)
        {
            if (inputSzalag != "" && pathTxtBx.Text != "")
            {
                int rowIndex = -1;
                while(index < inputSzalag.Length)
                {
                    int columnIndex = szabalyokDataGridView.Columns[inputSzalag[index].ToString()].Index;
                    foreach(DataGridViewRow row in szabalyokDataGridView.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(verem.Peek()))
                        {
                            rowIndex = row.Index;
                            break;
                        }
                    }
                    verem.Pop();
                    if (szabalyokDataGridView.Rows[rowIndex].Cells[columnIndex].Value == "")
                    {
                        stepsTxtBx.AppendText("Errorenous input!");
                        break;
                        
                    }
                    else if(szabalyokDataGridView.Rows[rowIndex].Cells[columnIndex].Value.ToString() == "pop")
                    {
                        index++;
                    }
                    else if(szabalyokDataGridView.Rows[rowIndex].Cells[columnIndex].Value.ToString() == "accept")
                    {
                        stepsTxtBx.AppendText("Accepted");
                        break;
                    }
                    else
                    {
                        string result = szabalyokDataGridView.Rows[rowIndex].Cells[columnIndex].Value.ToString();
                        result = result.Substring(1, result.Length - 2);
                        string[] parts = result.Split(',');
                        if (parts[0].ToString().Equals("eps"))
                        {
                            szabaly += parts[1].ToString();
                        }
                        else
                        {
                            for (int k = parts[0].Length - 1; k > -1; k--)
                            {
                                if (parts[0][k].ToString().Equals("'"))
                                {
                                    verem.Push(String.Concat(parts[0][k - 1], parts[0][k]));
                                    k--;
                                }
                                else
                                {
                                    verem.Push(parts[0][k].ToString());
                                }
                            }
                            szabaly += parts[1].ToString();
                        }
                        combo = new Tuple<string, string, string>(inputSzalag.Substring(index), veremTartalom(), szabaly);
                        stepsTxtBx.AppendText(combo + "\n");
                    }
                }
            }
            else
            {
                warningLbl.Text = "Üres a szalag vagy nincs fájl!";
            }

        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            szabalyokDataGridView.DataSource = null;
            szabalyokDataGridView.Rows.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName.EndsWith(".csv"))
            {
                pathTxtBx.Text = openFileDialog.FileName;
                try
                {
                    StreamReader sr = new StreamReader(pathTxtBx.Text);
                    string firstRow = sr.ReadLine().Replace(";", "");
                    szabalyokDataGridView.ColumnCount = firstRow.Length+1;
                    szabalyokDataGridView.Columns[0].Name = " "; 
                    for (int i = 1; i < szabalyokDataGridView.ColumnCount; i++)
                    {
                        szabalyokDataGridView.Columns[i].Name = firstRow[i-1].ToString();
                    }
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] cell = line.Split(';');
                        szabalyokDataGridView.Rows.Add(cell);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    throw new FileNotFoundException("Nem létezik a file.");
                    warningLbl.Text = "Nem létezik a file!";
                }
            }
            else
            {
                warningLbl.Text = "Nem jó formátum.";
            }
        }
        private string veremTartalom()
        {
            string tartalom = "";
            foreach(string c in verem)
            {
                tartalom += c;
            }
            return tartalom;
        }

    }
}
