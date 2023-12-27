using System;
using System.Linq;
using System.Threading.Tasks;
using desktop.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace desktop.Services;

public class TeacherReportService : IReportService
{
  private readonly IFilePickerService _filePickerService;

  public TeacherReportService(IFilePickerService filePickerService)
  {
    _filePickerService = filePickerService;
  }

  public async Task SaveDocument(TeacherStatistic[] teacherStatistics, DateRange dateRange)
  {
    Uri saveFile = await _filePickerService.SaveFile(IFilePickerService.Filter.Docx);
    if (saveFile == null)
      return;
    using (WordprocessingDocument wordprocessingDocument =
      WordprocessingDocument.Create(saveFile.AbsolutePath, WordprocessingDocumentType.Document))
    {
      var mainPart = wordprocessingDocument.AddMainDocumentPart();
      mainPart.Document = new Document();
      Body body = new Body();
      mainPart.Document.Append(body);
      Paragraph paragraph = new Paragraph();
      ParagraphProperties paragraphProperties = new ParagraphProperties();
      paragraphProperties.AddChild(new Justification() {Val = JustificationValues.Center});
      paragraph.PrependChild(paragraphProperties);
      body.AppendChild(paragraph);

      string title = "Отчет по заменам";
      if (dateRange != null && dateRange.DateOne != null && dateRange.DateTwo != null &&
          dateRange.DateOne <= dateRange.DateTwo)
        title += $" за период с {dateRange.DateOne:dddd, dd-MMMM-yyyy} по {dateRange.DateTwo:dddd, dd-MMMM-yyyy}";
      
      Run titleRun = new Run();
      titleRun.AppendChild(new Text(title));
      RunProperties runProperties = new RunProperties();
      runProperties.Append(new Bold());
      runProperties.Append(new FontSize(){Val = "28"});
      runProperties.Append(new RunFonts(){Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman"});
      titleRun.PrependChild(runProperties);
      paragraph.AppendChild(titleRun);

      var table = new Table();
      TableProperties tableProperties = new TableProperties();
      TableBorders borders = new TableBorders
      {
        TopBorder = new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single)},
        BottomBorder = new BottomBorder(){Val = new EnumValue<BorderValues>(BorderValues.Single)},
        LeftBorder = new LeftBorder() {Val = new EnumValue<BorderValues>(BorderValues.Single)},
        RightBorder = new RightBorder( ) {Val = new EnumValue<BorderValues>(BorderValues.Single)},
        InsideHorizontalBorder = new InsideHorizontalBorder(){Val = BorderValues.Single},
        InsideVerticalBorder = new InsideVerticalBorder(){Val = BorderValues.Single},
      };
      tableProperties.Append(borders);
      tableProperties.Append(new Justification(){Val = JustificationValues.Center});
      table.AppendChild(tableProperties);

      TableRow titleRableRow =  new TableRow();
      var tc1 = new TableCell();
      tc1.Append(new Paragraph(new Run(new Text("№"))));
      var tc2 = new TableCell();
      tc2.Append(new Paragraph(new Run(new Text("ФИО"))));
      var tc3 = new TableCell();
      tc3.Append(new Paragraph(new Run(new Text("Количество замен"))));
      titleRableRow.Append(tc1);
      titleRableRow.Append(tc2);
      titleRableRow.Append(tc3);
      table.Append(titleRableRow);

      for (int i = 0; i < teacherStatistics.Length; i++)
      {
        var newTableRow = new TableRow();
        var ntc1 = new TableCell();
        var ntc2 = new TableCell();
        var ntc3 = new TableCell();
        ntc1.Append(new Paragraph(new Run(new Text((i + 1).ToString()))));
        ntc2.Append(new Paragraph(new Run(new Text(teacherStatistics[i].Teacher.FIOTeacher))));
        ntc3.Append(new Paragraph(new Run(new Text(teacherStatistics[i].Count.ToString()))));
        newTableRow.Append(ntc1);
        newTableRow.Append(ntc2);
        newTableRow.Append(ntc3);
        table.Append(newTableRow);
      }
      body.Append(table);
      mainPart.Document.Save();
    }
  }
}
