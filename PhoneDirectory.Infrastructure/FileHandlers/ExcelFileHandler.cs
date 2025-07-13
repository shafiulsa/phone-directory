using OfficeOpenXml;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Infrastructure.FileHandlers;

public class ExcelFileHandler
{
    public List<Contact> ParseExcel(Stream stream)
    {
        var contacts = new List<Contact>();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[0];
        int rowCount = worksheet.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++)
        {
            contacts.Add(new Contact
            {
                Name = worksheet.Cells[row, 1].Text,
                Email = worksheet.Cells[row, 2].Text,
                PhoneNumber = worksheet.Cells[row, 3].Text,
                Balance = decimal.TryParse(worksheet.Cells[row, 4].Text, out var balance) ? balance : 0,
                Address = worksheet.Cells[row, 5].Text,
                Group = worksheet.Cells[row, 6].Text,
                Status = true
            });
        }
        return contacts;
    }
}