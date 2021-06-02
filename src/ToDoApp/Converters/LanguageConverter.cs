using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ToDoApp.DTO;

namespace ToDoApp.Converters
{
    class LanguageConverter
    {
        public static string convertToUnSign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        public static ObservableCollection<Task> filterSearch(ObservableCollection<Task> taskInfos, string searchString)
        {
            string lower = searchString.ToLower();
            string unSign = LanguageConverter.convertToUnSign(searchString).ToLower();

            var taskFiltered = from task in taskInfos
                               let content = task.Content.ToLower()
                               let contentUnsign = LanguageConverter.convertToUnSign(task.Content.ToLower())
                               where content.Contains(lower)
                                     || contentUnsign.Contains(unSign)
                               select task;
            return new ObservableCollection<Task>(taskFiltered.Cast<Task>());
        }
    }
}
