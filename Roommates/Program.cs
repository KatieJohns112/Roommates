using System;
using System.Collections.Generic;
using Roommates.Repositories;
using Roommates.Models;
using System.Linq;


namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                        case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                        case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                        case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

          
                        case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}.");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                        case ("Search for a chore"):
                        Console.Write("Chore Id: ");
                        int Id = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(Id);

                        Console.WriteLine($"{chore.Id} - {chore.Name}.)");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                        case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string Name = Console.ReadLine();


                        Chore choreToAdd = new Chore()
                        {
                            Name = Name,
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                        case ("Search for a Roommate"):
                        Console.Write("Roommate Information: ");
                        int rid = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(rid);

                        Console.WriteLine($" roommate id : {roommate.Id}\n roommate first name : {roommate.FirstName}\n roommate rent portion : ${roommate.RentPortion}.00");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                        case ("Show unassigned chores"):
                        List<Chore> getUnassigned = choreRepo.GetUnassignedChores();

                        foreach (Chore c in getUnassigned)
                        {
                            Console.WriteLine($"{c.Id}. {c.Name} is unassigned");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadLine();
                        break;

                        case "Assign a chore to a roommate":
                        List<Chore> showUnassigned = choreRepo.GetUnassignedChores();

                        foreach (Chore c in showUnassigned)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name} is unassigned");
                        }
                        Console.Write("Please select a chore number? ");
                        int assignChoreId = int.Parse(Console.ReadLine());

                        List<Roommate> showRoommates = roommateRepo.GetAll();

                        foreach (Roommate r in showRoommates)
                        {
                            Console.WriteLine($"{r.Id} - {r.FirstName} {r.LastName}");
                        }

                        Console.Write("Which roommate do you want to assign this chore too? ");
                        int assignRoommateId = int.Parse(Console.ReadLine());

                        choreRepo.AssignChore(assignRoommateId, assignChoreId);

                        Console.WriteLine();
                        Console.WriteLine("---------------");
                        Console.WriteLine("Process Complete!");
                        Console.WriteLine("---------------");
                        Console.WriteLine();
                        Console.Write("Press any key to continue.");
                        Console.ReadLine();
                        break;

                        case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Exit"):
                        runProgram = false;
                        break;

                }
            }
        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Search for chore",
                "Add a chore",
                "Search for Roommate",
                "Show unassigned chores",
                "Assign a chore to a roommate",
                "Update a room",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}
