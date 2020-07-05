namespace reg.Models.RegistrationProcess
{
    public class RegistrationProcessFilterModel
    {
        public int? EnvFilter { get; set; }
        public int? LevelFilter { get; set; }
        public string OrderBy { get; set; }
        public string SortDirection { get; set; }
        public string SearchType { get; set; }
        public string SearchValue { get; set; }
    }
}
