using System.ComponentModel.DataAnnotations;

namespace TopicsApi.Models;

/*{
"data": [
    {
        "id": "1", "name": "Angular is Fun", "description": "Angular Stuff For Me To Read About"
    },
    { 
        "id": "2", "name": "Services", "description": "Services Reading Stuff"}
    ]
}
*/

public record GetTopicListItemModel(string id, string name, string description);

public record GetTopicsModel(IEnumerable<GetTopicListItemModel> data);
//public record PostTopicRequestModel(
//    [Required]
//    [MinLength(3)]
//    [MaxLength(20)]
//    string name,
//    [Required]
//    [MinLength(1)]
//    [MaxLength(200)]
//    string description);

public record PostTopicRequestModel : IValidatableObject
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Name { get; init; } = "";

    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    public string Description { get; init; } = "";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(Name.Trim().ToLowerInvariant() == Description.Trim().ToLowerInvariant()) 
        {
            yield return new ValidationResult("Name and Description cannot be the same!", 
                new string[] { nameof(Name), nameof(Description) });
        }
    }
}