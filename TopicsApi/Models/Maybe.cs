namespace TopicsApi.Models;

public record Maybe<T>(bool hasValue, T? value); //can use this for any data type 
