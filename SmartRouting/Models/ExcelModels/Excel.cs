using Microsoft.Office.Interop.Excel;
namespace SmartRouting.Models.ExcelModels
{
    public class Excel
    {
        public string Path { get; set; }
        public _Application Application = new Application();
        public Microsoft.Office.Interop.Excel.Range Range { get; set; }

        Workbook wb { get; set; }
        Worksheet ws { get; set; }

        public Excel(string path,int sheet)
        {
            Path = path;
            wb = Application.Workbooks.Open(Path);
            ws = (Worksheet)wb.Worksheets[sheet];
            Range = ws.UsedRange;
        }

        public string ReadCell(int i,int j)
        {
            if (ws.Cells[i,j].value2 != null)
            {
                return ws.Cells[i,j].value2;
            }
            return string.Empty;
        }

        public double? ReadIntCell(int i,int j)
        {
            if (ws.Cells[i,j].value2 != null)
            {
                return ws.Cells[i, j].value2;
            }
            return null;
        }

        public void Close()
        {
            wb.Close();
        }
    }
}
