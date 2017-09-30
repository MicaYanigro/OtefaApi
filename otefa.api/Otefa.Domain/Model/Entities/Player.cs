using Otefa.Infrastructure.IoC;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;

namespace Otefa.Domain.Model.Entities
{

    public partial class Player : Entity
    {

        private string name;
        private string lastName;
        private string dni;
        private DateTime birthDate;
        private string email;
        private string celNumber;
        private string medicalInsurance;
        private bool isActive;

        public Player(string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance)
        {

            VerifyThatAnotherPlayerWithSameDniDoesNotExist(dni);

            Name = name;
            LastName = lastName;
            Dni = dni;
            BirthDate = birthDate;
            Email = email;
            CelNumber = celNumber;
            MedicalInsurance = medicalInsurance;
        }

        private void VerifyThatAnotherPlayerWithSameDniDoesNotExist(string dni)
        {

            var service = Container.Current.Resolve<IPlayerService>();

            var another = service.FindPlayerByDni(dni);

            if (another != null)
            {
                throw new ExistantPlayerDniException();
            }

        }

        public virtual bool IsActive
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

        public string Name
        {

            get
            {
                return name;
            }

            set
            {

                name = value;

            }

        }

        public virtual string LastName
        {

            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }

        }

        public virtual string Dni
        {

            get
            {
                return dni;
            }

            set
            {
                dni = value;
            }

        }

        public DateTime BirthDate
        {

            get
            {
                return birthDate;
            }

            set
            {

                birthDate = value;

            }

        }

        public string Email
        {

            get
            {
                return email;
            }

            set
            {

                email = value;

            }

        }

        public string CelNumber
        {

            get
            {
                return celNumber;
            }

            set
            {

                celNumber = value;

            }

        }

        public string MedicalInsurance
        {

            get
            {
                return medicalInsurance;
            }

            set
            {
                medicalInsurance = value;
            }

        }

        public void Update(string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance)
        {
            if (dni != Dni)
                VerifyThatAnotherPlayerWithSameDniDoesNotExist(dni);

            Name = name;
            LastName = lastName;
            Dni = dni;
            BirthDate = birthDate;
            Email = email;
            CelNumber = celNumber;
            MedicalInsurance = medicalInsurance;
        }
    }



    public class PlayerMetadata
    {

        [Required, StringLength(20)]
        public string Name { get; set; }

        [Required, StringLength(20)]
        public string LastName { get; set; }

        [Required]
        public string Dni { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string CelNumber { get; set; }

        public string MedicalInsurance { get; set; }

        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(PlayerMetadata))]
    public partial class Player
    {
        protected Player()
        {
        }
    }

}