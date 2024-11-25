namespace HenriksHobbylager.Repositories;


    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void CreateProduct(T entity);
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> Search(Func<T, bool> predicate);
    }
