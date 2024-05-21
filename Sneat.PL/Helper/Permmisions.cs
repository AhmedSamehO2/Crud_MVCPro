namespace Sneat.PL.Helper
{
    public class Permmisions
    {
        public static List<string> GeneratePermmisionList(string module)
        {
            return new List<string>()
            {
                $"Permission.{module}.View",
                $"Permission.{module}.Create",
                $"Permission.{module}.Edit",
                $"Permission.{module}.Delete",
            };
        }

        public static List<string> GenerateAllPermmision()
        {
            var allPermission = new List<string>();

            var modules = Enum.GetValues(typeof(Modules));
            foreach (var item in modules)
            {
                allPermission.AddRange(GeneratePermmisionList(item.ToString()));
            }

            return allPermission;
        }

        public static class Employee
        {
            public const string View = "Permission.Employee.View";
            public const string Create = "Permission.Employee.Create";
            public const string Edit = "Permission.Employee.Edit";
            public const string Delete = "Permission.Employee.Delete";
        }
    }
}
