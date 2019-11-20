using System.Collections.Generic;

namespace MyVeterinaria.Web.Data.Entities
{
    public class Owner
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Pet> Pets { get; set; }

        public ICollection<Agenda> Agendas { get; set; }

    }
}
