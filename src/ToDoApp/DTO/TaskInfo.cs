using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DTO
{
    public class TaskInfo
    {
        public TaskInfo(string content, DateTime timeCreated, bool status, bool isImportant)
        {
            Content = content;
            TimeCreated = timeCreated;
            IsStatusDone = status;
            IsImportant = isImportant;
        }

        public string getTimeString(DateTime time)
        {
            DateTime resDate;
            if (!DateTime.TryParseExact(time.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out resDate))
            {
                //Invalid date
                //log , show error
                return "";
            }
            if (DateTime.Today == resDate)
            {
                return "Hôm nay, " + time.ToString("hh:mm tt");
            }
            else
            {
                return time.ToString();
            }
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool IsStatusDone { get; set; }

        public bool IsImportant { get; set; }

        public string TimeString { get => getTimeString(TimeCreated); }

        public object BindingCommand
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
