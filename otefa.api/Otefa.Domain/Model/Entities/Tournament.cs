using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Otefa.Domain.Model.Entities
{

    public partial class Tournament : Entity
    {

        private string name;
        private TournamentFormat tournamentFormat;
        private ClasificationFormat clasificationFormat;
        private string rules;
        private string prices;
        private bool isActive;

        public Tournament(string name, TournamentFormat tournamentFormat, ClasificationFormat clasificationFormat, string rules, string prices)
        {
            ThrowExceptionIfNullorEmptyName(name);
            VerifyThatAnotherTournamentWithSameNameDoesNotExist(name);

            Name = name;
            TournamentFormat = tournamentFormat;
            ClasificationFormat = clasificationFormat;
            Rules = rules;
            Prices = prices;
            IsActive = true;

            matchesList = new Collection<Match>();
            tournamentDateList = new Collection<TournamentDate>();
            teamPlayersList = new Collection<TournamentTeamPlayers>();
            headquartersList = new Collection<Headquarter>();
        }

        private void VerifyThatAnotherTournamentWithSameNameDoesNotExist(string name)
        {
            var service = Container.Current.Resolve<ITournamentService>();

            var another = service.FindTournamentByName(name);

            if (another != null)
            {
                throw new ExistantTournamentNameException();
            }
        }

        public string Name
        {

            get
            {
                return name;
            }

            protected set
            {
                name = value;
            }

        }
        public TournamentFormat TournamentFormat
        {

            get
            {
                return tournamentFormat;
            }

            protected set
            {
                tournamentFormat = value;
            }

        }

        public ClasificationFormat ClasificationFormat
        {

            get
            {
                return clasificationFormat;
            }

            protected set
            {
                clasificationFormat = value;
            }

        }

        public string Rules
        {

            get
            {
                return rules;
            }

            protected set
            {
                rules = value;
            }

        }

        public string Prices
        {

            get
            {
                return prices;
            }

            protected set
            {
                prices = value;
            }

        }

        public bool IsActive
        {

            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }

        }

        public IEnumerable<Headquarter> GetHeadquarter()
        {
            return headquartersList;
        }

        public void AddHeadquarter(Headquarter headquarter)
        {
            this.headquartersList.Add(headquarter);
        }

        public IEnumerable<TournamentDate> GetTournamentDatesList()
        {
            return tournamentDateList;
        }

        public void AddTournamentDate(TournamentDate tournamentDate)
        {
            this.tournamentDateList.Add(tournamentDate);
        }

        public void AddTeamPlayers(TournamentTeamPlayers teamPlayers)
        {
            this.teamPlayersList.Add(teamPlayers);
        }

        public void Update(string name, TournamentFormat tournamentFormat, ClasificationFormat clasificationFormat, string rules, string prices, IEnumerable<Headquarter> headquarters, IEnumerable<TournamentDate> tournamentDates)
        {
            ThrowExceptionIfNullorEmptyName(name);
            VerifyThatAnotherTournamentWithSameNameDoesNotExist(name);

            Name = name;
            TournamentFormat = tournamentFormat;
            ClasificationFormat = clasificationFormat;
            Rules = rules;
            Prices = prices;

            headquartersList.Clear();
            foreach (var headquarter in headquarters)
            {
                headquartersList.Add(headquarter);
            }

            tournamentDateList.Clear();
            foreach (var tournamentDate in tournamentDates)
            {
                tournamentDateList.Add(tournamentDate);
            }

        }


        private void ThrowExceptionIfNullorEmptyName(string name)
        {
            if (name == null)
            {
                throw new NullNameException();
            }
            else if (name == "")
            {
                throw new EmptyNameException();
            }

        }

    }

    public class TournamentMetadata
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        public string Rules { get; set; }

        public string Prices { get; set; }

        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(TournamentMetadata))]
    public partial class Tournament
    {
        protected Tournament()
        { }


        [Obsolete]
        public virtual ICollection<Headquarter> HeadquartersList { get; set; }
        protected ICollection<Headquarter> headquartersList
        {
#pragma warning disable 612, 618
            get
            {
                return HeadquartersList;
            }
            set
            {
                HeadquartersList = value;
            }
#pragma warning restore 612, 618
        }

        [Obsolete]
        public virtual ICollection<TournamentDate> TournamentDatesList { get; set; }
        protected ICollection<TournamentDate> tournamentDateList
        {
#pragma warning disable 612, 618
            get
            {
                return TournamentDatesList;
            }
            set
            {
                TournamentDatesList = value;
            }
#pragma warning restore 612, 618
        }

        [Obsolete]
        public virtual ICollection<Match> MatchesList { get; set; }
        protected ICollection<Match> matchesList
        {
#pragma warning disable 612, 618
            get
            {
                return MatchesList;
            }
            set
            {
                MatchesList = value;
            }
#pragma warning restore 612, 618
        }

        [Obsolete]
        public virtual ICollection<TournamentTeamPlayers> TeamPlayersList { get; set; }
        protected ICollection<TournamentTeamPlayers> teamPlayersList
        {
#pragma warning disable 612, 618
            get
            {
                return TeamPlayersList;
            }
            set
            {
                TeamPlayersList = value;
            }
#pragma warning restore 612, 618
        }

    }

}