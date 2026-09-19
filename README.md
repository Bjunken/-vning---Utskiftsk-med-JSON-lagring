# Övning - Utskiftskö med JSON-lagring

Labb: Utskriftskö med JSON-lagring (C#, Visual Studio)

Verktyg: Visual Studio 2026 Community
Språk: C# (.NET 8 eller nyare, det som mallen erbjuder)
Nivå: Nybörjare
Tid: 45-60 minuter

Bakgrund: Varför den här labben?

Tänk dig en skrivare på ett kontor. Flera personer skickar dokument, och skrivaren tar dem i den ordning de kom. Den som skickade först får sitt dokument först. Det kallas FIFO (First In, First Out), och det är exakt hur en kö (Queue) fungerar.

Det finns en syskonstruktion, stacken (Stack), som fungerar tvärtom: LIFO (Last In, First Out). Tänk på en hög med tallrikar. Du tar alltid den översta, alltså den som lades dit sist.

Ett program glömmer allt när det stängs av. Därför lär du dig också att spara data i en JSON-fil, så att kön finns kvar nästa gång programmet startas. JSON används överallt i verkligheten, till exempel i webbtjänster, appar och inställningsfiler.

Lärandemål

Efter den här labben kan du:

Förklara skillnaden mellan en kö (FIFO) och en stack (LIFO) med egna ord.
Använda Queue<T> med Enqueue, Dequeue och Peek.
Spara C#-objekt till en JSON-fil (serialisering).
Läsa in objekt från en JSON-fil (deserialisering).
Hantera felaktig indata och tomma köer utan att programmet kraschar.
Använda felsökaren (debuggern) i Visual Studio för att se vad som händer i koden.
Förkunskaper

Du behöver kunna: variabler, if-satser, loopar (while, foreach), enkla metoder och klasser. Du behöver inte kunna något om köer eller JSON sedan tidigare.

Så här arbetar du
Gör ett steg i taget och testa efter varje steg innan du går vidare.
Försök själv först. Öppna en ledtråd först när du har suttit fast en stund. Läs dem i ordning, eftersom de blir mer avslöjande.
Svara på "Tänk efter"-frågorna i ett dokument eller som kommentarer i koden. Att formulera svaret hjälper dig att förstå, inte bara få koden att fungera.
Det är okej att få felmeddelanden. De är en del av lärandet. Läs dem noga, för de berättar oftast vad som är fel.
Uppsättning i Visual Studio
Starta Visual Studio och välj Create a new project (Skapa ett nytt projekt).
Välj mallen Console App (Konsolapp) för C#. Kontrollera att det står C#, inte Visual Basic eller F#.
Döp projektet till PrintQueue, välj en plats och klicka Next.
Välj den nyaste .NET-versionen och klicka Create.
Tryck Ctrl+F5 för att köra utan felsökare. Då stannar konsolfönstret kvar. Du ska se "Hello, World!".

Obs! Menynamnen kan vara på svenska eller engelska beroende på dina inställningar. Nyare mallar använder top-level statements, så Program.cs kan sakna Main och klass. Du kan skriva din kod där, eller skapa en Program-klass med static void Main(). Båda fungerar, men var konsekvent.

Var hamnar jobs.json? Med en relativ sökväg som "jobs.json" skapas filen i programmets arbetsmapp, oftast PrintQueue\bin\Debug\net8.0\ (mappnamnet följer din .NET-version). Högerklicka på projektet i Solution Explorer och välj Open Folder in File Explorer, gå sedan in i bin\Debug.

Varje utskriftsjobb ska ha minst dessa egenskaper:

Id (int), Document (string), Pages (int)
Steg 1: Skapa jobbklassen och kön

Syfte: Innan du kan lägga något i en kö måste du bestämma vad som ska ligga i den. Här lär du dig att en kö kan innehålla egna objekt, inte bara siffror och text.

Skapa en klass PrintJob. Skapa sedan en tom kö med PrintJob-objekt och en räknare som ger varje jobb ett unikt Id.

Ledtråd 1: Högerklicka på projektet i Solution Explorer och välj Add > Class för att lägga PrintJob i en egen fil. Det är god vana.
Ledtråd 2: Queue<T> finns i System.Collections.Generic. T är typen av sak du lagrar.
Ledtråd 3: För att JSON ska fungera senare ska du använda publika egenskaper ({ get; set; }), inte vanliga fält.
Ledtråd 4: Spara id-räknaren i en int-variabel som ökar med ett varje gång ett jobb läggs till.
Ledtråd 5: Om du ser gröna understrykningar om null eller "non-nullable" är det en varning om nullbarhet. Läs på om hur du ger en string-egenskap ett standardvärde, till exempel string.Empty.

Tänk efter: Varför behöver varje jobb ett unikt Id? Vad kunde gå fel om två jobb hade samma?

Kontrollpunkt: Programmet bygger (Ctrl+Shift+B) och du kan skapa en tom kö utan fel.

Steg 2: Lägg till ett jobb (Enqueue)

Syfte: Här ser du kärnan i en kö: nya element hamnar alltid sist. Du övar också på att aldrig lita blint på vad användaren skriver.

Skriv en metod som frågar efter dokumentnamn och antal sidor, skapar ett PrintJob och lägger det sist i kön.

Ledtråd 1: Console.ReadLine() returnerar alltid en sträng (och den kan vara null). Vilka metoder omvandlar en sträng till int?
Ledtråd 2: int.TryParse kastar inget undantag vid felaktig indata. Vad returnerar den i stället?
Ledtråd 3: Vad ska hända om användaren skriver "abc" som sidantal, eller ett negativt tal?
Ledtråd 4: Skapa objektet först och lägg sedan till det i kön.

Tänk efter: Om du lägger till jobb A, sedan B och sedan C, i vilken ordning tror du att de ligger i kön? Skriv ner din gissning och kontrollera den efteråt.

Felsökartips: Klicka i vänstermarginalen bredvid en kodrad för att sätta en brytpunkt (breakpoint) och tryck F5. När programmet pausar kan du hålla musen över kövariabeln, eller öppna Debug > Windows > Locals, för att se innehållet. Tryck F10 för att stega rad för rad.

Kontrollpunkt: Efter två tillagda jobb innehåller kön båda, med det första du la till längst fram.

Steg 3: Behandla ett jobb (Dequeue)

Syfte: Nu ser du den andra halvan av FIFO: element tas ut från framsidan. Du lär dig också att skydda koden mot ett vanligt fel, att ta ut något ur en tom struktur.

Skriv en metod som tar bort det främsta jobbet och skriver ut något i stil med Skriver ut: cv.pdf (2 sidor).

Ledtråd 1: Vilken metod i Queue<T> tar bort och returnerar det främsta elementet?
Ledtråd 2: Vad händer om du anropar den på en tom kö? Kontrollera Count innan du tar bort något och skriv ett vänligt meddelande i stället för att krascha.

Tänk efter: Prova att medvetet ta bort ur en tom kö utan din kontroll. Vilket felmeddelande får du, och vad säger det?

Kontrollpunkt: Jobben kommer ut i samma ordning som de lades in.

Steg 4: Visa kön

Syfte: Att kunna titta på data utan att förändra den är en viktig skillnad mot att ta bort från den.

Skriv en metod som visar alla väntande jobb i ordning, utan att ta bort något.

Ledtråd 1: En foreach-loop kan gå igenom en kö utan att ändra den.
Ledtråd 2: Vill du visa platsnummer (1:a, 2:a, ...) behöver du en egen räknarvariabel i loopen.
Ledtråd 3: Hantera även fallet med tom kö.

Tänk efter: Varför tror du att en foreach-loop inte tar bort elementen ur kön, medan Dequeue gör det?

Kontrollpunkt: Om du visar kön två gånger i rad ser du samma resultat båda gångerna.

Steg 5: Spara till JSON

Syfte: Här lär du dig serialisering, att förvandla objekt i minnet till text som kan lagras i en fil. Utan detta försvinner allt när programmet stängs.

Skriv en metod som sparar kön och id-räknaren till jobs.json.

Ledtråd 1: Lägg till using System.Text.Json; överst. (Nyare projekt inkluderar System.IO automatiskt via implicit usings.)
Ledtråd 2: Skapa en liten klass (till exempel SaveData) med två egenskaper: NextId och en lista med jobb. Då får du ett enda objekt att serialisera.
Ledtråd 3: Du kan kopiera en kö till en List<T> med new List<PrintJob>(queue) eller .ToList() (kräver using System.Linq;).
Ledtråd 4: Läs på om JsonSerializer.Serialize() och JsonSerializerOptions med WriteIndented = true för en läsbar fil.
Ledtråd 5: File.WriteAllText(path, text) skriver en sträng till en fil.

Tänk efter: Varför sparar vi också NextId, och inte bara jobben? Vad skulle hända med id:na utan det värdet?

Kontrollpunkt: Hitta jobs.json i bin\Debug\net... och öppna den i Visual Studio (File > Open > File). Du ska se dina jobb, snyggt formaterade.

Steg 6: Läs in från JSON vid start

Syfte: Nu sluter du cirkeln med deserialisering, som är text i filen tillbaka till objekt i minnet. Du övar också på att hantera att filen kanske inte finns.

När programmet startar ska det läsa jobs.json och återskapa kön och id-räknaren.

Ledtråd 1: File.Exists(path) visar om filen finns. Första gången programmet körs gör den inte det.
Ledtråd 2: File.ReadAllText(path) ger dig strängen, och JsonSerializer.Deserialize<T>() gör om den till ett objekt. Vad ska T vara?
Ledtråd 3: Deserialize kan returnera null. Hantera det fallet.
Ledtråd 4: Gå igenom den inlästa listan och lägg tillbaka varje jobb i kön (eller undersök Queue<T>-konstruktorn som tar en samling).
Ledtråd 5: Återställ id-räknaren från det sparade värdet så att id:n aldrig återanvänds.
Ledtråd 6: Vad händer om filen är tom eller trasig? (Valfritt: kapsla in inläsningen i try/catch för JsonException.)

Tänk efter: Vilka tre olika situationer kan programmet befinna sig i vid start (tänk på filen)? Hanterar din kod alla tre?

Kontrollpunkt: Lägg till jobb, avsluta, starta om programmet. Jobben finns kvar.

Steg 7: Bygg menyn

Syfte: Nu sätter du ihop alla delar till ett fungerande program. Här ser du hur små metoder som gör en sak var kan kombineras.

Bygg en loop som visar alternativ tills användaren avslutar:

1. Lägg till jobb
2. Behandla nästa jobb
3. Visa kön
4. Avsluta

Ledtråd 1: En while (true)-loop med break (eller en bool running-flagga) vid avslut fungerar bra.
Ledtråd 2: En switch-sats är ett rent sätt att hantera menyvalen.
Ledtråd 3: Anropa sparametoden efter varje ändring (eller åtminstone vid avslut). Fundera på vilket som är säkrast om programmet kraschar.
Ledtråd 4: Hantera ogiltiga menyval med ett meddelande, inte en krasch.
Ledtråd 5: Om du stoppar programmet med den röda fyrkanten i Visual Studio i stället för att välja Avsluta körs inte din "spara vid avslut"-kod. Vilken sparstrategi klarar det bättre?

Tänk efter: Vilka för- och nackdelar har det att spara efter varje ändring jämfört med bara vid avslut?

Slutlig testlista
 Jobben behandlas i ordningen först in, först ut
 Att behandla en tom kö kraschar inte
 Kön finns kvar när programmet stängs och startas igen
 Att ta bort jobs.json förstör inte starten
 Id:n upprepas aldrig, inte ens efter omstart
 Att skriva bokstäver där ett tal förväntas kraschar inte programmet
 Projektet bygger utan fel (läs gärna varningarna också)
Reflektion efter labben

Svara kort med egna ord:

Vad är skillnaden mellan en kö och en stack? Ge ett exempel från vardagen på varje.
Vilket steg var svårast, och hur löste du det?
Vad gör JSON till ett bra format för att spara data?
Om du skulle bygga om programmet, vad skulle du göra annorlunda?
Extrauppgifter
Stack-version: Lägg till alternativet "Ångra senast tillagda jobb" med en stack. Från vilken ände tar du bort den här gången, och hur skiljer det sig från kön? (Läs på om Stack<T>, Push och Pop.)
Peek: Lägg till ett val som visar nästa jobb utan att ta bort det.
Prioritet: Låt ett jobb markeras som "brådskande" och hoppa förbi de andra. (Vad händer med FIFO-regeln? Passar Queue<T> fortfarande, eller finns det en bättre struktur?)
Jobbhistorik: Spara avklarade jobb i en andra JSON-fil.
Fast filplats: Spara jobs.json i en mapp som inte ändras mellan körningar, med Environment.GetFolderPath eller AppContext.BaseDirectory.