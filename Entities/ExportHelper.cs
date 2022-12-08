﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concatenar.Entities
{
    public class ExportHelper
    {
        public bool ExportarCSV(DataGridView dgv)
        {
            bool exported = false;

            List<string> lines = new List<string>();

            //header
            DataGridViewColumnCollection header = dgv.Columns;
            bool firstDone = false;
            StringBuilder headerLine = new StringBuilder();
            foreach (DataGridViewColumn col in header)
            {
                if (!firstDone)
                {
                    headerLine.Append(col.DataPropertyName);
                    firstDone = true;
                }
                else
                {
                    headerLine.Append(";" + col.DataPropertyName);
                }
            }

            lines.Add(headerLine.ToString());
            //data lines
            foreach (DataGridViewRow row in dgv.Rows)
            {
                StringBuilder dataLine = new StringBuilder();
                firstDone = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!firstDone)
                    {
                        dataLine.Append(cell.Value);
                        firstDone = true;
                    }
                    else
                    {
                        dataLine.Append(";" + cell.Value);
                    }
                }
                lines.Add(dataLine.ToString());
            }

            string dia = DateTime.Now.ToString("dd_MM_yyyy");
            string hora = DateTime.Now.ToString("HH_mm_ss");
            string file = @"\\paris\eng\Engenharia de Produto\Codificação de Itens\ITENS NOVOS\ESTRUTURA_PINTURA_" + hora + "_" + dia + ".csv";
            System.IO.File.WriteAllLines(file, lines);
            
            //System.Diagnostics.Process.Start(file); // comando para abrir o arquivo exportado.

            return exported;

            

        }
    }
}
