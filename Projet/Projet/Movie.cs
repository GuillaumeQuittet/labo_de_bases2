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
        private int day;
        private int month;
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

        public void setDay(int day)
        {
            this.day = day;
        }

        public void setMonth(int month)
        {
            this.month = month;
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

        public string getTitle()
        {
            return title;
        }

        public int getDay() {
            return day;
        }
        public int getMonth() {
            return month;
        }
        public int getYear() {
            return year;
        }

        public string getImage()
        {
            return image;
        }
    }
}
