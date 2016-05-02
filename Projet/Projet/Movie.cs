using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    [Serializable]
    public class Movie
    {
        private String title;
        private int day;
        private int month;
        private int year;
        private String author;
        private String category;
        private Image image;
        private String description;

        public Movie(String title, int day, int month, int year, String author, String category, Image image, String description)
        {
            this.title = title;
            this.day = day;
            this.month = month;
            this.year = year;
            this.category = category;
            this.author = author;
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

        public void setAuthor(String author)
        {
            this.author = author;
        }

        public void setCategory(String category)
        {
            this.category = category;
        }

        public void setImage(Image image)
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

        public String getAuthor()
        {
            return author;
        }

        public String getCategory()
        {
            return category;
        }

        public String getDescription()
        {
            return description;
        }

        public Image getImage()
        {
            return image;
        }
    }
}
