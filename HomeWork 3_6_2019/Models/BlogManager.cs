using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HomeWork_3_6_2019.Models
{
    public class BlogManager
    {
        private string _connectionstring;
        public BlogManager(string _Connectionstring)
        {
            _connectionstring = _Connectionstring;
        }
        public BlogIndex GetNextFive(int? from)
        {
            BlogIndex blogs = new BlogIndex();
            blogs.FirstId = GetTopBlogId();
            blogs.LastId = GetLastBlogId();
            if(from==null || from<blogs.FirstId)
            {
                from = blogs.FirstId;
            }
            if(from>=GetLastBlogId())
            {
                from -= 5;
            }
            SqlConnection conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"select b.Id,b.Title,LEFT(b.Boichvie, 200) as Boichvie 
                                from Blog b
                                where id > @from
                                order by Id
                                OFFSET 0 rows
                                Fetch first 5 rows only";
            cmd.Parameters.AddWithValue("@from", from);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();         
            while(reader.Read())
            {
                blogs.blogs.Add(new Blog {
                    Id=(int)reader["Id"],
                    Title=(string)reader["Title"],
                    Boichvei = (string)reader["Boichvie"]
                });
            }
            conn.Close();
            conn.Dispose();
            blogs.PreviousIndex = blogs.blogs.First().Id;
            blogs.NextIndex = blogs.blogs.Last().Id + 1;
            return blogs;
        }
        public Blog GetBlogById(int id)
        {
            Blog blog = GetBlog(id);
            SqlConnection con = new SqlConnection(_connectionstring);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = @"select * from Blog b
                                join Comment C
                                on C.Blogid = b.Id
                                where Blogid = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                blog.Comments.Add(new BlogComment
                {
                    id=(int)reader["id"],
                    Comment=(string)reader["Comment"],
                    Name=(string)reader["Name"]
                });
            }
            con.Close();
            con.Dispose();
            return blog;
        }
        public void InsertComment(BlogComment comment)
        {
            SqlConnection conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"insert into Comment
                                Values (@Blogid,@Comment,@Name)";
            cmd.Parameters.AddWithValue("@Blogid",comment.Blogid);
            cmd.Parameters.AddWithValue("@Name", comment.Name);
            cmd.Parameters.AddWithValue("@Comment", comment.Comment);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }
        public int InsertBlog(Blog blog)
        {
            SqlConnection conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"insert into Blog
                                Values (@BlogTitle,@Boichvei,0)
                                select max(id) from Blog";
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@Boichvei", blog.Boichvei);

            conn.Open();
            int Scope = (int)cmd.ExecuteScalar();
            conn.Close();
            conn.Dispose();
            return Scope;
        }

        private Blog GetBlog(int id)
        {
            SqlConnection con = new SqlConnection(_connectionstring);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = @"select * from Blog
                                where id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            Blog blog = new Blog();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                blog.Id =(int)reader["id"];
                blog.Title = (string)reader["Title"];
                blog.Boichvei = (string)reader["Boichvie"];
            }
            con.Close();
            con.Dispose();
            return blog;
        }
        private int GetTopBlogId()
        {
            SqlConnection con = new SqlConnection(_connectionstring);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = @"select top 1 Id from Blog";
            con.Open();
            int x = (int)cmd.ExecuteScalar(); ;
            con.Close();
            con.Dispose();
            return x;
        }
        private int GetLastBlogId()
        {
            SqlConnection con = new SqlConnection(_connectionstring);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = @"select top 1 Id from Blog
                                ORDER BY Id Desc;";
            con.Open();
            int x = (int)cmd.ExecuteScalar(); ;
            con.Close();
            con.Dispose();
            return x;
        }
    }
}