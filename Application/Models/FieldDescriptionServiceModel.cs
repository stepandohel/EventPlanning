namespace EventPlanning.Models.FieldDescription
{
    public class FieldDescriptionServiceModel
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }

        public bool IsPredefined { get; set; }

        public string CreatorId { get; set; }
    }
}
