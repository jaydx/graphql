using GraphQL;

namespace StarWars.Types
{
    public abstract class StarWarsCharacter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Friends { get; set; }
        public int[] AppearsIn { get; set; }

        public Types Type { get; set; }
    }

    [GraphQLMetadata("Human", IsTypeOf = typeof(Human))]
    public class Human : StarWarsCharacter
    {
        public string HomePlanet { get; set; }

        public Human()
        {
            Type = Types.Human;
        }
    }


    [GraphQLMetadata("Droid", IsTypeOf = typeof(Droid))]
    public class Droid : StarWarsCharacter
    {
        public string PrimaryFunction { get; set; }

        public Droid()
        {
            Type = Types.Droid;
        }
    }

    
}
