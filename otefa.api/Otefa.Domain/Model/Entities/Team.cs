using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Otefa.Domain.Model.Entities
{

    public partial class Team : Entity
    {

        private string name;
        private string shieldImage;
        private string teamImage;
        private string teamDelegate;
        private bool isActive;

        public Team(string name, string teamDelegate, string shieldImage, string teamImage)
        {
            ThrowExceptionIfNullorEmptyName(name);
            ThrowExceptionIfNullorEmptyDelegate(teamDelegate);
            VerifyThatAnotherTeamWithSameNameDoesNotExist(name);

            Name = name;
            TeamDelegate = teamDelegate;
            ShieldImage = shieldImage;
            TeamImage = teamImage;

            playersList = new Collection<Player>();
            IsActive = true;
        }

        private void VerifyThatAnotherTeamWithSameNameDoesNotExist(string name)
        {
            var service = Container.Current.Resolve<ITeamService>();

            var another = service.FindTeamByName(name);

            if (another != null)
            {
                throw new ExistantTeamNameException();
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
        public string TeamDelegate
        {

            get
            {
                return teamDelegate;
            }

            protected set
            {
                teamDelegate = value;
            }

        }

        public string ShieldImage
        {

            get
            {
                return shieldImage;
            }

            protected set
            {
                shieldImage = value;
            }

        }

        public string TeamImage
        {

            get
            {
                return teamImage;
            }

            protected set
            {
                teamImage = value;
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

        public void AddPlayer(Player player)
        {
            this.playersList.Add(player);
        }

        public void Update(string name, string teamDelegate, string shieldImage, string teamImage)
        {
            ThrowExceptionIfNullorEmptyName(name);
            ThrowExceptionIfNullorEmptyDelegate(teamDelegate);
            VerifyThatAnotherTeamWithSameNameDoesNotExist(name);

            Name = name;
            TeamDelegate = teamDelegate;
            ShieldImage = shieldImage;
            TeamImage = teamImage;

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

        private void ThrowExceptionIfNullorEmptyDelegate(string teamDelegate)
        {
            if (teamDelegate == null)
            {
                throw new NullDelegateException();
            }
            else if (teamDelegate == "")
            {
                throw new EmptyDelegateException();
            }

        }


    }

    public class TeamMetadata
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string TeamDelegate { get; set; }

        public string ShieldImage { get; set; }

        public string TeamImage { get; set; }

        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(TeamMetadata))]
    public partial class Team
    {
        protected Team()
        { }


        [Obsolete]
        public virtual ICollection<Player> PlayersList { get; set; }
        protected ICollection<Player> playersList
        {
#pragma warning disable 612, 618
            get
            {
                return PlayersList;
            }
            set
            {
                PlayersList = value;
            }
#pragma warning restore 612, 618
        }

    }

}