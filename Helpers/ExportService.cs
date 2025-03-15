using ClosedXML.Excel;
using LabManagement.Models;
using Microsoft.JSInterop;

namespace LabManagement.Helpers
{
    public class ExportService
    {
        private readonly IJSRuntime _jsRuntime;

        public ExportService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ExportToExcelAsync(List<UserInfo> items)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Data");

            // Add headers
            worksheet.Cell(1, 1).Value = "User ID";
            worksheet.Cell(1, 2).Value = "User Name";
            // Add more headers as needed

            // Add data
            for (int i = 0; i < items.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = items[i].UserID;
                worksheet.Cell(i + 2, 2).Value = items[i].UserName;
                // Add more properties as needed
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            var content = new byte[stream.Length];
            await stream.ReadAsync(content, 0, (int)stream.Length);

            await _jsRuntime.InvokeVoidAsync("saveAsFile", "Data.xlsx", Convert.ToBase64String(content));
        }

     

    }
}
