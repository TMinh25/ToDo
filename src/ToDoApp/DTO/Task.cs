using System;
using System.Globalization;

namespace ToDoApp.DTO
{
    public class Task
    {
        public Task(string content, DateTime timeCreated, bool status, bool isImportant)
        {
            Content = content;
            TimeCreated = timeCreated;
            IsStatusDone = status;
            IsImportant = isImportant;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool IsStatusDone { get; set; }

        public bool IsImportant { get; set; }

        public string TimeString
        {
            get
            {
                DateTime resDate;
                if (!DateTime.TryParseExact(TimeCreated.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out resDate))
                    // lỗi tryParseExact thì sẽ trả về ""
                    return "";
                if (DateTime.Today == resDate)
                    return "Hôm nay, " + TimeCreated.ToString("hh:mm tt");
                else
                    return TimeCreated.ToString();
            }
        }

        public object UpdateField
        {
            get
            {
                return new
                {
                    Id = this.Id,
                    Content = this.Content,
                };
            }
        }

        public object BooleanField
        {
            get
            {
                return new
                {
                    Id = this.Id,
                    IsStatusDone = this.IsStatusDone,
                    IsImportant = this.IsImportant
                };
            }
        }
    }
}
