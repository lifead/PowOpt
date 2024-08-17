using System.Text.Json.Serialization;

namespace PowOpt.Core.Models
{
    public class GroupDbo
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
    }

    public class ParameterDbo
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public int GroupId { get; set; }
        public int TypeId { get; set; }  // Ссылка на идентификатор типа параметра
        public string Value { get; set; } // Значение параметра
        
        [JsonIgnore]
        public decimal CalculatedValue { get; internal set; }
    }

    public class ProjectDataDbo
    {
        public List<GroupDbo> Groups { get; set; }
        public List<ParameterDbo> Parameters { get; set; }
        public List<ParameterTypeDbo> ParameterTypes { get; set; }  // Новая коллекция для типов параметров
    }
}
