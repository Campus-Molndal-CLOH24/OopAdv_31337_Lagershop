# Individuell Rapport
Svara på frågorna nedan och lämna in det som en del av din inlämning.

## Hur fungerade gruppens arbete?
- Vi hade en bra kommunikation och alla i gruppen var delaktiga även om vi hade en liten dispyt om gruppledning i början så löste vi det och vi arbetade bra efter det. Alla är olika..
- Det har varit mycket lärorikt att arbeta på projekt ihop, för att se hur andra tänker och löser problem. Men också 
  för en själv att inte hoppa runt för mkt och fokusera på uppgiften som är framför en.

## Beskriv gruppens databasimplementation
- Vi implementerade SQLite som databas och använde Entity Framework Core för att skapa en DbContext och modeller. 
- Vi implementerade också MongoDB som databas och använde MongoDB.Driver för att skapa en DbContext och modeller.
- Vi skapade en DbContextFactory för att kunna använda olika databaser.
- Vi skapade en Singleton för SQLite och MongoDB.
- Vi skapade en Repository för att separera dataåtkomst från affärslogik.
- Vi skapade en ProductRepository för att hantera produkter.
- Vi skapade en Product för att hantera produkter.

## Vilka SOLID-principer implementerade ni och hur?

- **Single Responsibility Principle (SRP):**
  - Vi separerade ansvaret för dataåtkomst (Repository-mönstret) från affärslogik (Facade-mönstret).
  Varje klass har ett tydligt ansvar: till exempel ansvarar ProductRepository endast för CRUD-operationer på databasen.
  
- **Open/Closed Principle (OCP):**
  - Vi implementerade en DbContextFactory som gör det enkelt att lägga till stöd för fler databastyper (t.ex. SQLite 
  och MongoDB) utan att behöva ändra befintlig kod.
  Repositories är öppna för utbyggnad (genom nya implementationer) men stängda för förändringar i sin kärnstruktur.
  
- **Liskov Substitution Principle (LSP):**
  - Genom att använda ett IProductFacade-interface kan vi byta mellan olika implementationer, som SQLiteFacade och 
  MongoFacade, utan att ändra hur systemet använder dem.
  Detta möjliggör att samma kod kan hantera olika databasimplementationer utan att behöva känna till deras interna detaljer.
  
- **Interface Segregation Principle (ISP):**
  - Vi har separerat funktionalitet i specifika interfaces, till exempel IRepository och IProductFacade. Det innebär 
  att klasser endast implementerar de metoder som de faktiskt behöver, vilket håller dem fokuserade och undviker onödig komplexitet.
  
- **Dependency Inversion Principle (DIP):**
  - Genom att använda dependency injection och interface som IProductFacade och IRepository styrs vår kod av 
  abstraktioner snarare än specifika implementationer.
  Det gör att vi enkelt kan byta ut t.ex. MongoRepository mot SQLiteRepository utan att påverka beroende klasser.

## Vilka patterns använde ni och varför?
- Repository pattern användes för att separera dataåtkomst från affärslogik och för att göra det enklare att byta databas i framtiden.
- Vilket var bra för sen införande vi MongoDB.
- Factory pattern användes för att skapa en DbContext för att kunna använda olika databaser.
- Singleton för SQLite och MongoDB.

## Vilka tekniska utmaningar stötte ni på och hur löste ni dem?
- MongoDB skapar en hel massa filer, så valde att se MongoDB som en fristående VPS som kör Docker Composer och inte 
  som en del av projektet. Så la in mongodb/ i .gitignore.
- Vi började först med att bygga en lösning för SQLite, sen implemeterade vi Entity Framework och byggde om allt. 
  Det var ett onödigt tidskrävande moment.
- Sen implementerade vi MongoDB via Docker Composer, det var en utmaning att få det att fungera så bra som vi ville 
  med tiden vi fick på oss.
- Vi hade problem med att få appsettings.json att fungera för alla IDEs och operativsystem, men tillslut löste vi det.

## Hur planerade du ditt arbete?
- Vi planerade arbetet i en SCRUM-ish boarden i GitHub och hade standups varje dag kl 09.00, sen stämde vi av under 
  dagen via Discord. Jag höll i dessa.

## Vilka dela gjorde du?
- Projektledning. 
- Satte upp projektet i GitHub och skapade en .gitignore fil.
- Skapade en Kanban/Scrum board i GitHub som vi kallade SCRUMish och underhöll den, stämde av den mot verkligheten 
  varje dag kl 09.00 på dagliga standups.
- Styrde gruppen i rätt riktning och försökte strukturera upp en plan med standups varje dag.
- Arbetade ihop med Andreas och skapade en SQLite databas och en AppDbContext (som senare döptes om till 
  SQLiteDbContext för att få bättre struktur och läsbarhet, Clean Code).
- Refactoriserade om AppDbContext så den inte skapade databasen i /bin/debug/ utan i /Data/Database och också hämtade strängen från appsettings.json för ökad säkerhet.
- Satte upp MongoDB via Docker Composer och skapade en DbContext (MongoDbContext) för att kunna använda MongoDB.
  CRUD kopplingarna ska fungera, även om det inte finns någon för-inmatad data i DBn.
- Skapade en logo med hjälpa av ChatGTP, och la in den i README.md så den syns i GitHub.
- Refaktoriserade getAllProducts (R i CRUD) så den fungerade med EF.
- Refaktoriserade search (R i CRUD) för produkter (namn) så den fungerade med EF.
- La också in lite kul information för användare, ex vilken DB som är aktiv i menyn.
- Implementerade singletons för SQLite och MongoDB.

## Vilka utmaningar stötte du på och hur löste du dem?
- Kunskapsutmaningar var nog det största problemet, framförallt gällande Entiry Framework men också då vi inte övat 
  egentligen alls under utbildningen på att skriva kod. Då uppgifterna i utbildningamaterialet inte var i synk och 
  jag har upplevt en viss frustration över just detta. Då jag lär mig bäst när jag får skriva kod. Jag har försökt 
  själv, men det har varit krångligt.
- Vi skrev om varandras kod vid flera tillfällen, vilket gjorde att vi fick skriva om och felsöka mycket.

## Vad skulle du göra annorlunda nästa gång?
- Vi skulle börjat med EF direkt och inte byggt upp en SQLite lösning först. Vi skulle också haft en bättre planering 
och strukturering av projektet från början.
- **Eventuellt hinner Andreas göra dessa saker, men jag hann inte.**
  - Vi ville också skapa en ProductCategory för att hantera produktkategorier, men hann inte. 
  - Vi ville också skapa en ProductCategoryRepository för att hantera produktkategorier, men hann inte.
  - Vi ville också skapa en ProductCategory för att hantera produktkategorier, men hann inte.
  - Vi ville också skapa en ProductCategoryProduct för att hantera kopplingen mellan produkter och produktkategorier, men hann inte.
  - Vi ville också skapa en ProductCategoryProductRepository för att hantera kopplingen mellan produkter och produktkategorier, men hann inte.
