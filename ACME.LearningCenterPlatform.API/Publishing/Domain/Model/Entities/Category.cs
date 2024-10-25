using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Tutorial> Tutorials { get; }

    public Category()
    {
        Name = string.Empty;
    }
    
    public Category(string name)
    {
        Name = name;
    }
}