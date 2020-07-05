using System;

namespace reg.Queries.RegistrationProcess
{
    public class RegistrationProcessQuery
    {
		public string ReportDescription { get; set; }
		public string ReportSource { get; set; }
		public string LevelTypeName { get; set; }
		public string EnvironmentTypeName { get; set; }
		public int Events { get; set; }
		public Guid RegistrationProcessID { get; set; }		
		public string CreatedDate { get; set; }
	}
}
