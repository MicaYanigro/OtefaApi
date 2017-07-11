using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.Domain.Model.Entities
{

    public partial class Headquarter : Entity
    {

        private string name;
        private string adress;
        private string city;
        private bool isActive;

        public Headquarter(string name, string adress, string city)
        {
            ThrowExceptionIfNullorEmptyName(name);
            VerifyThatAnotherHeadquarterWithSameNameDoesNotExist(name);

            Name = name;
            Adress = adress;
            City = city;

            IsActive = true;
        }

        private void VerifyThatAnotherHeadquarterWithSameNameDoesNotExist(string name)
        {
            var service = Container.Current.Resolve<IHeadquarterService>();

            var another = service.FindHeadquarterByName(name);

            if (another != null)
            {
                throw new ExistantHeadquarterNameException();
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
        public string Adress
        {

            get
            {
                return adress;
            }

            protected set
            {
                adress = value;
            }

        }

        public string City
        {

            get
            {
                return city;
            }

            protected set
            {
                city = value;
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

        public void Update(string name, string adress, string city)
        {
            ThrowExceptionIfNullorEmptyName(name);
            VerifyThatAnotherHeadquarterWithSameNameDoesNotExist(name);

            Name = name;
            City = city;
            Adress = adress;
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

    public class HeadquarterMetadata
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Adress { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(HeadquarterMetadata))]
    public partial class Headquarter
    {
        protected Headquarter()
        { }
        
    }

}