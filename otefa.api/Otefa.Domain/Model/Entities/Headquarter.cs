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
        private string address;
        private string city;
        private bool isActive;

        public Headquarter(string name, string address, string city)
        {
            ThrowExceptionIfNullorEmptyName(name);
            VerifyThatAnotherHeadquarterWithSameNameDoesNotExist(name);

            Name = name;
            Address = address;
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
        public string Address
        {

            get
            {
                return address;
            }

            protected set
            {
                address = value;
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

        public void Update(string name, string address, string city)
        {
            ThrowExceptionIfNullorEmptyName(name);
            VerifyThatAnotherHeadquarterWithSameNameDoesNotExist(name);

            Name = name;
            City = city;
            Address = address;
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
        public string Address { get; set; }

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