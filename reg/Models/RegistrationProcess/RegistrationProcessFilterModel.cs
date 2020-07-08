namespace reg.Models.RegistrationProcess
{
    public class RegistrationProcessFilterModel
    {
        public int? EnvironmentTypeID { get; set; }
        public int? LevelTypeID { get; set; }
        public string OrderBy { get; set; }
        public string SortDirection { get; set; }
        public string SearchType { get; set; }
        public string SearchValue { get; set; }
        public int? IsActive { get; set; }
    }
}
