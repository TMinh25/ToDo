using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using ToDoApp.Model;
using ToDoApp.Converters;
using ToDoApp.DTO;
using ToDoApp.View;

namespace ToDoApp.Control
{
  /// Business Logic Layer
  partial class BLL
  {
    DAL dbProcess = new DAL();

    public bool isCustomMenuValid(string customMenuTitle, int userID)
    {
      try
      {
        var cmd = DAL.newSqlCommand("SELECT * FROM [dbo].[CustomMenu] WHERE [CustomMenu].title = @Title AND [CustomMenu].id_user = @userID");
        cmd.Parameters.AddWithValue("@Title", customMenuTitle);
        cmd.Parameters.AddWithValue("@userID", userID);
        DataTable table = dbProcess.getDataTableQuery(cmd);
        if (table.Rows.Count > 0)
        {
          new PromptDialog("Danh sách " + customMenuTitle.ToUpper() + " đã tồn tại", promptInput: false, cancelButton: false).ShowDialog();
          return false;
        }
        else if (table.Rows.Count == 0)
        {
          return true;
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      return false;
    }

    public ObservableCollection<MenuModel> getCustomMenu(int userID)
    {
      var cmd = DAL.newSqlCommand("SELECT * FROM CustomMenu WHERE [CustomMenu].id_user = @userID");
      cmd.Parameters.AddWithValue("@userID", userID);
      SqlDataReader dr = dbProcess.getDataQuery(cmd);
      ObservableCollection<MenuModel> customMenuModels = new ObservableCollection<MenuModel>();
      while (dr.Read())
      {
        int id = (int)dr.GetValue(dr.GetOrdinal("id_customMenu"));
        string title = dr.GetString(dr.GetOrdinal("title"));
        string iconFont = dr.GetString(dr.GetOrdinal("iconFont"));
        string backColor = dr.GetString(dr.GetOrdinal("backColor"));
        MenuModel menu = new MenuModel() { Title = title, IconFont = iconFont, BackColor = backColor, ID = id };
        customMenuModels.Add(menu);
      }
      dbProcess.CloseConnector();
      return customMenuModels;
    }

    public ObservableCollection<MenuModel> createCustomMenu(int userID, MenuModel menu, MainViewModel viewModel)
    {
      if (isCustomMenuValid(menu.Title, userID))
      {
        try
        {
          var cmd = DAL.newSqlCommand("INSERT INTO [dbo].[CustomMenu] ([id_user], [title], [iconFont], [backColor]) VALUES (@UserID, @Title, @IconFont, @BackColor) SELECT SCOPE_IDENTITY();");
          cmd.Parameters.AddWithValue("@UserID", userID);
          cmd.Parameters.AddWithValue("@Title", menu.Title);
          cmd.Parameters.AddWithValue("@IconFont", menu.IconFont);
          cmd.Parameters.AddWithValue("@BackColor", menu.BackColor);
          DataTable table = dbProcess.getDataTableQuery(cmd);
          if (table.Rows.Count == 1)
          {
            Console.WriteLine("Thêm Menu Thành Công");
          }
          else if (table.Rows.Count < 1)
          {
            Console.WriteLine("THẤT BẠI: Thêm Menu");
          }
        }
        catch (ArgumentNullException ex)
        {
          Console.WriteLine(ex);
        }
      }
      return getCustomMenu(userID);
    }

    public ObservableCollection<MenuModel> updateCustomMenu(int userID, int menuID, string title)
    {
      if (isCustomMenuValid(title, userID))
      {
        try
        {
          var cmd = DAL.newSqlCommand("UPDATE [dbo].[CustomMenu] SET [title] = @Title WHERE id_customMenu = @MenuID SELECT SCOPE_IDENTITY();");
          cmd.Parameters.AddWithValue("@Title", title);
          cmd.Parameters.AddWithValue("@MenuID", menuID);
          DataTable table = dbProcess.getDataTableQuery(cmd);
          if (table.Rows.Count == 1)
          {
            Console.WriteLine("Sửa Task Thành Công");
          }
          else if (table.Rows.Count < 1)
          {
            Console.WriteLine("THẤT BẠI: Sửa Task");
          }
        }
        catch (ArgumentNullException ex)
        {
          Console.WriteLine(ex);
        }
      }
      return getCustomMenu(userID);
    }

    public ObservableCollection<MenuModel> deleteCustomMenu(int userID, int menuID)
    {
      PromptDialog promptDialog = new PromptDialog("Bạn có muốn xóa công việc này?", promptInput: false);
      if (promptDialog.ShowDialog() == true)
      {
        try
        {
          var cmd = DAL.newSqlCommand("DELETE FROM [dbo].[CustomMenu] WHERE id_customMenu = @MenuID; SELECT SCOPE_IDENTITY();");
          cmd.Parameters.AddWithValue("@MenuID", menuID);
          DataTable table = dbProcess.getDataTableQuery(cmd);
          if (table.Rows.Count == 1)
          {
            new PromptDialog("Xóa Thành Công!", promptInput: false, cancelButton: false).ShowDialog();
            Console.WriteLine("Xóa Menu Thành Công");
          }
          else if (table.Rows.Count < 1)
          {
            Console.WriteLine("THẤT BẠI: Xóa Menu");
          }
        }
        catch (ArgumentNullException ex)
        {
          Console.WriteLine(ex);
        }
      }
      return getCustomMenu(userID);
    }

    public ObservableCollection<Task> getTaskForMenuModel(string title, int CurrentUserID, string searchString)
    {
      var result = new ObservableCollection<Task>();
      switch (title.ToLower())
      {
        case "hôm nay":
          result = getTaskListOfToday(userID: CurrentUserID);
          break;
        case "tất cả":
          result = getTaskListOfUser(userID: CurrentUserID);
          break;
        case "quan trọng":
          result = getImportantTaskList(userID: CurrentUserID);
          break;
        case "hoàn thành":
          result = getDoneTaskList(userID: CurrentUserID);
          break;
        default:
          result = getTaskListOfUser(userID: CurrentUserID, category: title);
          break;
      }
      if (searchString != "" && searchString != null)
        result = LanguageConverter.filterSearch(result, searchString);
      return new ObservableCollection<Task>(result.OrderBy(o => o.IsStatusDone)); ;
    }

    public ObservableCollection<Task> getTaskListOfUser(int userID, string category = "")
    {
      string cmdCategory = category == "" ? "" : " AND Task.category = @Category";
      var cmd = DAL.newSqlCommand("SELECT * FROM Task WHERE Task.id_user = @IdUser" + cmdCategory);
      if (cmdCategory != "" && category != "")
      {
        cmd.Parameters.AddWithValue("@Category", category);
      }
      cmd.Parameters.AddWithValue("@IdUser", userID);
      SqlDataReader dr = dbProcess.getDataQuery(cmd);
      ObservableCollection<Task> taskList = new ObservableCollection<Task>();
      while (dr.Read())
      {
        Task task = new Task(
            content: dr.GetString(dr.GetOrdinal("taskContent")),
            timeCreated: dr.GetDateTime(dr.GetOrdinal("timeCreated")),
            status: dr.GetBoolean(dr.GetOrdinal("status")),
            isImportant: dr.GetBoolean(dr.GetOrdinal("isImportant")));
        int categoryColumn = dr.GetOrdinal("category");
        task.Category = dr.IsDBNull(categoryColumn) ? "" : dr.GetString(categoryColumn);
        task.Id = dr.GetInt32(dr.GetOrdinal("id_task"));
        taskList.Add(task);
        //Console.WriteLine(task.Id + " " + task.Content + " " + task.TimeString);
      }
      dbProcess.CloseConnector();
      return taskList;
    }

    public ObservableCollection<Task> getTaskListOfToday(int userID)
    {
      var cmd = DAL.newSqlCommand("SELECT * FROM [dbo].[Task] WHERE [Task].id_user = @UserID AND CAST(Task.timeCreated AS DATE) = CAST(GETDATE() AS DATE)");
      cmd.Parameters.AddWithValue("@UserID", userID);
      SqlDataReader dr = dbProcess.getDataQuery(cmd);
      ObservableCollection<Task> taskList = new ObservableCollection<Task>();
      while (dr.Read())
      {
        Task task = new Task(
            content: dr.GetString(dr.GetOrdinal("taskContent")),
            timeCreated: dr.GetDateTime(dr.GetOrdinal("timeCreated")),
            status: dr.GetBoolean(dr.GetOrdinal("status")),
            isImportant: dr.GetBoolean(dr.GetOrdinal("isImportant")));
        task.Id = dr.GetInt32(dr.GetOrdinal("id_task"));
        task.Category = dr.IsDBNull(dr.GetOrdinal("category")) ? "" : dr.GetString(dr.GetOrdinal("category"));
        taskList.Add(task);
      }
      dbProcess.CloseConnector();
      return taskList;
    }

    public ObservableCollection<Task> getImportantTaskList(int userID)
    {
      var cmd = DAL.newSqlCommand("SELECT * FROM [dbo].[Task] WHERE [Task].id_user = @UserID AND [Task].isImportant = 1");
      cmd.Parameters.AddWithValue("@UserID", userID);
      SqlDataReader dr = dbProcess.getDataQuery(cmd);
      ObservableCollection<Task> taskList = new ObservableCollection<Task>();
      while (dr.Read())
      {
        Task task = new Task(
            content: dr.GetString(dr.GetOrdinal("taskContent")),
            timeCreated: dr.GetDateTime(dr.GetOrdinal("timeCreated")),
            status: dr.GetBoolean(dr.GetOrdinal("status")),
            isImportant: dr.GetBoolean(dr.GetOrdinal("isImportant")));
        task.Id = dr.GetInt32(dr.GetOrdinal("id_task"));
        task.Category = dr.IsDBNull(dr.GetOrdinal("category")) ? "" : dr.GetString(dr.GetOrdinal("category"));
        taskList.Add(task);
      }
      dbProcess.CloseConnector();
      return taskList;
    }

    public ObservableCollection<Task> getDoneTaskList(int userID)
    {
      var cmd = DAL.newSqlCommand("SELECT * FROM [dbo].[Task] WHERE [Task].id_user = @UserID AND [Task].status = 1");
      cmd.Parameters.AddWithValue("@UserID", userID);
      SqlDataReader dr = dbProcess.getDataQuery(cmd);
      ObservableCollection<Task> taskList = new ObservableCollection<Task>();
      while (dr.Read())
      {
        Task task = new Task(
            content: dr.GetString(dr.GetOrdinal("taskContent")),
            timeCreated: dr.GetDateTime(dr.GetOrdinal("timeCreated")),
            status: dr.GetBoolean(dr.GetOrdinal("status")),
            isImportant: dr.GetBoolean(dr.GetOrdinal("isImportant")));
        task.Id = dr.GetInt32(dr.GetOrdinal("id_task"));
        task.Category = dr.IsDBNull(dr.GetOrdinal("category")) ? "" : dr.GetString(dr.GetOrdinal("category"));
        taskList.Add(task);
        //Console.WriteLine(task.Id + " " + task.Content + " " + task.TimeString);
      }
      dbProcess.CloseConnector();
      return taskList;
    }

    public ObservableCollection<Task> addTaskForUser(int userID, Task task, MainViewModel viewModel)
    {
      try
      {
        var cmd = DAL.newSqlCommand("INSERT INTO [dbo].[Task] ([id_user], [taskContent], [timeCreated], [status], [isImportant]" + (task.Category == "" ? "" : ", [category]") + ") VALUES (@UserID, @TaskContent, @TimeCreated, @Status, @IsImportant" + (task.Category == "" ? "" : ", @Category") + ") SELECT SCOPE_IDENTITY();");
        cmd.Parameters.AddWithValue("@UserID", userID);
        cmd.Parameters.AddWithValue("@TaskContent", task.Content);
        cmd.Parameters.AddWithValue("@TimeCreated", task.TimeCreated);
        cmd.Parameters.AddWithValue("@Status", task.IsStatusDone);
        cmd.Parameters.AddWithValue("@IsImportant", task.IsImportant);
        if (task.Category != "")
        {
          cmd.Parameters.AddWithValue("@Category", task.Category);
        }
        DataTable table = dbProcess.getDataTableQuery(cmd);
        if (table.Rows.Count == 1)
        {
          Console.WriteLine("Thêm Task Thành Công");
        }
        else if (table.Rows.Count < 1)
        {
          Console.WriteLine("THẤT BẠI: Thêm Task");
        }
      }
      catch (ArgumentNullException ex)
      {
        Console.WriteLine(ex);
      }
      return viewModel.MenuModel?.TaskInfos;
    }

    public ObservableCollection<Task> updateTaskForUser(int taskID, MainViewModel viewModel, bool taskStatus, bool isImportant, string updateContent)
    {
      try
      {
        DataTable table = null;
        if (updateContent != "")
        {
          var cmd = DAL.newSqlCommand("UPDATE [dbo].[Task] SET [taskContent] = @TaskContent WHERE [Task].id_task = @IDTask; SELECT SCOPE_IDENTITY();");
          cmd.Parameters.AddWithValue("@TaskContent", updateContent);
          cmd.Parameters.AddWithValue("@IDTask", taskID);
          table = dbProcess.getDataTableQuery(cmd);
        }
        else
        {
          var cmd = DAL.newSqlCommand("UPDATE [dbo].[Task] SET [status] = @Status, [isImportant] = @IsImportant WHERE [Task].id_task = @IDTask; SELECT SCOPE_IDENTITY();");
          cmd.Parameters.AddWithValue("@Status", taskStatus);
          cmd.Parameters.AddWithValue("@IsImportant", isImportant);
          cmd.Parameters.AddWithValue("@IDTask", taskID);
          table = dbProcess.getDataTableQuery(cmd);
        }
        if (table.Rows.Count == 1)
        {
          Console.WriteLine("Sửa Task Thành Công");
        }
        else if (table.Rows.Count < 1)
        {
          Console.WriteLine("THẤT BẠI: Sửa Task");
        }
      }
      catch (ArgumentNullException ex)
      {
        Console.WriteLine(ex);
      }
      return viewModel.MenuModel.TaskInfos;
    }

    public ObservableCollection<Task> deleteTaskForUser(int taskID, MainViewModel viewModel)
    {
      PromptDialog promptDialog = new PromptDialog("Bạn có muốn xóa công việc này?", promptInput: false);
      if (promptDialog.ShowDialog() == true)
      {
        if (promptDialog.DialogResult == true)
        {
          try
          {
            var cmd = DAL.newSqlCommand("DELETE FROM [dbo].[Task] WHERE id_task = @IDTask; SELECT SCOPE_IDENTITY();");
            cmd.Parameters.AddWithValue("@IDTask", taskID);
            DataTable table = dbProcess.getDataTableQuery(cmd);
            if (table.Rows.Count == 1)
            {
              Console.WriteLine("Xóa Task Thành Công");
            }
            else if (table.Rows.Count < 1)
            {
              Console.WriteLine("THẤT BẠI: Xóa Task");
            }
          }
          catch (ArgumentNullException ex)
          {
            Console.WriteLine(ex);
          }
        }
      }
      return viewModel.MenuModel.TaskInfos;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private unsafe bool UnSafeCompareByteArray(byte[] strA, byte[] strB)
    {
      int length = strA.Length;
      if (length != strB.Length)
      {
        return false;
      }
      fixed (byte* str = strA)
      {
        byte* chPtr = str;
        fixed (byte* str2 = strB)
        {
          byte* chPtr2 = str2;
          byte* chPtr3 = chPtr;
          byte* chPtr4 = chPtr2;
          while (length >= 10)
          {
            if ((((*(((int*)chPtr3)) != *(((int*)chPtr4))) || (*(((int*)(chPtr3 + 2))) != *(((int*)(chPtr4 + 2))))) || ((*(((int*)(chPtr3 + 4))) != *(((int*)(chPtr4 + 4)))) || (*(((int*)(chPtr3 + 6))) != *(((int*)(chPtr4 + 6)))))) || (*(((int*)(chPtr3 + 8))) != *(((int*)(chPtr4 + 8)))))
            {
              break;
            }
            chPtr3 += 10;
            chPtr4 += 10;
            length -= 10;
          }
          while (length > 0)
          {
            if (*(((int*)chPtr3)) != *(((int*)chPtr4)))
            {
              break;
            }
            chPtr3 += 2;
            chPtr4 += 2;
            length -= 2;
          }
          return (length <= 0);
        }
      }
    }

    public int insertImage(byte[] image, string fileName)
    {
      var cmd = DAL.newSqlCommand("SELECT * FROM [dbo].[Avatar]");
      DataTable dt = dbProcess.getDataTableQuery(cmd);
      foreach (DataRow row in dt.Rows)
      {
        byte[] imgData = (byte[])row["data"];
        if (UnSafeCompareByteArray(image, imgData))
        {
          Console.WriteLine("Ảnh Đã Tồn Tại!");
          return (int)row["id_avatar"];
        }
        else
        {
          Console.WriteLine("Không Giống");
        }
      }
      try
      {
        cmd = DAL.newSqlCommand("INSERT INTO [dbo].[Avatar]([data], [fileName]) VALUES (@Image, @Filename) SELECT SCOPE_IDENTITY();");
        cmd.Parameters.AddWithValue("@Image", image);
        cmd.Parameters.AddWithValue("@Filename", fileName);
        dt = dbProcess.getDataTableQuery(cmd);
        if (dt.Rows.Count == 1)
        {
          Console.WriteLine("Thêm Ảnh Thành Công!");
          return int.Parse(dt.Rows[0][0].ToString());
        }
        Console.WriteLine("THẤT BẠI: Thêm Ảnh");
      }
      catch (Exception)
      {
        //throw;
      }
      return 0;
    }

    public bool createAccount(string username, string password, string displayName, Image avatar, string fileName)
    {
      try
      {
        dbProcess.OpenConnector();
        int idAvatar = 0;
        if (avatar != null)
        {
          idAvatar = insertImage(ImgConverter.ConvertImageToBytes(avatar), fileName);
        }
        else
        {
          Console.WriteLine("null image");
        }
        var cmd = DAL.newSqlCommand("INSERT INTO [dbo].[User]([displayName], [id_avatar], [username], [password]) VALUES (@displayName, @idAvatar, @username, @password)");
        cmd.Parameters.AddWithValue("@displayName", displayName);
        cmd.Parameters.AddWithValue("@idAvatar", idAvatar);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
          Console.WriteLine("Thêm Thành Công");
          return true;
        }
        else if (rowsAffected <= 0)
        {
          Console.WriteLine("THẤT BẠI: Thêm người dùng");
          return false;
        }
        dbProcess.CloseConnector();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
      return false;
    }

    public bool updateAccount(int id, string password, string displayName, string fileName, Image avatar = null)
    {
      try
      {
        dbProcess.OpenConnector();
        int idAvatar = 0;
        if (avatar != null)
        {
          idAvatar = insertImage(ImgConverter.ConvertImageToBytes(avatar), fileName);
        }
        else
        {
          Console.WriteLine("null image");
        }
        var cmd = DAL.newSqlCommand("UPDATE [dbo].[User] SET [displayName] = @displayName, [password] = @password " + (avatar != null ? ", [id_avatar] = @idAvatar" : "") + " WHERE [User].id_user = @userID");
        cmd.Parameters.AddWithValue("@displayName", displayName);
        if (idAvatar != 0 && avatar != null)
        {
          Console.WriteLine("add image to command");
          cmd.Parameters.AddWithValue("@idAvatar", idAvatar);
        }
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@userID", id);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
          Console.WriteLine("Chỉnh Sửa Thành Công");
          return true;
        }
        else if (rowsAffected <= 0)
        {
          Console.WriteLine("THẤT BẠI: Chỉnh sửa người dùng");
          return false;
        }
        dbProcess.CloseConnector();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
      return false;
    }

    public bool deleteAccount(int id)
    {
      try
      {
        dbProcess.OpenConnector();
        var cmd = DAL.newSqlCommand("DELETE FROM [dbo].[User] WHERE [User].id_user = @userID");
        cmd.Parameters.AddWithValue("@userID", id);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
          Console.WriteLine("Xóa Thành Công");
          return true;
        }
        else
        {
          Console.WriteLine("THẤT BẠI: Xóa người dùng");
          return false;
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      finally
      {
        dbProcess.CloseConnector();
      }
      return false;
    }

    public User getUser(int userID)
    {
      var cmd = DAL.newSqlCommand("SELECT * FROM [dbo].[User] WHERE [User].id_user = @UserID");
      cmd.Parameters.AddWithValue("@UserID", userID);
      DataTable dt = dbProcess.getDataTableQuery(cmd);
      if (dt.Rows.Count == 1)
      {
        var row = dt.Rows[0];
        var cmdAvatar = DAL.newSqlCommand("SELECT [Avatar].data, [Avatar].fileName FROM [dbo].[Avatar] WHERE [Avatar].id_avatar = @IdAvatar");
        cmdAvatar.Parameters.AddWithValue("@IdAvatar", row["id_avatar"]);
        DataTable dtAvatar = dbProcess.getDataTableQuery(cmdAvatar);
        Image avatar = null;
        string displayName = row["displayName"].ToString(), fileName = "";
        int id = (int)row["id_user"];
        if (dtAvatar.Rows.Count == 1)
        {
          avatar = ImgConverter.ConvertByteArrayToImage((byte[])dtAvatar.Rows[0]["data"]);
          fileName = dtAvatar.Rows[0]["fileName"].ToString();
        }
        User resUser = new User(displayName, avatar);
        resUser.ID = id;
        resUser.UserName = (string)row["username"];
        resUser.PassWord = (string)row["password"];
        return resUser;
      }
      else if (dt.Rows.Count < 1)
      {
        return null;
      }
      return null;
    }

    public User getUser(string userName)
    {
      var cmd = DAL.newSqlCommand("SELECT [User].id_user, [User].displayName, [User].id_avatar FROM [dbo].[User] WHERE [User].username = @UserName");
      cmd.Parameters.AddWithValue("@UserName", userName);
      DataTable dt = dbProcess.getDataTableQuery(cmd);
      if (dt.Rows.Count == 1)
      {
        var row = dt.Rows[0];
        var cmdAvatar = DAL.newSqlCommand("SELECT [Avatar].data, [Avatar].fileName FROM [dbo].[Avatar] WHERE [Avatar].id_avatar = @IdAvatar");
        cmdAvatar.Parameters.AddWithValue("@IdAvatar", row["id_avatar"]);
        DataTable dtAvatar = dbProcess.getDataTableQuery(cmdAvatar);
        Image avatar = null;
        string displayName = row["displayName"].ToString(), fileName = "";
        int id = (int)row["id_user"];
        if (dtAvatar.Rows.Count == 1)
        {
          avatar = ImgConverter.ConvertByteArrayToImage((byte[])dtAvatar.Rows[0]["data"]);
          fileName = dtAvatar.Rows[0]["fileName"].ToString();
        }
        User resUser = new User(displayName, avatar);
        resUser.ID = id;
        return resUser;
      }
      else if (dt.Rows.Count < 1)
      {
        return null;
      }
      return null;
    }

    public string signIn(string username, string password)
    {
      var cmd = DAL.newSqlCommand("SELECT [User].id_user, [User].displayName, [User].id_avatar FROM [dbo].[User] WHERE [User].username = @Username AND [User].password = @Password");
      cmd.Parameters.AddWithValue("@Username", username.Trim());
      cmd.Parameters.AddWithValue("@Password", password.Trim());
      DataTable dt = dbProcess.getDataTableQuery(cmd);
      try
      {
        if (dt.Rows.Count == 1)
        {
          var row = dt.Rows[0];
          User authenticatedUser = getUser((int)row["id_user"]);
          MainWindow mainWindow = new MainWindow(authenticatedUser);
          mainWindow.Show();
          return "";
        }
        else if (dt.Rows.Count < 1)
        {
          return "Sai thông tin đăng nhập!";
        }
      }
      finally
      {
        dbProcess.CloseConnector();
      }
      return "";
    }

    public void signOut()
    {
      Login login = new Login();
      login.Show();
    }
  }
}
