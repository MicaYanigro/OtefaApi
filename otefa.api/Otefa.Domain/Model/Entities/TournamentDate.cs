using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otefa.Domain.Model.Entities
{
    public partial class TournamentDate : Entity
    {
        public DateTime date;

        public TournamentDate(DateTime date)
        {
            Date = date;
        }


        public DateTime Date
        {

            get
            {
                return date;
            }

            protected set
            {
                date = value;
            }

        }

    }
    public class TournamentDateMetadata
    {
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

    }

    [MetadataType(typeof(TournamentDateMetadata))]
    public partial class TournamentDate
    {
        protected TournamentDate()
        { }

    }
}
