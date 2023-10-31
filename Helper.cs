using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ThiefAndPolice
{
    internal class Helper
    {
       
        public static void Movement(Person person, Person[,]matrix, int sizeX, int sizeY, List<Person>personList, List<Person>prisonList, List<Person> poorList)
        {
            int newX = person.X + person.XDirection;
            int newY = person.Y + person.YDirection;

            if (newX >= sizeX)
            {
                newX = 0;
            }

            if (newX < 0)
            {
                newX = sizeX - 1;
            }

            if (newY >= sizeY)
            {
                newY = 0;
            }

            if (newY < 0)
            {
                newY = sizeY - 1;
            }

            if (matrix[newX, newY] != null)
            {
                CheckCollision(newX, newY, matrix, person, personList,prisonList,poorList);
            }

            if (person is Thief)
            {
                

                Thief thief = (Thief)person; // Cast to Thief type

                // Clear the previous position
                matrix[thief.X, thief.Y] = null;

                // Update the thief's position
                thief.X = newX;
                thief.Y = newY;

                // Set the new position
                matrix[newX, newY] = thief;


            }
            else if (person is Police)
            {
                Police police = (Police)person; // Cast to Police type

                // Clear the previous position
                matrix[police.X, police.Y] = null;

                // Update the thief's position
                police.X = newX;
                police.Y = newY;

                // Set the new position
                matrix[newX, newY] = police;


            }
            else if (person is Citizen)
            {
                Citizen citizen = (Citizen)person; // Cast to Citizen type

                // Clear the previous position
                matrix[citizen.X, citizen.Y] = null;

                // Update the citizen's position
                citizen.X = newX;
                citizen.Y = newY;

                // Set the new position
                matrix[newX, newY] = citizen;


            }

            

        }


        public static void PrisonMovement(Person person, Person[,] prisonMatrix, int prisonX, int prisonY, List<Person> personList, List<Person> prisonList)
        {
            int newX = person.X + person.XDirection;
            int newY = person.Y + person.YDirection;

            if (newX >= prisonX)
            {
                newX = 0;
            }
            else if (newX < 0)
            {
                newX = prisonX - 1;
            }

            if (newY >= prisonY)
            {
                newY = 0;
            }
            else if (newY < 0)
            {
                newY = prisonY - 1;
            }




            Thief thief = (Thief)person; // Cast to Thief type

            if (thief.X > prisonX || thief.Y > prisonY) //if thief X,Y is bigger than prison size
            {
                Random random = new Random();
                // random theif pos 0-9 for prison
                thief.X = random.Next(10);
                thief.Y = random.Next(10);

            }




            // Clear the previous position
            if (prisonMatrix[thief.X, thief.Y] != null)
            {
                prisonMatrix[thief.X, thief.Y] = null;
            }


            // Update the thief's position
            thief.X = newX;
            thief.Y = newY;

            // Set the new position
            prisonMatrix[newX, newY] = thief;


        }    
        public static void PrisonMovement(Person person, Person[,] prisonMatrix, int prisonX, int prisonY)
        {
            int newX = person.X + person.XDirection;
            int newY = person.Y + person.YDirection;

            if (newX >= prisonX)
            {
                newX = 0;
            }
            else if (newX < 0)
            {
                newX = prisonX - 1;
            }

            if (newY >= prisonY)
            {
                newY = 0;
            }
            else if (newY < 0)
            {
                newY = prisonY - 1;
            }


            Thief thief = (Thief)person; // Cast to Thief type

            if(thief.X >= prisonX || thief.Y >= prisonY) //if thief X,Y is bigger than prison size
            {
                Random random = new Random();
                // random theif pos 0-9 for prison
                thief.X = random.Next(10);
                thief.Y = random.Next(10);
            }




            // Clear the previous position
            if (prisonMatrix[thief.X, thief.Y]!= null)
            {
                prisonMatrix[thief.X, thief.Y] = null;
            }
                
                // Update the thief's position
                thief.X = newX;
                thief.Y = newY;

                // Set the new position
                prisonMatrix[newX, newY] = thief;

            
            
        }

        public static void PoorMovement(Person person, Person[,] poorMatrix, int poorX, int poorY)
        {
            int newX = person.X + person.XDirection;
            int newY = person.Y + person.YDirection;

            if (newX >= poorX)
            {
                newX = 0;
            }
            else if (newX < 0)
            {
                newX = poorX - 1;
            }

            if (newY >= poorY)
            {
                newY = 0;
            }
            else if (newY < 0)
            {
                newY = poorY - 1;
            }




            Citizen citizen = (Citizen)person; // Cast to citizen type

            if (citizen.X >= poorX || citizen.Y >= poorY) //if citizen X,Y is bigger than poor size
            {
                Random random = new Random();
                
                citizen.X = random.Next(14);
                citizen.Y = random.Next(9);

            }


            // Clear the previous position
            if (poorMatrix[citizen.X, citizen.Y] != null)
            {
                poorMatrix[citizen.X, citizen.Y] = null;
            }


            // Update the citizen position
            citizen.X = newX;
            citizen.Y = newY;

            // Set the new position
            poorMatrix[newX, newY] = citizen;

        }

        public static void CheckCollision(int newX, int newY, Person[,] matrix, Person person, List<Person> personList,List<Person>prisonList, List<Person> poorList)
        {

            

            if (person is Thief)
            {
                Thief thief = (Thief)person; //cast to thief type?

                
                if (matrix[newX, newY] is Police) //thief collides with police
                {
                    if (thief.StolenItems.Count > 0)//if thief have stolen items
                    {

                        for (int i = 0; i < personList.Count; i++)
                        {
                            Person potentialPolice = personList[i];
                            if (potentialPolice is Police && potentialPolice.X == newX && potentialPolice.Y == newY) //if the police have the right X and Y 
                            {

                                Police police = (Police)potentialPolice; //Cast to Police type

                                
                                foreach (string stolenItem in thief.StolenItems)//transfer stolen items from thief to police
                                {
                                    thief.PrisonTime +=10;
                                    police.SizedItems.Add(stolenItem);
                                    Console.WriteLine("The police have sized stolen " + stolenItem + " at: " + police.X + "X " + police.Y + "Y");
                                    Thread.Sleep(4000);
                                }
 
                                thief.StolenItems.Clear(); //remove thief items
                                MoveFromCity(thief, personList, prisonList,matrix);




                            }

                        }


                    }

                }
                else if (matrix[newX, newY] is Citizen) //thief collides with citizen
                {

                    for (int i = 0; i < personList.Count; i++)
                    {
                        Person potentialCitizen = personList[i];
                        if (potentialCitizen is Citizen && potentialCitizen.X == newX && potentialCitizen.Y == newY)
                        {
                            Citizen citizen = (Citizen)potentialCitizen; //Cast to Citizen type

                            
                            if (citizen.Belongings.Count > 0) //if citizen have items
                            {
                               
                                int randomIndex = new Random().Next(0, citizen.Belongings.Count);//randomindex 0 to inventory length

                                string stolenItem = citizen.Belongings[randomIndex]; // Take  item at  randomindex
                                citizen.Belongings.RemoveAt(randomIndex);//remove same item
                                thief.StolenItems.Add(stolenItem);

                                if(citizen.Belongings.Count == 0)
                                {
                                    Console.WriteLine("A citizen have lost all items :(");
                                }

                                
                                Console.WriteLine("The thief have stolen " + stolenItem + " at: " + citizen.X + "X " + citizen.Y + "Y");
                                Thread.Sleep(2000);

                            }
                        }
                    }


                }

            }

            if (person is Police)
            {
                Police police = (Police)person;

                if (matrix[newX, newY] is Thief)
                {

                    for (int i = 0; i < personList.Count; i++)
                    {
                        Person potentialThief = personList[i];

                        if (potentialThief is Thief && potentialThief.X == newX && potentialThief.Y == newY)
                        {
                            Thief thief = (Thief)potentialThief;


                            if (thief.StolenItems.Count > 0)
                            {

                                foreach (string stolenItem in thief.StolenItems)
                                {
                                    thief.PrisonTime += 10;
                                    police.SizedItems.Add(stolenItem);
                                }

                                thief.StolenItems.Clear();
                                MoveFromCity(thief, personList, prisonList, matrix);


                                Console.WriteLine("The police have seized all stolen items from the thief at: " + thief.X + "X " + thief.Y + "Y");
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
                if (matrix[newX, newY] is Citizen) //police collides with citizen
                {

                    for (int i = 0; i < personList.Count; i++)
                    {
                        Person potentialCitizen = personList[i];
                        if (potentialCitizen is Citizen && potentialCitizen.X == newX && potentialCitizen.Y == newY)
                        {
                            Citizen citizen = (Citizen)potentialCitizen; //Cast to Citizen type


                            if (citizen.Belongings.Count <= 0) //if citizen have no items
                            {


                                MoveFromCity(citizen, personList, poorList, matrix);


                                Thread.Sleep(2000);

                            }
                        }
                    }
                }

                
            }

            if (person is Citizen)
            {
                Citizen citizen = (Citizen)person;

                if (matrix[newX, newY] is Thief)
                {
                    foreach (Person potentialThief in personList)
                    {
                        if (potentialThief is Thief && potentialThief.X == newX && potentialThief.Y == newY)
                        {
                            Thief thief = (Thief)potentialThief;


                            if (citizen.Belongings.Count > 0)
                            {
                                int randomIndex = new Random().Next(0, citizen.Belongings.Count);

                                string stolenItem = citizen.Belongings[randomIndex];
                                citizen.Belongings.RemoveAt(randomIndex);

                                thief.StolenItems.Add(stolenItem);

                                Console.WriteLine("Citizen walks into thief");
                                Console.WriteLine("The thief has stolen " + stolenItem + " from the citizen at: " + citizen.X + "X " + citizen.Y + "Y");
                                Thread.Sleep(2000);
                            }
                        }
                    }
                }
                if (matrix[newX, newY] is Police) //citizen collides with police
                {
                    if (citizen.Belongings.Count <= 0)//if citizen have no items
                    {

                        for (int i = 0; i < personList.Count; i++)
                        {
                            Person potentialPolice = personList[i];
                            if (potentialPolice is Police && potentialPolice.X == newX && potentialPolice.Y == newY) //if the police have the right X and Y 
                            {

                                Police police = (Police)potentialPolice; //Cast to Police type


                                
                                MoveFromCity(citizen, personList, poorList, matrix);




                            }

                        }


                    }

                }
            }


        }
                
            
        

        public static void MoveBackToCity(Person person, List<Person> personList, List<Person> prisonList, Person[,]prisonMatrix)
        {
            Thief thief = (Thief)person;

            prisonMatrix[thief.X, thief.Y] = null; //set matrix to null

            // Find the object you want to move.
            Person ThiefToMove = thief;

            // Check if the item is in the source list before moving.
            if (prisonList.Contains(ThiefToMove))
            {
                int index = prisonList.IndexOf(ThiefToMove);

                // Add the object to the destination list.
                personList.Add(ThiefToMove);

                // Set the object to null in the source list.
                prisonList[index] = null;




                Console.WriteLine("A thief was moved from prison");

                Thread.Sleep(2000);

            }
        }

        public static void MoveFromCity(Person person, List<Person> personList, List<Person> outOfCityList, Person[,] personMatrix)
        {

            if(person is Thief)
            {
                Thief thief = (Thief)person;

                personMatrix[thief.X, thief.Y] = null; //set matrix to null

                Random random = new Random();
                // random theif pos 0-9 for prison
                thief.X = random.Next(10);
                thief.Y = random.Next(10);

                // Find the object you want to move.
                Person ThiefToMove = thief;

                // Check if the item is in the source list before moving.
                if (personList.Contains(ThiefToMove))
                {
                    int index = personList.IndexOf(ThiefToMove);

                    // Add the object to the destination list.
                    outOfCityList.Add(ThiefToMove);

                    // Set the object to null in the source list.
                    personList[index] = null;


                    Console.WriteLine("A thief was moved to prison for " + thief.PrisonTime);

                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("Item not found in the source list.");
                }
                

            }
            else if (person is Citizen)
            {
                Citizen citizen = (Citizen)person;

                personMatrix[citizen.X, citizen.Y] = null; //set matrix to null

                Random random = new Random();
                // random theif pos 0-9 for prison
                citizen.X = random.Next(14);
                citizen.Y = random.Next(9);

                // Find the object you want to move.
                Person CitizenToMove = citizen;

                // Check if the item is in the source list before moving.
                if (personList.Contains(CitizenToMove))
                {
                    int index = personList.IndexOf(CitizenToMove);

                    // Add the object to the destination list.
                    outOfCityList.Add(CitizenToMove);

                    // Set the object to null in the source list.
                    personList[index] = null;


                    Console.WriteLine("A Citizen was moved to the Poor house");

                    Thread.Sleep(2000);
                }
            }




        }
    }
}
