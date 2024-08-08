using Data.Entites;

namespace Data.GraphQL.ObjectTypes;

public class CourseType : ObjectType<CourseEntity>
{
    protected override void Configure(IObjectTypeDescriptor<CourseEntity> descriptor)
    {
        descriptor.Field(x => x.Id).Type<NonNullType<IdType>>();
        descriptor.Field(x => x.Name).Type<StringType>();
        descriptor.Field(x => x.Description).Type<StringType>();
        descriptor.Field(x => x.Ingress).Type<StringType>();
        descriptor.Field(x => x.Price).Type<DecimalType>();
        descriptor.Field(x => x.HoursToComplete).Type<IntType>();
        descriptor.Field(x => x.LikesPercentage).Type<IntType>();
        descriptor.Field(x => x.LikesAmount).Type<IntType>();

        descriptor.Field(x => x.AuthorName).Type<StringType>();
        descriptor.Field(x => x.AuthorDescription).Type<StringType>();
        descriptor.Field(x => x.AuthorImage).Type<StringType>();
        descriptor.Field(x => x.AuthorYoutubeFollowersQty).Type<IntType>();
        descriptor.Field(x => x.AuthorFacebookFollowersQty).Type<IntType>();
    }
}