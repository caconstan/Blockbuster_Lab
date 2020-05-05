using System;
using System.Collections.Generic;

namespace Blockbuster_Lab
{
    class Blockbuster
    {
        static List<Movie> allmovies = new List<Movie>() { new DVD(Genre.Animated, "Transformers", 120, new List<string> {"Robots land on earth", "Prime dies", "The Autobots are victorious!"}),
            new DVD(Genre.Animated, "Pokemon Detective Pikachu", 120, new List<string> {"Introducing the Pokemon", "On the case", "Pikachu solves the mystery"}),
            new DVD(Genre.Drama, "Ford v Ferrari", 120, new List<string> {"Ferraris are awesome", "Ford fights back", "Ford wins the day"}),
            new VHS(Genre.Horror, "The Shining", 120, new List<string> {"A strange house", "Here's Johnny!", "Run away and stay away"}),
            new VHS(Genre.Horror, "The Stand", 120, new List<string> {"A pandemic occurs", "society goes awry", "A new life emerges"}),
            new VHS(Genre.Horror, "Scream", 120, new List<string> {"Someone dies in a bloody mess", "People learn to fight back", "The killer is revealed"}),};

        static void PrintMovies()
        {
            for (int i = 0; i < allmovies.Count; i++)
            {
                Console.WriteLine(i+1 + ") " + allmovies[i].Title);
            }
        }

        static int CheckOut()
        {
            PrintMovies();
            Console.WriteLine("\nWhich movie would you like to check out?");

            int movie = 0;
            while (!int.TryParse(Console.ReadLine(), out movie) ||
                (movie < 1 || movie > allmovies.Count))
            {
                Console.WriteLine("Invalid selection.  Please try again.");
            }

            movie--;// adjust selection for array[0]
            allmovies[movie].PrintInfo();
            return movie;
        }

        static void Main(string[] args)
        {

            string cont = "y";
            Console.WriteLine("Welcome to GC Blockbuster!");

            do
            {
                Console.WriteLine("Please select a movie from the list:");
                int movie = CheckOut();

                Console.WriteLine("\nDo you want to watch the movie? Y/N");
                cont = Console.ReadLine();

                if (cont.Equals("y"))
                {
                    allmovies[movie].Play();
                }

                Console.WriteLine("\nDo you want to pick another movie? Y/N");
                cont = Console.ReadLine();

            } while (cont.Equals("y"));

            Console.WriteLine("Bye!");
        }
    }
    
    public class VHS : Movie
    {
        public VHS(Genre categoryIn, string titleIn, int RunTime, List<string> Scenes) : base(categoryIn, titleIn, RunTime, Scenes)
        {
            CurrentTime = 0;
        }
        public int CurrentTime { get; set; }
        public override void Play()
        {
            CurrentTime++;
            Console.WriteLine("Play: Current Time=" + CurrentTime);
        }
        public void Rewind()
        {
            CurrentTime = 0;
        }
    }
    public class DVD : Movie
    {
        public DVD(Genre categoryIn, string titleIn, int RunTime, List<string> Scenes) : base(categoryIn, titleIn, RunTime, Scenes)
        {

        }

        public override void Play()
        {
            string cont = "y";

            while (cont.Equals("y"))
            {
                Console.WriteLine("Which scene of the DVD " + Title + " would you like to watch?  Select 1 to " + Scenes.Count);
                int scene = 0;
                while (!int.TryParse(Console.ReadLine(), out scene) ||
                            (scene < 1 || scene > Scenes.Count))
                {
                    Console.WriteLine("Invalid selection.  Please try again.");
                }
                Console.WriteLine("Scene "+scene+" " +Scenes[scene-1]);
                Console.WriteLine("Watch another scene? y/n");
                cont = Console.ReadLine();
            }
        }
    }

    public enum Genre
    {
        Animated = 1,
        Drama = 2,
        Horror = 3,
        Scifi = 4
    }

    public abstract class Movie : IComparable<Movie>
    {

        public Genre Category { get; set; }
        public string Title { get; set; }
        public List<string> Scenes { get; set; }
        public int RunTime { get; set; }

        public Movie(Genre categoryIn, string titleIn, int runTime = 0, List<string> Scenes = null)
        {
            this.Category = categoryIn;
            this.Title = titleIn;
            this.RunTime = runTime;
            this.Scenes = Scenes;
        }

        public int CompareTo(Movie other)
        {
            return this.Title.CompareTo(other.Title);
        }

        public abstract void Play();
        public virtual void PrintInfo()
        {
            Console.WriteLine(Title);
            Console.WriteLine("Category: " + Category);
            Console.WriteLine("RunTime: " + RunTime);
        }
        public void PrintScenes()
        {
            for (int i = 0; i < Scenes.Count; i++)
            {
                Console.WriteLine("Scene[" + i + "]=" + Scenes[i]);
            }
        }
    }
}

