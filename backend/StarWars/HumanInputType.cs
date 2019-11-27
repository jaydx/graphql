using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    public class HumanInputType : InputObjectGraphType<Human>
    {
        public HumanInputType()
        {
            Name = "HumanInput";
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Age);
            Field(x => x.Friends, nullable: true);
            Field(x => x.AppearsIn, nullable: true);
            Field(x => x.HomePlanet, nullable: true);
        }
    }
    public class DroidInputType : InputObjectGraphType<Droid>
    {
        public DroidInputType()
        {
            Name = "DroidInput";
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Age);
            Field(x => x.Friends, nullable: true);
            Field(x => x.AppearsIn, nullable: true);
            Field(x => x.PrimaryFunction, nullable: true);
        }
    }
}
