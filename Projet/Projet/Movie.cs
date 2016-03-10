using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    class Movie
    {
        private String title;
        private int year;
        private String image;
        private String description;

        public Movie(String title, int year, String image, String description)
        {
            this.title = title;
            this.year = year;
            this.image = image;
            this.description = description;
        }

        public void setTitle(String title)
        {
            this.title = title;
        }

        public void setYear(int year)
        {
            this.year = year;
        }

        public void setImage(String image)
        {
            this.image = image;
        }

        public void setDescription(String description)
        {
            this.description = description;
        }
    }
}
