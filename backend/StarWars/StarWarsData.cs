using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using StarWars.Types;

namespace StarWars
{
    public class StarWarsData
    {
        private readonly List<Human> _humans = new List<Human>();
        private readonly List<Droid> _droids = new List<Droid>();
        private readonly List<TypeEnum> _types = new List<TypeEnum>();

        public StarWarsData()
        {
            _humans.Add(new Human
            {
                Id = "1",
                Name = "Luke",
                Friends = new[] { "3", "4" },
                AppearsIn = new[] { 4, 5, 6 },
                HomePlanet = "Tatooine"
                
            });
            _humans.Add(new Human
            {
                Id = "2",
                Name = "Vader",
                AppearsIn = new[] { 4, 5, 6 },
                HomePlanet = "Tatooine"
            });

            _droids.Add(new Droid
            {
                Id = "3",
                Name = "R2-D2",
                Friends = new[] { "1", "4" },
                AppearsIn = new[] { 4, 5, 6 },
                PrimaryFunction = "Astromech"
            });
            _droids.Add(new Droid
            {
                Id = "4",
                Name = "C-3PO",
                AppearsIn = new[] { 4, 5, 6 },
                PrimaryFunction = "Protocol"
            });
            _types.Add(new TypeEnum
            {
                Name = "Human"
            });
            _types.Add(new TypeEnum
            {
                Name = "Droid"
            });
        }

        public IEnumerable<Human> GetHumans()
        {
            return _humans;
        }
        public IEnumerable<TypeEnum> GetTypes()
        {
            return _types;
        }
        public IEnumerable<StarWarsCharacter> GetFriends(StarWarsCharacter character)
        {
            if (character == null)
            {
                return null;
            }

            var friends = new List<StarWarsCharacter>();
            var lookup = character.Friends;
            if (lookup != null)
            {
                _humans.Where(h => lookup.Contains(h.Id)).Apply(friends.Add);
                _droids.Where(d => lookup.Contains(d.Id)).Apply(friends.Add);
            }
            return friends;
        }
        public IEnumerable<StarWarsCharacter> GetAll(string name, string type)
        {

            var all = new List<StarWarsCharacter>();
            
            {
                _humans.Where(h=>h.Name.Contains(name)).Apply(all.Add);
                _droids.Where(h => h.Name.Contains(name)).Apply(all.Add);
            }

            Types.Types searchType;
            if (!string.IsNullOrEmpty(type) && Enum.TryParse<Types.Types>(type, out searchType))
            {
                return all.Where(a => a.Type == searchType);
            }
            return all;
        }
       
        public Task<Human> GetHumanByIdAsync(string id)
        {
            return Task.FromResult(_humans.FirstOrDefault(h => h.Id == id));
        }

        public Task<Droid> GetDroidByIdAsync(string id)
        {
            return Task.FromResult(_droids.FirstOrDefault(h => h.Id == id));
        }

        public Human AddHuman(Human human)
        {
            human.Id = Guid.NewGuid().ToString();
            _humans.Add(human);
            return human;
        }

    }
}
