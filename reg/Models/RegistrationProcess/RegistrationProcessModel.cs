using System;

namespace reg.Models.RegistrationProcess
{
    public class RegistrationProcessModel
    {
        public string Title { get; set; }
        public string ReportDescription { get; set; }
        public string ReportSource { get; set; }
        public byte? LevelTypeID { get; set; }
        public int? Events { get; set; }
        public Guid? OwnerID { get; set; }
        public byte? EnvironmentTypeID { get; set; }
        public string Details { get; set; }
    }
}
