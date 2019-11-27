using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
  
    public class StarWarsMutation : ObjectGraphType
    {
        public StarWarsMutation(StarWarsData data)
        {
            Name = "Mutation";

            Field<HumanType>(
                "updateHuman",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HumanInputType>> {Name = "human"}
                ),
                resolve: context =>
                {
                    var human = context.GetArgument<Human>("human");
                    return data.UpdateHuman(human);
                });

            Field<DroidType>(
                "updateDroid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<DroidInputType>> { Name = "droid" }
                ),
                resolve: context =>
                {
                    var droid = context.GetArgument<Droid>("droid");
                    return data.UpdateDroid(droid);
                });
        }
    }
}
