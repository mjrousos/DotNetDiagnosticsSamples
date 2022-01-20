using System.Runtime.Serialization;

namespace TargetApp.Models
{
    [DataContract]
    public class ProductCategory
    {
        public ProductCategory(int id, string name, int parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ParentId { get; set; }
    }
}
