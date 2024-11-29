# Individuell Rapport

Svara på frågorna nedan och lämna in det som en del av din inlämning.

## Hur fungerade gruppens arbete?
- Vi hade en bra kommunikation och alla i gruppen var delaktiga även om vi hade en liten dispyt om gruppledning i början så löste vi det och vi arbetade bra efter det. Alla är olika..

## Beskriv gruppens databasimplementation
- Vi implementerade SQLite som databas och använde Entity Framework Core för att skapa en DbContext och modeller. Vi skapade en databas som innehöll en tabell för produkter och en för kategorier. 
- 

## Vilka SOLID-principer implementerade ni och hur?

## Vilka patterns använde ni och varför?
- Repository pattern användes för att separera dataåtkomst från affärslogik och för att göra det enklare att byta databas i framtiden.
- Vilket var bra för sen införande vi MongoDB.
- Factory pattern användes för att skapa en DbContext för att kunna använda olika databaser.
- Singleton för SQLite och MongoDB.

## Vilka tekniska utmaningar stötte ni på och hur löste ni dem?
- MongoDB skapar en hel massa filer, så valde att se MongoDB som en fristående VPS som kör Docker Composer och inte 
  som en del av projektet. Så la in mongodb/ i .gitignore.

## Hur planerade du ditt arbete?

## Vilka dela gjorde du?
- Projektledning. 
- Satte upp projektet i GitHub och skapade en .gitignore fil.
- Skapade en Kanban/Scrum board i GitHub och underhöll den.
- Styrde gruppen i rätt riktning och försökte strukturera upp en plan med standups varje dag.
- Arbetade ihop med Andreas och skapade en databas och en DbContext.
- Refactoriserade om AppDbContext så den inte skapade databasen i /bin/debug/ utan i /Data/Database och också hämtade strängen från appsettings.json för ökad säkerhet.
- Satte upp MongoDB via Docker Composer och skapade en DbContext för att kunna använda MongoDB.
  CRUD kopplingarna ska fungera, även om det inte finns någon förinmatad data i DBn.
- Skapade en logo med hjälpa av ChatGTP, och la in den i README.md så den syns i GitHub.
- Refaktoriserade getAllProducts så den fungerade med EF.
- Refaktoriserade search för produkter (namn) så den fungerade med EF.
- Implementerade singletons för SQLite och MongoDB.

## Vilka utmaningar stötte du på och hur löste du dem?

## Vad skulle du göra annorlunda nästa gång?
