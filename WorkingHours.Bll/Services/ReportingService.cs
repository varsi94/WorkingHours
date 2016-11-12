using Novacode;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Bll.Model;

namespace WorkingHours.Bll.Services
{
    public class ReportingService : IReportingService
    {
        private const string NameMarker = "#NAME#";
        private const string ProjectMarker = "#PROJECTNAME#";
        private const string StartDateMarker = "#STARTDATE#";
        private const string EndDateMarker = "#ENDDATE#";
        private const string ItemsMaker = "#ITEMS#";
        private const string TotalHoursMarker = "#TOTALHOURS#";

        public byte[] GetProjectWorkTimeReport(ReportData reportData)
        {
            var template = Resources.Resources.WorkTimeReport;
            using (var ms = new MemoryStream())
            {
                ms.Write(template, 0, template.Length);
                ms.Position = 0;
                using (var docx = DocX.Load(ms))
                {
                    docx.ReplaceText(NameMarker, $"{reportData.User.FullName} ({reportData.User.Email})");
                    docx.ReplaceText(ProjectMarker, reportData.Project.Name);
                    var cultureInfo = new CultureInfo("en-US");
                    docx.ReplaceText(StartDateMarker,
                        (!reportData.StartDate.HasValue) ? "N/A" : reportData.StartDate.Value.ToString(cultureInfo.DateTimeFormat.LongDatePattern, cultureInfo));
                    docx.ReplaceText(EndDateMarker,
                        (!reportData.EndDate.HasValue) ? "N/A" : reportData.EndDate.Value.ToString(cultureInfo.DateTimeFormat.LongDatePattern, cultureInfo));

                    var sum = reportData.WorkTimeList.Sum(x => x.Hours);
                    docx.ReplaceText(TotalHoursMarker, sum.ToString());

                    if (reportData.WorkTimeList.Count == 0)
                    {
                        docx.ReplaceText(ItemsMaker, "There is no worktime in this project for the given time period!");
                    }
                    else
                    {
                        Table t = docx.AddTable(reportData.WorkTimeList.Count + 1, 5);
                        t.Rows[0].Cells[0].Paragraphs.First().Append("Issue name").Bold();
                        t.Rows[0].Cells[1].Paragraphs.First().Append("Date").Bold();
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Name").Bold();
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Description").Bold();
                        t.Rows[0].Cells[4].Paragraphs.First().Append("Spent hours").Bold();
                        for (int i = 0; i < reportData.WorkTimeList.Count; i++)
                        {
                            var workTime = reportData.WorkTimeList[i];
                            t.Rows[i + 1].Cells[0].Paragraphs.First().Append(workTime.Issue.Name);
                            t.Rows[i + 1].Cells[1].Paragraphs.First().Append(workTime.Date.ToString(cultureInfo.DateTimeFormat.LongDatePattern, cultureInfo));
                            t.Rows[i + 1].Cells[2].Paragraphs.First().Append(workTime.Name);
                            t.Rows[i + 1].Cells[3].Paragraphs.First().Append(workTime.Description);
                            t.Rows[i + 1].Cells[4].Paragraphs.First().Append(workTime.Hours.ToString());
                        }
                        docx.InsertTable(t);
                        docx.ReplaceText(ItemsMaker, string.Empty);
                    }
                    docx.Save();
                    return ms.ToArray();
                }
            }
        }
    }
}
