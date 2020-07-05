using System;

namespace reg.Queries.RegistrationProcess
{
    public class RegistrationProcessByIdQuery
    {
		public string ReportSource { get; set; }
		public string LevelTypeName { get; set; }
		public int Events { get; set; }
		public Guid RegistrationProcessID { get; set; }
		public string CreatedDate { get; set; }
		public string Title { get; set; }
		public string Details { get; set; }
		public Guid OwnerID { get; set; }
		public string EnvironmentTypeName { get; set; }
		public string UserName { get; set; }
	}
}
