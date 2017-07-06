using System;

namespace Otefa.Domain.Model.Entities
{
    public abstract class Entity : IEntity
    {

        [Obsolete("Required by EF")]
        public int Id
        {
            get;
            set;
        }

        public int GetId()
        {
#pragma warning disable 612, 618
            return Id;
#pragma warning restore 612,618
        }

    }
}