using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_04_26.Data
{
    public class ImagesRepository
    {
        private string _connectionString;

        public ImagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Image> GetImages()
        {
            using var context = new ImageDataContext(_connectionString);
            return context.Images.ToList();
        }

        public void AddImage(Image img)
        {
            using var context = new ImageDataContext(_connectionString);
            context.Add(img);
            context.SaveChanges();
        }

        public Image GetById(int id)
        {
            using var context = new ImageDataContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public void UpdateLikes(int id)
        {
            using var context = new ImageDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE Images SET Likes = Likes + 1 WHERE Id = {id} ");

        }
    }
}
