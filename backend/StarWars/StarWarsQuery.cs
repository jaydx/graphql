using System;
using System.Collections.Generic;
using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    public class StarWarsQuery : ObjectGraphType<object>
    {
        public StarWarsQuery(StarWarsData data)
        {
            Name = "Query";

           // Field<CharacterInterface>("hero", resolve: context => data.GetAll());
            
            Field<HumanType>(
                "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }
                ),
                resolve: context => data.GetHumanByIdAsync(context.GetArgument<string>("id"))
            );

            Func<ResolveFieldContext, string, object> func = (context, id) => data.GetDroidByIdAsync(id);

            FieldDelegate<DroidType>(
                "droid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }
                ),
                resolve: func
            );

            Field<ListGraphType<HumanType>>("allhumans", resolve: context => data.GetHumans());
            Field<ListGraphType<TypeEnum>>("types", resolve: context => data.GetTypes());
            Field<ListGraphType<CharacterInterface>>(
                "characters",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "search", Description = "name of the character" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "type", Description = "the character type" }
                ),
                resolve: context => data.GetAll(context.GetArgument<string>("search"), context.GetArgument<string>("type")));

        }
    }
}
