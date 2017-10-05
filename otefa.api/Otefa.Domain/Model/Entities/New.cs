using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otefa.Domain.Model.Entities
{

    public partial class New : Entity
    {

        private DateTime date;
        private string title;
        private string body;
        private string image;
        private bool isActive;
        

        public New(DateTime date, string title, string body, string image)
        {
         
            Date = date;
            Title = title;
            Body = body;
            Image = image;
            IsActive = true;
            
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
        public string Title
        {

            get
            {
                return title;
            }

            protected set
            {
                title = value;
            }

        }

        public string Body
        {

            get
            {
                return body;
            }

            protected set
            {
                body = value;
            }

        }

        public string Image
        {

            get
            {
                return image;
            }

            protected set
            {
                image = value;
            }

        }

        public bool IsActive
        {

            get
            {
                return isActive;
            }

            protected set
            {
                isActive = value;
            }

        }


        public void Update(DateTime date, string title, string body, string image)
        {
            Date = date;
            Title = title;
            Body = body;
            Image = image;
        }

        public void Delete()
        {
            IsActive = false;
        }

        public void Active()
        {
            IsActive = true;
        }



    }

    public class NewMetadata
    {
        [Required]
        [Column(TypeName = "datetime2")]
        public string Date { get; set; }
        
    }

    [MetadataType(typeof(NewMetadata))]
    public partial class New
    {
        protected New()
        { }
        
    }

}