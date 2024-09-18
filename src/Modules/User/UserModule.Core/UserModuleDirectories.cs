namespace UserModule.Core;

public  class UserModuleDirectories
{
    public static string UserAvatar = "wwwroot/user/Images/avatar";

    public static string GetUserAvatar(string imageName) => $"{UserAvatar.Replace("wwwroot","")}/{imageName}";
}