namespace TaskManagementSystem;

class Program
{
    static void Main(string[] args)
    {
       
        // Task list creation
        int taskCounter = 1; // Task ID counter
        List<TaskItem> tasksList = new List<TaskItem>(); // List to store tasks
        int userChoice; // User's menu choice

     
        do // Loop to allow multiple task additions
        {
            // Display menu options
            Console.WriteLine("\n--- Task Menu ---");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Show Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("Choose an option: ");

            // Read user input and convert it to a number
            bool isValidInput = int.TryParse(Console.ReadLine(), out userChoice);

            // If input is invalid, show an error and retry
            if (!isValidInput)
            {
                Console.WriteLine("Invalid option. Please try again.");
                continue;
            }

            // Handle user choice with 'switch-case'
            switch (userChoice)
            {
                case 1:
                    // Add Task
                    Console.WriteLine("Enter a task description:");
                    string userTask = Console.ReadLine();

                    // Create a new task using the constructor
                    TaskItem newTask = new TaskItem(taskCounter, userTask);
                    taskCounter++; // Increment task ID for the next task

                    tasksList.Add(newTask);
                    Console.WriteLine("Task successfully added.");
                    break;

                case 2:
                    // Show Tasks
                    if (tasksList.Count == 0)
                    {
                        Console.WriteLine("No tasks registered.");
                    }
                    else
                    {
                        Console.WriteLine("\nRegistered Tasks:");
                        foreach (var task in tasksList)
                        {
                            Console.WriteLine($"ID: {task.GetID()}, Description: {task.GetDescription()}, Status: {(task.IsCompleted() ? "Completed" : "Pending")}");
                        }
                    }
                    break;

                case 3:
                    if (tasksList.Count == 0)
                    {
                        Console.WriteLine("No tasks registered.");
                        break;
                    }

                    do
                    {
                        // Show the updated list of tasks
                        Console.WriteLine("\nTasks available for completion:");
                        foreach (var task in tasksList)
                        {
                            Console.WriteLine($"ID: {task.GetID()}, Description: {task.GetDescription()}, Status: {(task.IsCompleted() ? "Completed" : "Pending")}");
                        }

                        // Ask the user for the ID of the task to complete
                        Console.WriteLine("\nEnter the ID of the task to mark as completed (or 0 to exit):");
                        if (!int.TryParse(Console.ReadLine(), out int taskId) || taskId < 0)
                        {
                            Console.WriteLine("Invalid ID. Please try again.");
                            continue;
                        }

                        if (taskId == 0) break; // Exit loop if user enters 0

                        // Search for the task in the list
                        TaskItem taskToComplete = tasksList.FirstOrDefault(t => t.GetID() == taskId);

                        if (taskToComplete != null && !taskToComplete.IsCompleted())
                        {
                            taskToComplete.MarkAsCompleted();
                            Console.WriteLine("Task marked as completed.");
                        }
                        else if (taskToComplete == null)
                        {
                            Console.WriteLine("No task found with that ID.");
                        }
                        else
                        {
                            Console.WriteLine("This task is already completed.");
                        }

                        Console.WriteLine("\nDo you want to mark another task as completed? (y/n)");
                    } while (Console.ReadLine()?.ToLower() == "y");

                    // Show updated task list
                    Console.WriteLine("\nUpdated Task List:");
                    foreach (var task in tasksList)
                    {
                        Console.WriteLine($"ID: {task.GetID()}, Description: {task.GetDescription()}, Status: {(task.IsCompleted() ? "Completed" : "Pending")}");
                    }
                    break;

                case 4:
                    if (tasksList.Count == 0)
                    {
                        Console.WriteLine("No tasks registered.");
                        break;
                    }

                    do
                    {
                        // Display current tasks
                        Console.WriteLine("\nTask List:");
                        foreach (var task in tasksList)
                        {
                            Console.WriteLine($"ID: {task.GetID()}, Description: {task.GetDescription()}, Status: {(task.IsCompleted() ? "Completed" : "Pending")}");
                        }

                        // Ask for the ID of the task to delete
                        Console.WriteLine("\nEnter the ID of the task to delete (or 0 to exit):");
                        if (!int.TryParse(Console.ReadLine(), out int taskId) || taskId < 0)
                        {
                            Console.WriteLine("Invalid ID. Please try again.");
                            continue;
                        }

                        if (taskId == 0) break; // Exit loop if user enters 0

                        // Search for the task
                        TaskItem taskToDelete = tasksList.FirstOrDefault(t => t.GetID() == taskId);

                        if (taskToDelete != null)
                        {
                            tasksList.Remove(taskToDelete);
                            Console.WriteLine("Task successfully deleted.");
                        }
                        else
                        {
                            Console.WriteLine("No task found with that ID.");
                        }

                        Console.WriteLine("\nDo you want to delete another task? (y/n)");
                    } while (Console.ReadLine()?.ToLower() == "y");

                    // Show updated task list
                    Console.WriteLine("\nUpdated Task List:");
                    foreach (var task in tasksList)
                    {
                        Console.WriteLine($"ID: {task.GetID()}, Description: {task.GetDescription()}, Status: {(task.IsCompleted() ? "Completed" : "Pending")}");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        } while (userChoice != 3); // Repeat until user chooses to exit

        Console.WriteLine("Program terminated.");
        Console.ReadLine();
    }

    
    // Class representing a task
    class TaskItem
    {
        private int id; // Task identifier
        private string description; // User-entered text
        private bool isCompleted; // Task status

        // Constructor
        public TaskItem(int id, string description)
        {
            this.id = id; // Assign ID
            this.description = description; // Assign description
            this.isCompleted = false; // Initially, task is not completed
        }

        // Getters
        public int GetID() { return id; }
        public string GetDescription() { return description; }
        public bool IsCompleted() { return isCompleted; }

        // Method to mark a task as completed
        public void MarkAsCompleted()
        {
            isCompleted = true;
        }
    }
}

