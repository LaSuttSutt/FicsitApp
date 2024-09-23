using System.Linq.Expressions;

namespace Shared.DataAccess;

public static class DataAccess
{
    private static FicsitDbContext DbContext { get; } = new();

    public static void Initialize()
    {
        DbContext.Database.EnsureCreated();
        DbContext.SaveChanges();
    }
    
    public static List<T> GetEntities<T>() where T : class
    {
        return DbContext.Set<T>().ToList();
    }
    
    public static List<T> GetEntities<T>(Expression<Func<T,bool>> clause) where T : class
    {
        return DbContext.Set<T>().Where(clause).ToList();
    }

    public static T? GetEntity<T>(Guid id) where T : class
    {
        return DbContext.Set<T>().Find(id);
    }

    public static void AddEntity<T>(T entity) where T : class
    {
        DbContext.Set<T>().Add(entity);
        DbContext.SaveChanges();
    }

    public static void UpdateEntity<T>(T entity) where T : class
    {
        DbContext.Set<T>().Update(entity);
        DbContext.SaveChanges();
    }

    public static void DeleteEntity<T>(T entity) where T : class
    {
        DbContext.Set<T>().Remove(entity);
        DbContext.SaveChanges();
    }

    public static void DeleteEntities<T>(IEnumerable<T> entities) where T : class
    {
        DbContext.Set<T>().RemoveRange(entities);
        DbContext.SaveChanges();
    }
}