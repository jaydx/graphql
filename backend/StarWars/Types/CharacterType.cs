/*
 * TODO - base for HumanType and DroidType
using GraphQL.Types;

namespace StarWars.Types
{
    public class CharacterType : ObjectGraphType<StarWarsCharacter>
    {
        public CharacterType(StarWarsData data)
        {
            Field(d => d.Id).Description("The id of the character.");
            Field(d => d.Name, nullable: true).Description("The name of the character.");
            Field(d => d.Age, nullable: true).Description("The age (in Tatooine years) of the character.");

            Field<ListGraphType<CharacterInterface>>(
                "friends",
                resolve: context => data.GetFriends(context.Source)
            );
            Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.");

            Interface<CharacterInterface>();
        }
    }
}
*/