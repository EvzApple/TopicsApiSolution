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