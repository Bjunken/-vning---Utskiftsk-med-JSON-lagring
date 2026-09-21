using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace övningar___Utskriftskö_med_JSON {
    internal class PrintJob {
        public int Id { get; set; }
        public int Pages { get; set; }
        public string? Document { get; set; }

        Queue<PrintJob> printJobs = new Queue<PrintJob>();
        int nextId = 1;
        public void createJob() {
            Console.Write("Name of the document?: ");
            string userDocument = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Number of pages?: ");
            string userPages = Console.ReadLine();
            if (!int.TryParse(userPages, out int pages) || (pages <= 0)) {
                Console.WriteLine("Error: Invalid input.");
            }
            else {
                PrintJob job = new PrintJob();
                job.Id = nextId;
                job.Document = userDocument;
                job.Pages = pages;
                printJobs.Enqueue(job);
                nextId++;
            }
        }
        public Queue<PrintJob> GetPrintJobs() {
            return printJobs;
        }
        public PrintJob? dequeueJobs() {
            if (printJobs.Count > 0) {
                PrintJob result = printJobs.Dequeue();
                return result;
            } else {
                return null;
            }
        }
        public void listJobs() {
            int queueNR = 1;
            if (printJobs.Count > 0) {
                foreach (PrintJob jobs in printJobs) {
                    Console.WriteLine($"{queueNR}) {jobs.Document}");
                    queueNR++;
                }
            } else {
                Console.WriteLine("The queue is empty.");
            }
        }
        public void saveJobs(Queue<PrintJob> queue) {
            List<PrintJob> listedJobs = new List<PrintJob>(queue);

            SaveData saveData = new SaveData();
            saveData.jobs = listedJobs;
            saveData.NextId = nextId;

            JsonSerializerOptions options = new JsonSerializerOptions {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(saveData, options);
            File.WriteAllText("jobs.json", json);
        }
        public void setNextId(int id) {
            nextId = id;
        }
        public void setJobQueue(List<PrintJob> jobs) {
           printJobs = new Queue<PrintJob>(jobs);
        }
    }
}