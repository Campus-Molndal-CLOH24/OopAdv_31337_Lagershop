
### Beskrivning av diagrammet:
1. **`Menu` klass**: UI-menyn som hanterar användarinteraktion i konsolen.
2. **`AddProduct`, `DeleteProduct`, etc.**: Visar operationerna under Facade och hur de utför specifika CRUD-funktioner.
3. **`IProductFacade`**: Är en nyckelkomponent som centraliserar alla metoder för produktåtkomst och manipulation.
4. **Program**: Central klass som kopplar UI (`Menu`) och databasen.

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
