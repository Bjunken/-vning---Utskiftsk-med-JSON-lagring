using System.Data;
using System.Globalization;
using System.Text.Json;

namespace övningar___Utskriftskö_med_JSON {
    internal class Program {
        static void Main(string[] args) {
            PrintJob printJob = new PrintJob();

            string path = Path.GetFullPath("jobs.json");
            if (!File.Exists(path)) {

            } else {
                string json = File.ReadAllText(path);
                SaveData? saveData = JsonSerializer.Deserialize<SaveData>(json);
                printJob.setNextId(saveData.NextId);
                printJob.setJobQueue(saveData.jobs);
            }
            bool programOff = false;
            do {
                Console.WriteLine("1. Lägg till jobb\n2. Behandla nästa jobb\n3. Visa kön\n4. Avsluta");
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int UserInput) && (UserInput < 5 && UserInput > 0)) {
                    switch (UserInput) {
                        case 1:
                            Console.Clear();
                            printJob.createJob();
                            printJob.saveJobs(printJob.GetPrintJobs());
                            break;
                        case 2:
                            if (printJob.GetPrintJobs().Count > 0) {
                                Console.Clear();
                                printJob.dequeueJobs();
                                printJob.saveJobs(printJob.GetPrintJobs());
                                
                            } else {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Error: ");
                                Console.ResetColor();
                                Console.Write("Finns inga job i kön.");
                            }
                            break;
                        case 3:
                            if (printJob.GetPrintJobs().Count > 0) {
                                Console.Clear();
                                printJob.listJobs();
                            } else {
                                Console.Write("Error: ");
                                Console.ResetColor();
                                Console.Write("Finns inga job i kön.");
                            }
                            break;
                        case 4:
                            printJob.saveJobs(printJob.GetPrintJobs());
                            programOff = true;
                            break;
                    }
                }
                else {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error: ");
                    Console.ResetColor();
                    Console.Write("Ogiltigt input");
                }
            } while (!programOff);
            /*
            Tänk efter: Varför behöver varje jobb ett unikt 'Id'? Vad kunde gå fel om två jobb hade samma?
            Svar: Dem behöver ett unikt 'Id' för att hålla sig organiserade, och om två jobb hade samma kan det bli fel i JSON-filen.
            
            Tänk efter: Om du lägger till jobb A, sedan B och sedan C, i vilken ordning tror du att de ligger i kön? Skriv ner din gissning och kontrollera den efteråt.
            Svar: Dem kommer ligga A -> B -> C
            
            Tänk efter: Prova att medvetet ta bort ur en tom kö utan din kontroll. Vilket felmeddelande får du, och vad säger det ?
            Svar: Man får ett "System.NullReferenceException" eftersom värdet inte är satt till ett objekt.

            Tänk efter: Varför tror du att en foreach-loop inte tar bort elementen ur kön, medan Dequeue gör det?
            Svar: en foreach-loop går bara igenom en lista, medan Dequeue är en metod som tar bort ett objekt i kön.

            Tänk efter: Varför sparar vi också NextId, och inte bara jobben? Vad skulle hända med id:na utan det värdet?
            Svar: NextId hjälper till att hålla reda på hur många jobb som skapats och vilket plats dem har. Med andra ord hjälper det till att hålla allt organiserat.

            Tänk efter: Vilka tre olika situationer kan programmet befinna sig i vid start (tänk på filen)? Hanterar din kod alla tre?
            Svar: Den ska kolla om json finns -> om inte, skapa fil, om den finns -> lägg till den gamla datan. Nej, min kod hanterar bara om filen finns.

            Tänk efter: Vilka för- och nackdelar har det att spara efter varje ändring jämfört med bara vid avslut?
            Svar: Neckdelar är att det kan lättare korrupta vid fel, och om programmet hade varit stort, hade det blivit långsammare.
            Fördelen är att man minskar risken att förlora det man gör i programmet ifall det skulle krascha.

            Reflektion
            1. Vad är skillnaden mellan en kö och en stack? Ge ett exempel från vardagen på varje.
            Kö (Queue) är istortsätt som det låter, en kö. Den följer FIFO, (First in, First Out), och fungerar precis som man tänker sig en kö ska fungera.
            Stack är motsatsen och följer FILO, (First in, Last Out), och metaforisk kan beskrivas som en hög med tallrikar, man börjar med den högst upp, inte den man satte in först.
            
            2. Vilket steg var svårast, och hur löste du det?
            Det svåraste var att komma ihåg hur man satte upp JSON filen, och jag löste det genom att bolla konceptet med AI.

            3. Vad gör JSON till ett bra format för att spara data?
            Det är ett bra format eftersom man kan skriva in nästan alla datatyper och konvertera tillbaka.

            4. Om du skulle bygga om programmet, vad skulle du göra annorlunda?
            Jag hade börjat med frontend och sedan gjort backend, SaveData -> PrintJob

            Extrauppgifter
            Jag valde att inte göra extrauppgifterna eftersom jag har en ny deadline att passa.
            */
        }
    }
}
