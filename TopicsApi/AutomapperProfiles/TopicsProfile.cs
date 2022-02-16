using AutoMapper;

namespace TopicsApi.AutomapperProfiles;

public class TopicsProfile : Profile
{
    public TopicsProfile()
    {
        //Topic --> GetTopicListItemModel
        CreateMap<Topic, TopicListItemModel>()
            .ForMember(dest => dest.id, cfg => cfg.MapFrom(src => src.Id.ToString()));
        //PostTOpicRequestModel --> Topic
        CreateMap<PostTopicRequestModel, Topic>()
            .ForMember(dest => dest.IsDeleted, cfg => cfg.MapFrom(_ => false));

        CreateMap<TopicListItemModel, Topic>()
            .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => int.Parse(src.id)));
    }
}
