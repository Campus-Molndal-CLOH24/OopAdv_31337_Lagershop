# Individuell Rapport

Svara på frågorna nedan och lämna in det som en del av din inlämning.

## Hur fungerade gruppens arbete?
Grupparbetet har funkat bra till och från och dåligt till och från men de viktigaste är att vi löste de konflikter som blev både inom gruppen och koden. Jag och Niklas arbetade super tillsamman när vi satt med CRUD och vi hade en superbra kommunikation samt samarbete. Vilket jag uppskattade så mycket, för de är typ så som jag vill att ett grupparbete ska vara att man lyssnar på varandra och hjälper varandra att komma framåt.

## Beskriv gruppens databasimplementation
Vi har 2 databaser, som Henrik önskade,  båda är uppbyggda av 4 tabeller, en för varje entitet. Vi har en tabell för produkter, en för kunder, en för ordrar och en för order rader.   
Vi skulle ha en relation mellan ordrar och kunder, en mellan ordrar och produkter och en mellan order rader och produkter men vi fick mycket strul med att få EF migrations att fungera.  Så detta funkar inte för tillfället.

## Vilka SOLID-principer implementerade ni och hur?
Jag tycker att vi följer alla SOLID-principer  
S- Singel responsibility : Våran kod är uppbyggd för att varje klass har ett ansvar, menuDB har tex bara hand om valen mellan databaserna  
O- Open closed principle: Vi har skapat ett IRepository<T>-gränssnitt som inte behöver ändras när vi lägger till nya databas-klasser. Istället skapar vi nya klasser som implementerar IRepository<T>.  
L- Liskov substitution principle: Alla våra repository-klasser följer IRepository<T>-gränssnittet. Detta gör att vi kan byta ut en repository med en annan utan problem.  
I- Interface segregation principle: Vi har ett interface för varje databas som implementerar IRepository, det gör att varje repository bara behöver implementera de metoder som är relevanta för den databasen.  
D- Dependency inversion principle: Vi använder IRepository<T>-gränssnittet i våra tjänster istället för att direkt använda specifika repository-klasser. Detta gör att vi kan byta ut repository-implementationer enkelt.

## Vilka patterns använde ni och varför?
Vi använder oss av lite olika patterns för att underlätta för oss att skapa en bra struktur i vårt projekt.   
Vi använder oss av Repository pattern för att separera vår data access logik från vår business logik. Vi respekterar DRY principen och refactorerade bort repeterad kod
Vi använder oss av Singleton pattern för att skapa en enda instans av vår databas och för att undvika att skapa flera anslutningar till databasen.  
Och vi använder oss av Interface Segregation Principle för att våta databas implementationer inte ska behöva implementera metoder som de inte använder sig av.


## Vilka tekniska utmaningar stötte ni på och hur löste ni dem?
Vi i gruppen har haft lite problem med vart .sln filen låg, jag fick tex problem med att databasen skapades och lades i bin mappen av någon anledning. Niklas hade stora problem med detta också.
Vi hade också stora problem mongoDB ID då mongo vill ha string och vi hade Int på id.
Jag satt en hel del tid med detta och löste mongoDB delen men pga att vi också hade problem med EF migrationer så löstes aldrig problemet.

## Hur planerade du ditt arbete?
Jag utgick efter vad våran team lead Fredrik sa åt oss behövde göras, Vi startade alltid dagen med att prata om vad som behövdes göras samt gå igenom koden som skrivits sedan förra mötet.
Vi hade en SKRUMish board där vi hade tickets som delades ut. Vi såg till att vi inte arbetade på samma saker.

## Vilka dela gjorde du?
Jag la grunden till Repository och IRepository<T> och  Implementerade en konkret ProductRepository-klass.
Jag optimerade sökningen med Index och gjorde migration för Indexeringen.
Jag skapade CRUD med EF som senare refactorerades av mig och Niklas och detta refactorerades senare av någon annan.
Jag skapade sök funktionen, men skickade den till Niklas när vi par programmerade för att jag inte hade mergat den ännu och vi satt ändå med att optimera CRUD.
Jag refactorerade så att Kategori fanns i alla CRUD metoder samt att man nu också kan söka på kategori.
Jag fixade även automatisk ID på mongo DB men de finns inte i main pga problem med att migrera.

## Vilka utmaningar stötte du på och hur löste du dem?
Den absolut största utmaningen har varit att i början av projektet så fanns de en del problem när de gällde grupp dynamiken och vem som ska ha vilken roll. Blev lite tjafs kring ledar rollen men de sköttes snyggt och löstes utan några större problem.

Ett annat större problem jag stötte på var just hur mongo DB skapade stora problem för mig när jag skulle migrera ändringen i databasen som behövdes göras pga mongoDB ID.
Jag hann tyvärr inte lösa detta problem men jag vet hur jag hade löst de i framtiden.

## Vad skulle du göra annorlunda nästa gång?
Vi skulle varit mer noggranna med att göra uppgifterna i mindre delar och också se till att de som va beroende av varandra inte delades ut samtidigt, så bättre planerat arbete.
Vi skulle också haft en lite bättre plan kring hur vi ville strukturera upp allt, fler personer = fler ideer och tankesätt. Så hade underlättat om vi alla pratade ihop oss lite mer i början.
Vi skulle också använda oss mera utav PR approves  för att se till att vi inte skriver om varandras fungerade kod. Men detta lärde vi oss senare.