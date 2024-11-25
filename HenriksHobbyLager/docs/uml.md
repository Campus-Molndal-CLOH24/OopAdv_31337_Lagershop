
### Beskrivning av diagrammet:
1. **`Menu` klass**: UI-menyn som hanterar användarinteraktion i konsolen.
2. **`AddProduct`, `DeleteProduct`, etc.**: Visar operationerna under Facade och hur de utför specifika CRUD-funktioner.
3. **`IProductFacade`**: Är en nyckelkomponent som centraliserar alla metoder för produktåtkomst och manipulation.
4. **Program**: Central klass som kopplar UI (`Menu`) och databasen.

### UML-diagram Flowchart.
```mermaid
flowchart TD
    Program -->|Startar applikationen| Menu
    Menu -->|Användarens val| Facade["IProductFacade"]
    
    Facade -->|CRUD-operationer| Repository["Repository<T>"]
    Repository -->|Använder| AppDbContext
    AppDbContext -->|Hanterar databas| SQLite["SQLite-databas"]

    subgraph FacadeLayer ["Facade & Operativa klasser"]
        Facade --> AddProduct
        Facade --> DeleteProduct
        Facade --> DisplayProduct
        Facade --> SearchProducts
        Facade --> ShowAllProducts
        Facade --> UpdateProduct
    end

    subgraph RepositoryLayer ["Repository Pattern"]
        Repository --> Product
    end

    subgraph DataLayer ["Data & Entiteter"]
        Product --> SQLite
    end
```

### UML-diagram Klassdiagram.
```mermaid
classDiagram
direction TB

class Program {
    +Main(args: string[]): void
}

class AppDbContext {
    <<EntityFramework>>
    +DbSet~Product~ Products
    +OnConfiguring(options: DbContextOptionsBuilder): void
}

class IRepository~T~ {
    <<Interface>>
    +T GetById(id: int): T
    +List~T~ GetAll(): List~T~
    +void Add(entity: T)
    +void Update(entity: T)
    +void Delete(id: int)
}

class Repository~T~ {
    <<Repository Pattern>>
    -DbContext _context
    +Repository(context: DbContext)
    +T GetById(id: int): T
    +List~T~ GetAll(): List~T~
    +void Add(entity: T)
    +void Update(entity: T)
    +void Delete(id: int)
}

class Product {
    <<Entity>>
    +int Id
    +string Name
    +int Quantity
    +decimal Price
}

class IProductFacade {
    <<Interface>>
    +Product GetProductById(id: int): Product
    +List~Product~ GetAllProducts(): List~Product~
    +void AddProduct(product: Product)
    +void UpdateProduct(product: Product)
    +void DeleteProduct(id: int)
}

class AddProduct {
    +void Execute(Product product)
}

class DeleteProduct {
    +void Execute(int id)
}

class DisplayProduct {
    +void Execute(int id)
}

class SearchProducts {
    +List~Product~ Execute(string query)
}

class ShowAllProducts {
    +List~Product~ Execute()
}

class UpdateProduct {
    +void Execute(Product product)
}

class Menu {
    <<UI>>
    +void DisplayMenu()
    +void HandleInput()
}

Program --> Menu
Program --> AppDbContext
AppDbContext --> Product
Menu --> IProductFacade
IProductFacade <|-- AddProduct
IProductFacade <|-- DeleteProduct
IProductFacade <|-- DisplayProduct
IProductFacade <|-- SearchProducts
IProductFacade <|-- ShowAllProducts
IProductFacade <|-- UpdateProduct
IRepository~T~ <|-- Repository~T~
Repository~T~ --> AppDbContext
Repository~T~ --> Product
```

### ER-diagram Entitetsrelation.
```mermaid
erDiagram
Product {
int Id PK
string Name
int Quantity
decimal Price
string Description
}

    Order {
        int Id PK
        datetime OrderDate
        decimal TotalPrice
    }

    OrderItem {
        int Id PK
        int Quantity
        decimal SubTotal
    }

    Product ||--o{ OrderItem : "Länk till Orderns produkter"
    Order ||--o{ OrderItem : "Länk till produkter i en Order"
```